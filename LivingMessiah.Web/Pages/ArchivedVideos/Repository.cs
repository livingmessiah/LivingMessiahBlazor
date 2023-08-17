using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LivingMessiah.Data;

namespace LivingMessiah.Web.Pages.ArchivedVideos;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top);
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump; }
	}


	public async Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top)
	{
		base.Parms = new DynamicParameters(new { Top = top });
		base.Sql = $@"

SELECT
  tvf.WeeklyVideoTypeId AS TypeId
, Descr
, tvf.ShabbatWeekId
, ROW_NUMBER () OVER (ORDER BY ShabbatDate DESC, tvf.WeeklyVideoTypeId )  AS RowNum
, ShabbatDateYMD
,	ShabbatDate
,	wv.Id AS WeeklyVideoId
--,	wv.ShabbatWeekId,	wv.WeeklyVideoTypeId
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
			var rows = await connection.QueryAsync<WeeklyVideoIndex>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

}
