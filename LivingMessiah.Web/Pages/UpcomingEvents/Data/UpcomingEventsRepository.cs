using Dapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using LivingMessiah.Data;                   // ToDo: Move this to LivingMessiah.Web.Data

using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.UpcomingEvents.Queries;
using static LivingMessiah.Web.Pages.SqlServer;
using LivingMessiah.Web.Pages.UpcomingEvents.EditMarkdown;

namespace LivingMessiah.Web.Pages.UpcomingEvents.Data
{
	public interface IUpcomingEventsRepository
	{
		string BaseSqlDump { get; }

		// Queries
		Task<List<UpcomingEvent>> GetEvents(int daysAhead, int daysPast);
		Task<Tuple<int, int, string>> Create(NonKeyDateCrudVM nonKeyDateCrudVM);
		Task<EditMarkdownVM> GetDescription(int id);

		// Commands
		Task<int> UpdateDescription(int id, string description);
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


		public async Task<EditMarkdownVM> GetDescription(int id)
		{
			base.Parms = new DynamicParameters(new {Id = id});

			base.Sql = $@"
--DECLARE @Id int=
SELECT Id, Title
, ISNULL(Description, '') AS Description -- MarkDig doesnt like nulls
FROM KeyDate.UpcomingEvent
WHERE Id = @Id
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<EditMarkdownVM>(sql: base.Sql, param: base.Parms);
				return rows.SingleOrDefault();
			});
		}

		public async Task<int> UpdateDescription(int id, string description)
		{
			base.Parms = new DynamicParameters(new { Id = id, Description = description });
			base.Sql = $"UPDATE KeyDate.UpcomingEvent SET Description = @Description WHERE Id=@Id; ";
			return await WithConnectionAsync(async connection =>
			{
				log.LogDebug($"base.Sql: {base.Sql}, base.Parms:{base.Parms}");
				var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
				return count;
			});
		}


		public async Task<Tuple<int, int, string>> Create(NonKeyDateCrudVM nonKeyDateCrudVM)
		{
			base.Sql = "KeyDate.stpUpcomingEventInsert";
			base.Parms = new DynamicParameters(new
			{
				YearId = nonKeyDateCrudVM.YearId,
				DateId = nonKeyDateCrudVM.DateId,
				DateTime = nonKeyDateCrudVM.EventDate,
				ShowBeginDate = nonKeyDateCrudVM.ShowBeginDate,
				ShowEndDate = nonKeyDateCrudVM.ShowEndDate,
				EventTypeId = nonKeyDateCrudVM.EventTypeEnum,
				Title = nonKeyDateCrudVM.Title,
				SubTitle = nonKeyDateCrudVM.SubTitle,
				Description = nonKeyDateCrudVM.Description,
				ImageUrl = nonKeyDateCrudVM.ImageUrl,
				WebsiteUrl = nonKeyDateCrudVM.WebsiteUrl,
				WebsiteDescr = nonKeyDateCrudVM.WebsiteDescr,
				YouTubeId = nonKeyDateCrudVM.YouTubeId
			});

			base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			base.Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

			int NewId = 0;
			int SprocReturnValue = 0;
			string ReturnMsg = "";

			return await WithConnectionAsync(async connection =>
			{
				base.log.LogDebug($"Inside {nameof(UpcomingEventsRepository)}!{nameof(Create)}, {nameof(nonKeyDateCrudVM.Title)}; about to execute SPROC: {base.Sql}");
				var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
				SprocReturnValue = base.Parms.Get<int>(ReturnValueName);
				int? x = base.Parms.Get<int?>("NewId");
				if (x == null)
				{
					if (SprocReturnValue == ReturnValueViolationInUniqueIndex)
					{
						ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {nonKeyDateCrudVM.Title}; ";
						base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
					}
					else
					{
						ReturnMsg = $"Database call falied; registration.EMail: {nonKeyDateCrudVM.Title}; SprocReturnValue: {SprocReturnValue}";
						base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
					}
				}
				else
				{
					NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
					ReturnMsg = $"Upcoming Event created for {nonKeyDateCrudVM.Title}; NewId={NewId}";
					base.log.LogDebug($"...Return NewId:{NewId}");
				}

				return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

			});
		}


		public async Task<List<UpcomingEvent>> GetEvents(int daysAhead, int daysPast)
		{
			base.Parms = new DynamicParameters(new
			{
				DaysAhead = daysAhead,
				DaysPast = daysPast
			});

			base.Sql = $@"
--DECLARE @DaysAhead int =100, @DaysPast int =-3
SELECT
  Id, EventDate, EventTypeEnum, DateTypeEnum
, EnumId  -- ToDo: Depricated
, DaysDiff, DaysDiffDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
, ISNULL(Description, '') AS Description -- MarkDig doesnt like nulls
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

	}
}
