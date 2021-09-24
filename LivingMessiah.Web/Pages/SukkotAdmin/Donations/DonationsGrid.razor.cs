using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data; // using SukkotApi.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain; //using SukkotApi.Domain.Donations.Queries;
using SukkotApi.Domain.Donations.Enums;
using SukkotApi.Domain.Registrations.Enums;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

using LivingMessiah.Web.Services;
using Syncfusion.Blazor.Grids;


namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	//[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class DonationsGrid
	{
		[Inject]
		public ILogger<DonationsGrid> Logger { get; set; }

		[Inject]
		public IDonationRepository db { get; set; }  

		[Inject]
		public ISukkotAdminService svc { get; set; }

		public IEnumerable<DonationReport> DonationReportList { get; set; }
		public IEnumerable<DonationDetail> DonationDetails { get; set; }


		public string SelectedRegistrant { get; set; }
		public int? RowIndex { get; set; } = 1; // 1003
		public void RowSelecthandler(RowSelectEventArgs<DonationReport> Args)
		{
			//SelectedRegistrant = Args.Data.FirstName + "  (" + Args.Data.Id + ")";
			SelectedRegistrant = Args.Data.FirstName + "  #" + Args.Data.Id;
			RowIndex = Args.Data.Id;
		}

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		public bool IsMealsAvailable { get; set; } = Sukkot.Constants.Other.IsMealsAvailable;  // ToDo: Not being used


		public BaseDonationStatusFilterSmartEnum CurrentFilter { get; set; } = BaseDonationStatusFilterSmartEnum.FullList;

		protected override async Task OnInitializedAsync()
		{
			await GetDataWithParms(CurrentFilter);
		}

		private SfGrid<DonationReport> DefaultGrid;

		public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
		{
			if (args.Item.Id == "Grid_excelexport") //Id is combination of Grid's ID and itemname
			{
				await this.DefaultGrid.ExcelExport();
			}
		}

		public void CustomizeCell(QueryCellInfoEventArgs<DonationReport> args)
		{
			if (args.Column.Field == nameof(DonationReport.LocationName)) 
			{
				BaseLocationSmartEnum e = BaseLocationSmartEnum.FromName(args.Data.LocationName, false);
				//Logger.LogDebug($"Inside {nameof(CustomizeCell)}; args.Column.Field:{args.Column.Field}; textColor:{textColor}");
				args.Cell.AddClass(new string[] { e.TextColor });
			}
		}


		//public async Task<int> InsertRegistrationDonation(DonationInsertModel donationInsertModel)
		//{


		//}

		private async Task GetDataWithParms(BaseDonationStatusFilterSmartEnum filter)
		{
			BaseRegistrationSortSmartEnum sortAndDirection = BaseRegistrationSortSmartEnum.ByFirstName;
			string sort = sortAndDirection.SqlTableColumnName + sortAndDirection.Order;

			Logger.LogDebug($"Inside {nameof(DonationsGrid)}!{nameof(GetDataWithParms)}; smartEnumFilter.Name:{filter.Name}; sort:{sort}");
			try
			{
				DonationReportList = await db.GetDonationReport(filter, sort);
				if (DonationReportList == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "DonationReportList NOT FOUND";
				}
				else
				{
					//ToDo make this a be one call to the db
					DonationDetails = await db.GetDonationDetailsAll();
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

		protected async void OnClickFilter(BaseDonationStatusFilterSmartEnum newFilter)
		{
			CurrentFilter = newFilter;
			Logger.LogDebug($"Inside {nameof(OnClickFilter)}; {newFilter.Name} is now the current filter");
			await GetDataWithParms(newFilter);
		}

		public string ActiveFilter(BaseDonationStatusFilterSmartEnum filter)
		{
			if (filter == CurrentFilter)
			{
				//Logger.LogDebug($"Inside {nameof(ActiveFilter)}; {filter.Name} now active");
				return "active";
			}
			else
			{
				return "";
			}
		}

	}
}

