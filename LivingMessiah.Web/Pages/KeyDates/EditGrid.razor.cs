using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

using LivingMessiah.Web.Pages.KeyDates.Data;
using LivingMessiah.Web.Pages.KeyDates.Queries;

using Syncfusion.Blazor.Grids;
using Blazored.Toast.Services;
using LivingMessiah.Web.Pages.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.KeyDates;

[Authorize(Roles = Roles.AdminOrKeyDates)]
public partial class EditGrid
{
	[Inject] public IKeyDateRepository? db { get; set; }
	[Inject] public ILogger<EditGrid>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	protected List<CalendarEntry>? CalendarEntries;

	private SfGrid<CalendarEntry>? Grid;

	#region SelectYearUI
	private string? selectedYearName = KeyDateYear.Next.Name;
	private int selectedYear = KeyDateYear.Next.Year;

	private async Task ChangingYearAsync(ChangeEventArgs e)
	{
		selectedYearName = e.Value!.ToString();
		selectedYear = KeyDateYear.FromName(selectedYearName).Year;
		Logger!.LogDebug(string.Format("... {0}, new selectedYear: {1}"
			, nameof(EditGrid) + "!" + nameof(ChangingYearAsync), selectedYear));
		await PopulateGrid(selectedYear);
	}
	
	private bool IsSelectedYear(string year)
	{
		return year == selectedYearName;
	}
	#endregion

	private string UserInterfaceMessage  = "";
	private string LogExceptionMessage = "";


	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(String.Format("Inside {0}, selectedYearValue: {1}"
			, nameof(EditGrid) + "!" + nameof(OnInitializedAsync), selectedYear));

		await PopulateGrid(selectedYear);
	}

	private async Task PopulateGrid(int year)
	{
		Logger!.LogDebug(String.Format("... {0}, year: {1}"
			, nameof(EditGrid) + "!" + nameof(PopulateGrid), year));
		try
		{
			CalendarEntries = await db!.GetCalendarEntries(year);
			if (CalendarEntries == null)
			{
				UserInterfaceMessage = "CalendarEntries NOT FOUND";
				Toast!.ShowWarning(UserInterfaceMessage);  // Service.UserInterfaceMessage
			}
			else
			{
				StateHasChanged();
			}
		}
		catch (Exception ex)
		{
			UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
			LogExceptionMessage = string.Format("  Inside catch of {0}"
				, nameof(EditGrid) + "!" + nameof(OnInitializedAsync));
			Logger!.LogError(ex, LogExceptionMessage);
			Toast!.ShowError(UserInterfaceMessage);
		}

	}

	public async Task OnSave(BeforeBatchSaveArgs<CalendarEntry> Args)
	{
		var BatchChanges = Args.BatchChanges;
		int rows = 0;

		if (BatchChanges.ChangedRecords.Count > 0)
		{
			Logger!.LogDebug($"Changed Records: {BatchChanges.ChangedRecords.Count}");
			try
			{
				foreach (var item in BatchChanges.ChangedRecords)
				{
					rows += await db!.UpdateKeyDateCalendar(selectedYear, item.CalendarTemplateId, item.Date); // YearId
				}
				Toast!.ShowInfo($"rows updated: {rows}");
			}
			catch (Exception ex)
			{
				UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
				LogExceptionMessage = string.Format("  Inside catch of {0}"
					, nameof(EditGrid) + "!" + nameof(OnSave));
				Logger!.LogError(ex, LogExceptionMessage);
				Toast!.ShowError(UserInterfaceMessage);
			}
			Logger!.LogDebug(string.Format("...rows: {0}", rows));
		}

	}

	#region ErrorHandling

	void Failure(FailureEventArgs e)
	{
		UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
		LogExceptionMessage = string.Format("  Inside catch of {0}"
			, nameof(EditGrid) + "!" + nameof(Failure));
		Logger!.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(EditGrid) + "!" + nameof(Failure), e.Error));
	}
	#endregion

}
