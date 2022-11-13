using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Calendar.Enums;

public abstract class FeastDayDetail : SmartEnum<FeastDayDetail>
{
	#region Id's
	private static class Id
	{
		internal const int SederMeal = 1;
		internal const int UnleavenedBreadDay1 = 2;
		internal const int OmerStart = 3;
		internal const int UnleavenedBreadDay7 = 4;

		internal const int TrumpetsErev = 5; // -1; We blow the trumpets at sunset
		internal const int TrumpetsDay = 6;  //  0; A high holy day

		internal const int YomKippurErev = 7;						// -1; at evening we afflict our souls
		internal const int YomKippurDayAfternoon = 8;		//  0; In the afternoon we have our Yom Kippur service
		internal const int YomKippurDayAfterSunset = 9; //  0; We break the fast after the sun sets

		internal const int SukkotDay0 = 10;
		internal const int SukkotDay1 = 11;
		internal const int SukkotLastGreatDay = 12;
		internal const int SukkotEndsAtSundown = 13;
		//HanukkahLastDay = 9;      // not using
		//YomKippurBegins = 10;      // not using
	}
	#endregion


	#region  Declared Public Instances
	public static readonly FeastDayDetail SederMeal = new SederMealSE();
	public static readonly FeastDayDetail UnleavenedBreadDay1 = new UnleavenedBreadDay1SE();
	public static readonly FeastDayDetail OmerStart = new OmerStartSE();
	public static readonly FeastDayDetail UnleavenedBreadDay7 = new UnleavenedBreadDay7SE();

	public static readonly FeastDayDetail TrumpetsErev = new TrumpetsErevSE();
	public static readonly FeastDayDetail TrumpetsDay = new TrumpetsDaySE();

	public static readonly FeastDayDetail YomKippurErev = new YomKippurErevSE();
	public static readonly FeastDayDetail YomKippurDayAfternoon = new YomKippurDayAfternoonSE();
	public static readonly FeastDayDetail YomKippurDayAfterSunset = new YomKippurDayAfterSunsetSE();

	public static readonly FeastDayDetail SukkotDay0 = new SukkotDay0SE();
	public static readonly FeastDayDetail SukkotDay1 = new SukkotDay1SE();
	public static readonly FeastDayDetail SukkotLastGreatDay = new SukkotLastGreatDaySE();
	public static readonly FeastDayDetail SukkotEndsAtSundown = new SukkotEndsAtSundownSE();
	// SE=SmartEnum
	#endregion

