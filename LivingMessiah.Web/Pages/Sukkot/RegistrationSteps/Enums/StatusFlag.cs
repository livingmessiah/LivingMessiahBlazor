using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

[Flags]
public enum StatusFlag
{
	EmailNotConfirmed = 1,
	EmailConfirmation = 2,
	AcceptedHouseRulesAgreement = 4,
	RegistrationFormCompleted = 8,
	PartiallyPaid = 16,
	FullyPaid = 32
}
