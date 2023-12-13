using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;


namespace LivingMessiah.Web.Features.Calendar.HealthChecks.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	//Task<List<CodeGenQuery>> GetCodeGenQuery();
	Task<List<CalendarQuery>> GetCalendarQuery(int yearId, int dateTypeId);
}
public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return SqlDump ?? ""; }
	}


	public async Task<List<CalendarQuery>> GetCalendarQuery(int yearId, int dateTypeId)
	{
		log.LogDebug(string.Format("Inside {0}, yearId={1}, dateTypeId={2}"
			, nameof(Repository) + "!" + nameof(GetCalendarQuery), yearId, dateTypeId));

		Parms = new DynamicParameters(new 
		{ YearId = yearId, 
			DateTypeId = dateTypeId }
		);

		Sql = $@"
--DECLARE @yearId  int = 2024
--DECLARE @dateTypeId  int = 2 -- 1: Month, 2: Feast 3: Season
SELECT
YearId, Date, DateYMD, EventDescr, TypeDescr, DateTypeId, EnumId
FROM KeyDate.vwHealthCheck
WHERE YearId=@yearId AND DateTypeId=@dateTypeId
ORDER BY Date
	";

		return await WithConnectionAsync(async connection =>
		{
			{
				var rows = await connection.QueryAsync<CalendarQuery>(sql: base.Sql, param: base.Parms);
				log.LogDebug(string.Format("... rows {0}; Sql{1}", rows.Count(), SqlDump));
				return rows.ToList();
			}
		});
	}


	/*
	public async Task<List<CodeGenQuery>> GetCodeGenQuery()
	{
		log.LogDebug(string.Format("Inside {0}", nameof(Repository) + "!" + nameof(GetCodeGenQuery)));
		Sql = $@"
SELECT Year(ShabbatDate) AS Year, COUNT(*) As RowsPerYear 
FROM ShabbatWeek 
GROUP By Year(ShabbatDate)
	";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<CodeGenQuery>(sql: Sql);
			log.LogDebug(string.Format("... rows {0}; Sql{1}", rows.Count(), SqlDump));
			return rows.ToList();
		});
	}
*/
}

