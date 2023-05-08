using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class PartlyPaidParagraph
{
	[Parameter, EditorRequired]
	public RegistrationStep? RegistrationStep { get; set; }

	public static MarkupString RemainderAndTotal(decimal remainingCost, decimal totalDonation)
	{
		string remainingCostToString = remainingCost.ToString("C0");
		string totalDonationToString = totalDonation.ToString("C0");
		return totalDonation == 0 && remainingCost==00 ?
				(MarkupString)(string.Empty) :
				(MarkupString)$"<p class='lead'>Remainder: <b>{remainingCostToString}</b>; Previous donation(s): <b>{totalDonationToString}</b></p>";
	}

}
