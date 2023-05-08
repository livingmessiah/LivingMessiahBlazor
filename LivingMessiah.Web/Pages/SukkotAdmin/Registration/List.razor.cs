using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
//using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using Syncfusion.Blazor.Grids;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration;

//[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class List
{
	[Inject] public ILogger<RegistrationGrid>? Logger { get; set; }
	[Inject] public IRegistrationAdminRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public IEnumerable<Domain.Registration>? Registrations { get; set; }

	private SfGrid<Domain.Registration>? Grid;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug($"Inside {nameof(List)}!{nameof(OnInitializedAsync)}");
		try
		{
			Registrations = await db!.GetAll();
			if (Registrations == null)
			{
				Toast!.ShowWarning($"{nameof(Registrations)} Registrations NOT FOUND");
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "Error reading database");
			Toast!.ShowError("Error reading database");
		}
		StateHasChanged();
	}
}
