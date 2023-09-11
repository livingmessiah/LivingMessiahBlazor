using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using static LivingMessiah.Web.Features.SpecialEvents.Data.SqlServer;
using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Features.SpecialEvents.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<List<vwSpecialEvent>> GetEventsByDateRange(DateTimeOffset? dateBegin, DateTimeOffset? dateEnd);
	Task<vwSpecialEvent?> GetEventById(int id);
	Task<List<vwSpecialEvent>> GetCurrentEvents();  // ToDo:  NOT REFERENCED 

	Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(FormVM formVM);
	Task<(int Affectedrows, string ReturnMsg)> UpdateSpecialEvent(SpecialEvents.FormVM formVM);
	Task<int> RemoveSpecialEvent(int id);
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump; }
	}

	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(SpecialEvents.FormVM formVM)
	{
		Sql = "SpecialEvent.stpSpecialEventInsert";
		Parms = new DynamicParameters(new
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

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		base.Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int newId = 0;
		int sprocReturnValue = 0;
		string returnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(Repository)}!{nameof(CreateSpecialEvent)}" +
				$", {nameof(formVM.Title)}; about to execute SPROC: {base.Sql}");
			var affectedrows = await connection.ExecuteAsync(
				sql: Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);


			/*
			System.Collections.Generic.KeyNotFoundException: The given key 'NewId' was not present in the dictionary.
			This F***ing thing doesn't work
			int? x = Parms!.Get<int?>("NewId"); 


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
				returnMsg = $"Special Event created for {formVM.Title}; NewId={newId}";
				base.log.LogDebug($"...Return newId:{newId}, Affected Rows: {affectedrows}");
			}
			*/

			// by passing the crap above.
			returnMsg = $"Special Event created for {formVM.Title}; NewId=YOUR GUESS IS AS GOOD AS MINE";
			base.log.LogDebug($"...Return newId:{newId}, Affected Rows: {affectedrows}");

			return (newId, sprocReturnValue, returnMsg);
		});
	}

	public async Task<(int Affectedrows, string ReturnMsg)> UpdateSpecialEvent(SpecialEvents.FormVM formVM)
	{
		base.Sql = "SpecialEvent.stpSpecialEventUpdate";
		base.Parms = new DynamicParameters(new
		{
			Id = formVM.Id,
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

		base.Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		string returnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug(string.Format("Inside {0}", nameof(Repository) + "!" + nameof(UpdateSpecialEvent)));

			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			
			returnMsg = $"Special Event updated for {formVM.Title}; Id={formVM.Id}";
			base.log.LogDebug(string.Format("...returnMsg: {0}", returnMsg));
			return (affectedrows, returnMsg);

		});
	}

	public async Task<int> RemoveSpecialEvent(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $"DELETE FROM SpecialEvent.Event WHERE Id=@Id";
		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug(string.Format("Inside {0}; deleting id: {1}"
				, nameof(Repository) + "!" + nameof(UpdateSpecialEvent), id));
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return affectedrows;
		});
	}

	public async Task<vwSpecialEvent?> GetEventById(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });

		base.Sql = $@"
--DECLARE @Id int =1
SELECT
  Id, [DateTime] AS EventDate
, ShowBeginDate, ShowEndDate
, SpecialEventTypeId
, Title, SubTitle
, ISNULL(Description, '') AS Description 
, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
FROM SpecialEvent.Event
WHERE Id=@Id
";
		return await WithConnectionAsync(async connection =>
		{
			var row = await connection.QueryAsync<vwSpecialEvent>(base.Sql, base.Parms);
			return row.SingleOrDefault();
		});
	}

	public async Task<List<vwSpecialEvent>> GetCurrentEvents()  // Models.SpecialEventVM
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
			var rows = await connection.QueryAsync<vwSpecialEvent>(sql: Sql);  //Models.SpecialEventVM
			return rows.ToList();
		});
	}

	//https://stackoverflow.com/questions/4331189/datetime-vs-datetimeoffset
	public async Task<List<vwSpecialEvent>> GetEventsByDateRange(DateTimeOffset? dateBegin, DateTimeOffset? dateEnd)
	{
		base.Parms = new DynamicParameters(new
		{
			DateBegin = dateBegin,
			DateEnd = dateEnd
		});

		base.Sql = $@"
--Description is modified because MarkDig doesn't like nulls
--DECLARE @DateBegin smalldatetime =  '2021-03-01', @DateEnd smalldatetime = '2023-01-31' 
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
			var rows = await connection.QueryAsync<vwSpecialEvent>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

}
