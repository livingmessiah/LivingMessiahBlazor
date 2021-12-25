﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.KeyDates.Enums;

using LivingMessiah.Web.Pages.KeyDates.Services;
using LivingMessiah.Web.Pages.KeyDates.Data;
using LivingMessiah.Web.Pages.KeyDates.Queries;

using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.DropDowns;
using LivingMessiah.Web.Services;
using System.Linq;

namespace LivingMessiah.Web.Pages.KeyDates
{
	public partial class CalendarGrid
	{
		[Inject]
		public IKeyDateService svc { get; set; }

		[Inject]
		ISmartEnumServiceForSfDropDownList svcDDL { get; set; }

		[Inject]
		public  IKeyDateRepository db { get; set; }

		[Inject]
		public ILogger<CalendarGrid> Logger { get; set; }

		protected List<CalendarEntry> CalendarEntries;

		protected async Task PopulateDetails(int year)
		{
			Logger.LogDebug(string.Format("Inside {0}, year={1}", nameof(CalendarGrid) + "!" + nameof(PopulateDetails), year) );
			try
			{
				CalendarEntries = await db.GetCalendarEntries(year);
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

		#region DropDownList
		protected List<DropDownListVM> DataSource => svcDDL.GetKeyDateYearVM().ToList(); 

		public string SelectedValue;
		public int SelectedId;
		public int SelectedYear;
		public async Task OnChange(ChangeEventArgs<string, DropDownListVM> args)
		{
			int i = int.TryParse(args.ItemData.Value, out i) ? i : 0;
			SelectedId = i;
			SelectedYear = BaseKeyDateYearSmartEnum.FromValue(SelectedId).Year;
			await PopulateDetails(SelectedYear);
		}
		#endregion

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



//public string ChangedID { get; set; }
//public string ChangedText { get; set; }


//private int InitLookupFieldsAndGetCurrentYear(string relative) 
//{
//	YearLookup yearLookup = svc.GetYearLookup("Current");
//	int currentYear = int.TryParse(yearLookup.ID, out currentYear) ? currentYear : 0;
//	ChangedID = currentYear.ToString();
//	ChangedText = yearLookup.Text;
//	return currentYear;
//}

//protected override async Task OnInitializedAsync()
//{
//	Logger.LogDebug(string.Format("Inside {0}", nameof(CalendarGrid) + "!" + nameof(OnInitializedAsync)));
//	try
//	{
//		YearLookupList = await svc.GetYearLookupList();

//		//int curYear = InitLookupFieldsAndGetCurrentYear("Current");
//		await PopulateCalendarEntries(curYear); 
//	}
//	catch (Exception ex)
//	{
//		//Logger.LogError(ex, String.Format("...ChangedID={0}, ChangedText={1}", ChangedID, ChangedText));
//		Logger.LogError(ex, String.Format("...ChangedID={0}, ChangedText={1}", ChangedID, ChangedText));
//	}
//}

//private async Task PopulateCalendarEntries(int relativeYear)
//{
//	Logger.LogDebug(String.Format("Inside {0}, relateiveYear: {1}", nameof(CalendarGrid) + "!" + nameof(PopulateCalendarEntries), relativeYear));
//	try
//	{
//		CalendarEntries = await db.GetCalendarEntries(relativeYear);
//		if (CalendarEntries == null)
//		{
//			DatabaseWarning = true;
//			DatabaseWarningMsg = "CalendarEntries NOT FOUND";
//		}
//		else
//		{
//			StateHasChanged();
//		}
//	}
//	catch (Exception ex)
//	{
//		DatabaseError = true;
//		DatabaseErrorMsg = $"Error reading database";
//		Logger.LogError(ex, $"...{DatabaseErrorMsg}");
//	}

//}

//public async Task OnChange(ChangeEventArgs<string, YearLookup> args)
//{
//	Logger.LogDebug(String.Format("Inside {0}, Previous ChangedId:{1}"
//		, nameof(CalendarGrid) + "!" + nameof(OnChange), ChangedID));

//	ChangedText = args.ItemData.Text;
//	ChangedID = args.ItemData.ID;

//	int currentYear = int.TryParse(args.ItemData.ID, out currentYear) ? currentYear : 0;

//	Logger.LogDebug(String.Format("...ChangedID:{0}, ChangedText:{1}, currentYear:{2}"
//		, ChangedID, ChangedText, currentYear));

//	await PopulateCalendarEntries(currentYear);
//}
