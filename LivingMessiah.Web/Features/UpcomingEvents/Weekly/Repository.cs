using Dapper;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Features.UpcomingEvents.Weekly;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<IReadOnlyList<vwCurrentWeeklyVideo>> GetCurrentWeeklyVideos(int daysOld);
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump!; }
	}


	public async Task<IReadOnlyList<vwCurrentWeeklyVideo>> GetCurrentWeeklyVideos(int daysOld)
	{
		base.Parms = new DynamicParameters(new { DaysOld = daysOld });
		base.Sql = $@"
--Declare @DaysOld int=12
SELECT
Id
, ShabbatWeekId
, WvtId
, ShabbatDate
, YouTubeId
, YouTubeUrl
, Title
, GraphicFile
, NotesFile
, WvtDescr
, WvtIcon
, BookChapterTitle
, Chapter
, BookTitle
, HebrewTitle
, HebrewName
, ParashaName
, BiblicalUrlReference
FROM dbo.vwCurrentWeeklyVideo
WHERE DATEADD(d, -@DaysOld, GETUTCDATE()) <= ShabbatDate
ORDER BY ShabbatWeekId DESC, wvtId
		";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwCurrentWeeklyVideo>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

}
