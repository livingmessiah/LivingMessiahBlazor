using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Pages.SukkotAdmin.Services;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Domain;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class Index
{
	[Inject] protected ILogger<Index>? Logger { get; set; }
	[Inject] protected ISukkotAdminService? svc { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	protected int rowCount { get; set; } = 0;

	protected EnumsOld.RegistrationSort MySort { get; set; } =	EnumsOld.RegistrationSort.FromEnum(EnumsOld.RegistrationSortEnum.LastName);
	
	protected List<Notes>? NotesList { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Index) + "!" + nameof(OnInitializedAsync)));
		try
		{
			NotesList = await svc!.GetNotes(MySort.RegistrationSortEnum);
		}

		catch (InvalidOperationException invalidOperationException)
		{
			Toast!.ShowError(invalidOperationException.Message);
		}
	}

}
