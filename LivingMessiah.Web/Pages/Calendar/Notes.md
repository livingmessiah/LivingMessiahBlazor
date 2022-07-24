
# Notes on Calendar

## Syncfusion.Blazor.Data
- Provides methods to generate query which can be executed against data source using Syncfusion.Blazor.Data.SfDataManager.
- Methods in this class are chainable.



## 

```html

	<SfSchedule TValue="ReadonlyEventsData" @ref="ScheduleRef"
							MonthsCount="@NumberOfMonths"
							MinDate="@CalendarRange.MinDate" MaxDate="@CalendarRange.MaxDate"  ** DELETED **
							ShowQuickInfo="true"
							Width="100%" Height="650px"
							@bind-SelectedDate="@CurrentDate" @bind-CurrentView="@ViewNow">

		<ScheduleEventSettings DataSource="@AppointmentDataList"></ScheduleEventSettings>
		<ScheduleEvents TValue="ReadonlyEventsData"
										EventRendered="OnEventRendered" />
		<ScheduleViews>
			<ScheduleView Option="View.Week"></ScheduleView>
			<ScheduleView Option="View.Month"></ScheduleView>
			<ScheduleView Option="View.Year"></ScheduleView>
		</ScheduleViews>

	</SfSchedule>


```

```csharp
		protected DateRange CalendarRange => new DateRange(DateTime.Parse("9/22/2021"), DateTime.Parse("1/21/2023"));

		public class DateRange
		{
			public DateTime MinDate { get; set; }
			public DateTime MaxDate { get; set; }
			public DateRange(DateTime x, DateTime y) => (MinDate, MaxDate) = (x, y);
		}


// 						HideEmptyAgendaDays="true" AgendaDaysCount="30"

	public bool HideEmptyAgendaDays = true;
	public int AgendaDaysCount = 7;
```



C:\Source\LivingMessiahBlazor\src\LivingMessiah.Web\Pages\Calendar\ShowHideGridButton.razor
@*<Button @onclick="@(e => ToggleButtonClick(IsCollapsed))"
					class="btn-primary btn-sm float-end">
		@ButtonText

	</Button>*@

			//ButtonText = IsCollapsed ? "Details ⬇️" : "Hide ⬆️";
		//ButtonText = IsCollapsed ? "Show Grid" : "Hide Grid";

		
	//public string ButtonText { get; set; } = "Show Grid"; // "Details ⬇️";



<ButtonShowHide Title="Appointment List">
		<ChildContent>
			<div class="card-body  m-0 px-4">

				<LoadingComponent IsLoading="AppointmentDataList==null">
					<TableTemplate Items="AppointmentDataList">
						<TableHeader>
							<th>Id</th>
							<th>Subject</th>
							<th>Description</th>
							<th>Start Time</th>
							<th>End Time</th>
						</TableHeader>
						<RowTemplate>
							<td>@context.Id</td>
							<td>@context.Subject</td>
							<td>@context.Description</td>
							<td>@context.StartTime.ToShortDateString()</td>
							<td>@context.EndTime.ToShortDateString()</td>
						</RowTemplate>
					</TableTemplate>
				</LoadingComponent>

			</div>
		</ChildContent>
	</ButtonShowHide>


