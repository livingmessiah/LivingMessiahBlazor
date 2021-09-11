using Ardalis.SmartEnum;

namespace LivingMessiah.Domain.KeyDates.Enums
{
	public abstract class BaseMySmartEnum: SmartEnum<BaseMySmartEnum>
	{

		#region Id's
		private static class Id
		{
			internal const int MyFirst = 1;
			internal const int MySecond = 2;
			//...
		}
		#endregion


		#region Declared Public Instances
		public static readonly BaseMySmartEnum MyFirst = new MyFirstSE();  // SE=SmartEnum
		public static readonly BaseMySmartEnum MySecond = new MySecondSE();  // SE=
		#endregion

		private BaseMySmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string Abrv { get; }
		public abstract string LongDescr { get; }
		#endregion

		#region Private Instantiation
		private sealed class MyFirstSE : BaseMySmartEnum
		{
			public MyFirstSE() : base($"{nameof(Id.MyFirst)}", Id.MyFirst) { }
			public override string Abrv => "My 1st SE"; public override string LongDescr => "My Very First Smart Enum"; 
		}

		private sealed class MySecondSE : BaseMySmartEnum
		{
			public MySecondSE() : base($"{nameof(Id.MySecond)}", Id.MySecond) { }
			public override string Abrv => "My 2nd SE"; public override string LongDescr => "My Imporoved Second Smart Enum";
		}
		#endregion

	}
}
