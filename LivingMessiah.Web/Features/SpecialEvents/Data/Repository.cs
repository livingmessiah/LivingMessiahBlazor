using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Features.SpecialEvents.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<List<vwSpecialEvent>> GetEventsByDateRange(DateTimeOffset? dateBegin, DateTimeOffset? dateEnd);
	Task<FormVM?> GetEventById(int id);
	Task<List<FormVM>> GetCurrentEvents();
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

	public async Task<FormVM?> GetEventById(int id)
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
			var row = await connection.QueryAsync<FormVM>(base.Sql, base.Parms);
			return row.SingleOrDefault();
		});
	}

	public async Task<List<FormVM>> GetCurrentEvents()  // Models.SpecialEventVM
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
			var rows = await connection.QueryAsync<FormVM>(sql: Sql);  //Models.SpecialEventVM
			return rows.ToList();
		});
	}
		
	public async Task<List<vwSpecialEvent>> GetEventsByDateRange(DateTimeOffset? dateBegin, DateTimeOffset? dateEnd)
	{
		Parms = new DynamicParameters(new
		{
			DateBegin = dateBegin,
			DateEnd = dateEnd
		});

		// FN2
		Sql = $@"
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

