using Blazored.Toast.Services;

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor.Schedule;

using static LivingMessiah.Web.Features.Calendar.ScheduleData;

using Page = LivingMessiah.Web.Links.Calendar;

namespace LivingMessiah.Web.Features.Calendar;

public partial class CalendarSfSchedule
{
	[Inject] public ILogger<CalendarSfSchedule>? Logger { get; set; }
	[Inject] public Data.IService? Svc { get; set; }
	[Inject] public IToastService? Toast { get; set; }
	
	[Parameter] public bool IsXsOrSm { get; set; }
	[Parameter] public int YearId { get; set; }

	protected PrintedCalendarEnum printedCalendarEnum = PrintedCalendarEnum.NotAvailable;
	protected SfSchedule<ReadonlyEventsData>? ScheduleRef;
	public DateTime CurrentDate = DateTime.Now;
	protected List<CalendarQuery>? CalendarQueries;
	public View ViewNow = View.Month;
	public List<ReadonlyEventsData>? AppointmentDataList { get; set; }


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

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}, YearId:{2}"
			, Page.Index, nameof(CalendarSfSchedule) + "!" + nameof(OnInitialized), YearId));

		try
		{
			AppointmentDataList = new List<ReadonlyEventsData>();
			AppointmentDataList = Svc!.GetData();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}"
				, nameof(CalendarSfSchedule) + "!" + nameof(OnInitialized)));
			Toast!.ShowError("An invalid operation occurred, contact your administrator");
		}

	}

	public void OnEventRendered(EventRenderedArgs<ReadonlyEventsData> args)
	{
		args.Attributes = ApplyCategoryColor(
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
