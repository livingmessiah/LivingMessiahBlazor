using Microsoft.AspNetCore.Components;
using Page = LivingMessiah.Web.Links.Sukkot.RegistrationSteps;

namespace LivingMessiah.Web.Features.Sukkot.Components;

public partial class RegistrationStepsLink
{
	[Parameter, EditorRequired]
	public bool BackwardDirection	{ get; set; }

	public string Title => BackwardDirection ? Page.BackToButtonText : Page.StartButtonText;
}

/*
	public string Title
	{
		get
		{
			return BackwardDirection ? Page.BackToButtonText : Page.StartButtonText;
		}
	}
 
 */