using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.KeyDates.Enums
{
	public abstract class BaseDateTypeSmartEnum : SmartEnum<BaseDateTypeSmartEnum>
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
		public static readonly BaseDateTypeSmartEnum Month = new MonthSE();
		public static readonly BaseDateTypeSmartEnum Feast = new FeastSE();
		public static readonly BaseDateTypeSmartEnum Season = new SeasonSE();
		// SE=SmartEnum
		#endregion

		private BaseDateTypeSmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string BadgeColor { get; }
		public abstract string Icon { get; }
		public abstract string TextColor { get; }
		#endregion

		#region Private Instantiation
		private sealed class MonthSE : BaseDateTypeSmartEnum
		{
			public MonthSE() : base($"{nameof(Id.Month)}", Id.Month) { }
			public override string BadgeColor => "badge-info"; 
			public override string Icon => "far fa-moon";
			public override string TextColor => "text-info";
		}

		private sealed class FeastSE : BaseDateTypeSmartEnum
		{
			public FeastSE() : base($"{nameof(Id.Feast)}", Id.Feast) { }
			public override string BadgeColor => "badge-primary"; 
			public override string Icon => "fas fa-glass-cheers";
			public override string TextColor => "text-primary";
		}

		private sealed class SeasonSE : BaseDateTypeSmartEnum
		{
			public SeasonSE() : base($"{nameof(Id.Season)}", Id.Season) { }
			public override string BadgeColor => "badge-success"; 
			public override string Icon => "fas fa-calendar-alt";  // See BaseSeasonSmartEnum
			public override string TextColor => "text-success";
		}
		#endregion

	}
}


