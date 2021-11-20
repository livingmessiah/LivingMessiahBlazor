using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using LivingMessiah.Data;                   // ToDo: Move this to LivingMessiah.Web.Data

using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.KeyDates.Queries;
using LivingMessiah.Web.Pages.KeyDates.Commands;

namespace LivingMessiah.Web.Pages.UpcomingEvents.Data
{
	public interface IUpcomingEventsRepository
	{
		string BaseSqlDump { get; }
		Task<List<UpcomingEvent>> GetEvents(int daysAhead, int daysPast);
		Task<List<DateUnion>> GetDateUnionList(RelativeYearEnum relativeYear);
		Task<List<DateExplode>> GetDateExplode(RelativeYearEnum relativeYear);
	}

	public class UpcomingEventsRepository : BaseRepositoryAsync, IUpcomingEventsRepository
	{
		public UpcomingEventsRepository(IConfiguration config, ILogger<UpcomingEventsRepository> logger) : base(config, logger)
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

		public async Task<List<DateExplode>> GetDateExplode(RelativeYearEnum relativeYear)
		{
			base.Sql = $@"
SELECT 
	YearId, Date, GregorianYear, DateTypeId AS DateTypeEnum, DateTypeEnumId
--Id, DateYMD, RowCntByGregorianYear, IsDateTypeContiguous, DateType, DateTypeValue
FROM KeyDate.vwDateExplode
CROSS JOIN KeyDate.Constants c
WHERE YearId = {GetYearId(relativeYear)}
ORDER BY Date
";
			//base.log.LogDebug($"Inside {nameof(GetDateExplode)}, Sql: {Sql}");
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<DateExplode>(sql: base.Sql);
				return rows.ToList();
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


		#region Command

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


		#endregion

	}
}
