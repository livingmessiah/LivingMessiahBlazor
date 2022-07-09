using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Link = LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Step6Payment
{
	[Inject]
	NavigationManager NavManager { get; set; }

	[Parameter, EditorRequired]
	public Enums.StatusFlag StatusFlag { get; set; }

	[Parameter, EditorRequired]
	public RegistrationStep RegistrationStep { get; set; }

	void Payment_ButtonClick(MouseEventArgs e, int id)
	{
		NavManager.NavigateTo(Link.Links2.Payment + "/" + id);
	}

	void Details_ButtonClick(MouseEventArgs e, int id, bool showPrintMsg)
	{
		NavManager.NavigateTo(Link.Details + "/" + id + "/" + showPrintMsg);
	}

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
