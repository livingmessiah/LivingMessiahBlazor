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

	protected CurrentStatus CurrentStatus { get; set; }

	protected override async Task OnInitializedAsync()
	{
		InitializeErrorHandling();
		InitializeAlertHandlingling();
		Logger.LogDebug(string.Format("Inside {0}"
			, nameof(RegistrationSteps) + "!" + nameof(Index) + "!" + nameof(OnInitialized)));

		try
		{
			CurrentStatus = await svc.GetCurrentStatus();
			GotRecord = true;
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
