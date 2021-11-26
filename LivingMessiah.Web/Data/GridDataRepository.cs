using Dapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.UpcomingEvents.Queries;
using LivingMessiah.Web.Pages.UpcomingEvents;
using static LivingMessiah.Web.Pages.SqlServer;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

// From Base Class
using System.Text;
using System.Data.SqlClient;

namespace LivingMessiah.Web.Data
{
	public interface IGridDataRepository
	{
		Task<List<NonKeyDateCrudVM>> GetNonKeyDataCrudList();
		Task<int> GetNonKeyDataCrudCount();
		Task Create(NonKeyDateCrudVM nonKeyDateCrudVM);
		Task UpdateNonKeyDate(NonKeyDateCrudVM vm);
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

		public string Sql { get; set; }
		public DynamicParameters Parms { get; set; }  // using Dapper; Note, only place dependent on Dapper


		string errMsg = "";
		string connectionString;

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

		public async Task<List<NonKeyDateCrudVM>> GetNonKeyDataCrudList()
		{
			Sql = $@"
SELECT 
  Id, YearId, DateId, DateTime AS EventDate
, EventTypeId AS EventTypeEnum
, ShowBeginDate, ShowEndDate
, Title, SubTitle, Description
, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
FROM KeyDate.UpcomingEvent
WHERE EventTypeId <> 1 -- 1=KeyDate
ORDER BY Id DESC
";
			using (var connect = new SqlConnection(connectionString))
			{
				await connect.OpenAsync();
				var rows = await connect.QueryAsync<NonKeyDateCrudVM>(Sql);
				return rows.ToList();
			}
		}

		public async Task<int> GetNonKeyDataCrudCount()
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

		public async Task UpdateNonKeyDate(NonKeyDateCrudVM nonKeyDateCrudVM)
		{
			Parms = new DynamicParameters(new
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

			Sql = $@"
UPDATE KeyDate.UpcomingEvent SET
	YearId = @YearId,
	DateId = @DateId,
	DateTime = @EventDate,
	ShowBeginDate = @ShowBeginDate,
	ShowEndDate = @ShowEndDate,
	EventTypeId = @EventTypeEnum,
	Title = @Title,
	SubTitle = @SubTitle,
	Description = @Description,
	ImageUrl = @ImageUrl,
	WebsiteUrl = @WebsiteUrl,
	WebsiteDescr = @WebsiteDescr
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

		public async Task Create(NonKeyDateCrudVM nonKeyDateCrudVM)
		{
			Sql = "KeyDate.stpUpcomingEventInsert";
			Parms = new DynamicParameters(new
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
						ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {nonKeyDateCrudVM.Title}; ";
						Logger.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
					}
					else
					{
						ReturnMsg = $"Database call falied; registration.EMail: {nonKeyDateCrudVM.Title}; SprocReturnValue: {SprocReturnValue}";
						Logger.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
					}
				}
				else
				{
					NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
					ReturnMsg = $"Upcoming Event created for {nonKeyDateCrudVM.Title}; NewId={NewId}";
					Logger.LogDebug($"...Return NewId:{NewId}");
				}
			}

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
