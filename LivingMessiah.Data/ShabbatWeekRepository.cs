using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using LivingMessiah.Domain;

namespace LivingMessiah.Data
{
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

		private static Tuple<string, bool> CurrentShabbatDate()
		{
			DateTime CompareDate = DateTime.Today;
			string sCompareDate = DateTime.Today.ToString("yyyy-MM-dd") + " 12:00:00 AM";

			bool isDayOfWeekSaturday = CompareDate.DayOfWeek == DayOfWeek.Saturday ? true : false;

			var dateTuple = new Tuple<string, bool>(sCompareDate, isDayOfWeekSaturday);
			return dateTuple;
		}

		#endregion


		#region Wirecast
		public async Task<Wirecast> GetCurrentWirecast()
		{
			var datesTuple = CurrentShabbatDate();
			string sCompareDate = datesTuple.Item1;
			bool isDayOfWeekSaturday = datesTuple.Item2;

			if (isDayOfWeekSaturday)
			{
				base.Sql = $@"
SELECT sw.Id, sw.ShabbatDate, sw.WirecastLink
FROM ShabbatWeek sw
WHERE sw.ShabbatDate >= '{sCompareDate}' AND sw.ShabbatDate <= '{sCompareDate}'
";
				return await WithConnectionAsync(async connection =>
				{
					var rows = await connection.QueryAsync<Wirecast>(sql: base.Sql);
					return rows.SingleOrDefault();
				});
			}
			else
			{
				base.Sql = $@"
SELECT sw.Id, sw.ShabbatDate, sw.WirecastLink
FROM ShabbatWeek sw
WHERE sw.ShabbatDate >= '{sCompareDate}'
";
				return await WithConnectionAsync(async connection =>
				{
					//ToDo: why does this use base.Parms?
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
				return rows.SingleOrDefault();
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
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<PsalmAndProverb>(sql: base.Sql, param: base.Parms);
				return rows.SingleOrDefault();
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

		#endregion

		#region Weekly Video

		public async Task<IReadOnlyList<vwCurrentWeeklyVideo>> GetCurrentWeeklyVideos(int daysOld)
		{
			base.Parms = new DynamicParameters(new { DaysOld = daysOld });
			base.Sql = $@"
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

		#region Bible

		//ToDo: Deprecated, see GetCurrentParashaAndChildren
		//Not True: used by Services\ShabbatWeekCacheService!GetCurrentParashaTorahBookById 
		//  via Book = await SvcCache.GetCurrentParashaTorahBookById(BookId); inside Pages\Parasha\IndexTable.razor.cs
		public async Task<BibleBook> GetTorahBookById(int id)
		{
			base.Parms = new DynamicParameters(new { Id = id });
			base.Sql = $@"
SELECT
Id, Abrv, Title AS EnglishTitle, HebrewTitle, HebrewName 
FROM Bible.Book
WHERE Id = @Id
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<BibleBook>(sql: base.Sql, param: base.Parms);
				return rows.SingleOrDefault();
			});
		}

		#endregion

		#region Parasha

		public async Task<LivingMessiah.Domain.Parasha.Queries.Parasha> GetCurrentParashaAndChildren()
		{
			base.Sql = $@"
SELECT 
Id, TriNum, ShabbatDate
, PrevId, NextId, BookId
, TorahLong, Haftorah, Brit
, ParashaName, AhavtaURL, NameUrl
, BaseParashaUrl
-- , Name, Meaning, ShabbatWeekId
FROM Bible.vwCurrentParasha;

SELECT Id, Abrv, Title AS EnglishTitle, HebrewTitle, HebrewName 
FROM Bible.Book
--WHERE Id = @Id
";
			return await WithConnectionAsync(async connection =>
			{
				var multi = await connection.QueryMultipleAsync(sql: base.Sql);
				/*
				*** NOTE THE ORDER OF THE  `multi.ReadAsync<foo>` MATTERS AND MUST MATCH UP WITH `base.Sql` ***
				*/
				var Parasha = await multi.ReadSingleOrDefaultAsync<LivingMessiah.Domain.Parasha.Queries.Parasha>();    // #1
				if (Parasha != null)
				{
					Parasha.BibleBook = (await multi.ReadAsync<LivingMessiah.Domain.Parasha.Queries.BibleBook>())
					.Where(w => w.Id == Parasha.BookId).SingleOrDefault();   // #2
				}
				return Parasha;
			});

		}

		public async Task<IReadOnlyList<LivingMessiah.Domain.Parasha.Queries.ParashaList>> GetParashotByBookId(int bookId)
		{
			base.Parms = new DynamicParameters(new { BookId = bookId });
			base.Sql = $@"
SELECT
Id
, ROW_NUMBER() OVER(PARTITION BY BookId ORDER BY Id ) AS RowCntByBookId
, BookId, Torah, Name, TriNum, ParashaName
, NameUrl, AhavtaURL, Meaning, IsNewBook, Haftorah, Brit
, ShabbatDate
, BaseParashaUrl, CurrentParashaUrl
--, ShabbatWeekId
FROM Bible.vwParasha
WHERE BookId = @BookId
ORDER BY Id
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<LivingMessiah.Domain.Parasha.Queries.ParashaList>(sql: base.Sql, param: base.Parms);
				return rows.ToList();
			});
		}


		/*
		public async Task<IReadOnlyList<Parasha>> GetParashotByBookIdOneDbCall(int bookId)
		{
			base.Parms = new DynamicParameters(new { BookId = bookId });
			base.Sql = $@"
SELECT
  p.Id
, ROW_NUMBER() OVER(PARTITION BY p.BookId ORDER BY p.Id ) AS RowCntByBookId
, ShabbatWeekId, BookId, Torah, Name, TriNum, ParashaName, NameUrl
, AhavtaURL, Meaning, StartNewBookID, Haftorah, Brit
FROM Bible.Parasha p
WHERE p.BookId = @BookId
ORDER BY p.Id;

SELECT Id, ShabbatDate
FROM ShabbatWeek
WHERE dbo.udfGetNextShabbatDate() = ShabbatDate

SELECT  b.Title, b.HebrewName, b.HebrewTitle
FROM Bible.Parasha p
INNER JOIN  Bible.Book b ON p.BookId = b.Id
INNER JOIN  ShabbatWeek sw ON p.ShabbatWeekId = sw.Id
WHERE dbo.udfGetNextShabbatDate() = ShabbatDate
";
			return await WithConnectionAsync(async connection =>
			{
				var multi = await connection.QueryMultipleAsync(sql: base.Sql, param: base.Parms);
				var parshot = multi.ReadAsync<IReadOnlyList<Parasha>>();
				var book = multi.ReadAsync<BibleBook>();
				var shabbatWeek = multi.ReadAsync<ShabbatWeek>();
				return Ok(new { Parasha = parshot, BibleBook = book, ShabbatWeek = shabbatWeek });
			});
		}
*/

		#endregion

		#region ToDo: Move somewhere else

		public async Task<List<Contact>> GetContacts(bool selectAll)
		{
			/*
			string Where = "";
			string Where = "WHERE LastName LIKE 'Marsing%'";
			string Where = "WHERE LastName LIKE 'Marsing%' OR (FirstName = 'Mark' AND LastName = 'Webb')"; 
			*/
			const string TOP = "TOP 500 ";
			string Selected = selectAll ? " 1 AS Selected" : " 0 AS Selected";
			string Where = "WHERE LastName LIKE 'Marsing%'";
			base.Sql = $@"SELECT {TOP} {Selected}, ROW_NUMBER() OVER(ORDER BY Id ASC)-1 AS ZeroBasedRowCnt, * FROM dbo.Contact {Where} ";

			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<Contact>(sql: base.Sql);
				return rows.ToList();
			});
		}

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
			//, EMail, StatusId, TotalDonation, MealCount, RegistrationFee, MealCost, CampCost
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
}
