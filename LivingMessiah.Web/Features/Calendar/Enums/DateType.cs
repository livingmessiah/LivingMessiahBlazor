using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.Calendar.Enums;


public abstract class DateType : SmartEnum<DateType>
{
	#region Id's
	private static class Id
	{
		internal const int All = -1;
		internal const int None = 0;

		internal const int Month = 1;
		internal const int Feast = 2;
		internal const int Season = 3;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DateType All = new AllSE();
	public static readonly DateType Month = new MonthSE();
	public static readonly DateType Feast = new FeastSE();
	public static readonly DateType Season = new SeasonSE();
	// SE=SmartEnum
	#endregion

	private DateType(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string BadgeColor { get; }
	public abstract string Icon { get; }
	public abstract string TextColor { get; }
	public abstract string CalendarColor { get; }
	public abstract string FilterLabel { get; }
	#endregion

	#region Private Instantiation

	private sealed class AllSE : DateType
	{
		public AllSE() : base($"{nameof(Id.All)}", Id.All) { }
		public override string BadgeColor => string.Empty;
		public override string Icon => string.Empty;
		public override string TextColor => string.Empty;
		public override string CalendarColor => string.Empty;
		public override string FilterLabel => "All";
	}

	private sealed class MonthSE : DateType
	{
		public MonthSE() : base($"{nameof(Id.Month)}", Id.Month) { }
		public override string BadgeColor => "bg-secondary";
		public override string Icon => "far fa-moon";
		public override string TextColor => "text-info";
		public override string CalendarColor => CalendarColors.Dark;
		public override string FilterLabel => "Lunar Month";
	}

	private sealed class FeastSE : DateType
	{
		public FeastSE() : base($"{nameof(Id.Feast)}", Id.Feast) { }
		public override string BadgeColor => "bg-primary";
		public override string Icon => "fas fa-glass-cheers";
		public override string TextColor => "text-primary";
		public override string CalendarColor => CalendarColors.Blue;
		public override string FilterLabel => "Feast";
	}

	private sealed class SeasonSE : DateType
	{
		public SeasonSE() : base($"{nameof(Id.Season)}", Id.Season) { }
		public override string BadgeColor => "bg-success";
		public override string Icon => "fas fa-calendar-alt";  // See BaseSeasonSmartEnum
		public override string TextColor => "text-success";
		public override string CalendarColor => CalendarColors.Olive;
		public override string FilterLabel => "Season";
	}
	#endregion

}


