using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEnums
{
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
}
