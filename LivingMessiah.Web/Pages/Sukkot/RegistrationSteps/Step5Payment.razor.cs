using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Step5Payment
{
	[Inject]
	NavigationManager NavManager { get; set; }

	[Parameter, EditorRequired]
	public Enums.StatusFlag StatusFlag { get; set; }

	[Parameter, EditorRequired]
	public RegistrationStep RegistrationStep { get; set; }

	void Payment_ButtonClick(MouseEventArgs e, int id)
	{
		NavManager.NavigateTo(Links2.Payment + "/" + id);
	}

	void Details_ButtonClick(MouseEventArgs e, int id, bool showPrintMsg)
	{
		NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.Details + "/" + id + "/" + showPrintMsg);
	}

}
