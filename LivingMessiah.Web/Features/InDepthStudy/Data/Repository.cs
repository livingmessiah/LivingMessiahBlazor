using Dapper;
using LivingMessiah.Web.Data;
using LivingMessiah.Web.Features.UpcomingEvents.Weekly;
using Microsoft.Extensions.Configuration;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

using Microsoft.Extensions.Logging;


//using LivingMessiah.Web.Features.Calendar.ManageKeyDates.Queries;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace LivingMessiah.Web.Features.InDepthStudy.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<IReadOnlyList<InDepthStudyQuery>>? GetIndepthVideos(int daysOld);
	Task<InDepthStudyQuery>? GetIndepth();
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}

	public async Task<InDepthStudyQuery>? GetIndepth()
	{
		//return (await GetIndepthVideos()!).FirstOrDefault()!;
		return (await GetIndepthVideos(100)!).FirstOrDefault()!;
	}

	public async Task<IReadOnlyList<InDepthStudyQuery>>? GetIndepthVideos(int daysOld = 12)
	{
		base.Parms = new DynamicParameters(new { DaysOld = daysOld });
		base.Sql = $@"
--Declare @DaysOld int=12
SELECT
Id
, ShabbatWeekId
, ShabbatDate
, YouTubeId
, YouTubeUrl
, Category
, SubCategory
, Title
, GraphicFile
, NotesFile
, BookChapterTitle
, Chapter
, BookTitle
--, HebrewTitle, HebrewName
, BiblicalUrlReference
FROM dbo.vwInDepthStudy
WHERE DATEADD(d, -@DaysOld, GETUTCDATE()) <= ShabbatDate
ORDER BY ShabbatDate DESC
		";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<InDepthStudyQuery>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});

	}


}
// Ignore Spelling: Indepth
//Task<InDepthStudyQuery>? GetIndepthById(int typeId);
//public Task<InDepthStudyQuery>? GetIndepthById(int typeId)	{		throw new NotImplementedException();	}

