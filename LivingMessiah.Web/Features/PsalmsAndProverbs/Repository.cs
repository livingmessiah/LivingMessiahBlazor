using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using LivingMessiah.Web.Data;
using EnumsDatabase = LivingMessiah.Web.Features.Admin.Database.Enums.Database;

namespace LivingMessiah.Web.Features.PsalmsAndProverbs;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList();
	Task<List<Psalms.PsalmsVM>> GetPsalms();
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, EnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}


	public async Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList()
	{
		var datesTuple = Helper.CurrentShabbatDate();
		base.Parms = new DynamicParameters(new { ShabbatDate = datesTuple.Item1 });

		/*
		string Where = $" WHERE ShabbatDate >= '{datesTuple.Item1}' ";

		FROM Bible.vwPsalmsAndProverbs v
		{ Where}  
		*/

		base.Sql = $@"
--DECLARE @ShabbatDate smalldatetime = '2020-12-13 00:00:00';  
SELECT 
  ShabbatWeekId, ShabbatDate, ShabbatDateYMD
, PsalmsBCV, PsalmsChapter, PsalmsVerseCount, IsWholeChapter
--, PsalmsKJVHtmlConcat
, PsalmsUrl, PsalmsTitle
, ProverbsBCV, ProverbsChapter, ProverbsVerseCount
--, ProverbsKJVHtmlConcat
, ProverbsUrl
, TotalVerseCount
FROM Bible.vwPsalmsAndProverbs v 
WHERE ShabbatDate >= @ShabbatDate
ORDER BY ShabbatWeekId
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwPsalmsAndProverbs>(sql: base.Sql, base.Parms);
			return rows.ToList();
		});
	}

	public async Task<List<Psalms.PsalmsVM>> GetPsalms()
	{
		base.Sql = $@"
SELECT 
  ps.Id, ps.BegVerse, ps.EndVerse, ps.EndVerse-ps.BegVerse + 1 AS VerseCount, ps.IsWholeChapter
, 'Psalms ' + CAST (ps.Chapter AS varchar(10)) + ':' + CAST (ps.BegVerse AS varchar(10)) + '-' + CAST (ps.EndVerse AS varchar(10)) AS BCV
, ps.Chapter
, ps.KJVHtmlConcat
, sw.Id AS ShabbatWeekId, CONVERT(VARCHAR(10), sw.ShabbatDate, 111) AS ShabbatDateYMD
FROM Bible.Psalms ps
	LEFT OUTER JOIN ShabbatWeek sw
		ON sw.PsalmsId = ps.Id
	INNER JOIN Bible.BookChapter bc 
		ON ps.BookId = bc.BookId AND ps.Chapter = bc.Chapter
ORDER BY ps.Chapter, ps.BegVerse
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Psalms.PsalmsVM>(sql: base.Sql);
			return rows.ToList();
		});
	}

}
