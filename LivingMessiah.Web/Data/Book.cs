using System;
using System.Collections.Generic;
using System.Linq;
using LivingMessiah.Web.Domain;

//ToDo: needs to be depreciated
//See LivingMessiah.Domain\BookEnumReplacement.cs
namespace LivingMessiah.Web.Data
{
	public enum BookEnum
	{
		Psalms = 1,
		Proverbs = 2
	}

	public class Book
	{
		public static List<Book> All { get; } = new List<Book>();
		public static Book Psalms { get; } = new Book(BookEnum.Psalms, 29, "Psalms", "Psa");
		public static Book Proverbs { get; } = new Book(BookEnum.Proverbs, 30, "Proverbs", "Pro");

		public BookEnum BookEnum { get; private set; }
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Abrv { get; private set; }

		private const string VersesBaseUrl = "https://myhebrewbible.com/";

		private Book(BookEnum bookEnum, int id, string name, string abrv)
		{
			BookEnum = bookEnum;
			Id = id;
			Name = name;
			Abrv = abrv;
			All.Add(this);
		}

		public static Book FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}

		public static Book FromEnum(BookEnum enumValue)
		{
			return All.SingleOrDefault(r => r.BookEnum == enumValue);
		}

		public static string GetUrl(BookEnum bookEnum, VerseRange vr, int chapter, string bookChapterSlug, bool isWholeChapter = false) 
		{
			Book book = Book.FromEnum(bookEnum);

			if (isWholeChapter)
			{
				return $"{VersesBaseUrl}BookChapter/{book.Name}/{chapter}/{bookChapterSlug}";
			}
			else
			{
				return $"{VersesBaseUrl}Verse/{book.Abrv}-{chapter}-{vr.BegVerse}-{vr.EndVerse}/Englishonly";
			}
		}

		public static string GetBCV(BookEnum bookEnum, VerseRange vr, int chapter)
		{
			Book book = Book.FromEnum(bookEnum);
			return $"{book.Name} {chapter}:{vr.BegVerse}-{vr.EndVerse}";
		}

	}
}
