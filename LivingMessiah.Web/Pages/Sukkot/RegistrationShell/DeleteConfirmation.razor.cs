using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using SukkotApi.Domain;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationShell;

public partial class DeleteConfirmation
{
	[Inject]
	public ILogger<DeleteConfirmation> Logger { get; set; }

	[Inject]
	public ISukkotService svc { get; set; }

	[Inject]
	public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

	[Inject]
	NavigationManager NavManager { get; set; }

	public ClaimsPrincipal User { get; set; }

	[Parameter]
	public int Id { get; set; }

	public vwRegistration vwRegistration { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0} Id:{1}"
		, nameof(DeleteConfirmation) + "!" + nameof(OnInitializedAsync), Id));

		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		User = authState.User;

		vwRegistration = new vwRegistration();
		try
		{
			vwRegistration = await svc.DeleteConfirmation(Id, User);
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

	protected async Task Delete_ButtonClick(int id)
	{
		Logger.LogDebug($"Inside {nameof(Delete_ButtonClick)}, Start Registration Deletion for id:{id} ");
		int count = 0;
		try
		{
			count = await svc.DeleteConfirmed(id);
			NavManager.NavigateTo(Links.Sukkot.Index);
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
