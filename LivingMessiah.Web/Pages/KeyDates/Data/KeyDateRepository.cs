using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Data;                   // ToDo: Move this to LivingMessiah.Web.Data
using LivingMessiah.Web.Pages.KeyDates.Enums;  
using LivingMessiah.Web.Pages.KeyDates.Queries;
using LivingMessiah.Web.Pages.KeyDates.Domain;  // ToDo: Not plural

namespace LivingMessiah.Web.Pages.KeyDates.Data
{
	public interface IKeyDateRepository
	{
		string BaseSqlDump { get; }
		Task<List<YearLookup>> GetYearLookupList();
		Task<List<LivingMessiah.Web.Pages.KeyDates.Domain.CalendarEntry>> GetCalendarEntries(int yearId);
		Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear);
		Task<List<DateUnion>> GetDateUnionList(RelativeYearEnum relativeYear);

		// Command
		Task<int> UpdateKeyDate(int Id, DateTime Date);
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

		public async Task<List<YearLookup>> GetYearLookupList()
		{
			log.LogDebug(String.Format("Inside {0}", nameof(KeyDateRepository) + "!" + nameof(GetYearLookupList)));
			base.Sql = $@"
SELECT CAST(PreviousYear AS char(4)) AS ID, 'Previous' AS Text FROM KeyDate.vwConstants UNION ALL
SELECT CAST(CurrentYear AS char(4))  AS ID, 'Current'	 AS Text FROM KeyDate.vwConstants UNION ALL
SELECT CAST(NextYear AS char(4))     AS ID, 'Next'     AS Text FROM KeyDate.vwConstants
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<YearLookup>(sql: base.Sql);
				return rows.ToList();
			});
		}

		public async Task<List<LivingMessiah.Web.Pages.KeyDates.Domain.CalendarEntry>> GetCalendarEntries(int yearId)
		{
			log.LogDebug(String.Format("Inside {0}, yearId={1}", nameof(KeyDateRepository) + "!" + nameof(GetCalendarEntries), yearId));
			base.Parms = new DynamicParameters(new { YearId = yearId });
			base.Sql = $@"
SELECT
YearId, CalendarTemplateId, Date, Detail, EventDescr, TypeDescr
, DateTypeId AS DateTypeEnum --, DateTypeId
--, DateYMD
FROM KeyDate.vwCalendar
WHERE YearId=@yearId
ORDER BY Date
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<LivingMessiah.Web.Pages.KeyDates.Domain.CalendarEntry>(sql: base.Sql, param: base.Parms);
				return rows.ToList();
			});
		}


		public async Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear)
		{
			log.LogDebug(String.Format("Inside {0}, relativeYear={1}", "UpcomingEventsRepository!GetHebrewYearAndChildren", relativeYear));
			string yearId = GetYearId(relativeYear);

			base.Sql = $@"

-- #1 KeyDates\Queries!CalendarYear
SELECT
Year, ShortDescr, ShortDescrHebrew, IsPregnant
FROM KeyDate.Year
CROSS JOIN KeyDate.Constants c
WHERE Year = {yearId};

-- #2  KeyDates\Queries!CalendarEntry
SELECT
YearId, Date, GregorianYear, DateTypeId, DateIdBeg, DateIdEnd, RowCntByGregorianYear
FROM KeyDate.vwDate_03_Union_DataTypes vw
CROSS JOIN KeyDate.Constants c
WHERE YearId = {yearId}

-- #3  KeyDates\Queries!FeastDay
SELECT Id, YearId, DateId, EnumId, Date, Name, Transliteration, Hebrew, Details, AddDaysDescr, AddDays, DetailCount
FROM KeyDate.vwFeastDay
CROSS JOIN KeyDate.Constants c
WHERE YearId = {yearId};

-- #4  KeyDates\Queries!LunarMonth
SELECT lm.Id, lm.YearId, DateId, d.Date, EnumId, Month, Hebrew, Length, Gregorian, BiblicalName, BiblicalHebrew 
FROM KeyDate.LunarMonth lm
INNER JOIN KeyDate.Date d ON lm.DateId=d.Id AND lm.YearId=d.YearId
CROSS JOIN KeyDate.Constants c
WHERE lm.YearId = {yearId};

-- #5  KeyDates\Queries!Season
SELECT Id, YearId, DateId, BadgeColor, Icon, Name, Type 
FROM KeyDate.Season
CROSS JOIN KeyDate.Constants c;

-- #6  KeyDates\Queries!FeastDayDetail
SELECT
Id, FeastDayId, Detail, Name, Transliteration, Hebrew, Note
FROM KeyDate.FeastDayDetail
";

			return await WithConnectionAsync(async connection =>
			{
				var multi = await connection.QueryMultipleAsync(sql: base.Sql);
				/*
				*** NOTE THE ORDER OF THE  `multi.ReadAsync<foo>` MATTERS AND MUST MATCH UP WITH `base.Sql` ***
				*/
				var calendarYear = await multi.ReadSingleOrDefaultAsync<CalendarYear>();    // #1
				if (calendarYear != null)
				{
					calendarYear.CalendarEntrys = (await multi.ReadAsync<LivingMessiah.Web.Pages.KeyDates.Queries.CalendarEntry>()).ToList();   // #2
					calendarYear.FeastDays = (await multi.ReadAsync<FeastDay>()).ToList();    // #3
					calendarYear.LunarMonths = (await multi.ReadAsync<LunarMonth>()).ToList();   // #4
					calendarYear.Seasons = (await multi.ReadAsync<Season>()).ToList();    // #5
				}

				//System.Text.StringBuilder debug = new System.Text.StringBuilder();
				if (calendarYear.FeastDays != null)
				{
					//ToDo Replace if BaseFeastDaySmartEnum can be used instead
					var fddList = (await multi.ReadAsync<FeastDayDetail>()).ToList();  // #6

					log.LogDebug(String.Format("...fddList.Count={0}", fddList.Count()));

					foreach (var item in calendarYear.FeastDays)
					{
						if (item.DetailCount > 0)  // ToDo: Bug, this is always zero and shoudn't 
						{
							var query = from fdd in fddList where fdd.FeastDayId == item.Id select fdd;
							query.ToList();

							foreach (var fdd in query)
							{
								item.FeastDayDetails.Add(new FeastDayDetail()
								{
									Id = fdd.Id,
									FeastDayId = fdd.FeastDayId,
									Detail = fdd.Detail,
									Name = fdd.Name,
									Transliteration = fdd.Transliteration,
									Hebrew = fdd.Hebrew,
									Note = fdd.Note
								}
								);
								log.LogDebug(String.Format("...fdd={0}", fdd.ToString()));
							}
						}
						else
						{
							log.LogDebug(String.Format("...FeastDays.DetailCount=0 for {0}", item.ToString()));
						}
					}
				}
				else
				{
					log.LogWarning("...calendarYear.FeastDays is null");
				}

				/*
				
				** EXAMPLE OF USING Serilog https://github.com/serilog/serilog/wiki/Structured-Data
				
				var inside = "UpcomingEventsRepository!GetHebrewYearAndChildren";
				var dump = debug.ToString();
				base.log.LogDebug("Inside {@Inside}, FddList: {@FddList}", inside, dump);
				*/

				return calendarYear;
			});
		}

		private string GetYearId(RelativeYearEnum relativeYear)
		{
			return relativeYear switch
			{
				RelativeYearEnum.Previous => "c.PreviousYear",
				RelativeYearEnum.Current => "c.CurrentYear",
				RelativeYearEnum.Next => "c.NextYear",
				RelativeYearEnum.None => "0",
				_ => "c.CurrentYear",
			};

		}

		public async Task<List<DateUnion>> GetDateUnionList(RelativeYearEnum relativeYear)
		{
			base.Sql = $@"
SELECT Id, Date, DateTypeId AS DateTypeEnum, Descr
FROM KeyDate.vwDateUnion
CROSS JOIN KeyDate.Constants c
WHERE YearId = {GetYearId(relativeYear)}
ORDER BY Date
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<DateUnion>(sql: base.Sql, param: base.Parms);
				return rows.ToList();
			});
		}


		#region Command

		public async Task<int> UpdateKeyDate(int id, DateTime date)
		{
			base.Parms = new DynamicParameters(new { Id = id, Date = date });
			base.Sql = $"UPDATE KeyDate.Date SET Date = @Date WHERE Id=@Id; ";
			return await WithConnectionAsync(async connection =>
			{
				log.LogDebug($"base.Sql: {base.Sql}, base.Parms:{base.Parms}");
				var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
				return count;
			});
		}
		
		#endregion
	}
}


/*

** EXAMPLE OF USING Serilog https://github.com/serilog/serilog/wiki/Structured-Data

var inside = "KeyDateRepository!GetHebrewYearAndChildren";
var dump = debug.ToString();
base.log.LogDebug("Inside {@Inside}, FddList: {@FddList}", inside, dump);
*/









