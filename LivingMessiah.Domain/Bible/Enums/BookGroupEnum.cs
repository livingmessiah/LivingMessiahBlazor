using System;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Domain.Bible.Enums
{
	public enum BookGroupEnum
	{
		Torah = 1,
		History = 2,
		Poetry = 3,
		MajorProphets = 4,
		MinorProphets = 5,
		Gospels = 6,
		PaulsEpistles = 7,
		GeneralEpistles = 8,
		Apocalypse = 9,
	}

	public class BookGroupLocal
	{
		public static List<BookGroupLocal> All { get; } = new List<BookGroupLocal>();
		public static BookGroupLocal Torah { get; } = new BookGroupLocal(BookGroupEnum.Torah, "Torah", true);
		public static BookGroupLocal History { get; } = new BookGroupLocal(BookGroupEnum.History, "History", true);
		public static BookGroupLocal Poetry { get; } = new BookGroupLocal(BookGroupEnum.Poetry, "Poetry", true);
		public static BookGroupLocal MajorProphets { get; } = new BookGroupLocal(BookGroupEnum.MajorProphets, "Major Prophets", true);
		public static BookGroupLocal MinorProphets { get; } = new BookGroupLocal(BookGroupEnum.MinorProphets, "Minor Prophets", true);
		public static BookGroupLocal Gospels { get; } = new BookGroupLocal(BookGroupEnum.Gospels, "Gospels", false);
		public static BookGroupLocal PaulsEpistles { get; } = new BookGroupLocal(BookGroupEnum.PaulsEpistles, "Pauls Epistles", false);
		public static BookGroupLocal GeneralEpistles { get; } = new BookGroupLocal(BookGroupEnum.GeneralEpistles, "Genera lEpistles", false);
		public static BookGroupLocal Apocalypse { get; } = new BookGroupLocal(BookGroupEnum.Apocalypse, "Apocalypse", false);

		public BookGroupEnum BookGroupEnum { get; private set; }
		public int Id { get; private set; }
		public string Descr { get; private set; } 
		public bool IsOldTestament { get; set; }


		private BookGroupLocal(BookGroupEnum bookGroupEnum, string descr, bool isOldTestament) // , string notes
		{
			BookGroupEnum = bookGroupEnum;
			Id = (int)bookGroupEnum;
			Descr = descr;
			IsOldTestament = isOldTestament;
			All.Add(this);
		}

		public static BookGroupLocal FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Descr, formatString, StringComparison.OrdinalIgnoreCase));
		}

		public static BookGroupLocal FromEnum(BookGroupEnum enumValue)
		{
			return All.SingleOrDefault(r => r.BookGroupEnum == enumValue);
		}

		public static BookGroupLocal FromInt(int intValue)
		{
			return All.SingleOrDefault(r => r.Id == intValue);
		}


	}
}

