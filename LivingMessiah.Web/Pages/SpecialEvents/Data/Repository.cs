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

namespace LivingMessiah.Web.Pages.SpecialEvents.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	//ToDo: should this be in `SpecialEventsStore.cs`

	// Commands
	Task<List<Models.SpecialEventVM>> GetEventsByDateRange(DateTime dateBegin, DateTime dateEnd);
	Task<List<Models.SpecialEventVM>> GetCurrentEvents();

	Task<int> UpdateDescription(int id, string description);
	Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(SpecialEvents.FormVM formVM);
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return SqlDump!; }
	}

	public async Task<List<Models.SpecialEventVM>> GetEventsByDateRange(DateTime dateBegin, DateTime dateEnd)
	{
		Parms = new DynamicParameters(new
		{
			DateBegin = dateBegin,
			DateEnd = dateEnd
		});

		Sql = $@"
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
			var rows = await connection.QueryAsync<Models.SpecialEventVM>(sql: Sql, param: Parms);
			return rows.ToList();
		});
	}

	public async Task<List<Models.SpecialEventVM>> GetCurrentEvents()
	{
		Sql = $@"
SELECT
  Id, EventDate
, ShowBeginDate, ShowEndDate
, SpecialEventTypeId
, DaysDiff, DaysDiffDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
, ISNULL(Description, '') AS Description 
FROM SpecialEvent.vwSpecialEvent
WHERE DATEADD(d, -1, ShowBeginDate) <= GETUTCDATE() AND  
			DATEADD(d, 1, ShowEndDate)		>= GETUTCDATE()
ORDER BY EventDate
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Models.SpecialEventVM>(sql: Sql);
			return rows.ToList();
		});
	}

	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(SpecialEvents.FormVM formVM)
	{
		Sql = "SpecialEvent.stpSpecialEventInsert";
		Parms = new DynamicParameters(new
		{
			DateTime = formVM.EventDate,
			formVM.ShowBeginDate,
			formVM.ShowEndDate,
			formVM.SpecialEventTypeId,
			formVM.Title,
			formVM.SubTitle,
			formVM.Description,
			formVM.ImageUrl,
			formVM.WebsiteUrl,
			formVM.WebsiteDescr,
			formVM.YouTubeId
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int newId = 0;
		int sprocReturnValue = 0;
		string returnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			log.LogDebug($"Inside {nameof(Repository)}!{nameof(CreateSpecialEvent)}, {nameof(formVM.Title)}; about to execute SPROC: {Sql}");
			var affectedrows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			sprocReturnValue = Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (sprocReturnValue == 2601) // Unique Index Violation
				{
					returnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {formVM.Title}; ";
					log.LogWarning($"...returnMsg: {returnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					returnMsg = $"Database call failed; registration.EMail: {formVM.Title}; SprocReturnValue: {sprocReturnValue}";
					log.LogWarning($"...returnMsg: {returnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				newId = int.TryParse(x.ToString(), out newId) ? newId : 0;
				returnMsg = $"Upcoming Event created for {formVM.Title}; NewId={newId}";
				log.LogDebug($"...Return newId:{newId}");
			}
			return (newId, sprocReturnValue, returnMsg);
		});
	}

	public async Task<int> UpdateDescription(int id, string description)
	{
		Parms = new DynamicParameters(new { Id = id, Description = description });
		Sql = $"UPDATE KeyDate.UpcomingEvent SET Description = @Description WHERE Id=@Id; ";
		return await WithConnectionAsync(async connection =>
		{
			log.LogDebug($"base.Sql: {Sql}, base.Parms:{Parms}");
			var count = await connection.ExecuteAsync(sql: Sql, param: Parms);
			return count;
		});
	}



}
