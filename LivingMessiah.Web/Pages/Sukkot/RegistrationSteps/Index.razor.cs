using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Link = LivingMessiah.Web.Links.Account;
using LivingMessiah.Web.Pages.Sukkot.Services;
using System.Threading.Tasks;
using System;
using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Index : ComponentBase
{
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public ISukkotService? svc { get; set; }
	[Inject] NavigationManager? NavigationManager { get; set; }
	[Inject] AppState? AppState { get; set; }

	protected IndexVM? IndexVM { get; set; }

	protected bool AttemptingToGetRecord;
	protected override async Task OnInitializedAsync()
	{
		// += operator allows you to subscribe to an event
		AppState!.StateChanged += async (Source, Property) => await AppState_StateChanged(Source, Property);
		await PopulateVM();
	}

	private async Task PopulateVM()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Index) + "!" + nameof(PopulateVM)));
		AttemptingToGetRecord = true;
		try
		{
			IndexVM = await svc!.GetRegistrationStep();
			AttemptingToGetRecord = false;
			Logger!.LogDebug(string.Format("...just called svc.{0}; Status: {1}, EmailAddress: {2}"
				, nameof(svc.GetRegistrationStep), IndexVM.Status, IndexVM.EmailAddress));
		}
		catch (InvalidOperationException invalidOperationException)
		{
			AppState!.UpdateMessage(this, invalidOperationException.Message);
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
		AppState!.StateChanged -= async (Source, Property) => await AppState_StateChanged(Source, Property);
	}

	void RedirectToLoginClick(string returnUrl)
	{
		NavigationManager!.NavigateTo($"{Link.Login}?returnUrl={returnUrl}", true);
	}

}
