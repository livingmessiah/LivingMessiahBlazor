using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Admin.Video.MasterDetail;
using LivingMessiah.Data;  // for public string BaseSqlDump { get { return SqlDump!; } }

namespace LivingMessiah.Web.Pages.Admin.Video.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<List<Models.ShabbatWeek>> GetShabbatWeekList(int top);
	Task<List<Models.WeeklyVideoTable>> GetWeeklyVideoTableList(int top);

	Task<AddEdit.FormVM_DTO> Get(int id);
	Task<Tuple<int, int, string>> WeeklyVideoInsert(Video.AddEdit.FormVM formVM);
	Task<Tuple<int, int, string>> WeeklyVideoUpdate(Video.AddEdit.FormVM formVM);
	Task<Tuple<int, int, string>> WeeklyVideoDelete(int id);
}


public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return SqlDump!; }
	}

	public async Task<List<Models.ShabbatWeek>> GetShabbatWeekList(int top = 3)
	{
		base.log.LogDebug(string.Format("Inside {0}, top={1}", nameof(Repository) + "!" + nameof(GetShabbatWeekList), top));
		Parms = new DynamicParameters(new { Top = top });
		Sql = $@"
-- DECLARE @Top int = 3
SELECT TOP (@Top) Id, ShabbatDate
FROM ShabbatWeek 
WHERE ShabbatDate <= dbo.udfGetNextShabbatDate()
ORDER BY ShabbatDate DESC
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Models.ShabbatWeek>(sql: Sql, param: Parms);
			base.log.LogDebug(string.Format("...Sql {0}", Sql));
			return rows.ToList();
		});
	}

	public async Task<List<Models.WeeklyVideoTable>> GetWeeklyVideoTableList(int top = 9)
	{
		base.log.LogDebug(string.Format("Inside {0}, top={1}", nameof(Repository) + "!" + nameof(GetWeeklyVideoTableList), top));
		Parms = new DynamicParameters(new { Top = top });

		Sql = $@"
-- DECLARE @Top int = 3
SELECT 
 wv.Id
, Descr AS WeeklyVideoTypeDescr
, ShabbatDate
, wv.ShabbatWeekId,	wv.WeeklyVideoTypeId
,	wv.YouTubeId
, wv.Title
--, LAG(wv.ShabbatWeekId, 1, 0) OVER (ORDER BY ShabbatDate DESC, tvf.WeeklyVideoTypeId) AS PrevShabbatWeekId
FROM tvfShabbatWeekCrossWeeklyVideoTypeByTop(@Top) tvf
LEFT OUTER JOIN WeeklyVideo wv 
	ON tvf.ShabbatWeekId = wv.ShabbatWeekId AND
	   tvf.WeeklyVideoTypeId = wv.WeeklyVideoTypeId
WHERE wv.Id IS NOT NULL
ORDER BY ShabbatDate DESC, tvf.WeeklyVideoTypeId
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Models.WeeklyVideoTable>(sql: Sql, param: Parms);
			return rows.ToList();
		});
	}


	public async Task<AddEdit.FormVM_DTO> Get(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
--DECLARE @Id int=427
SELECT Id
, WeeklyVideoTypeId
, ShabbatWeekId
, YouTubeId
, Title
--, GraphicFile
--, NotesFile
, Book
, Chapter
FROM dbo.WeeklyVideo
WHERE Id = @Id
";
		return await WithConnectionAsync(async connection =>
		{
			var row = await connection.QueryAsync<AddEdit.FormVM_DTO>(sql: base.Sql, base.Parms);
			return row.SingleOrDefault()!;
		});
	}


	public async Task<Tuple<int, int, string>> WeeklyVideoInsert(Video.AddEdit.FormVM formVM)
	{
		Sql = "dbo.stpWeeklyVideoInsert";
		Parms = new DynamicParameters(new
		{
			//formVM.Id, do I need this ???
			formVM.ShabbatWeekId,
			formVM.WeeklyVideoTypeId,
			formVM.@YouTubeId,
			formVM.@Title,
			formVM.@Book,
			formVM.@Chapter
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			string parameters = $"ShabbatWeekId: {formVM.ShabbatWeekId}, WeeklyVideoTypeId: {formVM.WeeklyVideoTypeId}";
			string inside = $"{nameof(Repository)}!{nameof(WeeklyVideoInsert)}; about to execute SPROC: {Sql}";
			log.LogDebug(string.Format("Inside {0}, parameters:{1}", inside, parameters));

			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");
			if (x is null)
			{
				ReturnMsg = $"Database call failed; video.ShabbatWeekId: {formVM.ShabbatWeekId}; SprocReturnValue: {SprocReturnValue}";
				log.LogWarning(string.Format("inside {0}, NewId is null, parameters:{1}, ReturnMsg:{2}, {3} Sql: {4}"
					, inside, parameters, ReturnMsg, Environment.NewLine, Sql));
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Video created for {formVM.ShabbatWeekId}/{formVM.WeeklyVideoTypeId}; NewId={NewId}";
				log.LogDebug(string.Format("...NewId {0}", NewId));
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<Tuple<int, int, string>> WeeklyVideoUpdate(Video.AddEdit.FormVM formVM)
	{
		Sql = "dbo.stpWeeklyVideoUpdate";
		Parms = new DynamicParameters(new
		{
			formVM.Id,
			formVM.WeeklyVideoTypeId,
			formVM.ShabbatWeekId,
			formVM.YouTubeId,
			//Title = DTOHelper.Scrub(formVM.Title),
			formVM.Title,
			formVM.Book,
			formVM.Chapter,
		});

		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int affectedRows = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			string parameters = $"Id: {formVM.Id}, ShabbatWeekId: {formVM.ShabbatWeekId}, WeeklyVideoTypeId: {formVM.WeeklyVideoTypeId}";
			string inside = $"{nameof(Repository)}!{nameof(WeeklyVideoUpdate)}; about to execute SPROC: {Sql}";
			log.LogDebug(string.Format("Inside {0}", inside));

			affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) 
			{
				ReturnMsg = $"Database call failed; {nameof(formVM.YouTubeId)}:{formVM.YouTubeId}; SprocReturnValue: {SprocReturnValue}";
				log.LogWarning(string.Format("inside {0}, NewId is null, parameters:{1}, ReturnMsg:{2}, {3} Sql: {4}"
					, inside, parameters, ReturnMsg, Environment.NewLine, Sql));
			}
			else
			{
				ReturnMsg = $"Video updated for {formVM.ShabbatWeekId}/{formVM.WeeklyVideoTypeId}";
			}

			return new Tuple<int, int, string>(affectedRows, SprocReturnValue, ReturnMsg);
		});
	}

	public async Task<Tuple<int, int, string>> WeeklyVideoDelete(int id)
	{
		Sql = "dbo.stpWeeklyVideoDelete";
		Parms = new DynamicParameters(new { Id = id });

		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			string inside = $"{nameof(Repository)}!{nameof(WeeklyVideoDelete)}, Id: {id}; about to execute SPROC: {Sql}";
			log.LogDebug(string.Format("Inside {0}", inside));
			RowsAffected = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) // ReturnValueOk
			{
				ReturnMsg = $"Database call failed to delete Id: {id}; SprocReturnValue: {SprocReturnValue}";
				log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
			}
			else
			{
				ReturnMsg = $"WeeklyVideo deleted for Id: {id}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);

		});
	}

	//	#endregion

}

/*
# Footnotes

FN1. Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md

*/
