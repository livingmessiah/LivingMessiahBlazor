using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Calendar.Enums;

public abstract class DateTypeHeaderRow : SmartEnum<DateTypeHeaderRow>
{
	#region Id's
	private static class Id
	{
		internal const int Month = 1;
		internal const int Feast = 2;
		internal const int Season = 3;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DateTypeHeaderRow Month = new MonthSE();
	public static readonly DateTypeHeaderRow Feast = new FeastSE();
	public static readonly DateTypeHeaderRow SeasonWinter = new SeasonWinterSE();
	// SE=SmartEnum
	#endregion

	private DateTypeHeaderRow(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string BadgeColor { get; }
	public abstract string Icon { get; }
	//public abstract string Title { get; } public override string Title => "far fa-moon";
	#endregion

	#region Private Instantiation

	private sealed class MonthSE : DateTypeHeaderRow
	{
		public MonthSE() : base($"{nameof(Id.Month)}", Id.Month) { }
		public override string BadgeColor => " bg-dark";
		public override string Icon => "far fa-moon";
	}

	private sealed class FeastSE : DateTypeHeaderRow
	{
		public FeastSE() : base($"{nameof(Id.Feast)}", Id.Feast) { }
		public override string BadgeColor => " bg-info";
		public override string Icon => "fas fa-glass-cheers";
	}

	private sealed class SeasonWinterSE : DateTypeHeaderRow
	{
		public SeasonWinterSE() : base($"{nameof(Id.Season)}", Id.Season) { }
		public override string BadgeColor => " bg-dark";
		public override string Icon => "far fa-moon";
	}

	#endregion
}
