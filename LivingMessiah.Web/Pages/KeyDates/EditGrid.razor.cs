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

namespace LivingMessiah.Web.Pages.KeyDates
{
	[Authorize(Roles = Roles.AdminOrKeyDates)]
	public partial class EditGrid
	{
		[Inject]
		public IKeyDateRepository db { get; set; }

		[Inject]
		public ILogger<EditGrid> Logger { get; set; }

		[Parameter]
		public int YearId { get; set; }

		protected List<CalendarEntry> CalendarEntries;

		private SfGrid<CalendarEntry> Grid;

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(String.Format("Inside {0}, YearId: {1}"
				, nameof(EditGrid) + "!" + nameof(OnInitializedAsync), YearId));

			try
			{
				CalendarEntries = await db.GetCalendarEntries(YearId);
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
						rows += await db.UpdateKeyDateCalendar(YearId, item.CalendarTemplateId, item.Date);
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

		#region ErrorHandling
		private void InitializeErrorHandling()
		{
			DatabaseInformationMsg = "";
			DatabaseInformation = false;
			DatabaseWarningMsg = "";
			DatabaseWarning = false;
			DatabaseErrorMsg = "";
			DatabaseError = false;
		}

		protected bool DatabaseInformation = false;
		protected string DatabaseInformationMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
		protected string DatabaseErrorMsg { get; set; }

		void Failure(FailureEventArgs e)
		{
			DatabaseErrorMsg = $"Error inside {nameof(Failure)}";  //; e.Error: {e.Error}
			Logger.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(EditGrid) + "!" + nameof(Failure), e.Error));
			DatabaseError = true;
		}
		#endregion

	}
}
