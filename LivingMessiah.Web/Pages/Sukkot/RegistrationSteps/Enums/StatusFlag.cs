using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

[Flags]
public enum StatusFlag
{
	NotAuthenticated = 1,
	EmailNotConfirmed = 2,
	AgreementNotSigned = 4,
	StartRegistraion = 8,
	RegistrationFormCompleted = 16,
	PartiallyPaid = 32,
	FullyPaid = 64
	//, Canceled = 128
}
