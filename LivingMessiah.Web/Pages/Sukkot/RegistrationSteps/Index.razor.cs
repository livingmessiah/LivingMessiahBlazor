using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Link = LivingMessiah.Web.Links.Account;
using Sukkot.Web.Service;
using System.Threading.Tasks;
using System;
using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Index : ComponentBase
{
	[Inject]
	public ILogger<Index> Logger { get; set; }

	[Inject]
	public ISukkotService svc { get; set; }

	[Inject]
	NavigationManager NavigationManager { get; set; }
	
	[Inject]
	AppState AppState { get; set; }

protected IndexVM IndexVM { get; set; }

	protected override async Task OnInitializedAsync()
	{
		// += operator allows you to subscribe to an event
		AppState.StateChanged += async (Source, Property) => await AppState_StateChanged(Source, Property);
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
			AppState.UpdateMessage(this, invalidOperationException.Message);
		}

		AttemptingToGetRecord = false;
		if (!GotRecord)
		{
			AppState.UpdateMessage(this, "Failed to get current status");
		}
	}

	private async Task AppState_StateChanged(ComponentBase Source, string Property)
	{
		if (Source != this)
		{
			await PopulateVM();
			await InvokeAsync(StateHasChanged);
		}
	}
	
	void IDisposable.Dispose()
	{
		// -= operator detaches you from an event
		AppState.StateChanged -= async (Source, Property) => await AppState_StateChanged(Source, Property);
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



}
