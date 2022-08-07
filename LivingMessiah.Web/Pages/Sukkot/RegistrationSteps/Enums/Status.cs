using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

public abstract class Status : SmartEnum<Status>
{
	#region Id's
	private static class Id
	{
		internal const int NotAuthenticated = 1;
		internal const int EmailNotConfirmed = 2;
		internal const int AgreementNotSigned = 3;
		internal const int StartRegistraion = 4;
		internal const int Payment = 5;
		internal const int Complete = 6;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Status NotAuthenticated = new NotAuthenticatedSE();
	public static readonly Status EmailNotConfirmed = new EmailNotConfirmedSE();
	public static readonly Status AgreementNotSigned = new AgreementNotSignedSE();
	public static readonly Status StartRegistraion = new StartRegistraionSE();
	public static readonly Status Payment = new PaymentSE();
	public static readonly Status Complete = new CompleteSE();
	// SE=SmartEnum
	#endregion

	private Status(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields

	public abstract int StepNumber { get; }
	public abstract bool UsedInDbOnly { get; }
	public abstract string Heading { get; }
	public abstract string Icon { get; }
	public abstract bool UsedByUI { get; }
	public abstract bool DisplayAsCompleted(Status status);
	public abstract bool DisplayRegistrationToggleButton { get; }

	#endregion

	#region Private Instantiation

	private sealed class NotAuthenticatedSE : Status
	{
		public NotAuthenticatedSE() : base($"{nameof(Id.NotAuthenticated)}", Id.NotAuthenticated) { }
		public override int StepNumber => 1;
		public override bool UsedInDbOnly => false;
		public override string Heading => "Login";
		public override string Icon => "fas fa-check";
		public override bool UsedByUI => true;
		public override bool DisplayAsCompleted(Status status) => false;
		public override bool DisplayRegistrationToggleButton => false;
	}

	private sealed class EmailNotConfirmedSE : Status
	{
		public EmailNotConfirmedSE() : base($"{nameof(Id.EmailNotConfirmed)}", Id.EmailNotConfirmed) { }
		public override int StepNumber => 2;
		public override bool UsedInDbOnly => false;
		public override string Heading => "Email Confirmation";
		public override string Icon => "fas fa-check";
		public override bool UsedByUI => true;
		public override bool DisplayAsCompleted(Status status) => status == Status.NotAuthenticated;
		public override bool DisplayRegistrationToggleButton => false;
	}

	private sealed class AgreementNotSignedSE : Status
	{
		public AgreementNotSignedSE() : base($"{nameof(Id.AgreementNotSigned)}", Id.AgreementNotSigned) { }
		public override int StepNumber => 3;
		public override bool UsedInDbOnly => false;
		public override string Heading => "Sign Agreement";
		public override string Icon => "fas fa-check";
		public override bool UsedByUI => true;
		public override bool DisplayAsCompleted(Status status)
		{
			return status == Status.NotAuthenticated ||
						 status == Status.EmailNotConfirmed;
		}
		public override bool DisplayRegistrationToggleButton => false;
	}

	private sealed class StartRegistraionSE : Status
	{
		public StartRegistraionSE() : base($"{nameof(Id.StartRegistraion)}", Id.StartRegistraion) { }
		public override int StepNumber => 4;
		public override bool UsedInDbOnly => true;
		public override string Heading => "Registration Form";
		public override string Icon => "fas fa-check";
		public override bool UsedByUI => true;
		public override bool DisplayAsCompleted(Status status)
		{
			return status == Status.NotAuthenticated ||
						 status == Status.EmailNotConfirmed ||
						 status == Status.AgreementNotSigned;
		}
		public override bool DisplayRegistrationToggleButton => false;
	}

	private sealed class PaymentSE : Status
	{
		public PaymentSE() : base($"{nameof(Id.Payment)}", Id.Payment) { }
		public override int StepNumber => 5;
		public override bool UsedInDbOnly => true;
		public override string Heading => "Payment";
		public override string Icon => "fas fa-check";  //fas fa-star-half
		public override bool UsedByUI => true;
		public override bool DisplayAsCompleted(Status status)
		{
			return status == Status.NotAuthenticated ||
						 status == Status.EmailNotConfirmed ||
						 status == Status.AgreementNotSigned ||
						 status == Status.StartRegistraion;
		}
		public override bool DisplayRegistrationToggleButton => true;

	}

	private sealed class CompleteSE : Status
	{
		public CompleteSE() : base($"{nameof(Id.Complete)}", Id.Complete) { }
		public override int StepNumber => 6; 
		public override bool UsedInDbOnly => true;
		public override string Heading => "Registration Complete";
		public override string Icon => "fas fa-check";
		public override bool UsedByUI => true;
		public override bool DisplayAsCompleted(Status status)
		{
			return status == Status.NotAuthenticated ||
						 status == Status.EmailNotConfirmed ||
						 status == Status.AgreementNotSigned ||
						 status == Status.StartRegistraion ||
						 status == Status.Payment ||
						 status == Status.Complete;
		}
		public override bool DisplayRegistrationToggleButton => true;

	}
	#endregion

	public string Dump
	{
		get
		{
			string s = "";
			s += $" {(this.DisplayAsCompleted(NotAuthenticated) ? NotAuthenticated.Name : "__")  }";
			s += $" {(this.DisplayAsCompleted(EmailNotConfirmed) ? EmailNotConfirmed.Name : "__")  }";
			s += $" {(this.DisplayAsCompleted(AgreementNotSigned) ? AgreementNotSigned.Name : "__")  }";
			s += $" {(this.DisplayAsCompleted(StartRegistraion) ? StartRegistraion.Name : "__")  }";
			s += $" {(this.DisplayAsCompleted(Payment) ? Payment.Name : "__")  }";
			s += $" {(this.DisplayAsCompleted(Complete) ? Complete.Name : "__")  }";

			return s;

		}
	}

}


/*
SELECT * FROM Sukkot.Status

*/