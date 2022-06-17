using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Enums;

public abstract class BaseStatusSmartEnum : SmartEnum<BaseStatusSmartEnum>
{

	#region Id's
	private static class Id
	{
		internal const int EmailNotConfirmed = 0;
		internal const int EmailConfirmation = 1;
		internal const int AcceptedHouseRulesAgreement = 2;  
		internal const int RegistrationFormCompleted = 3;
		internal const int PartiallyPaid = 4;
		internal const int FullyPaid = 5;
		internal const int Canceled = 6;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly BaseStatusSmartEnum EmailNotConfirmed = new EmailNotConfirmedSE();
	public static readonly BaseStatusSmartEnum EmailConfirmation = new EmailConfirmationSE();
	public static readonly BaseStatusSmartEnum AcceptedHouseRulesAgreement = new AcceptedHouseRulesAgreementSE();
	public static readonly BaseStatusSmartEnum RegistrationFormCompleted = new RegistrationFormCompletedSE();
	public static readonly BaseStatusSmartEnum PartiallyPaid = new PartiallyPaidSE();
	public static readonly BaseStatusSmartEnum FullyPaid = new FullyPaidSE();
	public static readonly BaseStatusSmartEnum Canceled = new CanceledSE();
	// SE=SmartEnum
	#endregion

	private BaseStatusSmartEnum(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Abrv { get; }
	public abstract bool CanTransitionTo(BaseStatusSmartEnum next);
	#endregion

	#region Private Instantiation

	private sealed class EmailNotConfirmedSE : BaseStatusSmartEnum
	{
		public EmailNotConfirmedSE() : base($"{nameof(Id.EmailNotConfirmed)}", Id.EmailNotConfirmed) { }
		public override string Abrv => "NC";
		public override bool CanTransitionTo(BaseStatusSmartEnum next) =>
			next == BaseStatusSmartEnum.EmailConfirmation;
	}


	private sealed class EmailConfirmationSE : BaseStatusSmartEnum
	{
		public EmailConfirmationSE() : base($"{nameof(Id.EmailConfirmation)}", Id.EmailConfirmation) { }
		public override string Abrv => "EC";
		public override bool CanTransitionTo(BaseStatusSmartEnum next) =>
			next == BaseStatusSmartEnum.AcceptedHouseRulesAgreement;
	}

	private sealed class AcceptedHouseRulesAgreementSE : BaseStatusSmartEnum
	{
		public AcceptedHouseRulesAgreementSE() : base($"{nameof(Id.AcceptedHouseRulesAgreement)}", Id.AcceptedHouseRulesAgreement) { }
		public override string Abrv => "AH";
		public override bool CanTransitionTo(BaseStatusSmartEnum next) =>
			next == BaseStatusSmartEnum.RegistrationFormCompleted ||
			next == BaseStatusSmartEnum.Canceled;
	}

	private sealed class RegistrationFormCompletedSE : BaseStatusSmartEnum
	{
		public RegistrationFormCompletedSE() : base($"{nameof(Id.RegistrationFormCompleted)}", Id.RegistrationFormCompleted) { }
		public override string Abrv => "FC";
		public override bool CanTransitionTo(BaseStatusSmartEnum next) =>
			next == BaseStatusSmartEnum.FullyPaid ||
			next == BaseStatusSmartEnum.PartiallyPaid ||
			next == BaseStatusSmartEnum.Canceled;
	}

	private sealed class PartiallyPaidSE : BaseStatusSmartEnum
	{
		public PartiallyPaidSE() : base($"{nameof(Id.PartiallyPaid)}", Id.PartiallyPaid) { }
		public override string Abrv => "PP";
		public override bool CanTransitionTo(BaseStatusSmartEnum next) =>
			next == BaseStatusSmartEnum.FullyPaid ||
			next == BaseStatusSmartEnum.Canceled;
	}

	private sealed class FullyPaidSE : BaseStatusSmartEnum
	{
		public FullyPaidSE() : base($"{nameof(Id.FullyPaid)}", Id.FullyPaid) { }
		public override string Abrv => "FP";
		public override bool CanTransitionTo(BaseStatusSmartEnum next) =>
			next == BaseStatusSmartEnum.Canceled;
	}

	private sealed class CanceledSE : BaseStatusSmartEnum
	{
		public CanceledSE() : base($"{nameof(Id.Canceled)}", Id.Canceled) { }
		public override string Abrv => "Ca";
		public override bool CanTransitionTo(BaseStatusSmartEnum next) => false;
	}

	#endregion

	public string Dump
	{
		get
		{
			string s = "";
			s += $" {(this.CanTransitionTo(EmailNotConfirmed) ? EmailNotConfirmed.Abrv : "__")  }";
			s += $" {(this.CanTransitionTo(EmailConfirmation) ? EmailConfirmation.Abrv : "__")  }";
			s += $" {(this.CanTransitionTo(AcceptedHouseRulesAgreement) ? AcceptedHouseRulesAgreement.Abrv : "__")  }";
			s += $" {(this.CanTransitionTo(RegistrationFormCompleted) ? RegistrationFormCompleted.Abrv : "__")  }";
			s += $" {(this.CanTransitionTo(FullyPaid) ? FullyPaid.Abrv : "__")  }";
			s += $" {(this.CanTransitionTo(Canceled) ? Canceled.Abrv : "__")  }";
			return s;

		}
	}

}


/*
SELECT * FROM Sukkot.Status

Id	Code		Descr
--	-------	-----------------------------
0		0. NoEC	Email Not Confirmed
1		1. EC		Email Confirmation
2		2. AHRA	Accepted House Rules Agreement -- This is new
3		3. RFC	Registration Form Completed    -- This was 2
4		4. PP		Partially Paid
5		5. FP		Fully Paid
6		6. Can	Cancel

*/