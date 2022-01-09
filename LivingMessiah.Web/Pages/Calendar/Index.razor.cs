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
using System.Linq;

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
		protected PrintedCalendarEnum printedCalendarEnum = PrintedCalendarEnum.ReadyForSale;

		protected SfSchedule<ReadonlyEventsData> ScheduleRef;

		public DateTime CurrentDate = DateTime.Now;

		protected List<KeyDates.Queries.CalendarEntry> CalendarEntries;

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
					LoadAppointmentDataList();
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}

		private void LoadAppointmentDataList()
		{
			Logger.LogDebug(string.Format("Inside {0}", nameof(Index) + "!" + nameof(LoadAppointmentDataList)));
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
			args.Attributes = ScheduleData.ApplyCategoryColor(
				args.Data.CategoryColor, args.Attributes, ViewNow);
		}

		#region Header
		private DateTime SystemTime { get; set; } = DateTime.UtcNow.AddHours(+2);

		public async void OnPrintClick()
		{
			await ScheduleRef.PrintAsync();
		}

		public async void OnExportClick(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
		{
			if (args.Item.Text == "Excel")
			{
				List<ReadonlyEventsData> ExportDatas = new List<ReadonlyEventsData>();
				List<ReadonlyEventsData> EventCollection = await ScheduleRef.GetEventsAsync();
				List<ReadonlyEventsData> datas = EventCollection.ToList();
				foreach (ReadonlyEventsData data in datas)
				{
					ExportDatas.Add(data);
				}
				ExportOptions Options = new ExportOptions()
				{
					ExportType = ExcelFormat.Xlsx,
					CustomData = ExportDatas,
					Fields = new string[] { "Id", "Subject", "StartTime", "EndTime" }
				};
				await ScheduleRef.ExportToExcelAsync(Options);
			}
			else
			{
				await ScheduleRef.ExportToICalendarAsync();
			}
		}
		#endregion


		#region ErrorHandling
		/*
		private void InitializeErrorHandling()
		{
			//DatabaseInformationMsg = "";
			//DatabaseInformation = false;
			DatabaseWarningMsg = "";
			DatabaseWarning = false;
			DatabaseErrorMsg = "";
			DatabaseError = false;
		}
		*/

		//protected bool DatabaseInformation = false;
		//protected string DatabaseInformationMsg { get; set; }
		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		//void Failure(Syncfusion.Blazor.Grids.FailureEventArgs e)
		//{
		//	DatabaseErrorMsg = $"Error inside {nameof(Failure)}";  //; e.Error: {e.Error}
		//	Logger.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(Index) + "!" + nameof(Failure), e.Error));
		//	DatabaseError = true;
		//}

		#endregion

	}
}
