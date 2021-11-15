using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

//using LivingMessiah.Web.Services;
using LivingMessiah.Domain.KeyDates.Enums;
using LivingMessiah.Web.Pages.KeyDate.Enums;
//using LivingMessiah.Domain.KeyDates.Queries;
using Domain = LivingMessiah.Web.Pages.KeyDate.Domain;

using LivingMessiah.Web.Pages.KeyDate.Data;
using LivingMessiah.Web.Pages.KeyDate.Domain;

using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.DropDowns;

namespace LivingMessiah.Web.Pages.KeyDate
{
	public partial class CalendarGrid
	{
		[Inject]
		public IKeyDateRepository db { get; set; }

		[Inject]
		public ILogger<CalendarGrid> Logger { get; set; }

		protected Domain.Constants Constants;
		protected List<CalendarEntry> CalendarEntries;

		private void PopulateYearLookupList() 
		{
			YearLookupList.Add(new YearLookup { ID = Constants.PreviousYear.ToString(), Text = "Previous Year"  });
			YearLookupList.Add(new YearLookup { ID = Constants.CurrentYear.ToString(), Text = "Current Year" });
			YearLookupList.Add(new YearLookup { ID = Constants.NextYear.ToString(), Text = "Next Year" });
		}
	
		private SfGrid<CalendarEntry> sfGrid;

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(CalendarGrid)}!{nameof(OnInitializedAsync)}");
			Constants = await db.GetConstants();
			PopulateYearLookupList();
			RelativeYear = BaseRelativeYearSmartEnum.Current;
			ChangedText = RelativeYear.Name;
			await PopulateCalendarEntries(RelativeYear);
		}

		private async Task PopulateCalendarEntries(int relativeYear)
		{
			Logger.LogDebug(String.Format("...inside {0}, relateiveYear: {1}", nameof(PopulateCalendarEntries), relativeYear ));
			try
			{
				CalendarEntries = await db.GetCalendarEntries(relativeYear);
				if (CalendarEntries == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "CalendarEntries NOT FOUND";
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
			StateHasChanged();
		}

		private BaseRelativeYearSmartEnum RelativeYear { get; set; }

		public string ChangedID { get; set; }
		public string ChangedText { get; set; }

		public async Task OnChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, YearLookup> args)
		{
			this.ChangedText = args.ItemData.Text;
			this.ChangedID = args.ItemData.ID;
			int i = int.TryParse(args.ItemData.ID, out i) ? i : 0;
			RelativeYear = BaseRelativeYearSmartEnum.FromValue(i);
			await PopulateCalendarEntries(RelativeYear);
		}

		public List<YearLookup> YearLookupList { get; set; } = new List<YearLookup>();


		public class YearLookup
		{
			public string ID { get; set; }
			public string Text { get; set; }
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