<div class="control-section">
	<div class="content-wrapper">
		<div class="schedule-overview">

			<div class="overview-header">
				<div class="overview-titlebar">
					<div class="left-panel">
						<div class="schedule-overview-title" style="border: 1px solid transparent;">Scheduler Overview Functionalities</div>
					</div>
					<div class="center-panel">
						<SfButton IconCss="e-icons e-schedule-timezone" Disabled="true" CssClass="title-bar-btn">@TimezoneData.Name</SfButton>
						<SfButton IconCss="e-icons e-schedule-clock" Disabled="true" CssClass="title-bar-btn">@SystemTime.ToString("hh:mm:ss tt")</SfButton>
					</div>
			</div>

			<div class="overview-toolbar">
				<div style="height: 70px;width: calc(100% - 90px);">
					<SfToolbar Width="auto" Height="70px" OverflowMode="OverflowMode.Scrollable" ScrollStep="100">
						<ToolbarItems> 	</ToolbarItems>
						<ToolbarEvents Created="OnToolbarCreated"></ToolbarEvents>
					</SfToolbar>
				</div>
				<div style="height:70px;width:90px;">
					<SfButton IconCss="e-icons e-schedule-toolbar-settings" CssClass="overview-toolbar-settings" IconPosition="IconPosition.Top" OnClick="OnSettingsClick">Settings</SfButton>
				</div>
			</div>
			

			<div class="overview-content">
				<div class="left-panel">
					<div class="overview-scheduler">
						<SfSchedule @ref="ScheduleRef" TValue="AppointmentData" CssClass="schedule-overview" Width="100%" Height="100%" 
						</SfContextMenu>
					</div>
				</div>
				<div class="right-panel @((this.IsSettingsVisible ? "" : "hide"))">


			</div>	@* "overview-content""> *@
		</div>		@* "schedule-overview"> *@
	</div>			@* "control-wrapper"> *@
</div>				@* "control-section"> *@






				<div style="height: 70px;">
					<SfToolbar Width="auto" Height="70px"
										 OverflowMode="OverflowMode.Scrollable" ScrollStep="100">
						<ToolbarItems>
							<ToolbarItem PrefixIcon="e-icons e-schedule-add-event"
													 TooltipText="New Event" Text="New Event"
													 OnClick="OnNewEventAdd"></ToolbarItem>
							@*<ToolbarItem Type="@ItemType.Separator"></ToolbarItem>
								<ToolbarItem PrefixIcon="e-icons e-schedule-ical-export"
														 TooltipText="Export to .Ics" Text="Export"
														 OnClick="ExportToIcs"></ToolbarItem>*@
						</ToolbarItems>
					</SfToolbar>
				</div>



				<div style="height: 70px;">
					<SfToolbar Width="auto" Height="70px"
										 OverflowMode="OverflowMode.Scrollable" ScrollStep="100">
						<ToolbarItems>
							<ToolbarItem PrefixIcon="e-icons e-schedule-add-event"
													 TooltipText="New Event" Text="New Event"
													 OnClick="OnNewEventAdd"></ToolbarItem>
							<ToolbarItem Type="@ItemType.Separator"></ToolbarItem>
							<ToolbarItem PrefixIcon="e-icons e-schedule-week-view" TooltipText="Week" Text="Week" OnClick="OnWeekView"></ToolbarItem>
							<ToolbarItem PrefixIcon="e-icons e-schedule-month-view" TooltipText="Month" Text="Month" OnClick="OnMonthView"></ToolbarItem>
							<ToolbarItem PrefixIcon="e-icons e-schedule-year-view" TooltipText="Year" Text="Year" OnClick="OnYearView"></ToolbarItem>

						</ToolbarItems>
					</SfToolbar>
				</div>

			</div> @* "overview-toolbar"> *@


			

		public async void ExportToIcs()
		{
			await ScheduleRef.ExportToICalendarAsync();
		}

		public class CalendarData
		{
			public string CalendarName { get; set; }
			public string CalendarColor { get; set; }
			public int CalendarId { get; set; }
		}


		public async void OnExportClick(Syncfusion.Blazor.SplitButtons.MenuEventArgs args)
		{
			if (args.Item.Text == "Excel")
			{
				List<ReadonlyEventsData> ExportDatas = new List<ReadonlyEventsData>();
				List<ReadonlyEventsData> EventCollection = await ScheduleRef.GetEventsAsync();
				//List<Syncfusion.Blazor.Schedule.Resource> ResourceCollection = ScheduleRef.GetResourceCollections();
				//List<CalendarData> ResourceData = ResourceCollection[0].DataSource as List<CalendarData>;
				//for (int a = 0, count = ResourceData.Count(); a < count; a++)
				//{
					//List<ReadonlyEventsData> datas = EventCollection.Where(e => e.CalendarId == ResourceData[a].CalendarId).ToList();
					List<ReadonlyEventsData> datas = EventCollection.ToList();
					foreach (ReadonlyEventsData data in datas)
					{
						ExportDatas.Add(data);
					}
				//}
				ExportOptions Options = new ExportOptions()
				{
					ExportType = ExcelFormat.Xlsx,
					CustomData = ExportDatas,
					Fields = new string[] { "Id", "Subject", "StartTime", "EndTime", "CalendarId" }
				};
				await ScheduleRef.ExportToExcelAsync(Options);
			}
			else
			{
				await ScheduleRef.ExportToICalendarAsync();
			}
		}


				private View CurrentView { get; set; } = View.Week;


		private async void OnNewEventAdd()
		{
			DateTime Date = this.ScheduleRef.SelectedDate;
			DateTime Today = DateTime.Now;
			ReadonlyEventsData eventData = new ReadonlyEventsData
			{
				Id = new Random().Next(1000),
				Subject = "",
				StartTime = new DateTime(Date.Year, Date.Month, Date.Day, Today.Hour, 0, 0),
				EndTime = new DateTime(Date.Year, Date.Month, Date.Day, Today.Hour + 1, 0, 0),
				Location = "",
				Description = "",
				IsAllDay = false
				//,CalendarId = this.ResourceRef.Value[0]
			};
			await ScheduleRef.OpenEditorAsync(eventData, CurrentAction.Add);
		}

				private void OnWeekView()
		{
			//this.CurrentView = this.ViewRef.Checked ? View.TimelineWeek : View.Week;
			this.CurrentView = View.Week;
		}

		private void OnMonthView()
		{
			//this.CurrentView = this.ViewRef.Checked ? View.TimelineMonth : View.Month;
			this.CurrentView = View.Month;
		}

		private void OnYearView()
		{
			//this.CurrentView = this.ViewRef.Checked ? View.TimelineYear : View.Year;
			this.CurrentView = View.Year;
		}


