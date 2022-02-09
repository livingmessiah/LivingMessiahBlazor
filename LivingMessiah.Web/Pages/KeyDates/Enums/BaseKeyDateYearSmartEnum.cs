using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.KeyDates.Enums;

public abstract class BaseKeyDateYearSmartEnum : SmartEnum<BaseKeyDateYearSmartEnum>
{
		#region Id's
		private static class Id
		{
				internal const int Previous = 1;
				internal const int Current = 2;
				internal const int Next = 3;
		}
		#endregion


		#region  Declared Public Instances
		public static readonly BaseKeyDateYearSmartEnum Previous = new PreviousSE();
		public static readonly BaseKeyDateYearSmartEnum Current = new CurrentSE();
		public static readonly BaseKeyDateYearSmartEnum Next = new NextSE();
		// SE=SmartEnum
		#endregion

		private BaseKeyDateYearSmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract int Year { get; }
		//public abstract string LongDescr { get; }
		#endregion

		#region Private Instantiation
		private sealed class PreviousSE : BaseKeyDateYearSmartEnum
		{
				public PreviousSE() : base($"{nameof(Id.Previous)}", Id.Previous) { }
				public override int Year => 2021;
		}

		private sealed class CurrentSE : BaseKeyDateYearSmartEnum
		{
				public CurrentSE() : base($"{nameof(Id.Current)}", Id.Current) { }
				public override int Year => 2022;
		}

		private sealed class NextSE : BaseKeyDateYearSmartEnum
		{
				public NextSE() : base($"{nameof(Id.Next)}", Id.Next) { }
				public override int Year => 2023;
		}
		#endregion

}
/*
	public enum RelativeYearEnum
	{
		Previous = 1,
		Current = 2,
		Next = 3
	}
*/