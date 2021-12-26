using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.KeyDates.Data;
using LivingMessiah.Web.Pages.KeyDates.Queries;

using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.KeyDates
{
	public partial class CalendarGrid
	{
		[Inject]
		public  IKeyDateRepository db { get; set; }

		[Inject]
		public ILogger<CalendarGrid> Logger { get; set; }

		[Parameter]
		public int YearId { get; set; }

		protected List<CalendarEntry> CalendarEntries;

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(string.Format("Inside {0}, year={1}", nameof(CalendarGrid) + "!" + nameof(OnInitializedAsync), YearId) );
			try
			{
				CalendarEntries = await db.GetCalendarEntries(YearId);
				if (CalendarEntries == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "Calendar Entries NOT FOUND";
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, string.Format("...Error calling={0}", nameof(db.GetCalendarEntries)));
			}
		}

		private SfGrid<CalendarEntry> Grid;  
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
			Logger.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(CalendarGrid) + "!" + nameof(Failure), e.Error));
			DatabaseError = true;
		}
		#endregion

	}
}
