using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class AgreementButtons
{

	[Inject]
	public ILogger<AgreementButtons> Logger { get; set; }

	[Inject]
	NavigationManager navigationManager { get; set; }

	[Parameter]
	public EventCallback AgreeButtonCallBack { get; set; }

	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}

	void DoNotAgree_ButtonClick(string goToPage)
	{
		Logger.LogDebug(string.Format("Event: {0} clicked; goToPage:{1}"
			, nameof(DoNotAgree_ButtonClick), goToPage));
		navigationManager.NavigateTo($"{goToPage}", true);
	}
}
