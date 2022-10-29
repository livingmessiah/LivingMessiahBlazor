using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.KeyDates.Enums;

public abstract class KeyDateYear : SmartEnum<KeyDateYear>
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
		public static readonly KeyDateYear Previous = new PreviousSE();
		public static readonly KeyDateYear Current = new CurrentSE();
		public static readonly KeyDateYear Next = new NextSE();
		// SE=SmartEnum
		#endregion

		private KeyDateYear(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract int Year { get; }
		//public abstract string LongDescr { get; }
		#endregion

		#region Private Instantiation
		private sealed class PreviousSE : KeyDateYear
		{
				public PreviousSE() : base($"{nameof(Id.Previous)}", Id.Previous) { }
				public override int Year => 2021;
		}

		private sealed class CurrentSE : KeyDateYear
		{
				public CurrentSE() : base($"{nameof(Id.Current)}", Id.Current) { }
				public override int Year => 2022;
		}

		private sealed class NextSE : KeyDateYear
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