using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.Calendar.Enums;

public abstract class Season : SmartEnum<Season>
{

	#region Id's
	private static class Id
	{
		internal const int Winter = 1;
		internal const int Spring = 2;
		internal const int Summer = 3;
		internal const int Fall = 4;
	}
	#endregion


	#region Declared Public Instances
	public static readonly Season Winter = new WinterSE();
	public static readonly Season Spring = new SpringSE();
	public static readonly Season Summer = new SummerSE();
	public static readonly Season Fall = new FallSE();
	#endregion

	private Season(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Type { get; }
	public abstract string BadgeColor { get; }
	public abstract string Icon { get; }
	public abstract string CalendarColor { get; }

	#endregion

	#region Private Instantiation
	private sealed class WinterSE : Season
	{
		public WinterSE() : base($"{nameof(Id.Winter)}", Id.Winter) { }
		public override string Type => "Solstice";
		public override string BadgeColor => "bg-primary";
		public override string Icon => "fas fa-snowflake";
		public override string CalendarColor => CalendarColors.Primary;
	}
	private sealed class SpringSE : Season
	{
		public SpringSE() : base($"{nameof(Id.Spring)}", Id.Spring) { }
		public override string Type => "Equinox"; // Equilux
		public override string BadgeColor => "bg-success";
		public override string Icon => "fas fa-cloud-sun-rain";
		public override string CalendarColor => CalendarColors.Success;
	}
	private sealed class SummerSE : Season
	{
		public SummerSE() : base($"{nameof(Id.Summer)}", Id.Summer) { }
		public override string Type => "Solstice";
		public override string BadgeColor => "bg-danger";
		public override string Icon => "far fa-sun";
		public override string CalendarColor => CalendarColors.Danger;
	}
	private sealed class FallSE : Season
	{
		public FallSE() : base("Fall", Id.Fall) { }
		public override string Type => "Equinox";  // Equilux
		public override string BadgeColor => "bg-warning";
		public override string Icon => "fab fa-canadian-maple-leaf";
		public override string CalendarColor => CalendarColors.Warning;
	}
	#endregion

}
// Ignore Spelling: Equilux