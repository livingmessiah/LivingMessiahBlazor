using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using SukkotApi.Data;
using SukkotApi.Domain.Donations.Queries;
using SukkotApi.Domain.Registrations.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	//[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class Donations
	{

		public BaseDonationStatusFilterSmartEnum DonationStatus { get; set; } = BaseDonationStatusFilterSmartEnum.FullList;
		public BaseRegistrationSortSmartEnum RegistrationSort { get; set; } = BaseRegistrationSortSmartEnum.ByLastName;

		[Inject]
		public ILogger<Donations> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		public List<DonationReport> DonationReportList { get; set; }

		decimal totalDonation = 0;
		decimal totalSurplus = 0;
		decimal totalGrand = 0;
		int rowCount = 0;

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		public bool IsMealsAvailable { get; set; } = Sukkot.Constants.Other.IsMealsAvailable;

		private async Task GetData(BaseDonationStatusFilterSmartEnum smartEnumFilter, BaseRegistrationSortSmartEnum smartEnumSort)
		{
			
			string sort = smartEnumSort.Name + smartEnumSort.Order;
			/*
			 smartEnumSort
				.When(BaseRegistrationSortSmartEnum.ById).Then(() => sort = "Id")
				.When(BaseRegistrationSortSmartEnum.ByIdDesc).Then(() => sort = "Id DESC");
			*/

			Logger.LogDebug($"Inside {nameof(Donations)}!{nameof(GetData)}; smartEnumFilter.Name:{smartEnumFilter.Name}; sort:{sort}");
			try
			{
				DonationReportList = await db.GetDonationReport(smartEnumFilter.Value, sort);
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
		}

		protected override async Task OnInitializedAsync()
		{
			
			await GetData(BaseDonationStatusFilterSmartEnum.FullList, BaseRegistrationSortSmartEnum.ByLastName);
		}

		protected async void SortById(BaseRegistrationSortSmartEnum registrationSortEnum)
		{
			RegistrationSort = RegistrationSort.FromEnum(registrationSortEnum);
		}


		//protected async void SortTable(BaseRegistrationSortSmartEnum registrationSortEnum)
		//{
		//	RegistrationSort = RegistrationSort.FromEnum(registrationSortEnum);
		//}

		//private bool IsSortedIdAscending;
		//private bool IsSortedLastNameAscending;
		//private string CurrentSortColumn;

	}
}



//protected void SortByIdClick(RegistrationSortEnum registrationSortEnum)
//protected async void SortByIdClick(RegistrationSortEnum registrationSortEnum)
//{
//	Logger.LogDebug($"Inside {nameof(Donations)}!{nameof(SortByIdClick)}; registrationSortEnum={registrationSortEnum}");
//	await GetData(registrationSortEnum);

//	/*

//	RegistrationSort = RegistrationSort.FromEnum(registrationSortEnum);
//	if (registrationSortEnum == RegistrationSortEnum.Id)
//	{
//		Logger.LogDebug($"...do OrderBy(x => x.Id).ToList()");
//		DonationReportList = DonationReportList.OrderBy(x => x.Id).ToList();

//	}
//	else
//	{
//		Logger.LogDebug($"...do OrderBy(x => x.FamilyName).ToList()");
//		DonationReportList = DonationReportList.OrderBy(x => x.FamilyName).ToList();
//	}
//	*/
//	//IsSortedAscending = !IsSortedAscending;
//	//StateHasChanged();
//}
