using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using SukkotApi.Domain;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

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
		RegistrationSummary = new RegistrationSummary();
		try
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			User = authState.User;
			RegistrationSummary = await svc.Summary(Id, User);
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
