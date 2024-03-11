using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.Calendar.Enums;

public abstract class FeastDayDetail : SmartEnum<FeastDayDetail>
{
	#region Id's
	private static class Id
	{
		internal const int PassoverDay = 1;
		internal const int UnleavenedBreadDay1 = 2;
		internal const int UnleavenedBreadDay7 = 3;
		internal const int TrumpetsErev = 4;
		internal const int YomKippurErev = 5;
		internal const int SukkotDay0 = 6;
		internal const int SukkotLastGreatDay = 7;
		internal const int SukkotEndsAtSundown = 8;
	}
	#endregion


	#region  Declared Public Instances
	public static readonly FeastDayDetail PassoverDay = new PassoverDaySE();
	public static readonly FeastDayDetail UnleavenedBreadDay1 = new UnleavenedBreadDay1SE();
	public static readonly FeastDayDetail UnleavenedBreadDay7 = new UnleavenedBreadDay7SE();
	public static readonly FeastDayDetail TrumpetsErev = new TrumpetsErevSE();
	public static readonly FeastDayDetail YomKippurErev = new YomKippurErevSE();
	public static readonly FeastDayDetail SukkotDay0 = new SukkotDay0SE();
	public static readonly FeastDayDetail SukkotLastGreatDay = new SukkotLastGreatDaySE();
	public static readonly FeastDayDetail SukkotEndsAtSundown = new SukkotEndsAtSundownSE();
	// SE=SmartEnum
	#endregion

	private FeastDayDetail(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract int ParentFeastDayId { get; }
	public abstract int AddDays { get;  }
	public abstract string Description { get;  }
	public abstract string Title { get; }
	public abstract string CalendarColor { get; }
	public abstract string ParentDate { get; }

	#endregion

	#region Private Instantiation

	private sealed class PassoverDaySE : FeastDayDetail
	{
		public PassoverDaySE() : base($"{nameof(Id.PassoverDay)}", Id.PassoverDay) { }
		public override int ParentFeastDayId => FeastDay.Passover.Value;
		public override int AddDays => -1;  // PassoverDay
		public override string Description => "Passover Seder Meal";
		public override string Title => "Passover";
		public override string CalendarColor => CalendarColors.Purple;
		public override string ParentDate => FeastDay.FromValue(ParentFeastDayId).Date.AddDays(AddDays).ToString(DateFormat.ddd_mm_dd);
	}

	private sealed class UnleavenedBreadDay1SE : FeastDayDetail
	{
		public UnleavenedBreadDay1SE() : base($"{nameof(Id.UnleavenedBreadDay1)}", Id.UnleavenedBreadDay1) { }
		public override int ParentFeastDayId => FeastDay.Passover.Value;
		public override int AddDays => 0;  // UnleavenedBreadDay1
		public override string Description => "First day of Unleavened Bread";
		public override string Title => "Unleavened Bread day 1";
		public override string CalendarColor => Enums.DateType.Feast.CalendarColor;
		public override string ParentDate => FeastDay.FromValue(ParentFeastDayId).Date.AddDays(AddDays).ToString(DateFormat.ddd_mm_dd);
	}

	private sealed class UnleavenedBreadDay7SE : FeastDayDetail
	{
		public UnleavenedBreadDay7SE() : base($"{nameof(Id.UnleavenedBreadDay7)}", Id.UnleavenedBreadDay7) { }
		public override int ParentFeastDayId => FeastDay.Passover.Value;
		public override int AddDays => 6; // UnleavenedBreadDay1 + 6
		public override string Description => "Last day of Unleavened Bread";
		public override string Title => "Unleavened Bread day 7";
		public override string CalendarColor => Enums.DateType.Feast.CalendarColor;
		public override string ParentDate => FeastDay.FromValue(ParentFeastDayId).Date.AddDays(AddDays).ToString(DateFormat.ddd_mm_dd);
	}

	private sealed class TrumpetsErevSE : FeastDayDetail
	{
		public TrumpetsErevSE() : base($"{nameof(Id.TrumpetsErev)}", Id.TrumpetsErev) { }
		public override int ParentFeastDayId => FeastDay.Trumpets.Value;
		public override int AddDays => -1; // TrumpetsErev
		public override string Description => "We blow the trumpets at sunset";
		public override string Title => "🎺 Erev Trumpets";
		public override string CalendarColor => Enums.DateType.Feast.CalendarColor;
		public override string ParentDate => FeastDay.FromValue(ParentFeastDayId).Date.AddDays(AddDays).ToString(DateFormat.ddd_mm_dd);
	}

	private sealed class YomKippurErevSE : FeastDayDetail
	{
		public YomKippurErevSE() : base($"{nameof(Id.YomKippurErev)}", Id.YomKippurErev) { }
		public override int ParentFeastDayId => FeastDay.YomKippur.Value;
		public override int AddDays => -1;  // YomKippurErev
		public override string Description => "at evening we afflict our souls";
		public override string Title => "Erev Yom Kippur"; 
		public override string CalendarColor => Enums.DateType.Feast.CalendarColor;
		public override string ParentDate => FeastDay.FromValue(ParentFeastDayId).Date.AddDays(AddDays).ToString(DateFormat.ddd_mm_dd);
	}

	private sealed class SukkotDay0SE : FeastDayDetail
	{
		public SukkotDay0SE() : base($"{nameof(Id.SukkotDay0)}", Id.SukkotDay0) { }
		public override int ParentFeastDayId => FeastDay.Tabernacles.Value;
		public override int AddDays => -1; // SukkotDay0
		public override string Description => "Preparation Day, High Sabbath begins at sunset";
		public override string Title => "Preparation Day 🎪 ⬆";
		public override string CalendarColor => Enums.DateType.Feast.CalendarColor;
		public override string ParentDate => FeastDay.FromValue(ParentFeastDayId).Date.AddDays(AddDays).ToString(DateFormat.ddd_mm_dd);
	}

	private sealed class SukkotLastGreatDaySE : FeastDayDetail
	{
		public SukkotLastGreatDaySE() : base($"{nameof(Id.SukkotLastGreatDay)}", Id.SukkotLastGreatDay) { }
		public override int ParentFeastDayId => FeastDay.Tabernacles.Value;
		public override int AddDays => 7;  // SukkotLastGreatDay
		public override string Description => "Last Day (Great Day), Sabbath";
		public override string Title => "Sukkot | Day 7";
		public override string CalendarColor => Enums.DateType.Feast.CalendarColor;
		public override string ParentDate => FeastDay.FromValue(ParentFeastDayId).Date.AddDays(AddDays).ToString(DateFormat.ddd_mm_dd);
	}

	private sealed class SukkotEndsAtSundownSE : FeastDayDetail
	{
		public SukkotEndsAtSundownSE() : base($"{nameof(Id.SukkotEndsAtSundown)}", Id.SukkotEndsAtSundown) { }
		public override int ParentFeastDayId => FeastDay.Tabernacles.Value;
		public override int AddDays => 8;  // SukkotEndsAtSundown
		public override string Description => "Sukkot ended previous night; tear down camp";
		public override string Title => "Camp tear down  🎪 ⬇";
		public override string CalendarColor => Enums.DateType.Feast.CalendarColor;
		public override string ParentDate => FeastDay.FromValue(ParentFeastDayId).Date.AddDays(AddDays).ToString(DateFormat.ddd_mm_dd);
	}

	#endregion
}

// Ignore Spelling: Descr
// Ignore Spelling: Erev
// Ignore Spelling: YomKippur
// Ignore Spelling: Kippur
// Ignore Spelling: Yom

