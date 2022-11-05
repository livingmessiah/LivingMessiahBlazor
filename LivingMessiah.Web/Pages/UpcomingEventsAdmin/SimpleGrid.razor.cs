using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

//using LivingMessiah.Web.Pages.UpcomingEvents.Enums;

using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using LivingMessiah.Web.Pages.UpcomingEvents.Queries;

using Syncfusion.Blazor.Grids;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin;

public partial class SimpleGrid
{
	[Inject] public IUpcomingEventsRepository db { get; set; }
	[Inject] public ILogger<SimpleGrid> Logger { get; set; }
	[Inject] public IToastService Toast { get; set; }

	protected List<SpecialEvent> SpecialEvents;

	private const int DaysPast = -600; //daysPast: -3
	private const int DaysAhead = 100;
	private int RowCnt = 0;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			Logger.LogDebug(string.Format("Inside {0}"
				, nameof(SimpleGrid) + "!" + nameof(OnInitializedAsync)));
			SpecialEvents = await db.GetEvents(daysAhead: DaysAhead, daysPast: DaysPast);  
			if (SpecialEvents is not null)
			{
				RowCnt = SpecialEvents.Count;
				Logger.LogDebug(string.Format("...UpcomingEventList.Count:{0}", SpecialEvents.Count));
			}
			else
			{
				Toast.ShowWarning($"{nameof(SpecialEvents)} NOT FOUND");
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("  Inside catch of {0}"
				, nameof(SimpleGrid) + "!" + nameof(OnInitializedAsync)));
			Toast.ShowError("An invalid operation occurred, contact your administrator");
		}
	}

}


