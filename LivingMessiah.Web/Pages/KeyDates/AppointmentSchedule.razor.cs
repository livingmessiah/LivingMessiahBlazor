using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.KeyDates.Queries;
using LivingMessiah.Web.Pages.KeyDates.Data;
using System.Threading.Tasks;
using System;
using Syncfusion.Blazor.Schedule;

namespace LivingMessiah.Web.Pages.KeyDates
{
	public partial class AppointmentSchedule
	{
		[Inject]
		public ILogger<AppointmentSchedule> Logger { get; set; }

		[Inject]
		public IKeyDateRepository db { get; set; }

		[Parameter]
		public int YearId { get; set; }

		protected const bool DumpData = true;

		public DateTime CurrentDate = new DateTime(2022, 1, 1); // ToDo: fix this

		protected List<CalendarEntry> CalendarEntries;

		//ToDo: need to assign this dynamically 
		protected DateRange CalendarRange  => new DateRange(DateTime.Parse("9/22/2021"), DateTime.Parse("1/21/2023"));

		public class DateRange
		{
			public DateTime MinDate { get; set; }
			public DateTime MaxDate { get; set; }
			public DateRange(DateTime x, DateTime y) => (MinDate, MaxDate) = (x, y);
		}

		public List<AppointmentData> AppointmentDataList { get; set; }

		public View ViewNow = View.Week;

		List<DropDownData> ViewData = new List<DropDownData>() {
				new DropDownData { Name = "Day", Value = View.Day },
				new DropDownData { Name = "Week", Value = View.Week },
				new DropDownData { Name = "Month", Value = View.Month },
				new DropDownData { Name = "Year", Value = View.Year }
		};

		public class DropDownData
		{
			public string Name { get; set; }
			public View Value { get; set; }
		}

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(AppointmentSchedule)}!{nameof(OnInitializedAsync)}");
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
					LoadAppointmentDataLista();
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}

		//  #1aaa55=Forest; #357cd2=Blue; #7fa900=Olive; #ea7a57=Clay; #00bdae=Turqoise; #f57f17=pumpkin

		private void LoadAppointmentDataLista()
		{
			Logger.LogDebug($"Inside {nameof(AppointmentSchedule)}!{nameof(LoadAppointmentDataLista)}");
			AppointmentDataList = new List<AppointmentData>();

			try
			{
				foreach (var item in CalendarEntries)
				{
					AppointmentDataList.Add(new AppointmentData
					{
						Id = item.Id,
						Subject = item.Descr,
						Description = item.Descr,
						StartTime = item.Date,
						EndTime = item.Date,
						CategoryColor = "#ea7a57",
						//CategoryColor = SmartEnums.item.DateTypeEnum
						IsAllDay = true
					}
					);
				}

			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error loading AppointmentDataList";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}

		}

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		protected int NumberOfMonths { get; set; } = 16;
		protected int FirstMonthOfYear { get; set; } = 9;

		/*
		public string[] ResourceName = { "Categories" };

		public List<ResourceData> TaskData { get; set; } = new List<ResourceData> {
				new ResourceData{ Text = "Month", Id= 1, Color = "#df5286" },
				new ResourceData{ Text = "Feast", Id= 2, Color = "#7fa900" },
				new ResourceData{ Text = "Season", Id= 3, Color = "#ea7a57" }
		};

		public class ResourceData
		{
			public int Id { get; set; }
			public string Text { get; set; }
			public string Color { get; set; }
		}
		*/
	}
}
