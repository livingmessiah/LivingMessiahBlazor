using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.KeyDates.Enums
{
	public abstract class BaseSeasonSmartEnum : SmartEnum<BaseSeasonSmartEnum>
	{

		#region Id's
		private static class Id
		{
			internal const int Fall = 1;
			internal const int Winter = 2;
			internal const int Spring = 3;
			internal const int Summer = 4;
		}
		#endregion


		#region Declared Public Instances
		public static readonly BaseSeasonSmartEnum Fall = new FallSE();
		public static readonly BaseSeasonSmartEnum Winter = new WinterSE();
		public static readonly BaseSeasonSmartEnum Spring = new SpringSE();
		public static readonly BaseSeasonSmartEnum Summer = new SummerSE();
		#endregion

		private BaseSeasonSmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string Type { get; }
		public abstract string BadgeColor { get; }
		public abstract string Icon { get; }

		#endregion

		#region Private Instantiation

		private sealed class FallSE : BaseSeasonSmartEnum
		{
			public FallSE() : base($"{nameof(Id.Fall)}", Id.Fall) { }
			public override string Type => "Equinox";
			public override string BadgeColor => "badge-warning";
			public override string Icon => "fab fa-canadian-maple-leaf";
		}
		private sealed class WinterSE : BaseSeasonSmartEnum
		{
			public WinterSE() : base($"{nameof(Id.Winter)}", Id.Winter) { }
			public override string Type => "Solstice";
			public override string BadgeColor => "badge-primary";
			public override string Icon => "fas fa-snowflake";
		}
		private sealed class SpringSE : BaseSeasonSmartEnum
		{
			public SpringSE() : base($"{nameof(Id.Spring)}", Id.Spring) { }
			public override string Type => "Equinox";
			public override string BadgeColor => "badge-success";
			public override string Icon => "fas fa-cloud-sun-rain";
		}
		private sealed class SummerSE : BaseSeasonSmartEnum
		{
			public SummerSE() : base($"{nameof(Id.Summer)}", Id.Summer) { }
			public override string Type => "Solstice";
			public override string BadgeColor => "badge-danger";
			public override string Icon => "far fa-sun";
		}
		#endregion

	}
}
