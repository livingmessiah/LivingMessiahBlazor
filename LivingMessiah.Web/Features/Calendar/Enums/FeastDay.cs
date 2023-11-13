
using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.Calendar.Enums;

public abstract class FeastDay : SmartEnum<FeastDay>
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
	}
	#endregion

	#region Declared Public Instances
	public static readonly FeastDay Hanukkah = new HanukkahSE();
	public static readonly FeastDay Purim = new PurimSE();
	public static readonly FeastDay Passover = new PassoverSE();
	public static readonly FeastDay Weeks = new WeeksSE();
	public static readonly FeastDay Trumpets = new TrumpetsSE();
	public static readonly FeastDay YomKippur = new YomKippurSE();
	public static readonly FeastDay Tabernacles = new TabernaclesSE();
	// Note; SE=SmartEnum
	#endregion

	private FeastDay(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Transliteration { get; }
	public abstract string Hebrew { get; }
	public abstract string Details { get; }
	public abstract string AddDaysDescr { get; }
	public abstract int? AddDays { get; }
	public abstract bool HasCalendarDetails { get; }

	// This is a sanity check based on the idea for some feast days you can determine how many days are in between the dates
	// E.g. There's 9 days between Trumpets and Yom Kippur, so if the difference between those dates is off then one of the dates is wrong.
	public abstract int? DaysFromPrevFeast { get; }
	#endregion

	#region Private Instantiation

	private sealed class HanukkahSE : FeastDay
	{
		public HanukkahSE() : base($"{nameof(Id.Hanukkah)}", Id.Hanukkah) { }
		public override string Transliteration => "";
		public override string Hebrew => "חֲנֻכָּה";
		public override string Details => "";
		public override string AddDaysDescr => "Last day";
		public override int? AddDays => 8;
		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => null;  // This is the beginning of the year
	}

	private sealed class PurimSE : FeastDay
	{
		public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
		public override string Transliteration => "";
		public override string Hebrew => "פוּרִים";
		public override string Details => "Tradition is to read the book of Esther";
		public override string AddDaysDescr => "";
		public override int? AddDays => 0;
		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => null;  // Hanukkah comes before and it's to fluid to track, so null
	}
	private sealed class PassoverSE : FeastDay
	{
		public PassoverSE() : base($"{nameof(Id.Passover)}", Id.Passover) { }
		public override string Transliteration => "Pesach";
		public override string Hebrew => "פֶּסַח";
		public override string Details => "The Seder Meal is prepared on the 14th of Aviv and rolls into the first day of Unleavened bread";
		public override string AddDaysDescr => "";
		public override int? AddDays => 0;
		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => null;  // Purim comes before and it's to fluid to track, so null
	}
	private sealed class WeeksSE : FeastDay
	{
		public WeeksSE() : base($"{nameof(Id.Weeks)}", Id.Weeks) { }
		public override string Transliteration => "Shavu'ot";
		public override string Hebrew => "שָׁבוּעוֹת";
		public override string Details => "Also called Pentecost";
		public override string AddDaysDescr => "";
		public override int? AddDays => 0;
		public override bool HasCalendarDetails => false;
		public override int? DaysFromPrevFeast => 51;  // Pesach is before and so a hard business rule can be made ... I think ... why isn't it 50?
	}
	private sealed class TrumpetsSE : FeastDay
	{
		public TrumpetsSE() : base($"{nameof(Id.Trumpets)}", Id.Trumpets) { }
		public override string Transliteration => "Yom Teruah";
		public override string Hebrew => "יוֹם תְּרוּעָה";
		public override string Details => "";
		public override string AddDaysDescr => "Blow trumpets sundown";
		public override int? AddDays => -1;
		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => 113;  // Shavuot / Weeks is before and so a hard business rule can be made ... I think 
	}
	private sealed class YomKippurSE : FeastDay
	{
		public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
		public override string Transliteration => "Yom Kippur";
		public override string Hebrew => "יוֹם כִּיפּוּר";
		public override string Details => "";
		public override string AddDaysDescr => "Begins sundown";
		public override int? AddDays => -1;
		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => 9;  // Trumpets is before and so a hard business rule can be made
	}
	private sealed class TabernaclesSE : FeastDay
	{
		public TabernaclesSE() : base($"{nameof(Id.Tabernacles)}", Id.Tabernacles) { }
		public override string Transliteration => "Sukkot";
		public override string Hebrew => "סֻּכּוֹת";
		public override string Details => "";
		public override string AddDaysDescr => "";
		public override int? AddDays => 0;
		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => 5;  // Yom Kippur is before and so a hard business rule can be made 
	}

	#endregion

}

// Ignore Spelling: Descr