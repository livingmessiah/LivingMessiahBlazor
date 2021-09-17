using Ardalis.SmartEnum;

namespace SukkotApi.Domain.Donations.Enums
{
	public abstract class BaseDonationStatusFilterSmartEnum: SmartEnum<BaseDonationStatusFilterSmartEnum>
	{
		#region Id's
		private static class Id
		{                                         // Sukkot.Status
			internal const int FullList = 0;				// 
			internal const int NoPayments = 1;      // 2 RFC Registration Form Completed; 3 MFC	Meal Form Completed						
			internal const int PartiallyPaid = 2;   // 4 pp  PartiallPaid
			internal const int FullyPaid = 3;       // 5 FP	 Fully Paid
		}
		#endregion


		#region  Declared Public Instances
		public static readonly BaseDonationStatusFilterSmartEnum FullList = new FullListSE();  
		public static readonly BaseDonationStatusFilterSmartEnum NoPayments = new NoPaymentsSE();
		public static readonly BaseDonationStatusFilterSmartEnum PartiallyPaid = new PartiallyPaidSE();
		public static readonly BaseDonationStatusFilterSmartEnum FullyPaid = new FullyPaidSE();
		// SE=SmartEnum
		#endregion

		private BaseDonationStatusFilterSmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string ButtonName { get; }
		#endregion

		#region Private Instantiation
		private sealed class FullListSE : BaseDonationStatusFilterSmartEnum
		{
			public FullListSE() : base($"{nameof(Id.FullList)}", Id.FullList) { }
			public override string ButtonName => "All";
		}

		private sealed class NoPaymentsSE : BaseDonationStatusFilterSmartEnum
		{
			public NoPaymentsSE() : base($"{nameof(Id.NoPayments)}", Id.NoPayments) { }
			public override string ButtonName => "No Payments";
		}

		private sealed class PartiallyPaidSE : BaseDonationStatusFilterSmartEnum
		{
			public PartiallyPaidSE() : base($"{nameof(Id.PartiallyPaid)}", Id.PartiallyPaid) { }
			public override string ButtonName => "Partially Paid";
		}

		private sealed class FullyPaidSE : BaseDonationStatusFilterSmartEnum
		{
			public FullyPaidSE() : base($"{nameof(Id.FullyPaid)}", Id.FullyPaid) { }
			public override string ButtonName => "Fully Paid";
		}
		#endregion

	}
}
