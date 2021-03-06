using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class PartlyPaidParagraph
{
	[Parameter, EditorRequired]
	public RegistrationStep RegistrationStep { get; set; }

	public static MarkupString Remainder(decimal remainingCost)
	{
		string remainingCostToString = remainingCost.ToString("C0");
		return remainingCost == 0 ?
				(MarkupString)(string.Empty) :
				(MarkupString)$"<p class='lead'>Remainder: <b>{remainingCostToString}</b></p>";
	}

	public static MarkupString Total(decimal totalDonation)
	{
		string remainingCostToString = totalDonation.ToString("C0");
		return totalDonation == 0 ?
				(MarkupString)(string.Empty) :
				(MarkupString)$"<p class='lead'>Previous donation(s): <b>{remainingCostToString}</b></p>";
	}
}
