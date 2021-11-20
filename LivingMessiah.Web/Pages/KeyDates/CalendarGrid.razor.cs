using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

//using LivingMessiah.Web.Services;
using LivingMessiah.Web.Pages.KeyDates.Enums;
using Domain = LivingMessiah.Web.Pages.KeyDates.Domain;

using LivingMessiah.Web.Pages.KeyDates.Services;
//using LivingMessiah.Web.Pages.KeyDates.Data;
using LivingMessiah.Web.Pages.KeyDates.Domain;

using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.DropDowns;

namespace LivingMessiah.Web.Pages.KeyDates
{
	public partial class CalendarGrid
	{
		[Inject]
		public IKeyDateService svc { get; set; }

		[Inject]
		public ILogger<CalendarGrid> Logger { get; set; }

		protected List<CalendarEntry> CalendarEntries;
		
		protected List<YearLookup> YearLookupList { get; set; } 

		
		public string ChangedID { get; set; }
		public string ChangedText { get; set; }

		private SfGrid<CalendarEntry> sfGrid;

		private int InitLookupFieldsAndGetCurrentYear(string relative) 
		{
			YearLookup yearLookup = svc.GetYearLookup("Current");
			int currentYear = int.TryParse(yearLookup.ID, out currentYear) ? currentYear : 0;
			ChangedID = currentYear.ToString();
			ChangedText = yearLookup.Text;
			return currentYear;
		}

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(string.Format("Inside {0}", nameof(CalendarGrid) + "!" + nameof(OnInitializedAsync)));
			try
			{
				YearLookupList = await svc.GetYearLookupList();
				int curYear = InitLookupFieldsAndGetCurrentYear("Current");
				await PopulateCalendarEntries(curYear); 
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, String.Format("...ChangedID={0}, ChangedText={1}", ChangedID, ChangedText));
			}
		}

		private async Task PopulateCalendarEntries(int relativeYear)
		{
			Logger.LogDebug(String.Format("Inside {0}, relateiveYear: {1}", nameof(CalendarGrid) + "!" + nameof(PopulateCalendarEntries), relativeYear));
			try
			{
				CalendarEntries = await svc.GetCalendarEntries(relativeYear);
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

		public async Task OnChange(ChangeEventArgs<string, YearLookup> args)
		{
			Logger.LogDebug(String.Format("Inside {0}, Previous ChangedId:{1}"
				, nameof(CalendarGrid) + "!" + nameof(OnChange), ChangedID));

			ChangedText = args.ItemData.Text;
			ChangedID = args.ItemData.ID;

			int currentYear = int.TryParse(args.ItemData.ID, out currentYear) ? currentYear : 0;

			Logger.LogDebug(String.Format("...ChangedID:{0}, ChangedText:{1}, currentYear:{2}"
				, ChangedID, ChangedText, currentYear));

			await PopulateCalendarEntries(currentYear);
		}

		// ToDo: This doesn't work
		private SfGrid<CalendarEntry> GridReport;
		public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
		{
			//Logger.LogDebug(String.Format("Inside {0}", nameof(CalendarGrid) + "!" + nameof(ToolbarClickHandler)));

			if (args.Item.Id == "Grid_excelexport") //Id is combination of Grid's ID and itemname
			{
				//Logger.LogDebug("...Calling GridReport.ExcelExport");
				await this.GridReport.ExcelExport();
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
		#endregion

	}
}
