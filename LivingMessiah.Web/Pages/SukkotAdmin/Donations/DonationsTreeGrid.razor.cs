using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data; // using SukkotApi.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain; //using SukkotApi.Domain.Donations.Queries;
using SukkotApi.Domain.Donations.Enums;
using SukkotApi.Domain.Registrations.Enums;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

using Syncfusion.Blazor.Grids;


namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class DonationsTreeGrid
	{
		[Inject]
		public ILogger<DonationsTreeGrid> Logger { get; set; }

		[Inject]
		public IDonationRepository db { get; set; }

		public IEnumerable<DonationReport> DonationReportList { get; set; }
		public IEnumerable<DonationDetail> DonationDetails { get; set; }

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		//public BaseDonationStatusFilterSmartEnum CurrentFilter { get; set; } = BaseDonationStatusFilterSmartEnum.FullList;
		protected override async Task OnInitializedAsync()
		{
			await GetDataWithParms(BaseDonationStatusFilterSmartEnum.FullList);
		}

		private async Task GetDataWithParms(BaseDonationStatusFilterSmartEnum filter)
		{
			BaseRegistrationSortSmartEnum sortAndDirection = BaseRegistrationSortSmartEnum.ByFirstName;
			string sort = sortAndDirection.SqlTableColumnName + sortAndDirection.Order;

			Logger.LogDebug($"Inside {nameof(DonationsTreeGrid)}!{nameof(GetDataWithParms)}; smartEnumFilter.Name:{filter.Name}; sort:{sort}");
			try
			{
				DonationReportList = await db.GetDonationReport(filter, sort);
				if (DonationReportList == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "DonationReportList NOT FOUND";
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
			StateHasChanged(); 
		}


	}
}
