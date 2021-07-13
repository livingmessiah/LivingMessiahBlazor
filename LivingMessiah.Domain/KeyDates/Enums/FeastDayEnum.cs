using System.Collections.Generic;
using System.Linq;


namespace LivingMessiah.Domain.KeyDates.Enums
{
	public enum FeastDayEnum
	{
		Hanukkah = 1,
		Purim = 2,
		Passover = 3,
		Weeks = 4,
		Trumpets = 5,
		YomKippur = 6,
		Tabernacles = 7,
	}

	public class FeastDayLocal
	{
		public static List<FeastDayLocal> All { get; } = new List<FeastDayLocal>();

		/*
		select Name, Transliteration, Hebrew, Details, AddDaysDescr, AddDays, '' AS Icon FROM KeyDate.FeastDay

		*/
		public static FeastDayLocal Hanukkah { get; } = new FeastDayLocal(FeastDayEnum.Hanukkah, 1, "Hanukkah", "", "חֲנֻכָּה", "", "Last day", 8, "fas fa-hanukiah");
		public static FeastDayLocal Purim { get; } = new FeastDayLocal(FeastDayEnum.Purim, 2, "Purim", "", "פֶּסַח", "", null, null, "fas fa-mask");
		public static FeastDayLocal Passover { get; } = new FeastDayLocal(FeastDayEnum.Passover, 3, "Passover", "Pesach", "פֶּסַח", "", null, null, "fas fa-frog");
		public static FeastDayLocal Weeks { get; } = new FeastDayLocal(FeastDayEnum.Hanukkah, 4, "Weeks", "Shavu'ot", "שָׁבוּעוֹת", "Also called Pentecost", null, null, "fas fa-swimmer");
		public static FeastDayLocal Trumpets { get; } = new FeastDayLocal(FeastDayEnum.Trumpets, 5, "Trumpets", "Yom Teruah", "יוֹם תְּרוּעָה", "", "Trumpets Day", 1, "fas fa-bullhorn");
		public static FeastDayLocal YomKippur { get; } = new FeastDayLocal(FeastDayEnum.YomKippur, 6, "Yom Kippur", "", "יוֹם כִּיפּוּר", "Day of Atonement", "Begins sundown", -1, "fas fa-praying-hands");  // "far fa-handshake"
		public static FeastDayLocal Tabernacles { get; } = new FeastDayLocal(FeastDayEnum.Tabernacles, 7, "Tabernacles", "Sukkot", "סֻּכּוֹת", "", null, null, "fas fa-campground");

		public FeastDayEnum FeastDayEnum { get; private set; }
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Transliteration { get; private set; }
		public string Hebrew { get; private set; }
		public string AddDaysDescr { get; private set; }
		public int? AddDays { get; private set; }
		public string Icon { get; private set; }
		public string Details { get; private set; }

		private FeastDayLocal(FeastDayEnum feastDayEnum, int id, string name, string transliteration
			, string hebrew, string details, string addDaysDescr, int? addDays, string icon) 
		{
			FeastDayEnum = feastDayEnum;
			Id = id;
			Name = name;
			Transliteration = transliteration;
			Hebrew = hebrew;
			Details = details;
			AddDaysDescr = addDaysDescr;
			AddDays = addDays;
			Icon = icon;
			All.Add(this);
		}

		public static FeastDayLocal FromEnum(FeastDayEnum enumValue)
		{
			return All.SingleOrDefault(r => r.FeastDayEnum == enumValue);
		}


		public static FeastDayLocal FromInt(int intValue)
		{
			return All.SingleOrDefault(r => r.Id == intValue);
		}


	} // class FeastDayLocal
} // namespace

