using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Link = LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Step4StartRegistration
{
	[Inject]
	public ILogger<Step4StartRegistration> Logger { get; set; }

	[Inject]
	NavigationManager navigationManager { get; set; }
	
	[Parameter, EditorRequired]
	public bool IsXs { get; set; } = false;

	void Add_ButtonClick()
	{
		Logger.LogDebug($"Event: {nameof(Add_ButtonClick)} clicked; Navigate to CreateEdit");
		navigationManager.NavigateTo(Link.CreateEdit);
	}

}
