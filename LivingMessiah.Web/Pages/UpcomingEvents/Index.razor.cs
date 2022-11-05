using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.UpcomingEvents.Queries;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using Markdig;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.UpcomingEvents;

public partial class Index
{
	[Inject] public IUpcomingEventsRepository db { get; set; }
	[Inject] public ILogger<Index> Logger { get; set; }
	[Inject] public IToastService Toast { get; set; }

	protected List<Queries.SpecialEvent> SpecialEvents;

	protected MarkdownPipeline pipeline { get; set; }

	private const int DaysPast = -5;  //
	private const int DaysAhead = 100;  //
	private int RowCnt = 0;

	private string UserInterfaceMessage = "";
	private string LogExceptionMessage = "";
	protected bool TurnSpinnerOff = false;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			Logger.LogDebug(string.Format("Inside {0} i:{1}"
				, nameof(Index) + "!" + nameof(OnInitializedAsync), 0));

			SpecialEvents = await db.GetEvents(daysAhead: DaysAhead, daysPast: DaysPast);  //daysPast: -3
			if (SpecialEvents is not null)
			{
				RowCnt = SpecialEvents.Count;
				Logger.LogDebug(string.Format("...UpcomingEventList.Count:{0}", RowCnt));
				pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
			}
			else
			{
				UserInterfaceMessage = $"{nameof(SpecialEvents)} NOT FOUND";
				Toast.ShowWarning(UserInterfaceMessage);
			}
		}
		catch (Exception ex)
		{
			UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
			LogExceptionMessage = string.Format("  Inside catch of {0}"
				, nameof(Index) + "!" + nameof(OnInitializedAsync));
			Logger.LogError(ex, LogExceptionMessage);
			Toast.ShowError(UserInterfaceMessage);
		}
		finally
		{
			TurnSpinnerOff = true;
		}
	}


}

