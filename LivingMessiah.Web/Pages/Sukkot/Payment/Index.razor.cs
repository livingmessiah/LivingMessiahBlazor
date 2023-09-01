using Blazored.Toast.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using LivingMessiah.Web.Pages.Sukkot.Services;
using LivingMessiah.Web.Pages.Sukkot.Domain;
using LivingMessiah.Web.Settings;

namespace LivingMessiah.Web.Pages.Sukkot.Payment;

public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public ISukkotService? svc { get; set; }
	[Inject] public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
	[Inject] public IToastService? Toast { get; set; }
	[Inject] public IOptions<SukkotSettings>? SukkotSettings { get; set; }

	[Parameter] public int Id { get; set; }

	private string? StripeBuyButtonId;
	private string? StripePublishableKey;

	public RegistrationSummary? RegistrationSummary { get; set; }

	public ClaimsPrincipal? User { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0} Id:{1}"
			, nameof(Index) + "!" + nameof(OnInitializedAsync), Id));
		
		StripeBuyButtonId = SukkotSettings!.Value.StripeBuyButtonId;
		StripePublishableKey = SukkotSettings!.Value.StripePublishableKey;

		InitializeAlertHandlingling();
		RegistrationSummary = new RegistrationSummary();
		try
		{
			var authState = await AuthenticationStateProvider!.GetAuthenticationStateAsync();
			User = authState.User;
			RegistrationSummary = await svc!.Summary(Id, User);
			GotRecord = true;
		}
		catch (PaymentSummaryException paymentSummaryRecordNotFoundException)
		{
			Toast!.ShowInfo(paymentSummaryRecordNotFoundException.Message);
		}
		catch (UserNotAuthoirizedException userNotAuthoirizedException)
		{
			Toast!.ShowInfo(userNotAuthoirizedException.Message);
		}
		catch (InvalidOperationException invalidOperationException)
		{
			Toast!.ShowError(invalidOperationException.Message);
		}
		AttemptingToGetRecord = false;
		if (!GotRecord)
		{
			AttemptingToGetRecordMsg = "Failed to get registration";
		}

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
	protected string? AttemptingToGetRecordMsg;
	#endregion

}
