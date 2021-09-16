using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;
using Microsoft.AspNetCore.Components;
using SukkotApi.Domain.Donations.Queries;
using SukkotApi.Domain.Donations.Enums;
using SukkotApi.Domain.Registrations.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	public partial class IndexTable
	{
		[Inject]
		public ILogger<IndexTable> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		public List<DonationReport> DonationReportList { get; set; }

		decimal totalDonation = 0;
		decimal totalSurplus = 0;
		decimal totalGrand = 0;
		int rowCount = 0;

		//public RegistrationSort RegistrationSort { get; private set; } = RegistrationSort.ById;  //ByLastName
		public DonationStatus DonationStatus { get; private set; } = DonationStatus.FullList;
		//private string sortField;

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		public bool IsMealsAvailable { get; set; } = Sukkot.Constants.Other.IsMealsAvailable;

		private async Task GetData(RegistrationSortEnum registrationSortEnum)
		{
			RegistrationSort s = RegistrationSort.FromEnum(registrationSortEnum);
			string sortField;
			//sortField = RegistrationSort == RegistrationSort.ByLastName ?
			sortField = s == RegistrationSort.ByLastName ?
				DonationStatus.SortFieldName : DonationStatus.SortFieldId;
			Logger.LogDebug($"Inside {nameof(IndexTable)}!{nameof(GetData)}; sortField={sortField}");
			try
			{
				DonationReportList = await db.GetDonationReport(DonationStatus.DonationStatusEnum, sortField, true);
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
			await GetData(RegistrationSortEnum.LastName);
		}

		//protected void SortByIdClick(RegistrationSortEnum registrationSortEnum)
		protected async void SortByIdClick(RegistrationSortEnum registrationSortEnum)
		{
			Logger.LogDebug($"Inside {nameof(IndexTable)}!{nameof(SortByIdClick)}; registrationSortEnum={registrationSortEnum}");
			await GetData(registrationSortEnum);

			/*
			
			RegistrationSort = RegistrationSort.FromEnum(registrationSortEnum);
			if (registrationSortEnum == RegistrationSortEnum.Id)
			{
				Logger.LogDebug($"...do OrderBy(x => x.Id).ToList()");
				DonationReportList = DonationReportList.OrderBy(x => x.Id).ToList();

			}
			else
			{
				Logger.LogDebug($"...do OrderBy(x => x.FamilyName).ToList()");
				DonationReportList = DonationReportList.OrderBy(x => x.FamilyName).ToList();
			}
			*/
			//IsSortedAscending = !IsSortedAscending;
			//StateHasChanged();
		}


	}
}
