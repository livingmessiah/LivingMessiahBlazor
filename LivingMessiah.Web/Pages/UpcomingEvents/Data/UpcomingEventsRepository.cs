using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LivingMessiah.Data;                   // ToDo: Move this to LivingMessiah.Web.Data

using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.UpcomingEvents.Queries;
using static LivingMessiah.Web.Pages.SqlServer;
using LivingMessiah.Web.Pages.UpcomingEventsAdmin.EditMarkdown;
using LivingMessiah.Web.Pages.UpcomingEventsAdmin.CRUD;

namespace LivingMessiah.Web.Pages.UpcomingEvents.Data;

public interface IUpcomingEventsRepository
{
	string BaseSqlDump { get; }

	// Queries
	//ToDo: Delete
	Task<List<Queries.SpecialEvent>> GetEvents(int daysAhead, int daysPast);

	Task<List<Queries.SpecialEvent>> GetEventsByDateRange(DateTime dateBegin, DateTime dateEnd);
	Task<EditMarkdownVM> GetDescription(int id);

	// Commands
	Task<int> UpdateDescription(int id, string description);
	Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(FormVM formVM);
	Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(SpecialEvents.FormVM formVM);
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

	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(FormVM formVM)
	{
		base.Sql = "SpecialEvent.stpSpecialEventInsert";
		base.Parms = new DynamicParameters(new
		{
			DateTime = formVM.EventDate,
			ShowBeginDate = formVM.ShowBeginDate,
			ShowEndDate = formVM.ShowEndDate,
			SpecialEventTypeId = formVM.SpecialEventTypeId,
			Title = formVM.Title,
			SubTitle = formVM.SubTitle,
			Description = formVM.Description,
			ImageUrl = formVM.ImageUrl,
			WebsiteUrl = formVM.WebsiteUrl,
			WebsiteDescr = formVM.WebsiteDescr,
			YouTubeId = formVM.YouTubeId
		});

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		base.Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int newId = 0;
		int sprocReturnValue = 0;
		string returnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(UpcomingEventsRepository)}!{nameof(Create)}, {nameof(formVM.Title)}; about to execute SPROC: {base.Sql}");
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			sprocReturnValue = base.Parms.Get<int>(ReturnValueName);
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (sprocReturnValue == ReturnValueViolationInUniqueIndex)
				{
					returnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {formVM.Title}; ";
					base.log.LogWarning($"...returnMsg: {returnMsg}; {Environment.NewLine} {base.Sql}");
				}
				else
				{
					returnMsg = $"Database call failed; registration.EMail: {formVM.Title}; SprocReturnValue: {sprocReturnValue}";
					base.log.LogWarning($"...returnMsg: {returnMsg}; {Environment.NewLine} {base.Sql}");
				}
			}
			else
			{
				newId = int.TryParse(x.ToString(), out newId) ? newId : 0;
				returnMsg = $"Upcoming Event created for {formVM.Title}; NewId={newId}";
				base.log.LogDebug($"...Return newId:{newId}");
			}
			return (newId, sprocReturnValue, returnMsg);
		});
	}

	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(SpecialEvents.FormVM formVM)
	{
		base.Sql = "SpecialEvent.stpSpecialEventInsert";
		base.Parms = new DynamicParameters(new
		{
			DateTime = formVM.EventDate,
			ShowBeginDate = formVM.ShowBeginDate,
			ShowEndDate = formVM.ShowEndDate,
			SpecialEventTypeId = formVM.SpecialEventTypeId,
			Title = formVM.Title,
			SubTitle = formVM.SubTitle,
			Description = formVM.Description,
			ImageUrl = formVM.ImageUrl,
			WebsiteUrl = formVM.WebsiteUrl,
			WebsiteDescr = formVM.WebsiteDescr,
			YouTubeId = formVM.YouTubeId
		});

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		base.Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int newId = 0;
		int sprocReturnValue = 0;
		string returnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(UpcomingEventsRepository)}!{nameof(CreateSpecialEvent)}, {nameof(formVM.Title)}; about to execute SPROC: {base.Sql}");
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			sprocReturnValue = base.Parms.Get<int>(ReturnValueName);
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (sprocReturnValue == ReturnValueViolationInUniqueIndex)
				{
					returnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {formVM.Title}; ";
					base.log.LogWarning($"...returnMsg: {returnMsg}; {Environment.NewLine} {base.Sql}");
				}
				else
				{
					returnMsg = $"Database call failed; registration.EMail: {formVM.Title}; SprocReturnValue: {sprocReturnValue}";
					base.log.LogWarning($"...returnMsg: {returnMsg}; {Environment.NewLine} {base.Sql}");
				}
			}
			else
			{
				newId = int.TryParse(x.ToString(), out newId) ? newId : 0;
				returnMsg = $"Upcoming Event created for {formVM.Title}; NewId={newId}";
				base.log.LogDebug($"...Return newId:{newId}");
			}
			return (newId, sprocReturnValue, returnMsg);
		});
	}


	public async Task<EditMarkdownVM> GetDescription(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });

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
			return rows.SingleOrDefault()!;
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


	// Invalid cast from 'System.Int32' to 'LivingMessiah.Web.Pages.UpcomingEvents.Enums.SpecialEventType'.
	public async Task<List<Queries.SpecialEvent>> GetEvents(int daysAhead, int daysPast)
	{
		base.Parms = new DynamicParameters(new
		{
			DaysAhead = daysAhead,
			DaysPast = daysPast
		});

		base.Sql = $@"
--Description is modified because MarkDig doesn't like nulls
--DECLARE @DaysAhead int =100, @DaysPast int =-3
SELECT
  Id, EventDate
, SpecialEventTypeId
, DaysDiff, DaysDiffDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
, ISNULL(Description, '') AS Description 
, ShowBeginDate, ShowEndDate
FROM SpecialEvent.vwSpecialEvent
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
			var rows = await connection.QueryAsync<Queries.SpecialEvent>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}
	public async Task<List<Queries.SpecialEvent>> GetEventsByDateRange(DateTime dateBegin, DateTime dateEnd)
	{
		base.Parms = new DynamicParameters(new
		{
			DateBegin = dateBegin,
			DateEnd = dateEnd
		});

		base.Sql = $@"
--Description is modified because MarkDig doesn't like nulls
--DECLARE @DaysAhead int =100, @DaysPast int =-3
SELECT
  Id, EventDate
, SpecialEventTypeId
, DaysDiff, DaysDiffDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
, ISNULL(Description, '') AS Description 
, ShowBeginDate, ShowEndDate
FROM SpecialEvent.vwSpecialEvent
WHERE EventDate >= @DateBegin AND EventDate <=  @DateEnd
ORDER BY EventDate
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Queries.SpecialEvent>(sql: base.Sql, param: base.Parms);
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
