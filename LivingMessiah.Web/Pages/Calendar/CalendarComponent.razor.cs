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

using Microsoft.Extensions.Caching.Memory;
using CacheSettings = LivingMessiah.Web.Settings.Constants.CalendarCache;
using Page = LivingMessiah.Web.Links.Calendar;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.Calendar;

public partial class CalendarComponent
{
	[Inject] public ILogger<CalendarComponent>? Logger { get; set; }
	[Inject] public IKeyDateRepository? db { get; set; }
	[Inject] public IMemoryCache? Cache { get; set; }
	[Inject] public IToastService? Toast { get; set; }


	[Parameter] public bool IsXsOrSm { get; set; }
	[Parameter] public int YearId { get; set; }

	protected PrintedCalendarEnum printedCalendarEnum = PrintedCalendarEnum.ReadyForSale;

	protected SfSchedule<ReadonlyEventsData>? ScheduleRef;

	public DateTime CurrentDate = DateTime.Now;

	protected List<CalendarVM>? CalendarVMs;

	public List<ReadonlyEventsData>? AppointmentDataList { get; set; }

	public View ViewNow = View.Month;

	List<DropDownData> ViewData = new List<DropDownData>() {
				new DropDownData { Name = "Week", Value = View.Week },
				new DropDownData { Name = "Month", Value = View.Month },
				new DropDownData { Name = "Year", Value = View.Year }
		};

	public class DropDownData
	{
		public string? Name { get; set; }
		public View? Value { get; set; }
	}

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}, YearId:{2}"
			, Page.Index, nameof(CalendarComponent) + "!" + nameof(OnInitializedAsync), YearId));
		await PopulateCalendar(YearId);
	}

	private async Task PopulateCalendar(int year)
	{
		Logger!.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}, year:{2}"
			, Page.Index, nameof(CalendarComponent) + "!" + nameof(PopulateCalendar), year));

		CalendarVMs = Cache!.Get<List<CalendarVM>>(CacheSettings.Key);
		if (CalendarVMs is null)
		{
			try
			{
				CalendarVMs = await db!.GetPlannerVM(YearId, Enums.DateTypeFilter.FullList);
				if (CalendarVMs != null)
				{
					Logger!.LogDebug(string.Format("... Data gotten from DATABASE"));
					Cache.Set(CacheSettings.Key, CalendarVMs, TimeSpan.FromMinutes(CacheSettings.FromMinutes));
					LoadAppointmentDataList();
				}
				else
				{
					Toast!.ShowWarning("CalendarEntries NOT FOUND");
				}
			}
			catch (Exception ex)
			{
				Logger!.LogError(ex, string.Format("...Inside catch of {0}"
					, nameof(CalendarComponent) + "!" + nameof(PopulateCalendar)));
				Toast!.ShowError("An invalid operation occurred, contact your administrator");
			}
		}
		else
		{
			Logger!.LogDebug(string.Format("... Data gotten from CACHE"));
			LoadAppointmentDataList();
		}
	}


	private void LoadAppointmentDataList()
	{
		Logger!.LogDebug(string.Format("...{0}", nameof(CalendarComponent) + "!" + nameof(LoadAppointmentDataList)));
		AppointmentDataList = new List<ReadonlyEventsData>();

		string color = "";
		try
		{
			Enums.DateType dateType;
			Enums.Season season;

			foreach (var item in CalendarVMs!)
			{
				dateType = Enums.DateType.FromValue(item.DateTypeId);
				if (dateType.Value == Enums.DateType.Season)
				{
					season = Enums.Season.FromValue(item.Detail);
					color = season.CalendarColor;
				}
				else
				{
					color = dateType.CalendarColor;
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
			Logger!.LogError(ex, string.Format("...Inside catch of {0}"
				, nameof(CalendarComponent) + "!" + nameof(LoadAppointmentDataList)));
			Toast!.ShowError("An invalid operation occurred, contact your administrator");
		}

	}

	public void OnEventRendered(EventRenderedArgs<ReadonlyEventsData> args)
	{
		args.Attributes = ScheduleData.ApplyCategoryColor(
			args.Data.CategoryColor!, args.Attributes, ViewNow);
	}


	#region Header
	private DateTime SystemTime { get; set; } = DateTime.UtcNow.AddHours(+2);

	public async void OnPrintClick()
	{
		await ScheduleRef!.PrintAsync();
	}

	public async void OnExportClick(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
	{
		if (args.Item.Text == "Excel")
		{
			List<ReadonlyEventsData> ExportDatas = new List<ReadonlyEventsData>();
			List<ReadonlyEventsData> EventCollection = await ScheduleRef!.GetEventsAsync();
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
			await ScheduleRef!.ExportToICalendarAsync();
		}
	}
	#endregion
}
