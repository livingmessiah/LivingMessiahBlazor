using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;
using SukkotApi.Domain.Donations.Queries;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	public partial class RegistrationIdTable
	{
		[Inject]
		public ILogger<RegistrationIdTable> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		[Parameter]
		public int Id { get; set; }

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		public List<DonationDetail> DonationDetails { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(RegistrationIdTable)}!{nameof(OnInitializedAsync)}");
			try
			{
				DonationDetails = await db.GetDonationsByRegistrationId(Id);
				if (DonationDetails == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "DonationDetails NOT FOUND";
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
