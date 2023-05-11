using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using Syncfusion.Blazor.Grids;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

//[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class RegistrationGrid
{
	[Inject] public ILogger<RegistrationGrid>? Logger { get; set; }
	[Inject] public IRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public IEnumerable<ViewModel>? Registrations { get; set; }

	private SfGrid<ViewModel>? Grid;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug($"Inside {nameof(RegistrationGrid)}!{nameof(OnInitializedAsync)}");
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
