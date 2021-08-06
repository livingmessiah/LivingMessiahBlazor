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
using Microsoft.AspNetCore.Components.Web;

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

		//[TempData]
		public string ExceptionMessage { get; set; }

		public RegistrationSort Sort { get; private set; }
		public List<vwRegistration> Registrations { get; set; }
		
		public bool IsMealsAvailable { get; set; } = Sukkot.Constants.Other.IsMealsAvailable;


		public RegistrationSort RegistrationSort { get; set; } = RegistrationSort.FamilyName;

		[Parameter]
		public int Id { get; set; }

		public async Task OnInitializedAsync()
		{
			try
			{
				Registrations = await svc.GetAll(RegistrationSort);
			}
			catch (Exception)
			{
				ExceptionMessage = svc.ExceptionMessage;
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
		}

		void Add_ButtonClick(MouseEventArgs e)
		{
			NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/");
		}

		void DeleteConfirmation_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.DeleteConfirmation + "/" + id);
		}

		void Payment_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Donations.Index + "/" + id);
		}

		void DetailsMealTicket_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Meals.Index + "/" + id);
		}

		void EditMeals_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Meals.Index + "/" + id);
		}

		void Edit_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
		}
	}
}
