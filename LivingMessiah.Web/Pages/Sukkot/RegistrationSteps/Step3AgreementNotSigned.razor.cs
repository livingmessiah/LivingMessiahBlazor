using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Step3AgreementNotSigned
{
	[Inject]
	public ILogger<Step3AgreementNotSigned> Logger { get; set; }

	[Inject]
	NavigationManager navigationManager { get; set; }

	[Parameter, EditorRequired]
	public bool IsXs { get; set; } = false;

	[Parameter, EditorRequired]
	public Enums.StatusFlag StatusFlag { get; set; }

	[Parameter] 
	public EventCallback AgreeButtonCallBack { get; set; }

	protected string FormatSize;

	protected override void OnInitialized()
	{
		FormatSize = IsXs ? "" : "lead"; // IsXs2 ?
	}

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
