﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using LivingMessiah.Domain;

namespace LivingMessiah.Data;

public interface IShabbatWeekRepository
{
	string BaseSqlDump { get; }

	// Wirecast
	Task<int> UpdateWirecastLink(int id, string wireCastLink);
	Task<int> UpdateScratchpad(string scratchPad);
	Task<Wirecast> GetCurrentWirecast();
	Task<ScratchPad> GetScratchPadWireCast();

	// Psalms and Proverbs
	Task<PsalmAndProverb> GetCurrentPsalmAndProverb();
	Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList();
	Task<List<PsalmsVM>> GetPsalms();


	// Weekly Videos
	Task<IReadOnlyList<vwCurrentWeeklyVideo>> GetCurrentWeeklyVideos(int daysOld);
	Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top);
	Task<int> WeeklyVideoAdd(WeeklyVideoModel dto);
	Task<int> WeeklyVideoUpdate(WeeklyVideoModel dto);
	Task<int> WeeklyVideoDelete(int id);


	#region ToDo: Move somewhere else

	// ToDo Why are these here? It needs to be pulled out of here and ISukkotAdminRepository
	// and put into something like LivingMessiahAdmin
	Task<int> LogErrorTest();
	Task<List<zvwErrorLog>> GetzvwErrorLog();
	Task<int> EmptyErrorLog();


	// ToDo does this go with LivingMessiahAdmin as well
	Task<int> UpdateContactSukkotInviteDate(int id);
	Task<List<Download>> GetDownloads(bool selectAll, bool testEmails);
	#endregion
}


