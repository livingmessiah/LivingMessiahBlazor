using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using SukkotApi.Data;
using SukkotApi.Domain.Donations.Queries;
using SukkotApi.Domain.Donations.Enums;
using SukkotApi.Domain.Registrations.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	//[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class Donations
	{
		[Inject]
		public ILogger<Donations> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		public List<DonationReport> DonationReportList { get; set; }

		protected decimal totalDonation = 0;
		private decimal totalSurplus = 0;
		private decimal totalGrand = 0;
		private int rowCount = 0;

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		public bool IsMealsAvailable { get; set; } = Sukkot.Constants.Other.IsMealsAvailable;

		public BaseDonationStatusFilterSmartEnum CurrentFilter { get; set; } = BaseDonationStatusFilterSmartEnum.FullList;
		public BaseRegistrationSortSmartEnum CurrentSortAndDirection { get; set; } = BaseRegistrationSortSmartEnum.ByLastName;

		protected override async Task OnInitializedAsync()
		{
			CurrentFilter = BaseDonationStatusFilterSmartEnum.FullList;
			CurrentSortAndDirection = BaseRegistrationSortSmartEnum.ByFirstNameDesc;
			await GetDataWithParms(CurrentFilter, CurrentSortAndDirection);
		}

		private async Task GetDataWithParms(BaseDonationStatusFilterSmartEnum filter, BaseRegistrationSortSmartEnum sortAndDirection)
		{
			string sort = sortAndDirection.SqlTableColumnName + sortAndDirection.Order;

			Logger.LogDebug($"Inside {nameof(Donations)}!{nameof(GetDataWithParms)}; smartEnumFilter.Name:{filter.Name}; sort:{sort}");
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
			StateHasChanged();  // https://stackoverflow.com/questions/56436577/blazor-form-submit-needs-two-clicks-to-refresh-view
		}

		protected async void SortById()
		{
			CurrentSortAndDirection = BaseRegistrationSortSmartEnum.ById;
			await GetDataWithParms(CurrentFilter, CurrentSortAndDirection);
		}

		protected async void SortByIdDesc()
		{
			CurrentSortAndDirection = BaseRegistrationSortSmartEnum.ByIdDesc;
			await GetDataWithParms(CurrentFilter, CurrentSortAndDirection);
		}

		protected async void SortByFirstName()
		{
			CurrentSortAndDirection = BaseRegistrationSortSmartEnum.ByFirstName;
			await GetDataWithParms(CurrentFilter, CurrentSortAndDirection);
		}

		protected async void SortByFirstNameDesc()
		{
			CurrentSortAndDirection = BaseRegistrationSortSmartEnum.ByFirstNameDesc;
			await GetDataWithParms(CurrentFilter, CurrentSortAndDirection);
		}

		protected async void SortByLastName()
		{
			CurrentSortAndDirection = BaseRegistrationSortSmartEnum.ByLastName;
			await GetDataWithParms(CurrentFilter, CurrentSortAndDirection);
		}

		protected async void SortByLastNameDesc()
		{
			CurrentSortAndDirection = BaseRegistrationSortSmartEnum.ByLastNameDesc;
			await GetDataWithParms(CurrentFilter, CurrentSortAndDirection);
		}

		protected async void OnClickFilter(BaseDonationStatusFilterSmartEnum newFilter)
		{
			CurrentFilter = newFilter;
			Logger.LogDebug($"Inside {nameof(OnClickFilter)}; {newFilter.Name} is now the current filter");
			await GetDataWithParms(newFilter, CurrentSortAndDirection);
		}

		public string ActiveFilter(BaseDonationStatusFilterSmartEnum filter)
		{
			if (filter == CurrentFilter)
			{
				Logger.LogDebug($"Inside {nameof(ActiveFilter)}; {filter.Name} now active");
				return "active";
			}
			else
			{
				return "";
			}
		}

	}
}

