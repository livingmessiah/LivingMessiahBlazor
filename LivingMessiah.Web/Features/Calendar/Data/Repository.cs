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

}
