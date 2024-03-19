using Dapper;
using System.Data;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;
using System;

namespace LivingMessiah.Web.Features.SpecialEvents.Data;

public interface ICommands
{
	string BaseSqlDump { get; }
	Task<Tuple<int, int, string>> CreateSpecialEvent(FormVM formVM);
	Task<Tuple<int, string>> UpdateSpecialEvent(FormVM formVM);
	Task<int> RemoveSpecialEvent(int id);
}


public class Commands : BaseRepositoryAsync, ICommands
{
	public Commands(IConfiguration config, ILogger<Commands> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return SqlDump!; }
	}

	public async Task<Tuple<int, int, string>> CreateSpecialEvent(FormVM formVM)
	{
		Sql = "SpecialEvent.stpSpecialEventInsert";
		string inside = $"{nameof(Commands)}!{nameof(CreateSpecialEvent)}; about to execute SPROC: {Sql}";
		Parms = new DynamicParameters(new
		{
			formVM.EventDate,
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

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			log.LogDebug($"{nameof(Commands)}!{nameof(CreateSpecialEvent)}" +
				$", {nameof(formVM.Title)}; about to execute SPROC: {base.Sql}");
			var affectedrows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");  

			if (x is null)
			{
				ReturnMsg = $"Database call failed; NewId is null";
				log.LogWarning(string.Format("inside {0}, ReturnMsg:{1}", inside, ReturnMsg));
			}
			else
			{
				int NewId = 0;
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Special Event created for {formVM.Title}; NewId={NewId}";
				log.LogDebug("{Class}!{Method}; NewId:{NewId}; ReturnMsg: {ReturnMsg}"
				, nameof(Commands), nameof(CreateSpecialEvent), NewId, ReturnMsg);

			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<Tuple<int, string>> UpdateSpecialEvent(FormVM formVM)
	{
		base.Sql = "SpecialEvent.stpSpecialEventUpdate";
		base.Parms = new DynamicParameters(new
		{
			formVM.Id,
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

		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			log.LogDebug("{Class}!{Method}; Id: {Id}"
			, nameof(Commands), nameof(UpdateSpecialEvent), formVM.Id);

			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			ReturnMsg = $"Special Event updated for {formVM.Title}; Id={formVM.Id}";
			log.LogDebug("{Class}!{Method}; ReturnMsg: {ReturnMsg}"
			, nameof(Commands), nameof(UpdateSpecialEvent), ReturnMsg);
			return new Tuple<int, string>(SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<int> RemoveSpecialEvent(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $"DELETE FROM SpecialEvent.Event WHERE Id=@Id";
		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug(string.Format("Inside {0}; deleting id: {1}"
				, nameof(Commands) + "!" + nameof(UpdateSpecialEvent), id));
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return affectedrows;
		});
	}

}

