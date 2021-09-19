using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;
using SukkotApi.Domain.Donations.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LivingMessiah.Web.Services.Auth0;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class ByRegistrationId
	{
		[Inject]
		public ILogger<ByRegistrationId> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		//[Parameter]
		public string FamilyName { get; set; } = "???";

		//[Parameter]
		public int RegistrationId { get; set; } = 0;

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		public List<DonationDetail> Donations { get; set; }

		protected override async Task OnInitializedAsync()
		{
			//, id={id}, familyName={familyName}
		  	Logger.LogDebug($"Inside {nameof(ByRegistrationId)}!{nameof(OnInitializedAsync)}");
			try
			{
				Donations = await db.GetDonationsByRegistrationId(RegistrationId);
				if (Donations == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "Donations NOT FOUND";
				}

			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}

		/*
		ViewData[VDD.FamilyName] = familyName;
		ViewData[VDD.RegistrationId] = id;
		*/



	}
}
