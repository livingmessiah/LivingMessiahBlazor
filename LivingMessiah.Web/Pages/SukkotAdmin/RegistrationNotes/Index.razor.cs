using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using SukkotApi.Domain.Registrations.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class Index
{
		[Inject]
		protected ILogger<Index> Logger { get; set; }

		[Inject]
		protected ISukkotAdminService svc { get; set; }

		[Inject]
		protected NavigationManager NavManager { get; set; }

		protected int rowCount { get; set; } = 0;

		protected RegistrationSort MySort { get; set; } = RegistrationSort.FromEnum(RegistrationSortEnum.LastName);

		protected string ExceptionMessage { get; set; }

		protected List<SukkotApi.Domain.Notes> NotesList { get; set; }

		protected override async Task OnInitializedAsync()
		{
				//MySort= RegistrationSort.FromEnum(RegistrationSortEnum.LastName);
				//Logger.LogDebug($"Inside: {nameof(Index)}!{nameof(OnInitializedAsync)}, Sort:{MySort.Id}, calling {nameof(svc.GetNotes)}");
				Logger.LogDebug($"Inside: {nameof(Index)}!{nameof(OnInitializedAsync)}, calling {nameof(svc.GetNotes)}");
				try
				{
						NotesList = await svc.GetNotes(MySort.RegistrationSortEnum);
				}
				catch (Exception)
				{
						ExceptionMessage = svc.ExceptionMessage;
						NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
				}
		}
}
