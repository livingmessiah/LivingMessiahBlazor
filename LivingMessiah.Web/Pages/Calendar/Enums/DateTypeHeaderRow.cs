using Ardalis.SmartEnum;
using KeyDateEnum = LivingMessiah.Web.Pages.KeyDates.Enums.DateTypeEnum;

namespace LivingMessiah.Web.Pages.Calendar.Enums;

public abstract class DateTypeHeaderRow : SmartEnum<DateTypeHeaderRow>
{
	#region Id's
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
	#endregion

	#region Private Instantiation

	private sealed class MonthSE : DateTypeHeaderRow
	{
		public MonthSE() : base($"{nameof(KeyDateEnum.Month)}", (int)KeyDateEnum.Month) { }
		public override string BadgeColor => " bg-dark";
		public override string Icon => "far fa-moon";
	}

	private sealed class FeastSE : DateTypeHeaderRow
	{
		public FeastSE() : base($"{nameof(KeyDateEnum.Feast)}", (int)KeyDateEnum.Feast) { }
		public override string BadgeColor => " bg-info";
		public override string Icon => "fas fa-glass-cheers";
	}

	private sealed class SeasonWinterSE : DateTypeHeaderRow
	{
		public SeasonWinterSE() : base($"{nameof(KeyDateEnum.Season)}", (int)KeyDateEnum.Season) { }
		public override string BadgeColor => " bg-dark";
		public override string Icon => "far fa-moon";
	}

	#endregion
}
