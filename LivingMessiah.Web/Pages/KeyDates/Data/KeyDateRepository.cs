using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Data;                   // ToDo: Move this to LivingMessiah.Web.Data
using LivingMessiah.Web.Pages.KeyDates.Queries;

namespace LivingMessiah.Web.Pages.KeyDates.Data;

public interface IKeyDateRepository
{
	string BaseSqlDump { get; }
	Task<List<LivingMessiah.Web.Pages.Calendar.CalendarVM>> GetPlannerVM(int yearId, LivingMessiah.Web.Pages.Calendar.Enums.DateTypeFilter filter);
	Task<List<CalendarEntry>> GetCalendarEntries(int yearId);

	// Command
	Task<int> UpdateKeyDateCalendar(int yearId, int calendarTemplateId, DateTime date);
}
public class KeyDateRepository : BaseRepositoryAsync, IKeyDateRepository
{
	public KeyDateRepository(IConfiguration config, ILogger<KeyDateRepository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump; }
	}

	public async Task<List<LivingMessiah.Web.Pages.Calendar.CalendarVM>> GetPlannerVM(int yearId, LivingMessiah.Web.Pages.Calendar.Enums.DateTypeFilter filter)
	{
		log.LogDebug(String.Format("Inside {0}, yearId={1}", nameof(KeyDateRepository) + "!" + nameof(GetCalendarEntries), yearId));
		base.Parms = new DynamicParameters(new 
		{ YearId = yearId,
			DateTypeId = filter.Value
		});
		base.Sql = $@"
-- DECLARE @yearId int=2022, @dateTypeId int=0
SELECT
ct.Id, c.YearId, c.CalendarTemplateId, 
c.Date
, ct.Detail, ct.Descr, ct.DateTypeId
FROM KeyDate.Calendar c
	INNER JOIN KeyDate.CalendarTemplate ct
		ON c.CalendarTemplateId = ct.Id 
WHERE YearId=@yearId
AND (ct.DateTypeId = @DateTypeId) OR (@DateTypeId = 0)
ORDER BY Date
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<LivingMessiah.Web.Pages.Calendar.CalendarVM>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

	public async Task<List<CalendarEntry>> GetCalendarEntries(int yearId)
	{
		log.LogDebug(String.Format("Inside {0}, yearId={1}", nameof(KeyDateRepository) + "!" + nameof(GetCalendarEntries), yearId));
		base.Parms = new DynamicParameters(new { YearId = yearId });
		base.Sql = $@"
-- DECLARE @yearId int=9999
SELECT
c.CalendarTemplateId, c.Date, ct.Descr, ct.DateTypeId AS DateTypeEnum
--, ct.Id, c.YearId, ct.Detail
FROM KeyDate.Calendar c
	INNER JOIN KeyDate.CalendarTemplate ct
		ON c.CalendarTemplateId = ct.Id 
WHERE YearId=@yearId
ORDER BY Date
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<CalendarEntry>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}



	#region Command

	public async Task<int> UpdateKeyDateCalendar(int yearId, int calendarTemplateId, DateTime date)
	{
		base.Parms = new DynamicParameters(new { YearId = yearId, CalendarTemplateId = calendarTemplateId, Date = date });
		base.Sql = $@"
-- DECLARE int @YearId={yearId}, int @CalendarTemplateId={calendarTemplateId}
UPDATE KeyDate.Calendar SET Date = @Date 
WHERE YearId = @YearId AND CalendarTemplateId=@CalendarTemplateId;
";
		return await WithConnectionAsync(async connection =>
		{
			log.LogDebug($"base.Sql: {base.Sql}, base.Parms:{base.Parms}");
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}
	#endregion

}







