using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using LivingMessiah.Domain.KeyDates.Enums;
using LivingMessiah.Domain.KeyDates.Queries;

namespace LivingMessiah.Data
{

	public interface IUpcomingEventsRepository
	{
		string BaseSqlDump { get; }
		Task<List<UpcomingEvent>> GetEvents(int daysAhead, int daysPast);
		Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear);
	}

	public class UpcomingEventsRepository : BaseRepositoryAsync, IUpcomingEventsRepository
	{
		public UpcomingEventsRepository(IConfiguration config, ILogger<ShabbatWeekRepository> logger) : base(config, logger)
		{
		}

		public string BaseSqlDump
		{
			get { return base.SqlDump; }
		}

		public async Task<List<UpcomingEvent>> GetEvents(int daysAhead, int daysPast)
		{
			base.Parms = new DynamicParameters(new
			{
				DaysAhead = daysAhead,
				DaysPast = daysPast
			});

			base.Sql = $@"
SELECT
  EventDate, EventTypeEnum, DateTypeEnum, EnumId
, DaysDiff, DaysDiffDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId, Description
FROM KeyDate.vwUpcomingEvent
WHERE DATEADD(d, @DaysAhead, GETUTCDATE()) >= EventDate
  AND DATEADD(d, @DaysPast, GETUTCDATE()) <= EventDate
ORDER BY EventDate

";

			var inside = "UpcomingEventsRepository!GetEvents";
			var dump = base.Sql;
			var whereclause = $"WHERE DATEADD(d, {@daysAhead}, GETUTCDATE()) >= EventDate AND DATEADD(d, {@daysPast}, GETUTCDATE()) <= EventDate";
			base.log.LogDebug("Inside {@Inside}, Sql: {@Sql}, WhereClause: {@WhereClause}", inside, dump, whereclause);

			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<UpcomingEvent>(sql: base.Sql, param: base.Parms);
				return rows.ToList();
			});
		}

		public async Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear)
		{
			string yearId = relativeYear switch
			{
				RelativeYearEnum.Previous => "c.PreviousYear",
				RelativeYearEnum.Current => "c.CurrentYear",
				RelativeYearEnum.Next => "c.NextYear",
				RelativeYearEnum.None => "0",
				_ => "c.CurrentYear",
			};

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
SELECT Id, YearId, DateId, Date, Name, Transliteration, Hebrew, Details, AddDaysDescr, AddDays, DetailCount
FROM KeyDate.vwFeastDay
CROSS JOIN KeyDate.Constants c
WHERE YearId = {yearId};

-- #4  KeyDates\Queries!LunarMonth
SELECT lm.Id, lm.YearId, DateId, d.Date, EnumId, Month, Hebrew, Length, Gregorian, BiblicalName, BiblicalHebrew 
FROM KeyDate.LunarMonth lm
INNER JOIN KeyDate.Date d ON lm.DateId=d.Id
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
					calendarYear.CalendarEntrys = (await multi.ReadAsync<CalendarEntry>()).ToList();   // #2
					calendarYear.FeastDays = (await multi.ReadAsync<FeastDay>()).ToList();    // #3
					calendarYear.LunarMonths = (await multi.ReadAsync<LunarMonth>()).ToList();   // #4
					//calendarYear.Seasons = (await multi.ReadAsync<Season>()).ToList();    // #5
					calendarYear.SeasonOrEqinoxList = (await multi.ReadAsync<SeasonOrEquinox>()).ToList();    // #5
				}

				//System.Text.StringBuilder debug = new System.Text.StringBuilder();
				if (calendarYear.FeastDays != null)
				{
					var fddList = (await multi.ReadAsync<FeastDayDetail>()).ToList();  // #6

					foreach (var item in calendarYear.FeastDays)
					{
						if (item.DetailCount > 0)
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
							}
							// debug.AppendLine($"fddList item {item}");
						}
					}
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

	}
}

