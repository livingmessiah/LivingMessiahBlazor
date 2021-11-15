using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Data;                   // ToDo: Move this to LivingMessiah.Web.Data
using LivingMessiah.Domain.KeyDates.Enums;  // ToDo: Move this to LivingMessiah.Web.Enums

using LivingMessiah.Web.Pages.KeyDate.Domain;

namespace LivingMessiah.Web.Pages.KeyDate.Data
{
	public interface IKeyDateRepository
	{
		string BaseSqlDump { get; }
		Task<Domain.Constants> GetConstants();
		Task<List<CalendarEntry>> GetCalendarEntries(int yearId); 
		Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear);

		/*
		Task<List<UpcomingEvent>> GetEvents(int daysAhead, int daysPast);
		Task<List<Domain.KeyDates.Commands.DateUnion>> GetDateUnionList(RelativeYearEnum relativeYear);
		Task<List<DateExplode>> GetDateExplode(RelativeYearEnum relativeYear);
		*/
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

		public async Task<Domain.Constants> GetConstants()
		{
			log.LogDebug(String.Format("Inside {0}", "KeyDateRepository!GetConstants"));
			base.Sql = $"SELECT PreviousYear, CurrentYear, NextYear FROM KeyDate.vwConstants";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<Domain.Constants>(sql: base.Sql);
				return rows.SingleOrDefault();
			});
		}


		public async Task<List<CalendarEntry>> GetCalendarEntries(int yearId)
		{
			log.LogDebug(String.Format("Inside {0}, yearId={1}", "KeyDateRepository!GetCalendar", yearId));
			//int yearId = GetYearId(relativeYear);
			//string yearId = (int)GetYearIdWhereArg(relativeYear);

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
				var rows = await connection.QueryAsync<CalendarEntry>(sql: base.Sql, param: base.Parms);
				return rows.ToList();
			});
		}


		public async Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear)
		{
			log.LogDebug(String.Format("Inside {0}, relativeYear={1}", "KeyDateRepository!GetHebrewYearAndChildren", relativeYear));
			string yearId = GetYearIdWhereArg(relativeYear);

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
WHERE YearId = {yearId}";

			return await WithConnectionAsync(async connection =>
			{
				var multi = await connection.QueryMultipleAsync(sql: base.Sql);
				/*
				*** NOTE THE ORDER OF THE  `multi.ReadAsync<foo>` MATTERS AND MUST MATCH UP WITH `base.Sql` ***
				*/
				var calendarYear = await multi.ReadSingleOrDefaultAsync<CalendarYear>();    // #1
				if (calendarYear != null)
				{
					calendarYear.CalendarEntryDateRanges = (await multi.ReadAsync<CalendarEntryDateRange>()).ToList();   // #2

					//calendarYear.FeastDays = (await multi.ReadAsync<FeastDay>()).ToList();    // #3
					//calendarYear.LunarMonths = (await multi.ReadAsync<LunarMonth>()).ToList();   // #4
					//calendarYear.Seasons = (await multi.ReadAsync<Season>()).ToList();    // #5
				}
				return calendarYear;
			});
		}

		private string GetYearIdWhereArg(RelativeYearEnum relativeYear)
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

	}
}


/*

** EXAMPLE OF USING Serilog https://github.com/serilog/serilog/wiki/Structured-Data

var inside = "KeyDateRepository!GetHebrewYearAndChildren";
var dump = debug.ToString();
base.log.LogDebug("Inside {@Inside}, FddList: {@FddList}", inside, dump);
*/
