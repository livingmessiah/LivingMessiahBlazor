using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using LivingMessiah.Web.Pages.SukkotAdmin.Enums;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Enums;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

using Syncfusion.Blazor.Grids;


namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class DonationsGrid
{
	[Inject]
	public ILogger<DonationsGrid> Logger { get; set; }

	[Inject]
	public IDonationRepository db { get; set; }

	public IEnumerable<DonationReport> DonationReportList { get; set; }

	protected bool DatabaseError { get; set; } = false;
	protected string DatabaseErrorMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }

	public DonationStatusFilter CurrentFilter { get; set; } = DonationStatusFilter.FullList;

	protected override async Task OnInitializedAsync()
	{
		await GetDataWithParms(CurrentFilter);
	}

	private SfGrid<DonationReport> Grid;
	public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
	{
		if (args.Item.Id == SyncFusionToolbar.Pdf.ArgId)
		{
			await this.Grid.ExportToPdfAsync();
		}
		if (args.Item.Id == SyncFusionToolbar.Excel.ArgId)
		{
			await this.Grid.ExportToExcelAsync();
		}
		if (args.Item.Id == SyncFusionToolbar.Csv.ArgId)
		{
			await this.Grid.ExportToCsvAsync();
		}

	}


	private async Task GetDataWithParms(DonationStatusFilter filter)
	{
		RegistrationSort sortAndDirection = RegistrationSort.ByFirstName;
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
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
		StateHasChanged();  // https://stackoverflow.com/questions/56436577/blazor-form-submit-needs-two-clicks-to-refresh-view
	}


	protected async void OnClickFilter(DonationStatusFilter newFilter)
	{
		CurrentFilter = newFilter;
		Logger.LogDebug($"Inside {nameof(OnClickFilter)}; {newFilter.Name} is now the current filter");
		await GetDataWithParms(newFilter);
	}

	public string ActiveFilter(DonationStatusFilter filter)
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

