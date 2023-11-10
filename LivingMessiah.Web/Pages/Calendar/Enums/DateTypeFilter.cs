using Ardalis.SmartEnum;

using KeyDateEnum = LivingMessiah.Web.Pages.KeyDates.Enums.DateTypeEnum;

namespace LivingMessiah.Web.Pages.Calendar.Enums;

public abstract class DateTypeFilter : SmartEnum<DateTypeFilter>
{
	#region Id's
	private static class FlagId
	{
		internal const int All = -1;
		internal const int None = 0;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DateTypeFilter All = new AllSE();
	public static readonly DateTypeFilter Month = new MonthSE();
	public static readonly DateTypeFilter Feast = new FeastSE();
	public static readonly DateTypeFilter Season = new SeasonSE();
	// SE=SmartEnum
	#endregion

	private DateTypeFilter(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string ButtonName { get; }
	#endregion

	#region Private Instantiation


	private sealed class AllSE : DateTypeFilter
	{
		public AllSE() : base($"{nameof(FlagId.All)}", FlagId.All) { }
		public override string ButtonName => "All";
	}


	private sealed class MonthSE : DateTypeFilter
	{
		public MonthSE() : base($"{nameof(KeyDateEnum.Month)}", (int)KeyDateEnum.Month) { }
		public override string ButtonName => "Lunar Month";
	}

	private sealed class FeastSE : DateTypeFilter
	{
		public FeastSE() : base($"{nameof(KeyDateEnum.Feast)}", (int)KeyDateEnum.Feast) { }
		public override string ButtonName => "Feast";
	}

	private sealed class SeasonSE : DateTypeFilter
	{
		public SeasonSE() : base($"{nameof(KeyDateEnum.Season)}", (int)KeyDateEnum.Season) { }
		public override string ButtonName => "Season";
	}
	#endregion

}


