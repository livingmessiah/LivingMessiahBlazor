﻿//ToDo: This needs to be DELETED!
//  It's equivalent needs to be found in...
//	- Pages\Sukkot\NormalUser\Index.razor
//	- Pages\Sukkot\MasterDetail\Index.razor

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

using LivingMessiah.Web.Features.Sukkot.Services;
using LivingMessiah.Web.Features.Sukkot.Domain;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Features.Sukkot.Details;

public partial class Index
{
	[Inject] public ISukkotService? svc { get; set; }
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
	[Inject] NavigationManager? NavManager { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter] public int Id { get; set; }
	[Parameter] public bool showPrintInstructionMessage { get; set; } = true;

	public vwRegistration? vwRegistration { get; set; }
	public ClaimsPrincipal? User { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0} Id:{1}"
			, nameof(Index) + "!" + nameof(OnInitializedAsync), Id));

		var authState = await AuthenticationStateProvider!.GetAuthenticationStateAsync();
		User = authState.User;

		try
		{
			vwRegistration = await svc!.Details(Id, User, showPrintInstructionMessage);
		}
		catch (UserNotAuthoirizedException userNotAuthoirizedException)
		{
			Toast!.ShowInfo(userNotAuthoirizedException.Message);
		}
		catch (InvalidOperationException invalidOperationException)
		{
			Toast!.ShowError($"{invalidOperationException.Message}");
		}
	}

}
