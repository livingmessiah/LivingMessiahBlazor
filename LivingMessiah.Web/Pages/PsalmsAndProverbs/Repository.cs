using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using LivingMessiah.Data;


namespace LivingMessiah.Web.Pages.PsalmsAndProverbs;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList();

}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump; }
	}


	public async Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList()
	{
		var datesTuple = CurrentShabbatDate();
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
, PsalmsBCV, PsalmsChapter, PslamsVerseCount, IsWholeChapter
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

	private static (string CompareDate, bool IsDayOfWeekSaturday) CurrentShabbatDate()
	{
		DateTime CompareDate = DateTime.Today;
		string sCompareDate = DateTime.Today.ToString("yyyy-MM-dd") + " 12:00:00 AM";
		bool isDayOfWeekSaturday = CompareDate.DayOfWeek == DayOfWeek.Saturday ? true : false;
		return (sCompareDate, isDayOfWeekSaturday);
	}

}
