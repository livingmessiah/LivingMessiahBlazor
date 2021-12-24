using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.KeyDates.Data;
using LivingMessiah.Web.Pages.KeyDates.Queries;

using Syncfusion.Blazor.Grids;


namespace LivingMessiah.Web.Pages.KeyDates.Edit
{
	[Authorize(Roles = Roles.AdminOrKeyDates)]
	public partial class GridComponent
	{
		[Inject]
		public IKeyDateRepository db { get; set; }

		[Inject]
		public ILogger<GridComponent> Logger { get; set; }

		protected List<CalendarEntry> CalendarEntries;
		
		[Parameter]
		public string YearId { get; set; }
		//public int YearId { get; set; }

		[Parameter]
		public string YearDescr { get; set; }

		//[Parameter]
		//public YearLookup YearLookup { get; set; }

		protected int yearId;

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(String.Format("Inside {0}, YearId: {1}"
				, nameof(GridComponent) + "!" + nameof(OnInitializedAsync), YearId));
			
			yearId = int.TryParse(YearId, out yearId) ? yearId : 0;
			try
			{
				//CalendarEntries = await svc.GetCalendarEntries(relativeYear);
				CalendarEntries = await db.GetCalendarEntries(yearId);
				if (CalendarEntries == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "CalendarEntries NOT FOUND";
				}
				else
				{
					StateHasChanged();
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
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
						rows += await db.UpdateKeyDateCalendar(yearId, item.CalendarTemplateId, item.Date);
					}

				}
				catch (Exception ex)
				{
					DatabaseError = true;
					DatabaseErrorMsg = $"Error updating database";
					Logger.LogError(ex, $"...{DatabaseErrorMsg}");
				}
				Logger.LogDebug($"...rows: {rows}");
			}

		}

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

	}
}
