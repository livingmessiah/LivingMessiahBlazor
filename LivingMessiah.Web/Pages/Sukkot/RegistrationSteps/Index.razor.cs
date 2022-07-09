using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Link = LivingMessiah.Web.Links.Account;
using Sukkot.Web.Service;
using System.Threading.Tasks;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Index : ComponentBase
{
	[Inject]
	public ILogger<Index> Logger { get; set; }

	[Inject]
	public ISukkotService svc { get; set; }

	[Inject]
	NavigationManager NavigationManager { get; set; }

	protected IndexVM IndexVM { get; set; }

	protected override async Task OnInitializedAsync()
	{
		await PopulateVM();
	}

	private async Task PopulateVM()
	{
		InitializeAlertHandlingling();
		Logger.LogDebug(string.Format("Inside {0}"
			, nameof(Index) + "!" + nameof(PopulateVM)));
		try
		{
			IndexVM = await svc.GetRegistrationStep();
			GotRecord = true;
			Logger.LogDebug(string.Format("...just called svc.{0}; Status: {1}, EmailAddress: {2}"
				, nameof(svc.GetRegistrationStep), IndexVM.Status, IndexVM.EmailAddress));

		}
		catch (InvalidOperationException invalidOperationException)
		{
			DatabaseError = true;
			DatabaseErrorMsg = invalidOperationException.Message;
		}
		AttemptingToGetRecord = false;
		if (!GotRecord)
		{
			AttemptingToGetRecordMsg = "Failed to get current status";
		}
	}

	private async Task AgreeButtonCallBackHandler()
	{
		string eMail = IndexVM.EmailAddress;
		Logger.LogDebug(string.Format("Event: {0}"
			, nameof(Index) + "!" + nameof(AgreeButtonCallBackHandler)));
		int id = 0;
		try
		{
			id = await svc.AddHouseRulesAgreementRecord(eMail, GetLocalTimeZone());
			DatabaseInformationMsg = "Record updated";
			DatabaseInformation = true;
			await PopulateVM();
		}
		catch (InvalidOperationException invalidOperationException)
		{
			DatabaseError = true;
			DatabaseErrorMsg = invalidOperationException.Message;
		}
	}

	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}

	void RedirectToLoginClick(string returnUrl)
	{
		NavigationManager.NavigateTo($"{Link.Login}?returnUrl={returnUrl}", true);
	}

	#region AlertHandling
	private void InitializeAlertHandlingling()
	{
		AttemptingToGetRecord = true;
		GotRecord = false;
		AttemptingToGetRecordMsg = "";
	}
	protected bool GotRecord;
	protected bool AttemptingToGetRecord;
	protected string AttemptingToGetRecordMsg;
	#endregion

	#region ErrorHandling
	protected bool DatabaseInformation = false;
	protected string DatabaseInformationMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }
	protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
	protected string DatabaseErrorMsg { get; set; }
	#endregion


}
