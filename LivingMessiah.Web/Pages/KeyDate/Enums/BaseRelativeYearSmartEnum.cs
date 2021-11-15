using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.KeyDate.Enums
{
	public abstract class BaseRelativeYearSmartEnum: SmartEnum<BaseRelativeYearSmartEnum>
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
		public static readonly BaseRelativeYearSmartEnum Previous = new PreviousSE();  
		public static readonly BaseRelativeYearSmartEnum Current = new CurrentSE();
		public static readonly BaseRelativeYearSmartEnum Next = new NextSE();
		// SE=SmartEnum
		#endregion

		private BaseRelativeYearSmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		//public abstract string Abrv { get; }
		//public abstract string LongDescr { get; }
		//public override string Abrv => "My 1st SE"; public override string LongDescr => "My Very First Smart Enum"; 
		#endregion

		#region Private Instantiation
		private sealed class PreviousSE : BaseRelativeYearSmartEnum
		{
			public PreviousSE() : base($"{nameof(Id.Previous)}", Id.Previous) { }
		}

		private sealed class CurrentSE : BaseRelativeYearSmartEnum
		{
			public CurrentSE() : base($"{nameof(Id.Current)}", Id.Current) { }
		}

		private sealed class NextSE : BaseRelativeYearSmartEnum
		{
			public NextSE() : base($"{nameof(Id.Next)}", Id.Next) { }
		}
		#endregion

	}
}

/*
 * 
			


	public enum RelativeYearEnum
	{
		None = 0,
		Previous = 1,
		Current = 2,
		Next = 3
	}
*/