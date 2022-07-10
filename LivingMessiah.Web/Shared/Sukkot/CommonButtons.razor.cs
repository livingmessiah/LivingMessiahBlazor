using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Shared.Sukkot;

public partial class CommonButtons
{
	[Inject]
	NavigationManager NavManager { get; set; }

	[Parameter, EditorRequired]
	public int? RegistrationStepId { get; set; }

	[Parameter, EditorRequired]
	public Enums.NavButton NavButton { get; set; }

	void ButtonClick(int? id)
	{
		if (id is null)
		{
			NavManager.NavigateTo($"{NavButton.Route}");
		}
		else
		{
			NavManager.NavigateTo($"{NavButton.Route}/{id}{NavButton.RouteSuffix}");
		}
		
	}


}
