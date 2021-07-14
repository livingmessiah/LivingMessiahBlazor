using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain.KeyDates.Queries;
using System;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.UpcomingEvents
{
	public partial class Index
	{
		[Inject]
		public IUpcomingEventService Svc { get; set; }

		[Inject]
		public ILogger<Index> Logger { get; set; }

		protected List<UpcomingEvent> UpcomingEventList;

		/*
		ToDo: Can I delete these
		Need this because<Header UpcomingEvent="@item" ></Header> Doesn't work
		protected string CurrentYear;
		protected string CurrentMonth;
		protected string MonthMarginTop;
		*/

		protected bool LoadFailed = false;

		protected override async Task OnInitializedAsync()
		{
			LoadFailed = false;
			try
			{
				UpcomingEventList = await Svc.GetEvents(daysAhead:100, daysPast:-3);
				Logger.LogDebug($"Inside {nameof(Index)}!{nameof(OnInitializedAsync)}; UpcomingEventList.Count:{UpcomingEventList.Count}");
			}
			catch (Exception ex)
			{
				LoadFailed = true;
				Logger.LogError(ex, $"Failed to load page {nameof(UpcomingEvents)}");
			}
		}

	}
}