public class ShabbatWeekRepository : BaseRepositoryAsync, IShabbatWeekRepository
{
	public ShabbatWeekRepository(IConfiguration config, ILogger<ShabbatWeekRepository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump; }
	}


	#region ShabbatWeek

	private static (string CompareDate, bool IsDayOfWeekSaturday) CurrentShabbatDate()
	{
		DateTime CompareDate = DateTime.Today;
		string sCompareDate = DateTime.Today.ToString("yyyy-MM-dd") + " 12:00:00 AM";
		bool isDayOfWeekSaturday = CompareDate.DayOfWeek == DayOfWeek.Saturday ? true : false;
		return (sCompareDate, isDayOfWeekSaturday);
	}

	#endregion


	#region Wirecast
	public async Task<Wirecast> GetCurrentWirecast()
	{
		var datesTuple = CurrentShabbatDate();
		log.LogDebug(String.Format("Inside {0}, CompareDate={1}; IsDayOfWeekSaturday={2}"
			, nameof(ShabbatWeekRepository) + "!" + nameof(GetCurrentWirecast), datesTuple.CompareDate, datesTuple.IsDayOfWeekSaturday));

		base.Parms = new DynamicParameters(new { CompareDate = datesTuple.CompareDate }); 


		if (datesTuple.IsDayOfWeekSaturday)
		{
			base.Sql = $@"
--DECLARE @CompareDate varchar(30) =  '2022-11-12 12:00:00 AM'
SELECT sw.Id, sw.ShabbatDate, sw.WirecastLink
FROM ShabbatWeek sw
WHERE sw.ShabbatDate >= @CompareDate AND sw.ShabbatDate <= @CompareDate
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<Wirecast>(sql: base.Sql, param: base.Parms);
				return rows.SingleOrDefault()!;
			});
		}
		else
		{
			base.Sql = $@"
--DECLARE @CompareDate varchar(30) =  '2022-11-12 12:00:00 AM'
SELECT sw.Id, sw.ShabbatDate, sw.WirecastLink
FROM ShabbatWeek sw
WHERE sw.ShabbatDate >= @CompareDate
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<Wirecast>(sql: base.Sql, param: base.Parms);
				return rows.First();
			});
		}

	}

	public async Task<ScratchPad> GetScratchPadWireCast()
	{
		base.Sql = "SELECT WireCast FROM ScratchPad";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<ScratchPad>(sql: base.Sql);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<int> UpdateWirecastLink(int id, string wireCastLink)
	{
		base.Parms = new DynamicParameters(new { Id = id, WireCastLink = wireCastLink });
		base.Sql = $@"UPDATE dbo.ShabbatWeek SET WirecastLink = @WireCastLink WHERE Id = @Id";

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}

	public async Task<int> UpdateScratchpad(string wireCast)
	{
		base.Parms = new DynamicParameters(new { WireCast = wireCast });
		base.Sql = $@"UPDATE dbo.ScratchPad SET WireCast = @WireCast";

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}
	#endregion

	#region PsalmsAndProverbs

	public async Task<PsalmAndProverb> GetCurrentPsalmAndProverb()
	{
		var datesTuple = CurrentShabbatDate();
		bool isDayOfWeekSaturday = datesTuple.Item2;

		string Where = $" WHERE ShabbatDate >= '{datesTuple.Item1}' ";

		base.Sql = $@"
SELECT 
  ShabbatWeekId, ShabbatDate
, PsalmsBCV, PsalmsChapter
, PsalmsKJVHtmlConcat
, PsalmsUrl, PsalmsTitle
, ProverbsBCV, ProverbsChapter
, ProverbsKJVHtmlConcat
, ProverbsUrl
FROM Bible.vwPsalmsAndProverbs v 
WHERE ShabbatDate = dbo.udfGetNextShabbatDate()
";

		log.LogDebug(String.Format("Inside {0}, Sql={1}"
			, nameof(ShabbatWeekRepository) + "!" + nameof(GetCurrentPsalmAndProverb), base.Sql));


		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<PsalmAndProverb>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList()
	{
		var datesTuple = CurrentShabbatDate();
		bool isDayOfWeekSaturday = datesTuple.Item2;

		string Where = $" WHERE ShabbatDate >= '{datesTuple.Item1}' ";

		base.Sql = $@"
SELECT 
  ShabbatWeekId, ShabbatDate, ShabbatDateYMD
, PsalmsBCV, PsalmsChapter, PslamsVerseCount, IsWholeChapter
--, PsalmsKJVHtmlConcat
, PsalmsUrl, PsalmsTitle
, ProverbsBCV, ProverbsChapter, ProverbsVerseCount
--, ProverbsKJVHtmlConcat
, ProverbsUrl
, TotalVerseCount
FROM Bible.vwPsalmsAndProverbs v 
 {Where}
ORDER BY ShabbatWeekId
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwPsalmsAndProverbs>(sql: base.Sql);
			return rows.ToList();
		});
	}

	public async Task<List<PsalmsVM>> GetPsalms()
	{
		base.Sql = $@"
SELECT 
  ps.Id, ps.BegVerse, ps.EndVerse, ps.EndVerse-ps.BegVerse + 1 AS VerseCount, ps.IsWholeChapter
, 'Psalms ' + CAST (ps.Chapter AS varchar(10)) + ':' + CAST (ps.BegVerse AS varchar(10)) + '-' + CAST (ps.EndVerse AS varchar(10)) AS BCV
, ps.Chapter
, ps.KJVHtmlConcat
, sw.Id AS ShabbatWeekId, CONVERT(VARCHAR(10), sw.ShabbatDate, 111) AS ShabbatDateYMD
FROM Bible.Psalms ps
	LEFT OUTER JOIN ShabbatWeek sw
		ON sw.PsalmsId = ps.Id
	INNER JOIN Bible.BookChapter bc 
		ON ps.BookId = bc.BookId AND ps.Chapter = bc.Chapter
ORDER BY ps.Chapter, ps.BegVerse
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<PsalmsVM>(sql: base.Sql);
			return rows.ToList();
		});
	}

	#endregion

	#region Weekly Video

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
	/**/

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

	public async Task<int> WeeklyVideoAdd(WeeklyVideoModel dto)
	{
		base.Parms = new DynamicParameters(new
		{
			dto.ShabbatWeekId,
			dto.WeeklyVideoTypeId,
			dto.YouTubeId,
			dto.Title,
			dto.Book,
			dto.Chapter
		});
		//dto.GraphicFileRoot,dto.NotesFileRoot, ... GraphicFile, NotesFile ... , @GraphicFile, @NotesFile
		base.Sql = $@"
INSERT INTO WeeklyVideo
(ShabbatWeekId, WeeklyVideoTypeId, YouTubeId, Title, Book, Chapter)
VALUES (@ShabbatWeekId, @WeeklyVideoTypeId, @YouTubeId, @Title, @Book, @Chapter)
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

	public async Task<int> WeeklyVideoUpdate(WeeklyVideoModel dto)
	{
		base.Parms = new DynamicParameters(new
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
		base.Sql = $@"
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


	#region ToDo: Move somewhere else


	public async Task<int> UpdateContactSukkotInviteDate(int id)
	{
		const int ArizonaUtcMinus7 = -7;
		DateTime azdt = DateTime.UtcNow.AddHours(ArizonaUtcMinus7);
		base.Parms = new DynamicParameters(new { Id = id, SukkotInviteDate = azdt });
		base.Sql = "UPDATE dbo.Contact SET SukkotInviteDate = @SukkotInviteDate WHERE Id=@id";

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}

	public async Task<List<Download>> GetDownloads(bool selectAll, bool testEmails)
	{
		const string TOP = "TOP 250 ";
		string Selected = selectAll ? " 1 AS Selected" : " 0 AS Selected";
		//string Where = testEmails? " WHERE FamilyName LIKE 'Marsing%' OR (FirstName = 'Mark' AND FamilyName = 'Webb') OR (FirstName = 'Ralphie' AND FamilyName = 'Cratty')" : "";
		//string Where = testEmails ? " WHERE FamilyName LIKE 'Marsing%' " : "";
		string Where = "";
		base.Sql = $@"SELECT {TOP}  {Selected}, ROW_NUMBER() OVER(ORDER BY Id ASC)-1 AS ZeroBasedRowCnt, Id, FamilyName, FirstName, SpouseName, EMail FROM Sukkot.Download {Where} ";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Download>(sql: base.Sql);
			return rows.ToList();
		});
	}

	public async Task<int> LogErrorTest()
	{
		base.Sql = "dbo.stpLogErrorTest ";
		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, commandType: System.Data.CommandType.StoredProcedure);
			return count;
		});
	}

	public async Task<List<zvwErrorLog>> GetzvwErrorLog()
	{
		base.Sql = $@"SELECT TOP 75 * FROM zvwErrorLog ORDER BY ErrorLogID DESC";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<zvwErrorLog>(sql: base.Sql);
			return rows.ToList();
		});
	}

	public async Task<int> EmptyErrorLog()
	{
		base.Sql = "dbo.stpLogErrorEmpty";
		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, commandType: System.Data.CommandType.StoredProcedure);
			return affectedrows;
		});
	}

	#endregion
}
