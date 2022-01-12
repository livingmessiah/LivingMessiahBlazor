using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual
{
	public interface IWeeklyVideosRepository
	{
		string BaseSqlDump { get; }

		// Query 		
		Task<List<ShabbatWeekLookup>> GetShabbatWeekLookup(int top);
		Task<List<EditGridVM>> GetTopWeeklyVideos(int top);

		// Command
		Task<int> WeeklyVideoAdd(EditGridVM dto);
		Task<int> WeeklyVideoUpdate(EditGridVM dto);
		Task<int> WeeklyVideoDelete(int id);
	}

	public class WeeklyVideosRepository : LivingMessiah.Data.BaseRepositoryAsync, IWeeklyVideosRepository
	{
		public WeeklyVideosRepository(IConfiguration config, ILogger<WeeklyVideosRepository> logger) : base(config, logger)
		{
		}

		public string BaseSqlDump
		{
			get { return base.SqlDump; }
		}

		#region Query
		public async Task<List<EditGridVM>> GetTopWeeklyVideos(int top)
		{
			base.Parms = new DynamicParameters(new { Top = top });
			base.Sql = $@"
-- DECLARE @Top int = 3
SELECT
	wv.Id
, tvf.WeeklyVideoTypeId AS WeeklyVideoTypeEnum
, Descr
, tvf.ShabbatWeekId
, ROW_NUMBER () OVER (ORDER BY ShabbatDate DESC, tvf.WeeklyVideoTypeId )  AS RowNum
, ShabbatDateYMD
,	ShabbatDate

,	wv.ShabbatWeekId,	wv.WeeklyVideoTypeId
,	wv.YouTubeId

, wv.Title
--, CASE 
--   WHEN tvf.WeeklyVideoTypeId = 1 or tvf.WeeklyVideoTypeId = 2 -- MS Eng/Esp
--	   THEN wv.Title
--	   ELSE '' -- ELSE 'Use Book/Chapter' 
--	END AS Title

--, wv.GraphicFile , wv.NotesFile
, CASE 
   WHEN wv.Id IS NULL OR wv.GraphicFile IS NOT NULL
			THEN wv.GraphicFile
			ELSE CAST(tvf.WeeklyVideoTypeId AS varchar(10)) + '-graphic-' +  ShabbatDateYMD -- + '-' + ISNULL(wv.Title, '***title***')
	END AS GraphicFile
, CASE 
   WHEN wv.Id IS NULL OR wv.NotesFile IS NOT NULL
			THEN wv.NotesFile
			ELSE CAST(tvf.WeeklyVideoTypeId AS varchar(10)) + '-notes-' +  ShabbatDateYMD -- + '-' + ISNULL(wv.Title, '***title***')
	END AS NotesFile

, wv.Book
, wv.Chapter
FROM tvfShabbatWeekCrossWeeklyVideoTypeByTop(@Top) tvf
LEFT OUTER JOIN WeeklyVideo wv 
	ON tvf.ShabbatWeekId = wv.ShabbatWeekId AND
	   tvf.WeeklyVideoTypeId = wv.WeeklyVideoTypeId
ORDER BY ShabbatDate DESC, tvf.WeeklyVideoTypeId
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<EditGridVM>(sql: base.Sql, param: base.Parms);
				return rows.ToList();
			});
		}

		public async Task<List<ShabbatWeekLookup>> GetShabbatWeekLookup(int top) 
		{
			base.Parms = new DynamicParameters(new { Top = top });
			base.Sql = $@"
-- DECLARE @Top int = 3
SELECT TOP (@Top) Id, ShabbatDate
FROM ShabbatWeek 
WHERE ShabbatDate <= dbo.udfGetNextShabbatDate()
ORDER BY ShabbatDate DESC
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<ShabbatWeekLookup>(sql: base.Sql, param: base.Parms);
				return rows.ToList();
			});
		}

		#endregion

		#region Command

		public async Task<int> WeeklyVideoAdd(EditGridVM dto)
		{
			base.Parms = new DynamicParameters(new
			{
				dto.ShabbatWeekId,
				dto.WeeklyVideoTypeEnum,
				dto.YouTubeId,
				dto.Title,
				dto.Book,
				dto.Chapter
			});
			//dto.GraphicFileRoot,dto.NotesFileRoot, ... GraphicFile, NotesFile ... , @GraphicFile, @NotesFile
			base.Sql = $@"
INSERT INTO WeeklyVideo
(ShabbatWeekId, WeeklyVideoTypeId, YouTubeId, Title, Book, Chapter)
VALUES (@ShabbatWeekId, @WeeklyVideoTypeEnum, @YouTubeId, @Title, @Book, @Chapter)
; SELECT CAST(SCOPE_IDENTITY() as int)
";
			int newId;
			return await WithConnectionAsync(async connection =>
			{
				//var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.Text);
				//return count;
				//var returnId = this.db.Query<int>(sql, dto).SingleOrDefault();
				newId = await connection.ExecuteScalarAsync<int>(base.Sql, base.Parms);
				return newId;

			});
		}

		public async Task<int> WeeklyVideoUpdate(EditGridVM dto)
		{
			base.Parms = new DynamicParameters(new
			{
				dto.Id,
				dto.ShabbatWeekId,
				dto.WeeklyVideoTypeEnum,
				dto.YouTubeId,
				dto.Title,
				//dto.GraphicFileRoot,
				//dto.NotesFileRoot,
				dto.Book,
				dto.Chapter

			});
			base.Sql = $@"
UPDATE WeeklyVideo SET
  ShabbatWeekId = @ShabbatWeekId
, WeeklyVideoTypeId = @WeeklyVideoTypeEnum
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
				var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
				return affectedrows;
			});
		}

		public async Task<int> WeeklyVideoDelete(int id)
		{
			base.Sql = "DELETE FROM WeeklyVideo WHERE Id = @id";
			base.Parms = new DynamicParameters(new { Id = id });
			return await WithConnectionAsync(async connection =>
			{
				var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
				return affectedrows;
			});
		}

		#endregion

	}
}
