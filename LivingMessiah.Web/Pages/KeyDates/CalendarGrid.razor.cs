﻿using System;
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

		[Parameter]
		public bool IsXsOrSm { get; set; }
		protected string DateFormat; // = "ddd, MMMM dd, yyyy";

		protected List<CalendarEntry> CalendarEntries;

		protected override async Task OnInitializedAsync()
		{
			DateFormat = IsXsOrSm ? "yyyy/MM/dd" : "ddd, MMMM dd, yyyy";
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
