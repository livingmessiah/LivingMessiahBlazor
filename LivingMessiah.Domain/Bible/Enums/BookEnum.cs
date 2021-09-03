using System;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Domain.Bible.Enums
{
	/*
	 - SELECT Title + ' = ' + cast(Id as varchar(5)) + ','  as codegen FROM Bible.Book
	 - Origianlly copied from LivingMessiah.Domain\BibleBookEnumReplacement.cs
	*/
	public enum BookEnum
	{
		Genesis = 1,
		Exodus = 2,
		Leviticus = 3,
		Numbers = 4,
		Deuteronomy = 5,
		Joshua = 6,
		Judges = 7,
		Ruth = 8,
		FirstSamuel = 9,
		SecondSamuel = 10,
		FirstKings = 11,
		SecondKings = 12,
		FirstChronicles = 13,
		SecondChronicles = 14,
		Ezra = 15,
		Nehemiah = 16,
		Esther = 17,
		Job = 18,
		Psalms = 19,
		Proverbs = 20,
		Ecclesiastes = 21,
		SongofSolomon = 22,
		Isaiah = 23,
		Jeremiah = 24,
		Lamentations = 25,
		Ezekiel = 26,
		Daniel = 27,
		Hosea = 28,
		Joel = 29,
		Amos = 30,
		Obadiah = 31,
		Jonah = 32,
		Micah = 33,
		Nahum = 34,
		Habakkuk = 35,
		Zephaniah = 36,
		Haggai = 37,
		Zechariah = 38,
		Malachi = 39,
		Matthew = 40,
		Mark = 41,
		Luke = 42,
		John = 43,
		Acts = 44,
		Romans = 45,
		FirstCorinthians = 46,
		SecondCorinthians = 47,
		Galatians = 48,
		Ephesians = 49,
		Philippians = 50,
		Colossians = 51,
		FirstThessalonians = 52,
		SecondThessalonians = 53,
		FirstTimothy = 54,
		SecondTimothy = 55,
		Titus = 56,
		Philemon = 57,
		Hebrews = 58,
		James = 59,
		FirstPeter = 60,
		SecondPeter = 61,
		FirstJohn = 62,
		SecondJohn = 63,
		ThirdJohn = 64,
		Jude = 65,
		Revelation = 66,
	}

	public class BookLocal
	{
		public static List<BookLocal> All { get; } = new List<BookLocal>();
		public static BookLocal Genesis { get; } = new BookLocal(BookEnum.Genesis, "Genesis", "Gen", BookGroupEnum.Torah);
		public static BookLocal Exodus { get; } = new BookLocal(BookEnum.Exodus, "Exodus", "Exo", BookGroupEnum.Torah);
		public static BookLocal Leviticus { get; } = new BookLocal(BookEnum.Leviticus, "Leviticus", "Lev", BookGroupEnum.Torah);
		public static BookLocal Numbers { get; } = new BookLocal(BookEnum.Numbers, "Numbers", "Num", BookGroupEnum.Torah);
		public static BookLocal Deuteronomy { get; } = new BookLocal(BookEnum.Deuteronomy, "Deuteronomy", "Deu", BookGroupEnum.Torah);
		public static BookLocal Psalms { get; } = new BookLocal(BookEnum.Psalms, "Psalms", "Psa", BookGroupEnum.Poetry);
		public static BookLocal Proverbs { get; } = new BookLocal(BookEnum.Proverbs, "Proverbs", "Pro", BookGroupEnum.Poetry);

		public BookEnum BookEnum { get; private set; }
		public int Id { get; private set; }
		public string Name { get; private set; }  // Called Title in LivingMessiah.Bible.Book database
		public string Abrv { get; private set; }
		public BookGroupEnum BookGroupEnum { get; set; }

		//public string EnglishTitle { get; private set; }
		//public string HebrewTitle { get; private set; }
		//public string HebrewName { get; private set; }

		private BookLocal(BookEnum bookEnum, string name, string abrv, BookGroupEnum bookGroupEnum)
		{
			BookEnum = bookEnum;
			Id = (int)bookEnum;
			Name = name;
			Abrv = abrv;
			BookGroupEnum = bookGroupEnum;
			All.Add(this);
		}

		public static BookLocal FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}

		public static BookLocal FromEnum(BookEnum enumValue)
		{
			return All.SingleOrDefault(r => r.BookEnum == enumValue);
		}

		public static BookLocal FromInt(int intValue)
		{
			return All.SingleOrDefault(r => r.Id == intValue);
		}

		/*
		ToDo: Are these usefull?

		private const string VersesBaseUrl = "https://myhebrewbible.com/";

		public static string GetUrl(BookEnum bookEnum, BibleVerseRange vr, int chapter, string bookChapterSlug, bool isWholeChapter = false)
		{
			BookLocal book = BookLocal.FromEnum(bookEnum);

			if (isWholeChapter)
			{
				return $"{VersesBaseUrl}/BookChapter/{book.Name}/{chapter}/{bookChapterSlug}";
			}
			else
			{
				return $"{VersesBaseUrl}/Verse/{book.Abrv}-{chapter}-{vr.BegVerse}-{vr.EndVerse}/Englishonly";
			}
		}

		
		public static string GetBCV(BookEnum bookEnum, BibleVerseRange vr, int chapter)
		{
			BookLocal book = BookLocal.FromEnum(bookEnum);
			return $"{book.Name} {chapter} {vr.BegVerse}:{vr.EndVerse}";
		}
		*/

	}
}

/*
 
# LivingMessiah.Bible.Book database
	Id: 1	
	Title: Genesis	
	Abrv: Gen	
	ScriptureId_Beg: 1	
	ScriptureId_End: 1533	
	BookGroupId: 1	
	HebrewTitle: Beresheeth
	HebrewName:	בְּרֵאשִׁית	
	LastChapter: 50

---

# ToDo: Should LivingMessiah.Domain!BibleBook be ported here?
- Used by 
	- [I]ShabbatWeekRepository  !Task<BibleBook> GetTorahBookById(int id); 
  - [I]ShabbatWeekCacheService!Task<BibleBook> GetTorahBookById(int id);
  - LivingMessiah.Web\Pages\Parasha\IndexTable.razor.cs ! 		protected BibleBook Book { get; set; }

namespace LivingMessiah.Domain
{
	public class BibleBook
	{
		public int Id { get; set; }
		public string Abrv { get; set; }
		public string EnglishTitle { get; set; }
		public string HebrewTitle { get; set; }
		public string HebrewName { get; set; }
	}
} 


---

# ToDo: there is this as well...
namespace LivingMessiah.Domain.Parasha.Enums
{
	public enum BookFilterEnum
	{
		All = 0,
		Genesis = 1,
		Exodus = 2,
		Leviticus = 3,
		Numbers = 4,
		Deuteronomy = 5
	}
}


 */