using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain.Enums;

//ToDo 070-refactor-StatusEnum-with-SmartEnum
public enum StatusEnum
{
		[Display(Name = "eMail Not Confirmed (Step 1)")]
		EmailNotConfirmed = 0,

		[Display(Name = "eMail Confirmation (Step 2)")]
		EmailConfirmation = 1,

		[Display(Name = "Accepted House Rules Agreement (Step 3)")]
		AcceptedHouseRulesAgreement = 2,

		[Display(Name = "Registration Form Completed (Step 4)")]
		RegistrationFormCompleted = 3,

		[Display(Name = "Partially Paid (Step 5)")]
		PartiallyPaid = 4,

		[Display(Name = "Fully Paid (Step 6)")]
		FullyPaid = 5,

		[Display(Name = "Canceled")]
		Canceled = 6

}

/*
SELECT * FROM Sukkot.Status

Id	Code		Descr
--	-------	-----------------------------
0		0. NoEC	Email Not Confirmed
1		1. EC		Email Confirmation
2		2. RFC	Registration Form Completed
3		3. MFC	Meal Form Completed
4		4. PP		Partially Paid
5		5. FP		Fully Paid
6		6. Can	Canceled
*/
