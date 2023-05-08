using Dapper;  // Required for e.g. DynamicParameters, QueryAsync, ExecuteAsync; Installed in ref proj. e.g. LivingMessiah.Data for
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.UpcomingEvents.Queries;
using LivingMessiah.Web.Pages.UpcomingEventsAdmin.Edit;
using static LivingMessiah.Web.Pages.SqlServer;

// From Base Class
using System.Text;
using System.Data.SqlClient;

/*
	ToDo: THIS NEEDS TO BE FULL CONVERTED OVER FROM KeyDate.UpcomingEvent TO SpecialEvent.Event 
*/

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin.Edit;

public interface IGridDataRepository
{
	Task<List<EditVM>> GetUpcomingEventsEditList();
	Task<int> GetUpcomingEventsEditCount();
	Task Create(EditVM EditVM);
	Task UpdateNonKeyDate(EditVM vm);
	Task RemoveNonKeyDate(int id);
}

public class GridDataRepository : IGridDataRepository
{
	public GridDataRepository(IConfiguration config, ILogger<GridDataRepository> logger)
	{
		this.config = config;
		this.Logger = logger;
		connectionString = config[configationConnectionKey];
	}

	public string BaseSqlDump
	{
		get { return SqlDump; }
	}

	#region BaseClass
	const string configationConnectionKey = "ConnectionStrings:LivingMessiah";
	private readonly IConfiguration config;
	protected readonly ILogger Logger;

	public string? Sql { get; set; }
	public DynamicParameters? Parms { get; set; }  // using Dapper; Note, only place dependent on Dapper
		string? connectionString;

	public string SqlDump
	{
		get
		{
			string s = "";
			s = Sql ?? "SQL IS NULL";
			if (Parms != null)
			{
				string v = "";
				var sb = new StringBuilder();
				foreach (var name in Parms.ParameterNames) // Why is this empty? 
				{
					var pValue = Parms.Get<dynamic>(name);
					v = (pValue != null) ? pValue.ToString() : "null";
					sb.AppendFormat($"name {name}={v}\n");
				}

				s += ", parameter: " + sb.ToString();

			}
			return s;
		}
	}
	#endregion

	public async Task<List<EditVM>> GetUpcomingEventsEditList()
	{
		Sql = $@"
SELECT 
  Id, DateTime AS EventDate
, SpecialEventTypeId  ---, EventTypeId AS EventTypeEnum
, ShowBeginDate, ShowEndDate
, Title, SubTitle
, ISNULL(Description, '') AS Description -- MarkDig does not like nulls
, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
FROM SpecialEvent.Event
ORDER BY EventDate DESC
";

/*
FROM SpecialEvent.vwSpecialEvent
WHERE DATEADD(d, @DaysAhead, GETUTCDATE()) >= EventDate
	AND DATEADD(d, @DaysPast, GETUTCDATE()) <= EventDate
*/

		using (var connect = new SqlConnection(connectionString))
		{
			await connect.OpenAsync();
			var rows = await connect.QueryAsync<EditVM>(Sql);
			return rows.ToList();
		}
	}

	public async Task<int> GetUpcomingEventsEditCount()
	{
		Sql = $@"
SELECT COUNT(*)
FROM KeyDate.UpcomingEvent
WHERE EventTypeId <> 1 -- 1=KeyDate
";
		using (IDbConnection connect = new SqlConnection(connectionString))
		{
			int result = await connect.ExecuteScalarAsync<int>(Sql);
			return result;
		}
	}

	public async Task UpdateNonKeyDate(EditVM EditVM)
	{
		Parms = new DynamicParameters(new
		{
			Id = EditVM.Id,
			EventDate = EditVM.EventDate,
			ShowBeginDate = EditVM.ShowBeginDate,
			ShowEndDate = EditVM.ShowEndDate,
			SpecialEventType = EditVM.SpecialEventType,
			Title = EditVM.Title,
			SubTitle = EditVM.SubTitle,
			Description = EditVM.Description,
			ImageUrl = EditVM.ImageUrl,
			WebsiteUrl = EditVM.WebsiteUrl,
			WebsiteDescr = EditVM.WebsiteDescr,
			YouTubeId = EditVM.YouTubeId
		});

		Sql = $@"
UPDATE KeyDate.UpcomingEvent SET
	DateTime = @EventDate,
	ShowBeginDate = @ShowBeginDate,
	ShowEndDate = @ShowEndDate,
	SpecialEventType = @SpecialEventType,
	Title = @Title,
	SubTitle = @SubTitle,
	Description = @Description,
	ImageUrl = @ImageUrl,
	WebsiteUrl = @WebsiteUrl,
	WebsiteDescr = @WebsiteDescr,
	YouTubeId = @YouTubeId
WHERE Id = @Id
";
		using (IDbConnection connect = new SqlConnection(connectionString))
		{
			await connect.ExecuteAsync(Sql, Parms);
		}
	}

	public async Task RemoveNonKeyDate(int id)
	{
		Parms = new DynamicParameters(new { Id = id });
		Sql = $@" DELETE KeyDate.UpcomingEvent WHERE Id = @Id";
		using (IDbConnection connect = new SqlConnection(connectionString))
		{
			await connect.ExecuteAsync(Sql, Parms);
		}
	}

	public async Task Create(EditVM EditVM)
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(GridDataRepository) + "!" + nameof(EditVM)));

		Sql = "KeyDate.stpUpcomingEventInsert";
		Parms = new DynamicParameters(new
		{
			DateTime = EditVM.EventDate,
			ShowBeginDate = EditVM.ShowBeginDate,
			ShowEndDate = EditVM.ShowEndDate,
			SpecialEventType = EditVM.SpecialEventType,
			Title = EditVM.Title,
			SubTitle = EditVM.SubTitle,
			Description = EditVM.Description,
			ImageUrl = EditVM.ImageUrl,
			WebsiteUrl = EditVM.WebsiteUrl,
			WebsiteDescr = EditVM.WebsiteDescr,
			YouTubeId = EditVM.YouTubeId
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		using (var connect = new SqlConnection(connectionString))
		{
			await connect.OpenAsync();
			var affectedrows = await connect.ExecuteAsync(Sql, Parms, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>(ReturnValueName);
			int? x = Parms.Get<int?>("NewId");

			if (x == null)
			{
				if (SprocReturnValue == ReturnValueViolationInUniqueIndex)
				{
					ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {EditVM.Title}; ";
					Logger.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; registration.EMail: {EditVM.Title}; SprocReturnValue: {SprocReturnValue}";
					Logger.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Upcoming Event created for {EditVM.Title}; NewId={NewId}";
				Logger.LogDebug($"...Return NewId:{NewId}");
			}
		}

	}


}
