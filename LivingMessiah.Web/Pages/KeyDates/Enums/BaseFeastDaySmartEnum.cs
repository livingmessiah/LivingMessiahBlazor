using Ardalis.SmartEnum;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.KeyDates.Enums
{
	public abstract class BaseFeastDaySmartEnum : SmartEnum<BaseFeastDaySmartEnum>
	{
		#region Id's
		private static class Id
		{
			internal const int Hanukkah = 1;
			internal const int Purim = 2;
			internal const int Passover = 3;
			internal const int Weeks = 4;
			internal const int Trumpets = 5;
			internal const int YomKippur = 6;
			internal const int Tabernacles = 7;
			internal const int HanukkahEOY = 8;
		}
		#endregion

		#region Declared Public Instances
		public static readonly BaseFeastDaySmartEnum Hanukkah = new HanukkahSE();
		public static readonly BaseFeastDaySmartEnum Purim = new PurimSE();
		public static readonly BaseFeastDaySmartEnum Passover = new PassoverSE();
		public static readonly BaseFeastDaySmartEnum Weeks = new WeeksSE();
		public static readonly BaseFeastDaySmartEnum Trumpets = new TrumpetsSE();
		public static readonly BaseFeastDaySmartEnum YomKippur = new YomKippurSE();
		public static readonly BaseFeastDaySmartEnum Tabernacles = new TabernaclesSE();
		public static readonly BaseFeastDaySmartEnum HanukkahEOF = new HanukkahEOYSE();
		// Note; SE=SmartEnum
		#endregion

		private BaseFeastDaySmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string Transliteration { get; }
		public abstract string Hebrew { get; }
		public abstract string Details { get; }
		public abstract string AddDaysDescr { get; }
		public abstract int? AddDays { get; }
//		public abstract MarkupString HtmlTR { get; }
		#endregion

		#region Private Instantiation

		private sealed class HanukkahSE : BaseFeastDaySmartEnum
		{
			public HanukkahSE() : base($"{nameof(Id.Hanukkah)}", Id.Hanukkah) { }
			public override string Transliteration => "";
			public override string Hebrew => "חֲנֻכָּה";
			public override string Details => "";
			public override string AddDaysDescr => "Last day";
			public override int? AddDays => 8;
		}
		private sealed class PurimSE : BaseFeastDaySmartEnum
		{
			public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
			public override string Transliteration => "";
			public override string Hebrew => "פוּרִים";
			public override string Details => "Tradition is to read the book of Esther";
			public override string AddDaysDescr => "";
			public override int? AddDays => 0;
		}
		private sealed class PassoverSE : BaseFeastDaySmartEnum
		{
			public PassoverSE() : base($"{nameof(Id.Passover)}", Id.Passover) { }
			public override string Transliteration => "Pesach";
			public override string Hebrew => "פֶּסַח";
			public override string Details => "The Seder Meal is prepared on the 14th of Aviv and rolls into the first day of Unleavened bread";
			public override string AddDaysDescr => "";
			public override int? AddDays => 0;
		}
		private sealed class WeeksSE : BaseFeastDaySmartEnum
		{
			public WeeksSE() : base($"{nameof(Id.Weeks)}", Id.Weeks) { }
			public override string Transliteration => "Shavu'ot";
			public override string Hebrew => "שָׁבוּעוֹת";
			public override string Details => "Also called Pentecost";
			public override string AddDaysDescr => "";
			public override int? AddDays => 0;
		}
		private sealed class TrumpetsSE : BaseFeastDaySmartEnum
		{
			public TrumpetsSE() : base($"{nameof(Id.Trumpets)}", Id.Trumpets) { }
			public override string Transliteration => "Yom Teruah";
			public override string Hebrew => "יוֹם תְּרוּעָה";
			public override string Details => "";
			public override string AddDaysDescr => "Trumpets Day";
			public override int? AddDays => 1;
		}
		private sealed class YomKippurSE : BaseFeastDaySmartEnum
		{
			public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
			public override string Transliteration => "Yom Kippur";
			public override string Hebrew => "יוֹם כִּיפּוּר";
			public override string Details => "";
			public override string AddDaysDescr => "Begins sundown";
			public override int? AddDays => -1;
		}
		private sealed class TabernaclesSE : BaseFeastDaySmartEnum
		{
			public TabernaclesSE() : base($"{nameof(Id.Tabernacles)}", Id.Tabernacles) { }
			public override string Transliteration => "Sukkot";
			public override string Hebrew => "סֻּכּוֹת";
			public override string Details => "";
			public override string AddDaysDescr => "";
			public override int? AddDays => 0;
		}
		private sealed class HanukkahEOYSE : BaseFeastDaySmartEnum
		{
			public HanukkahEOYSE() : base($"{nameof(Id.HanukkahEOY)}", Id.HanukkahEOY) { }
			public override string Transliteration => "";
			public override string Hebrew => "חֲנֻכָּה";
			public override string Details => "";
			public override string AddDaysDescr => "Last day";
			public override int? AddDays => 8;
		}
		#endregion

	}
}
