using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using System;
using System.Threading.Tasks;
using static LivingMessiah.Web.Pages.Sukkot.Constants.Other;
using Page = LivingMessiah.Web.Links.Sukkot.RegistrationSteps;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Step3AcceptedHouseRulesAgreement
{
	[Inject]
	public ILogger<Step3AcceptedHouseRulesAgreement> Logger { get; set; }

	[Inject]
	public ISukkotService svc { get; set; }

	[Inject]
	NavigationManager navigationManager { get; set; }

	[Parameter, EditorRequired]
	public bool IsXs { get; set; } = false;

	[Parameter, EditorRequired]
	public Enums.StatusFlag StatusFlag { get; set; }

	[Parameter, EditorRequired]
	public string EmailAddress { get; set; }

	private string TimeZone { get; set; }
	private string DateAndTime { get; set; }
	private string DateAndTimeUtc { get; set; }
	private string DateAndTimeOffset { get; set; }

	protected string FormatSize;

	protected override void OnInitialized()
	{
		FormatSize = IsXs ? "" : "lead"; // IsXs2 ?
	}

	private string GetLocalTimeZone() 
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}

	void DoNotAgree_ButtonClick(string returnUrl)
	{
		Logger.LogDebug(string.Format("Event: {0} clicked; returnUrl:{1}"
			, nameof(DoNotAgree_ButtonClick), returnUrl));
		navigationManager.NavigateTo($"{Page.Index}?returnUrl={returnUrl}", true);
	}


	private async Task Agree_ButtonClick()
	{
		Logger.LogDebug($"Event: {nameof(Agree_ButtonClick)} clicked; Navigate to {Page.Index}");
		TimeZone = GetLocalTimeZone();
		int id = 0;

		try
		{
			id = await svc.AddHouseRulesAgreementRecord(EmailAddress, TimeZone);
			//navigationManager.NavigateTo(Page.Index);
			DatabaseInformationMsg = "Record updated";
			DatabaseInformation = true;
			StateHasChanged();
		}
		catch (InvalidOperationException invalidOperationException)
		{
			DatabaseError = true;
			DatabaseErrorMsg = invalidOperationException.Message;
		}
		
	}


	void Add_ButtonClick()
	{
		Logger.LogDebug($"Event: {nameof(Add_ButtonClick)} clicked; Navigate to CreateEdit");
		navigationManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.CreateEdit);
	}

	#region ErrorHandling
	private void InitializeErrorHandling()
	{
		DatabaseInformationMsg = "";
		DatabaseInformation = false;
		DatabaseWarningMsg = "";
		DatabaseWarning = false;
		DatabaseErrorMsg = "";
		DatabaseError = false;
	}

	protected bool DatabaseInformation = false;
	protected string DatabaseInformationMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }
	protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
	protected string DatabaseErrorMsg { get; set; }
	#endregion


}
