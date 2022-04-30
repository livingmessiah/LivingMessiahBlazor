using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;

// From Base Class
using System.Text;
using System.Data.SqlClient;
using LivingMessiah.Data;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public interface IWeeklyVideosRepository
{
	string BaseSqlDump { get; }

	// Query 
	Task<List<ShabbatWeek>> GetShabbatWeekList(int top);
	Task<List<WeeklyVideoTable>> GetWeeklyVideoTableList(int top);
	Task<WeeklyVideoUpdateVM> GetWeeklyVideoById(int id);

	// Command
	Task<int> WeeklyVideoAdd(WeeklyVideoInsert dto);
	Task<int> WeeklyVideoUpdate(WeeklyVideoUpdate dto);
	Task<int> WeeklyVideoDelete(int id);
}

public class WeeklyVideosRepository : BaseRepositoryAsync, IWeeklyVideosRepository
{
	public WeeklyVideosRepository(IConfiguration config, ILogger<WeeklyVideosRepository> logger) : base(config, logger)
	{

	}

	public string BaseSqlDump
	{
		get { return SqlDump; }
	}

	#region Query

	public async Task<List<WeeklyVideoTable>> GetWeeklyVideoTableList(int top = 9)
	{
		base.log.LogDebug(string.Format("Inside {0}, top={1}", nameof(WeeklyVideosRepository) + "!" + nameof(GetWeeklyVideoTableList), top));
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
			var rows = await connection.QueryAsync<WeeklyVideoTable>(sql: Sql, param: Parms);
			return rows.ToList();
		});
	}

	public async Task<List<ShabbatWeek>> GetShabbatWeekList(int top = 3)
	{
		base.log.LogDebug(string.Format("Inside {0}, top={1}", nameof(WeeklyVideosRepository) + "!" + nameof(GetShabbatWeekList), top));
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
			var rows = await connection.QueryAsync<ShabbatWeek>(sql: Sql, param: Parms);
			base.log.LogDebug(string.Format("...Sql {0}", Sql));
			return rows.ToList();
		});
	}

	public async Task<WeeklyVideoUpdateVM> GetWeeklyVideoById(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });

		//$@"
		base.Sql = @"
SELECT Id
, ShabbatWeekId
, WeeklyVideoTypeId
, YouTubeId
, Title
--, GraphicFile
--, NotesFile
, Book
, Chapter
FROM dbo.WeeklyVideo
WHERE Id = @Id";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WeeklyVideoUpdateVM>(sql: base.Sql, base.Parms);
			return rows.SingleOrDefault();
		});
	}

	#endregion

	#region Command


	public async Task<int> WeeklyVideoAdd(WeeklyVideoInsert dto)
	{
		base.log.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(WeeklyVideoAdd)));
		Parms = new DynamicParameters(new
		{
			dto.ShabbatWeekId,
			dto.WeeklyVideoTypeId,
			dto.YouTubeId,
			dto.Title,
			dto.Book,
			dto.Chapter
		});
		//dto.GraphicFileRoot,dto.NotesFileRoot, ... GraphicFile, NotesFile ... , @GraphicFile, @NotesFile
		Sql = $@"
INSERT INTO WeeklyVideo
(ShabbatWeekId, WeeklyVideoTypeId, YouTubeId, Title, Book, Chapter)
VALUES (@ShabbatWeekId, @WeeklyVideoTypeId, @YouTubeId, @Title, @Book, @Chapter)
; SELECT CAST(SCOPE_IDENTITY() as int)
";
		int newId;

		return await WithConnectionAsync(async connection =>
		{
			newId = await connection.ExecuteScalarAsync<int>(Sql, Parms);
			//base.log.LogDebug(string.Format("...newId={0}, Sql {1}", newId, SqlDump));
			return newId;
		});


	}

	public async Task<int> WeeklyVideoUpdate(WeeklyVideoUpdate dto)
	{
		base.log.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(WeeklyVideoUpdate)));
		Parms = new DynamicParameters(new
		{
			dto.Id,
			dto.ShabbatWeekId,
			dto.WeeklyVideoTypeId,
			dto.YouTubeId,
			dto.Title,
			//dto.GraphicFileRoot,
			//dto.NotesFileRoot,
			dto.Book,
			dto.Chapter

		});
		Sql = $@"
UPDATE WeeklyVideo SET
  ShabbatWeekId = @ShabbatWeekId
, WeeklyVideoTypeId = @WeeklyVideoTypeId
, YouTubeId = @YouTubeId
, Title = @Title
--, GraphicFile = @GraphicFile
--, NotesFile = @NotesFile
, Book = @Book
, Chapter = @Chapter
WHERE Id = @Id
";

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: Sql, param: Parms);
			base.log.LogDebug(string.Format("...affectedrows={0}, SqlDump {1}", affectedrows, SqlDump));
			return affectedrows;
		});
	}

	public async Task<int> WeeklyVideoDelete(int id)
	{
		Sql = "DELETE FROM WeeklyVideo WHERE Id = @id";
		Parms = new DynamicParameters(new { Id = id });

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: Sql, param: Parms);
			base.log.LogDebug(string.Format("...affectedrows={0}, SqlDump {1}", affectedrows, SqlDump));
			return affectedrows;
		});
	}

	#endregion
}
