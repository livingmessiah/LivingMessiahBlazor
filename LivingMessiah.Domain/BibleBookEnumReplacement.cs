using System;
using System.Collections.Generic;

using System.Linq;

namespace LivingMessiah.Domain
{

	public enum BibleBookEnum
	{
		Psalms = 1,
		Proverbs = 2
	}

	public class BibleBookEnumReplacement
	{
		public static List<BibleBookEnumReplacement> All { get; } = new List<BibleBookEnumReplacement>();
		public static BibleBookEnumReplacement Psalms { get; } = new BibleBookEnumReplacement(BibleBookEnum.Psalms, 29, "Psalms", "Psa");
		public static BibleBookEnumReplacement Proverbs { get; } = new BibleBookEnumReplacement(BibleBookEnum.Proverbs, 30, "Proverbs", "Pro");

		public BibleBookEnum BibleBookEnum { get; private set; }
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Abrv { get; private set; }

		private const string VersesBaseUrl = "https://myhebrewbible.com/";

		private BibleBookEnumReplacement(BibleBookEnum bookEnum, int id, string name, string abrv)
		{
			BibleBookEnum = bookEnum;
			Id = id;
			Name = name;
			Abrv = abrv;
			All.Add(this);
		}

		public static BibleBookEnumReplacement FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}

		public static BibleBookEnumReplacement FromEnum(BibleBookEnum enumValue)
		{
			return All.SingleOrDefault(r => r.BibleBookEnum == enumValue);
		}

		//ToDo: where is this used?
		public static string GetUrl(BibleBookEnum bookEnum, BibleVerseRange vr, int chapter, string bookChapterSlug, bool isWholeChapter = false)
		{
			BibleBookEnumReplacement book = BibleBookEnumReplacement.FromEnum(bookEnum);

			if (isWholeChapter)
			{
				return $"{VersesBaseUrl}/BookChapter/{book.Name}/{chapter}/{bookChapterSlug}";
			}
			else
			{
				return $"{VersesBaseUrl}/Verse/{book.Abrv}-{chapter}-{vr.BegVerse}-{vr.EndVerse}/Englishonly";
			}
		}

		//ToDo: where is this used?
		public static string GetBCV(BibleBookEnum bookEnum, BibleVerseRange vr, int chapter)
		{
			BibleBookEnumReplacement book = BibleBookEnumReplacement.FromEnum(bookEnum);
			return $"{book.Name} {chapter} {vr.BegVerse}:{vr.EndVerse}";
		}


	}
}
