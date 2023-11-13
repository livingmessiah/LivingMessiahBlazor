using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

using LivingMessiah.Web.Features.Calendar.ManageKeyDates.Data;
using LivingMessiah.Web.Features.Calendar.ManageKeyDates.Queries;

using Syncfusion.Blazor.Grids;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Features.Calendar.ManageKeyDates;

[Authorize(Roles = Roles.AdminOrKeyDates)]
public partial class EditGrid
{
	[Inject] public IRepository? db { get; set; }
	[Inject] public ILogger<EditGrid>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	private SfGrid<CalendarEntry>? Grid;

	protected List<CalendarEntry>? CalendarEntries;
	[Parameter, EditorRequired] public List<CalendarEntry>? ParamCalendarEntries { get; set; }
	[Parameter, EditorRequired] public int Year { get; set; }

	private string LogExceptionMessage = "";

	protected string inside = $"{nameof(EditGrid)}";

	protected override void OnParametersSet()
	{
		Logger!.LogDebug(string.Format("Inside {0}, ParamYear: {1}"
			, nameof(EditGrid) + "!" + nameof(OnParametersSet), Year));
		CalendarEntries = ParamCalendarEntries;
	}


	public async Task OnSave(BeforeBatchSaveArgs<CalendarEntry> Args)
	{
		var BatchChanges = Args.BatchChanges;
		int rows = 0;

		if (BatchChanges.ChangedRecords.Count > 0)
		{
			Logger!.LogDebug(string.Format("...Changed Records:{0}", BatchChanges.ChangedRecords.Count));
			try
			{
				foreach (var item in BatchChanges.ChangedRecords)
				{
					rows += await db!.UpdateKeyDateCalendar(Year, item.Detail, item.Date); 
				}
				Toast!.ShowInfo($"rows updated: {rows}");
			}
			catch (Exception ex)
			{
				LogExceptionMessage = string.Format("  Inside catch of {0}"
					, nameof(EditGrid) + "!" + nameof(OnSave));
				Logger!.LogError(ex, LogExceptionMessage);
				Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(OnSave)}");
			}
			Logger!.LogDebug(string.Format("...rows: {0}", rows));
		}

	}

	#region ErrorHandling

	void Failure(FailureEventArgs e)
	{
		LogExceptionMessage = string.Format("  Inside catch of {0}"
			, nameof(EditGrid) + "!" + nameof(Failure));
		Logger!.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(EditGrid) + "!" + nameof(Failure), e.Error));
	}
	#endregion

}
