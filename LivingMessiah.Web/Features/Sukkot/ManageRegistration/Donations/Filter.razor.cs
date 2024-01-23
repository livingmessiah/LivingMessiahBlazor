
using LivingMessiah.Web.Features.SukkotAdmin.Donations.Enums;
using LivingMessiah.Web.Features.SukkotAdmin.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;
using System.Threading.Tasks;
using System;
using LivingMessiah.Web.Features.SukkotAdmin.Donations.Domain;
using System.Collections.Generic;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.Donations;

public partial class Filter
{
	[Inject] public ILogger<Filter>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }
	[Inject] public LivingMessiah.Web.Features.SukkotAdmin.Donations.Data.IDonationRepository? db { get; set; }

	public IEnumerable<DonationReport>? DonationReportList { get; set; }
	public DonationStatusFilter CurrentFilter { get; set; } = DonationStatusFilter.FullList;

	protected async void OnClickFilter(DonationStatusFilter newFilter)
	{
		CurrentFilter = newFilter;
		Logger!.LogDebug($"Inside {nameof(OnClickFilter)}; {newFilter.Name} is now the current filter");
		await GetDataWithParms(newFilter);
	}

	private async Task GetDataWithParms(DonationStatusFilter filter)
	{
		RegistrationSort sortAndDirection = RegistrationSort.ByFirstName;
		string sort = sortAndDirection.SqlTableColumnName + sortAndDirection.Order;

		string message = $"Inside {nameof(Filter)}!{nameof(GetDataWithParms)}; smartEnumFilter.Name:{filter.Name}; sort:{sort}";
		Logger!.LogDebug(message);
		try
		{
			DonationReportList = await db!.GetDonationReport(filter, sort);
			if (DonationReportList == null)
			{
				Toast!.ShowWarning("DonationReportList NOT FOUND");
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, $"...Error reading database");
			Toast!.ShowError("Error reading database");

		}
		StateHasChanged();  // https://stackoverflow.com/questions/56436577/blazor-form-submit-needs-two-clicks-to-refresh-view
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
