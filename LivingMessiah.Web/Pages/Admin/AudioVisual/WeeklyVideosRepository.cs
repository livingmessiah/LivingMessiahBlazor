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


namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public interface IWeeklyVideosRepository
{
	string BaseSqlDump { get; }

	// Query 
	Task<List<ShabbatWeek>> GetShabbatWeekList(int top);
	Task<List<WeeklyVideoTable>> GetWeeklyVideoTableList(int top);

	// Command
	Task<int> WeeklyVideoAdd(WeeklyVideoInsert dto);
	Task<int> WeeklyVideoUpdate(WeeklyVideoUpdate dto);
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



	public async Task<List<WeeklyVideoTable>> GetWeeklyVideoTableList(int top = 9)
	{
		Logger.LogDebug(string.Format("Inside {0}, top={1}", nameof(WeeklyVideosRepository) + "!" + nameof(GetWeeklyVideoTableList), top));
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

		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		var rows = await connect.QueryAsync<WeeklyVideoTable>(sql: Sql, param: Parms);
		//Logger.LogDebug(string.Format("...Sql {0}", Sql));
		return rows.ToList();
	}


	public async Task<List<ShabbatWeek>> GetShabbatWeekList(int top = 3)
	{
		Logger.LogDebug(string.Format("Inside {0}, top={1}", nameof(WeeklyVideosRepository) + "!" + nameof(GetShabbatWeekList), top));
		Parms = new DynamicParameters(new { Top = top });
		Sql = $@"
-- DECLARE @Top int = 3
SELECT TOP (@Top) Id, ShabbatDate
FROM ShabbatWeek 
WHERE ShabbatDate <= dbo.udfGetNextShabbatDate()
ORDER BY ShabbatDate DESC
";
		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		var rows = await connect.QueryAsync<ShabbatWeek>(sql: Sql, param: Parms);
		Logger.LogDebug(string.Format("...Sql {0}", Sql));
		return rows.ToList();
	}


	#endregion

	#region Command

	
	public async Task<int> WeeklyVideoAdd(WeeklyVideoInsert dto)
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(WeeklyVideoAdd)));
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

		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		newId = await connect.ExecuteScalarAsync<int>(Sql, Parms);
		Logger.LogDebug(string.Format("...newId={0}, Sql {1}", newId, SqlDump));
		return newId;
	}

	public async Task<int> WeeklyVideoUpdate(WeeklyVideoUpdate dto)
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(WeeklyVideoUpdate)));
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
