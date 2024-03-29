﻿using Dapper;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Components.ShabbatWeek;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<PsalmAndProverbCurrentQuery> GetPsalmAndProverbCurrentQuery();
	Task<PsalmAndProverbCurrentVM> GetCurrentPsalmAndProverb();
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}


	public async Task<PsalmAndProverbCurrentQuery> GetPsalmAndProverbCurrentQuery()
	{
		var datesTuple = Helper.CurrentShabbatDate();
		bool isDayOfWeekSaturday = datesTuple.Item2;

		string Where = $" WHERE ShabbatDate >= '{datesTuple.Item1}' ";

		base.Sql = $@"
SELECT 
  ShabbatDate
, PsalmsBCV, PsalmsKJVHtmlConcat, PsalmsTitle
, ProverbsBCV, ProverbsKJVHtmlConcat, PsalmsTitle
FROM Bible.vwPsalmsAndProverbs v 
WHERE ShabbatDate = dbo.udfGetNextShabbatDate()
";

		log.LogDebug(String.Format("Inside {0}, Sql={1}"
			, nameof(Repository) + "!" + nameof(GetCurrentPsalmAndProverb), base.Sql));


		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<PsalmAndProverbCurrentQuery>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}


	public async Task<PsalmAndProverbCurrentVM> GetCurrentPsalmAndProverb()
	{
		var datesTuple = Helper.CurrentShabbatDate();
		bool isDayOfWeekSaturday = datesTuple.Item2;

		string Where = $" WHERE ShabbatDate >= '{datesTuple.Item1}' ";

		base.Sql = $@"
SELECT 
  ShabbatWeekId, ShabbatDate
, PsalmsBCV, PsalmsChapter
, PsalmsKJVHtmlConcat
, PsalmsUrl, PsalmsTitle
, ProverbsBCV, ProverbsChapter
, ProverbsKJVHtmlConcat
, ProverbsUrl
FROM Bible.vwPsalmsAndProverbs v 
WHERE ShabbatDate = dbo.udfGetNextShabbatDate()
";

		log.LogDebug(String.Format("Inside {0}, Sql={1}"
			, nameof(Repository) + "!" + nameof(GetCurrentPsalmAndProverb), base.Sql));


		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<PsalmAndProverbCurrentVM>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}

}