# Index.razor
				<!-- regular button"
				<button @onclick="OnPrintClick"
								class="btn-primary btn-sm">
					<span class="fas fa-print"></span> Print
				</button>
				-->



				<!-- Bootstrap Solution"
				<div class="btn-group">
					<button type="button" class="btn btn-primary">Export</button>
					<button type="button" class="btn btn-primary dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
						<span class="sr-only">Toggle Dropdown</span>
					</button>
					<div class="dropdown-menu">
						<button class="dropdown-item" href="#">iCalendar</button>
						<button class="dropdown-item" href="#">Excel</button>
					</div>
				</div>
				-->


# GridDetail

		/*
		 * ddd, MMMM dd/yyyy
		 * 
		return addedDaysDescr + " " + Date.AddDays(d).ToShortDateString();		
				public string DateHtml
				{
					get
					{
						if (AddDaysDescr == "" || AddDays == 0)
						{
							return "<span class='float-end'>" + Date.ToString("ddd, MM/dd") + "</span>";
						}
						else
						{
							if (AddDays < 0)
							{
								return $@"
		 <span class='float-end'> 
		{AddDaysDescr} {Date.AddDays(AddDays):ddd, MM/dd} 
		</span>
		<br /> 
		<span class='float-end'> 
		{Date:ddd, MM/dd}
		</span>
		";
							}
							else
							{
								return $@"
		 <span class='float-end'>
		{Date:ddd, MM/dd}
		</span>
		<br /> 
		<span class='float-end'> 
		{AddDaysDescr} {Date.AddDays(AddDays):ddd, MM/dd} 
		</span>
		";
							}

						}
					}
				}
		*/
