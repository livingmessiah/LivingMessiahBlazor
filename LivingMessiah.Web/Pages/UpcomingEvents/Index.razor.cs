using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using LivingMessiah.Web.Pages.KeyDates.Queries;
using LivingMessiah.Web.Pages.KeyDates.Enums;
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

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		protected override async Task OnInitializedAsync()
		{
			try
			{
				Logger.LogDebug($"Inside {nameof(Index)}!{nameof(OnInitializedAsync)}");
				UpcomingEventList = await Svc.GetEvents(daysAhead: 100, daysPast: -3);
				if (UpcomingEventList is not null)
				{
					Logger.LogDebug($"...UpcomingEventList.Count:{UpcomingEventList.Count}");
				}
				else
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = $"{nameof(UpcomingEventList)} NOT FOUND";
					//Logger.LogDebug($"{nameof(UpcomingEventList)} is null, Sql:{db.BaseSqlDump}");
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}


	}
}