	private FeastDayDetail(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	//public abstract FeastDay ParentId { get; }
	public abstract int ParentFeastDayId { get; }
	public abstract int AddDays { get;  }
	public abstract string Descr { get;  }
	//public abstract bool IsSabbathDay { get; }
	#endregion

	#region Private Instantiation

	private sealed class SederMealSE : FeastDayDetail
	{
		public SederMealSE() : base($"{nameof(Id.SederMeal)}", Id.SederMeal) { }
		public override int ParentFeastDayId => Enums.FeastDay.Passover.Value;
		public override int AddDays => 0;
		public override string Descr => "Seder Meal";
	}

	private sealed class UnleavenedBreadDay1SE : FeastDayDetail
	{
		public UnleavenedBreadDay1SE() : base($"{nameof(Id.UnleavenedBreadDay1)}", Id.UnleavenedBreadDay1) { }
		public override int ParentFeastDayId => Enums.FeastDay.Passover.Value;
		public override int AddDays => 1;
		public override string Descr => "First day of Unleavened Bread";
	}

	private sealed class OmerStartSE : FeastDayDetail
	{
		public OmerStartSE() : base($"{nameof(Id.OmerStart)}", Id.OmerStart) { }
		public override int ParentFeastDayId => Enums.FeastDay.Passover.Value;
		public override int AddDays => 2;
		public override string Descr => "Omer Start";
	}

	private sealed class UnleavenedBreadDay7SE : FeastDayDetail
	{
		public UnleavenedBreadDay7SE() : base($"{nameof(Id.UnleavenedBreadDay7)}", Id.UnleavenedBreadDay7) { }
		public override int ParentFeastDayId => Enums.FeastDay.Passover.Value;
		public override int AddDays => 7;
		public override string Descr => "Last day of Unleavened Bread";
	}



	private sealed class TrumpetsErevSE : FeastDayDetail
	{
		public TrumpetsErevSE() : base($"{nameof(Id.TrumpetsErev)}", Id.TrumpetsErev) { }
		public override int ParentFeastDayId => Enums.FeastDay.Trumpets.Value;
		public override int AddDays => -1;
		public override string Descr => "We blow the trumpets at sunset";
	}

	private sealed class TrumpetsDaySE : FeastDayDetail
	{
		public TrumpetsDaySE() : base($"{nameof(Id.TrumpetsDay)}", Id.TrumpetsDay) { }
		public override int ParentFeastDayId => Enums.FeastDay.Trumpets.Value;
		public override int AddDays => 0;
		public override string Descr => "A high holy day sabbath";
	}



	private sealed class YomKippurErevSE : FeastDayDetail
	{
		public YomKippurErevSE() : base($"{nameof(Id.YomKippurErev)}", Id.YomKippurErev) { }
		public override int ParentFeastDayId => Enums.FeastDay.YomKippur.Value;
		public override int AddDays => -1;
		public override string Descr => "at evening we afflict our souls";
	}

	private sealed class YomKippurDayAfternoonSE : FeastDayDetail
	{
		public YomKippurDayAfternoonSE() : base($"{nameof(Id.YomKippurDayAfternoon)}", Id.YomKippurDayAfternoon) { }
		public override int ParentFeastDayId => Enums.FeastDay.YomKippur.Value;
		public override int AddDays => 0;
		public override string Descr => "In the afternoon we have our Yom Kippur service";
	}

	private sealed class YomKippurDayAfterSunsetSE : FeastDayDetail
	{
		public YomKippurDayAfterSunsetSE() : base($"{nameof(Id.YomKippurDayAfterSunset)}", Id.YomKippurDayAfterSunset) { }
		public override int ParentFeastDayId => Enums.FeastDay.YomKippur.Value;
		public override int AddDays => 0;
		public override string Descr => "We break the fast after the sun sets";
	}




	private sealed class SukkotDay0SE : FeastDayDetail
	{
		public SukkotDay0SE() : base($"{nameof(Id.SukkotDay0)}", Id.SukkotDay0) { }
		public override int ParentFeastDayId => Enums.FeastDay.Tabernacles.Value;
		public override int AddDays => -1;
		public override string Descr => "Preparation Day, High Sabbath begins at sunset";
	}

	private sealed class SukkotDay1SE : FeastDayDetail
	{
		public SukkotDay1SE() : base($"{nameof(Id.SukkotDay1)}", Id.SukkotDay1) { }
		public override int ParentFeastDayId => Enums.FeastDay.Tabernacles.Value;
		public override int AddDays => 0;
		public override string Descr => "First Day of Sukkot (Sabbath)";
	}

	private sealed class SukkotLastGreatDaySE : FeastDayDetail
	{
		public SukkotLastGreatDaySE() : base($"{nameof(Id.SukkotLastGreatDay)}", Id.SukkotLastGreatDay) { }
		public override int ParentFeastDayId => Enums.FeastDay.Tabernacles.Value;
		public override int AddDays => 7;
		public override string Descr => "Last Day (Great Day), Sabbath";
	}

	private sealed class SukkotEndsAtSundownSE : FeastDayDetail
	{
		public SukkotEndsAtSundownSE() : base($"{nameof(Id.SukkotEndsAtSundown)}", Id.SukkotEndsAtSundown) { }
		public override int ParentFeastDayId => Enums.FeastDay.Tabernacles.Value;
		public override int AddDays => 8;
		public override string Descr => "Sukkot ended previous night; tear down camp";
	}

	#endregion
}
