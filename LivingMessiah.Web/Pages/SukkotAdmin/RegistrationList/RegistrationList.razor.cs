using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationList
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class RegistrationList
	{
		[Inject]
		public ILogger<RegistrationList> Logger { get; set; }

		[Inject]
		public ISukkotAdminService svc { get; set; }

		[Inject]
		NavigationManager NavManager { get; set; }

		public string ExceptionMessage { get; set; }

		public RegistrationSortEnum Sort { get; private set; }
		public List<vwRegistration> Registrations { get; set; }

		public List<vwRegistration> RegistrationsGHTHF { get; set; }
		public List<vwRegistration> RegistrationsWildernessRanch { get; set; }
		public List<vwRegistration> RegistrationsWindmillRanch { get; set; }

		public bool IsMealsAvailable { get; set; } = Sukkot.Constants.Other.IsMealsAvailable;

		public RegistrationSortEnum RegistrationSort { get; set; } = RegistrationSortEnum.LastName;

		public int RecordCount { get; set; } = 0;

		protected override async Task OnInitializedAsync()
		{
			try
			{
				Logger.LogDebug($"Inside: {nameof(RegistrationList)}!{nameof(OnInitializedAsync)}, RegistrationSort:{RegistrationSort}, calling {nameof(svc.GetAll)}");
				//Seasons = CalendarYear.Seasons.Where(w => w.YearId == CalendarYear.Year).ToList();
				Registrations = await svc.GetAll(RegistrationSort);
				if (Registrations != null) 
				{ 
					RecordCount = Registrations.Count;
					RegistrationsGHTHF = Registrations.Where(w => w.LocationEnum == LocationEnum.GreenHouseTrolleyHobbyFarm).ToList();
					RegistrationsWildernessRanch = Registrations.Where(w => w.LocationEnum == LocationEnum.WildernessRanch).ToList();
					RegistrationsWindmillRanch = Registrations.Where(w => w.LocationEnum == LocationEnum.WindmillRanch).ToList();
				}
				
			}
			catch (Exception)
			{
				ExceptionMessage = svc.ExceptionMessage;
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
		}

		async Task Sort_ButtonClick(RegistrationSortEnum sort)
		{
			Logger.LogDebug($"Inside: {nameof(RegistrationList)}!{nameof(Sort_ButtonClick)}, sort:{sort}");

			RegistrationSort = sort;
			RecordCount = 0;
			try
			{
				Registrations = await svc.GetAll(RegistrationSort);
				if (Registrations != null) { RecordCount = Registrations.Count; }
			}
			catch (Exception)
			{
				ExceptionMessage = svc.ExceptionMessage;
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
			StateHasChanged();
		}

		void Add_ButtonClick()
		{
			NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/");
		}

		void DeleteConfirmation_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.Sukkot.DeleteConfirmation + "/" + id);
		}

		void Payment_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Links2.Payment + "/" + id);
		}

		void Details_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Details + "/" + id + "/False");
		}

		void Edit_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
		}

		void DetailsMealTicket_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Meals.Index + "/" + id);
		}

		void EditMeals_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Meals.Index + "/" + id);
		}
	}
}
