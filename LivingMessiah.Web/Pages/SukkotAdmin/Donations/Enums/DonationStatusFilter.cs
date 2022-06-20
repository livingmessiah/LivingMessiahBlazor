using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations.Enums;

public abstract class DonationStatusFilter : SmartEnum<DonationStatusFilter>
{
	#region Id's
	private static class Id
	{                                       // Sukkot.Status
		internal const int FullList = 0;    // 
		internal const int NoPayments = 1;  // 2 AHRA	Accepted House Rules Agreement 3 RFC Registration Form Completed
		internal const int PartiallyPaid = 2;   // 4 pp  PartiallPaid
		internal const int FullyPaid = 3;   // 5 FP	 Fully Paid
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DonationStatusFilter FullList = new FullListSE();
	public static readonly DonationStatusFilter NoPayments = new NoPaymentsSE();
	public static readonly DonationStatusFilter PartiallyPaid = new PartiallyPaidSE();
	public static readonly DonationStatusFilter FullyPaid = new FullyPaidSE();
	// SE=SmartEnum
	#endregion

	private DonationStatusFilter(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string ButtonName { get; }
	#endregion

	#region Private Instantiation
	private sealed class FullListSE : DonationStatusFilter
	{
		public FullListSE() : base($"{nameof(Id.FullList)}", Id.FullList) { }
		public override string ButtonName => "All";
	}

	private sealed class NoPaymentsSE : DonationStatusFilter
	{
		public NoPaymentsSE() : base($"{nameof(Id.NoPayments)}", Id.NoPayments) { }
		public override string ButtonName => "No Payments";
	}

	private sealed class PartiallyPaidSE : DonationStatusFilter
	{
		public PartiallyPaidSE() : base($"{nameof(Id.PartiallyPaid)}", Id.PartiallyPaid) { }
		public override string ButtonName => "Partially Paid";
	}

	private sealed class FullyPaidSE : DonationStatusFilter
	{
		public FullyPaidSE() : base($"{nameof(Id.FullyPaid)}", Id.FullyPaid) { }
		public override string ButtonName => "Fully Paid";
	}
	#endregion

}
