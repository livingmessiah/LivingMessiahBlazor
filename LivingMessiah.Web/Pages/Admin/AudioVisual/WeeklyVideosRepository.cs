﻿using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;

// From Base Class
using System.Text;
using System.Data.SqlClient;


namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public interface IWeeklyVideosRepository
{
		string BaseSqlDump { get; }

		// Query 
		Task<Tuple<int, int, List<ShabbatWeekLookup>>> GetShabbatWeekLookup(int top = 3);
		Task<List<EditGridVM>> GetTopWeeklyVideos(int top = 3);

		// Command
		Task<int> WeeklyVideoAdd(EditGridVM dto);
		Task<int> WeeklyVideoUpdate(EditGridVM dto);
		Task<int> WeeklyVideoDelete(int id);
}

public class WeeklyVideosRepository : IWeeklyVideosRepository
{
		public WeeklyVideosRepository(IConfiguration config, ILogger<WeeklyVideosRepository> logger)
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


		#region Query
		public async Task<List<EditGridVM>> GetTopWeeklyVideos(int top = 3)
		{
				Logger.LogDebug(string.Format("Inside {0}, top={1}", nameof(WeeklyVideosRepository) + "!" + nameof(GetTopWeeklyVideos), top));
				Parms = new DynamicParameters(new { Top = top });

				Sql = $@"
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

				using var connect = new SqlConnection(connectionString);
				await connect.OpenAsync();
				var rows = await connect.QueryAsync<EditGridVM>(sql: Sql, param: Parms);
				//Logger.LogDebug(string.Format("...rows={0}, Sql {1}", rows, Sql));
				Logger.LogDebug(string.Format("...Sql {0}", Sql));
				return rows.ToList();
		}

		public async Task<Tuple<int, int, List<ShabbatWeekLookup>>> GetShabbatWeekLookup(int top = 3)
		{
				Logger.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(GetShabbatWeekLookup)));
				Parms = new DynamicParameters(new { Top = top });
				Sql = $@"
-- DECLARE @Top int = 3
SELECT TOP (@Top) Id, ShabbatDate
FROM ShabbatWeek 
WHERE ShabbatDate <= dbo.udfGetNextShabbatDate()
ORDER BY ShabbatDate DESC
";
				int MinId = 0;
				int MaxId = 0;

				using var connect = new SqlConnection(connectionString);
				await connect.OpenAsync();
				var rows = await connect.QueryAsync<ShabbatWeekLookup>(sql: Sql, param: Parms);
				if (rows.Any())
				{
						// Because the query is in DESC order, MaxId=First & MinId=Last
						MaxId = rows.FirstOrDefault().Id;
						MinId = rows.LastOrDefault().Id;
				}
				Logger.LogDebug(string.Format("...MinId={0}, MaxId={1}, Sql {2}", MinId, MaxId, Sql));
				return new Tuple<int, int, List<ShabbatWeekLookup>>(MinId, MaxId, rows.ToList());
		}

		#endregion

		#region Command

		public async Task<int> WeeklyVideoAdd(EditGridVM dto)
		{
				Logger.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(WeeklyVideoAdd)));
				Parms = new DynamicParameters(new
				{
						dto.ShabbatWeekId,
						dto.WeeklyVideoTypeEnum,
						dto.YouTubeId,
						dto.Title,
						dto.Book,
						dto.Chapter
				});
				//dto.GraphicFileRoot,dto.NotesFileRoot, ... GraphicFile, NotesFile ... , @GraphicFile, @NotesFile
				Sql = $@"
INSERT INTO WeeklyVideo
(ShabbatWeekId, WeeklyVideoTypeId, YouTubeId, Title, Book, Chapter)
VALUES (@ShabbatWeekId, @WeeklyVideoTypeEnum, @YouTubeId, @Title, @Book, @Chapter)
; SELECT CAST(SCOPE_IDENTITY() as int)
";
				int newId;

				using var connect = new SqlConnection(connectionString);
				await connect.OpenAsync();
				newId = await connect.ExecuteScalarAsync<int>(Sql, Parms);
				Logger.LogDebug(string.Format("...newId={0}, Sql {1}", newId, SqlDump));
				return newId;
		}

		public async Task<int> WeeklyVideoUpdate(EditGridVM dto)
		{
				Logger.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(WeeklyVideoUpdate)));
				Parms = new DynamicParameters(new
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
				Sql = $@"
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

				using var connect = new SqlConnection(connectionString);
				await connect.OpenAsync();
				var affectedrows = await connect.ExecuteAsync(sql: Sql, param: Parms);
				Logger.LogDebug(string.Format("...affectedrows={0}, SqlDump {1}", affectedrows, SqlDump));
				return affectedrows;
		}

		public async Task<int> WeeklyVideoDelete(int id)
		{
				Sql = "DELETE FROM WeeklyVideo WHERE Id = @id";
				Parms = new DynamicParameters(new { Id = id });
				using var connect = new SqlConnection(connectionString);
				await connect.OpenAsync();
				var affectedrows = await connect.ExecuteAsync(sql: Sql, param: Parms);
				Logger.LogDebug(string.Format("...affectedrows={0}, SqlDump {1}", affectedrows, SqlDump));
				return affectedrows;
		}

		#endregion

}
