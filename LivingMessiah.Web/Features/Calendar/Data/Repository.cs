using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Features.Calendar.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<List<CalendarQuery>> GetCalendarQuery(int yearId);
	Task<List<PlannerQuery>> GetPlannerQueries(int yearId, Enums.DateType filter);
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
	
	public async Task<List<CalendarQuery>> GetCalendarQuery(int yearId)
	{
		log.LogDebug(String.Format("Inside {0}, yearId={1}", nameof(Repository) + "!" + nameof(GetCalendarQuery), yearId));
		base.Parms = new DynamicParameters(new
		{
			YearId = yearId
		});
		base.Sql = $@"
--DECLARE @yearId int=2024
SELECT
	Date
, Detail
, DateTypeId
, EnumId
, Description
FROM KeyDate.Calendar
WHERE YearId=@yearId
ORDER BY Date
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<CalendarQuery>(sql: base.Sql, param: base.Parms);
			log.LogDebug(string.Format("... rows {0}; Sql{1}", rows.Count(), base.SqlDump));
			return rows.ToList();
		});
	}

	public async Task<List<PlannerQuery>> GetPlannerQueries(int yearId, Enums.DateType filter)
	{
		log.LogDebug(String.Format("Inside {0}, yearId={1}, filter.Name={2}"
			, nameof(Repository) + "!" + nameof(GetPlannerQueries), yearId, filter.Name));
		base.Parms = new DynamicParameters(new
		{
			YearId = yearId,
			DateTypeId = (filter.Value == Enums.DateType.All.Value ? 0 : filter.Value)
		});
		base.Sql = $@"
-- DECLARE @yearId int=2024, @dateTypeId int=0
SELECT
	Date
, Detail
, DateTypeId
, EnumId
, Description
FROM KeyDate.Calendar
WHERE YearId=@yearId
AND ((DateTypeId = @DateTypeId) OR (@DateTypeId = 0))
ORDER BY Date
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<PlannerQuery>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

}
