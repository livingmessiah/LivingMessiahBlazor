using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;
using CalendarEnums = LivingMessiah.Web.Features.Calendar.Enums;

using LivingMessiah.Web.Features.Calendar.ManageKeyDates.Queries;


namespace LivingMessiah.Web.Features.Calendar.ManageKeyDates.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<List<CalendarEntry>> GetCalendarEntries(int yearId, CalendarEnums.DateType filter);
	Task<List<CalendarAnalysisQuery>> GetCalendarAnalysisQuery(int yearId, CalendarEnums.DateType filter);
	Task<KeyDateConstantsQuery> GetKeyDateConstants();
	Task<int> UpdateKeyDateCalendar(int yearId, int detail, DateTime date);
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


	public async Task<List<CalendarEntry>> GetCalendarEntries(int yearId, CalendarEnums.DateType filter)
	{
		log.LogDebug(String.Format("Inside {0}, yearId={1}", nameof(Repository) + "!" + nameof(GetCalendarEntries), yearId));
		base.Parms = new DynamicParameters(new { YearId = yearId });

		base.Sql = $@"
-- DECLARE @yearId int=9999
SELECT
Date, Detail, DateTypeId, EnumId, Description
FROM KeyDate.Calendar
WHERE YearId=@yearId
ORDER BY Date
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<CalendarEntry>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

	public async Task<List<CalendarAnalysisQuery>> GetCalendarAnalysisQuery(int yearId, CalendarEnums.DateType filter)
	{
		log.LogDebug(String.Format("Inside {0}, yearId={1}, filter:{2}"
			, nameof(Repository) + "!" + nameof(GetCalendarAnalysisQuery), yearId, filter.Name));

		base.Parms = new DynamicParameters(new
		{
			YearId = yearId,
			DateTypeId = filter.Value
		});

		base.Sql = $@"
-- DECLARE @YearId int=2023, @DateTypeId int=2
SELECT
YearId, EventDescr, DateTypeId, Detail, EnumId, DiffFromPrevDate, PrevDateYMD, DateYMD
FROM KeyDate.tvfCalendarAnalysis_02_Union(@YearId) 
WHERE ((DateTypeId = @DateTypeId) OR (@DateTypeId = -1))  -- All is -1 NOT 0!!!
ORDER BY Date
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<CalendarAnalysisQuery>(sql: base.Sql, param: base.Parms);
			log.LogDebug(string.Format("... rows {0}; Sql{1}", rows.Count(), base.SqlDump));
			return rows.ToList();
		});
	}

	public async Task<KeyDateConstantsQuery> GetKeyDateConstants() 
	{
		base.Sql = $"SELECT PreviousYear, CurrentYear, NextYear FROM KeyDate.Constants";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<KeyDateConstantsQuery>(sql: base.Sql);
			return rows.SingleOrDefault()!;
		});
	}

	#region Command

	public async Task<int> UpdateKeyDateCalendar(int yearId, int detail, DateTime date)
	{
		base.Parms = new DynamicParameters(new { YearId = yearId, Detail = detail, Date = date });
		base.Sql = $@"
-- DECLARE int @YearId={yearId}, int @Detail={detail}
UPDATE KeyDate.Calendar SET Date = @Date 
WHERE YearId = @YearId AND Detail=@Detail;
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


/*
## ShiftYearForward NOT USED
- `KeyDate.stpShiftYearForward` is never used in the UI
	
```csharp
  Task<(int SprocReturnValue, string ReturnMsg)> ShiftYearForward(int NewCurrentYear);

	public async Task<(int SprocReturnValue, string ReturnMsg)> ShiftYearForward(int newCurrentYear)
	{
		base.Sql = "KeyDate.stpShiftYearForward ";

		Parms = new DynamicParameters(new
		{
			NewCurrentYear = newCurrentYear
		});
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int SprocReturnValue = 0;
		string ReturnMsg = "";


		return await WithConnectionAsync(async connection =>
		{
			string parameters = $"newCurrentYear: {newCurrentYear}";
			string inside = $"{nameof(Repository)}!{nameof(ShiftYearForward)}; about to execute SPROC: {Sql}";
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0)
			{
				ReturnMsg = $"Database call failed; newCurrentYear={newCurrentYear}; SprocReturnValue: {SprocReturnValue}";
				log.LogWarning(string.Format("inside {0}, SprocReturnValue != 0, parameters:{1}, ReturnMsg:{2}, {3} Sql: {4}"
					, inside, parameters, ReturnMsg, Environment.NewLine, Sql));
			}
			else
			{
				ReturnMsg = $"Key dates shifted forward; {parameters}";
			}

			
			
			//return new Tuple<int, int, string>(affectedRows, SprocReturnValue, ReturnMsg);
			return (affectedrows, ReturnMsg);
		});
	}
```

*/
