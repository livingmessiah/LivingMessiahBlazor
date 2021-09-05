using Ardalis.SmartEnum;

/*
	public enum DonationStatusEnum
	{
		FullList = 0,
		NoPayments = 2,  // either 2 (RFC) or 3 (MFC)
		PartiallyPaid = 4,
		FullyPaid = 5
	}

// LivingMessiah.Web\Pages\Sukkot\RegistrationEnums.cs

	[Flags]
	public enum StatusFlagEnum
	{
		EmailConfirmation = 1,
		RegistrationFormCompleted = 2,
		MealsFormCompleted = 4,
		PartiallyPaid = 8,
		FullyPaid = 16,
		AcceptedHouseRules = 32
	}	

*/

namespace SukkotApi.Domain
{


	public abstract class RegistrationStep : SmartEnum<RegistrationStep>
	{
		public static readonly RegistrationStep EmailConfirmation = new EmailConfirmationStep();
		public static readonly RegistrationStep AcceptedHouseRules = new AcceptedHouseRulesStep();
		public static readonly RegistrationStep FormCompletion = new FormCompletionStep();
		//public static readonly RegistrationStep MealsFormCompletion = new MealsFormCompletionStep();
		//public static readonly RegistrationStep RegistrationPaid = new RegistrationPaidStep();
		public static readonly RegistrationStep FullyPaid = new FullyPaidStep();
		public static readonly RegistrationStep Cancelled = new CancelledStep();

		private RegistrationStep(string name, int value) : base(name, value)
		{
		}

		private static class Id
		{
			internal const int EmailConfirmation = 1;
			internal const int AcceptedHouseRules = 2;
			internal const int FormCompletion = 3;
			internal const int FullyPaid = 4;
			internal const int Cancelled = 5;

		}



		public abstract bool CanTransitionTo(RegistrationStep next);

		private sealed class EmailConfirmationStep : RegistrationStep
		{
			public EmailConfirmationStep() : base("EmailConfirmation", Id.EmailConfirmation)
			{
			}
			public override bool CanTransitionTo(RegistrationStep next) =>
					next == RegistrationStep.AcceptedHouseRules || next == RegistrationStep.Cancelled;
		}

		private sealed class AcceptedHouseRulesStep : RegistrationStep
		{
			public AcceptedHouseRulesStep() : base("AcceptedHouseRules", Id.AcceptedHouseRules)
			{
			}
			public override bool CanTransitionTo(RegistrationStep next) =>
					next == RegistrationStep.FormCompletion || next == RegistrationStep.Cancelled;
		}


		private sealed class FormCompletionStep : RegistrationStep
		{
			public FormCompletionStep() : base("FormCompletion", Id.FormCompletion)
			{
			}
			public override bool CanTransitionTo(RegistrationStep next) =>
					next == RegistrationStep.FullyPaid || next == RegistrationStep.Cancelled;
		}


		private sealed class FullyPaidStep : RegistrationStep
		{
			public FullyPaidStep() : base("FullyPaid", Id.FullyPaid)
			{
			}
			public override bool CanTransitionTo(RegistrationStep next) =>
					next == RegistrationStep.Cancelled;
		}

		private sealed class CancelledStep : RegistrationStep
		{
			public CancelledStep() : base("Cancelled", Id.Cancelled)
			{
			}
			public override bool CanTransitionTo(RegistrationStep next) =>
					false;
		}

	}
}
