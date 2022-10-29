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

namespace LivingMessiah.Web.Pages.KeyDates;

[Authorize(Roles = Roles.AdminOrKeyDates)]
public partial class EditGrid
{
	[Inject]
	public IKeyDateRepository db { get; set; }

	[Inject]
	public ILogger<EditGrid> Logger { get; set; }
	
	[Inject]
	public IToastService Toast { get; set; }

	[Parameter]
	public int YearId { get; set; }

	protected List<CalendarEntry> CalendarEntries;

	private SfGrid<CalendarEntry> Grid;

	private string UserInterfaceMessage  = "";
	private string LogExceptionMessage = "";


	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(String.Format("Inside {0}, YearId: {1}"
			, nameof(EditGrid) + "!" + nameof(OnInitializedAsync), YearId));

		try
		{
			CalendarEntries = await db.GetCalendarEntries(YearId);
			if (CalendarEntries == null)
			{
				UserInterfaceMessage = "CalendarEntries NOT FOUND";
				Toast.ShowWarning(UserInterfaceMessage);  // Service.UserInterfaceMessage
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
			Logger.LogError(ex, LogExceptionMessage);
			Toast.ShowError(UserInterfaceMessage);
		}
	}

	public async Task OnSave(BeforeBatchSaveArgs<CalendarEntry> Args)
	{
		var BatchChanges = Args.BatchChanges;
		int rows = 0;

		if (BatchChanges.ChangedRecords.Count > 0)
		{
			Logger.LogDebug($"Changed Records: {BatchChanges.ChangedRecords.Count}");
			try
			{
				foreach (var item in BatchChanges.ChangedRecords)
				{
					rows += await db.UpdateKeyDateCalendar(YearId, item.CalendarTemplateId, item.Date);
				}
				Toast.ShowInfo($"rows updated: {rows}");
			}
			catch (Exception ex)
			{
				UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
				LogExceptionMessage = string.Format("  Inside catch of {0}"
					, nameof(EditGrid) + "!" + nameof(OnSave));
				Logger.LogError(ex, LogExceptionMessage);
				Toast.ShowError(UserInterfaceMessage);
			}
			Logger.LogDebug(string.Format("...rows: {0}", rows));
		}

	}

	#region ErrorHandling

	void Failure(FailureEventArgs e)
	{
		UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
		LogExceptionMessage = string.Format("  Inside catch of {0}"
			, nameof(EditGrid) + "!" + nameof(Failure));
		Logger.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(EditGrid) + "!" + nameof(Failure), e.Error));
	}
	#endregion

}
