using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

using LivingMessiah.Web.Pages.Sukkot.Services;
using LivingMessiah.Web.Pages.Sukkot.Domain;

namespace LivingMessiah.Web.Pages.Sukkot.Details;

public partial class Details
{
	[Inject]
	public ISukkotService svc { get; set; }

	[Inject]
	public ILogger<Details> Logger { get; set; }

	[Inject]
	public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

	[Inject]
	NavigationManager NavManager { get; set; }

	[Parameter]
	public int Id { get; set; }

	[Parameter]
	public bool showPrintInstructionMessage { get; set; } = true;

	public vwRegistration vwRegistration { get; set; }
	public ClaimsPrincipal User { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0} Id:{1}"
			, nameof(Details) + "!" + nameof(OnInitializedAsync), Id));

		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		User = authState.User;

		try
		{
			vwRegistration = await svc.Details(Id, User, showPrintInstructionMessage);
		}
		catch (InvalidOperationException invalidOperationException)
		{
			DatabaseError = true;
			DatabaseErrorMsg = invalidOperationException.Message;
		}
	}

	protected bool MakeModalVisible = false;

	void PaymentInstructions_ButtonClick()
	{
		Logger.LogDebug($"Event: {nameof(PaymentInstructions_ButtonClick)} clicked");
		MakeModalVisible = true;
		StateHasChanged();

	}
	void CancelModal_ButtonClick()
	{
		Logger.LogDebug($"Event: {nameof(CancelModal_ButtonClick)} clicked");
		MakeModalVisible = false;
		StateHasChanged();
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
