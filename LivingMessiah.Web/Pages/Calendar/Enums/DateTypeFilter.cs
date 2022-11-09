using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Calendar.Enums;

public abstract class DateTypeFilter : SmartEnum<DateTypeFilter>
{
	#region Id's
	private static class Id
	{
		internal const int FullList = 0;
		internal const int Month = 1;
		internal const int Feast = 2;
		internal const int Season = 3;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DateTypeFilter FullList = new FullListSE();
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


	private sealed class FullListSE : DateTypeFilter
	{
		public FullListSE() : base($"{nameof(Id.FullList)}", Id.FullList) { }
		public override string ButtonName => "All";
	}


	private sealed class MonthSE : DateTypeFilter
	{
		public MonthSE() : base($"{nameof(Id.Month)}", Id.Month) { }
		public override string ButtonName => "Lunar Month";
	}

	private sealed class FeastSE : DateTypeFilter
	{
		public FeastSE() : base($"{nameof(Id.Feast)}", Id.Feast) { }
		public override string ButtonName => "Feast";
	}

	private sealed class SeasonSE : DateTypeFilter
	{
		public SeasonSE() : base($"{nameof(Id.Season)}", Id.Season) { }
		public override string ButtonName => "Season";
	}
	#endregion

}


