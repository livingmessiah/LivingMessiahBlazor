using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

using LivingMessiah.Web.Pages.Sukkot.Services;
using LivingMessiah.Web.Pages.Sukkot.Domain;

namespace LivingMessiah.Web.Pages.Sukkot.Payment;

public partial class Payment
{
	[Inject]
	public ILogger<Payment> Logger { get; set; }

	[Inject]
	public ISukkotService svc { get; set; }

	[Inject]
	public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

	[Parameter]
	public int Id { get; set; }

	public RegistrationSummary RegistrationSummary { get; set; }

	public ClaimsPrincipal User { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0} Id:{1}"
			, nameof(Payment) + "!" + nameof(OnInitializedAsync), Id));
		InitializeErrorHandling();
		InitializeAlertHandlingling();
		RegistrationSummary = new RegistrationSummary();
		try
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			User = authState.User;
			RegistrationSummary = await svc.Summary(Id, User);
			GotRecord = true;
		}
		catch (PaymentSummaryException paymentSummaryRecordNotFoundException)
		{
			DatabaseInformation = true;
			DatabaseInformationMsg = paymentSummaryRecordNotFoundException.Message;
		}
		catch (UserNotAuthoirizedException userNotAuthoirizedException)
		{
			DatabaseWarning = true;
			DatabaseWarningMsg = userNotAuthoirizedException.Message;
		}
		catch (InvalidOperationException invalidOperationException)
		{
			DatabaseError = true;
			DatabaseErrorMsg = invalidOperationException.Message;
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
