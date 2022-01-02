using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Syncfusion.Blazor.Schedule;

using LivingMessiah.Web.Settings;
using static LivingMessiah.Web.Pages.Calendar.ScheduleData;

using LivingMessiah.Web.Pages.KeyDates.Data;
using LivingMessiah.Web.Pages.KeyDates.Enums;


namespace LivingMessiah.Web.Pages.Calendar
{
	public partial class Index
	{
		[Inject]
		public IOptions<AppSettings> AppSettings { get; set; }

		[Inject]
		public ILogger<Index> Logger { get; set; }

		[Inject]
		public IKeyDateRepository db { get; set; }

		public int YearId { get; set; }

		public DateTime CurrentDate = new DateTime(2022, 1, 1); // ToDo: fix this

		protected List<KeyDates.Queries.CalendarEntry> CalendarEntries;

		//ToDo: need to assign this dynamically 
		protected DateRange CalendarRange => new DateRange(DateTime.Parse("9/22/2021"), DateTime.Parse("1/21/2023"));

		public class DateRange
		{
			public DateTime MinDate { get; set; }
			public DateTime MaxDate { get; set; }
			public DateRange(DateTime x, DateTime y) => (MinDate, MaxDate) = (x, y);
		}

		public List<ReadonlyEventsData> AppointmentDataList { get; set; }

		public View ViewNow = View.Month;

		List<DropDownData> ViewData = new List<DropDownData>() {
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
			YearId = AppSettings.Value.YearId;
			Logger.LogDebug(string.Format("Inside {0} YearId:{1}", nameof(Index) + "!" + nameof(OnInitializedAsync), YearId));
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

		private void LoadAppointmentDataLista()
		{
			Logger.LogDebug(string.Format("Inside {0}", nameof(Index) + "!" + nameof(LoadAppointmentDataLista)));
			AppointmentDataList = new List<ReadonlyEventsData>();

			string color = "";
			try
			{
				BaseDateTypeSmartEnum baseDateTypeSmartEnum;
				BaseSeasonSmartEnum baseSeasonSmartEnum;

				foreach (var item in CalendarEntries)
				{
					baseDateTypeSmartEnum = BaseDateTypeSmartEnum.FromValue((int)item.DateTypeEnum);

					if (baseDateTypeSmartEnum.Value == BaseDateTypeSmartEnum.Season)
					{
						baseSeasonSmartEnum = BaseSeasonSmartEnum.FromValue(item.Detail);
						color = baseSeasonSmartEnum.CalendarColor;
					}
					else
					{
						color = baseDateTypeSmartEnum.CalendarColor;
					}
					
						AppointmentDataList.Add(new ReadonlyEventsData
					{
						Id = item.Id,
						Subject = item.Descr,
						Description = item.Descr,
						StartTime = item.Date,
						EndTime = item.Date,
						CategoryColor = color, // item.DateTypeSmartEnum.CalendarColor,
						IsAllDay = true,
						IsReadonly = true
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

		public void OnEventRendered(EventRenderedArgs<ReadonlyEventsData> args)
		{
			args.Attributes = ScheduleData.ApplyCategoryColor(args.Data.CategoryColor, args.Attributes, ViewNow);
		}

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		protected int NumberOfMonths { get; set; } = 16;
		protected int FirstMonthOfYear { get; set; } = 9;

	}
}
