using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static LivingMessiah.Web.Pages.Sukkot.Constants.Other;
using Page = LivingMessiah.Web.Links.Sukkot.RegistrationSteps;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Step3AcceptedHouseRulesAgreement
{
	[Inject]
	public ILogger<Step3AcceptedHouseRulesAgreement> Logger { get; set; }
	
	[Inject]
	NavigationManager navigationManager { get; set; }

	[Parameter]
	public bool IsXs { get; set; } = false;
	
	[Parameter]
	public StatusFlagEnum StatusFlagEnum { get; set; }

	protected string FormatSize;

	protected override async Task OnInitializedAsync()
	{
		base.OnInitialized();
		await Task.Delay(0);

		FormatSize = IsXs ? "" : "lead"; // IsXs2 ?
	}


	void BeginRegistration_ButtonClick()
	{
		Logger.LogDebug($"Event: {nameof(BeginRegistration_ButtonClick)} clicked");
		//MakeModalVisible = true;
		//StateHasChanged();
	}

	void CancelModal_ButtonClick()
	{
		Logger.LogDebug($"Event: {nameof(CancelModal_ButtonClick)} clicked");
		//MakeModalVisible = false;
		//StateHasChanged();
	}

	void DoNotAgree_ButtonClick(string returnUrl)
	{
		Logger.LogDebug(string.Format("Event: {0} clicked; returnUrl:{1}"
			, nameof(DoNotAgree_ButtonClick), returnUrl));
		navigationManager.NavigateTo($"{Page.Index}?returnUrl={returnUrl}", true);
	}

	//async Task Agree_ButtonClick()
	void Agree_ButtonClick()
	{
		Logger.LogDebug($"Event: {nameof(Agree_ButtonClick)} clicked; Navigate to CreateEdit");
		navigationManager.NavigateTo(Page.Index);
	}

}
