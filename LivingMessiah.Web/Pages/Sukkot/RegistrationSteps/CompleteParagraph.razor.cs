using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class CompleteParagraph
{
	[Parameter, EditorRequired]
	public int RegistrationStepId { get; set; }

}
