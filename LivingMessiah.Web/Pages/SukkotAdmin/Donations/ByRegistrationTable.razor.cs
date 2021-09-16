using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;
using SukkotApi.Domain.Donations.Queries;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	public partial class ByRegistrationTable
	{
		[Inject]
		public ILogger<ByRegistrationTable> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		public List<DonationsByRegistration> DonationsByRegistration { get; set; }

		int prevId = 0;

		public bool IsMealsAvailable { get; set; } = Sukkot.Constants.Other.IsMealsAvailable;

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(ByRegistrationTable)}!{nameof(OnInitializedAsync)}");
			try
			{
				DonationsByRegistration = await db.GetDonationsByRegistration();
				if (DonationsByRegistration == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "DonationsByRegistration NOT FOUND";
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}

		}

	}
}
