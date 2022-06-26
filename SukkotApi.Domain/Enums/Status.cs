using Ardalis.SmartEnum;

namespace SukkotApi.Domain.Enums;

public abstract class Status : SmartEnum<Status>
{

	#region Id's
	private static class Id
	{
		internal const int EmailNotConfirmed = 1;
		internal const int EmailConfirmation = 2;
		internal const int AcceptedHouseRulesAgreement = 3;
		internal const int RegistrationFormCompleted = 4;
		internal const int PartiallyPaid = 5;
		internal const int FullyPaid = 6;
		internal const int Canceled = 7;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Status EmailNotConfirmed = new EmailNotConfirmedSE();
	public static readonly Status EmailConfirmation = new EmailConfirmationSE();
	public static readonly Status AcceptedHouseRulesAgreement = new AcceptedHouseRulesAgreementSE();
	public static readonly Status RegistrationFormCompleted = new RegistrationFormCompletedSE();
	public static readonly Status PartiallyPaid = new PartiallyPaidSE();
	public static readonly Status FullyPaid = new FullyPaidSE();
	public static readonly Status Canceled = new CanceledSE();
	// SE=SmartEnum
	#endregion

	private Status(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields

	//ToDo: Deprecated because I made the Id "1 based" i.e. not zero based.
	public abstract int StepNumber { get; }
	public abstract int Flag { get; }
	public abstract string Abrv { get; }
	public abstract bool CanTransitionTo(Status next);
	#endregion

	#region Private Instantiation

	private sealed class EmailNotConfirmedSE : Status
	{
		public EmailNotConfirmedSE() : base($"{nameof(Id.EmailNotConfirmed)}", Id.EmailNotConfirmed) { }
		public override int StepNumber => 1;
		public override int Flag => 1;
		public override string Abrv => "NC";
		public override bool CanTransitionTo(Status next) =>
			next == Status.EmailConfirmation;
	}


	private sealed class EmailConfirmationSE : Status
	{
		public EmailConfirmationSE() : base($"{nameof(Id.EmailConfirmation)}", Id.EmailConfirmation) { }
		public override int StepNumber => 2;
		public override int Flag => 2;
		public override string Abrv => "EC";
		public override bool CanTransitionTo(Status next) =>
			next == Status.AcceptedHouseRulesAgreement;
	}

	private sealed class AcceptedHouseRulesAgreementSE : Status
	{
		public AcceptedHouseRulesAgreementSE() : base($"{nameof(Id.AcceptedHouseRulesAgreement)}", Id.AcceptedHouseRulesAgreement) { }
		public override int StepNumber => 3;
		public override int Flag => 4;
		public override string Abrv => "AH";
		public override bool CanTransitionTo(Status next) =>
			next == Status.RegistrationFormCompleted ||
			next == Status.Canceled;
	}

	private sealed class RegistrationFormCompletedSE : Status
	{
		public RegistrationFormCompletedSE() : base($"{nameof(Id.RegistrationFormCompleted)}", Id.RegistrationFormCompleted) { }
		public override int StepNumber => 4;
		public override int Flag => 8;
		public override string Abrv => "FC";
		public override bool CanTransitionTo(Status next) =>
			next == Status.FullyPaid ||
			next == Status.PartiallyPaid ||
			next == Status.Canceled;
	}

	private sealed class PartiallyPaidSE : Status
	{
		public PartiallyPaidSE() : base($"{nameof(Id.PartiallyPaid)}", Id.PartiallyPaid) { }
		public override int StepNumber => 5;
		public override int Flag => 16;
		public override string Abrv => "PP";
		public override bool CanTransitionTo(Status next) =>
			next == Status.FullyPaid ||
			next == Status.Canceled;
	}

	private sealed class FullyPaidSE : Status
	{
		public FullyPaidSE() : base($"{nameof(Id.FullyPaid)}", Id.FullyPaid) { }
		public override int StepNumber => 6;
		public override int Flag => 32;
		public override string Abrv => "FP";
		public override bool CanTransitionTo(Status next) =>
			next == Status.Canceled;
	}

	private sealed class CanceledSE : Status
	{
		public CanceledSE() : base($"{nameof(Id.Canceled)}", Id.Canceled) { }
		public override int StepNumber => 7;
		public override int Flag => 0;
		public override string Abrv => "Ca";
		public override bool CanTransitionTo(Status next) => false;
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