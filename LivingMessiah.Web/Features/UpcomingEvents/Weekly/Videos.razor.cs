using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace LivingMessiah.Web.Features.UpcomingEvents.Weekly;

public partial class Videos
{
	[Inject] public ICacheService? svc { get; set; }  
	[Inject] public ILogger<Videos>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	private string UserInterfaceMessage = "";
	private string LogExceptionMessage = "";
	protected bool TurnSpinnerOff = false;

	public DateTime AlertExpiration { get; set; } = new System.DateTime(2022, 01, 01);

	protected IReadOnlyList<vwCurrentWeeklyVideo>? CurrentWeeklyVideos;

	readonly string inside = $"...class:{nameof(Videos)}; {nameof(OnInitializedAsync)}";

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("{0}", inside));
		try
		{
			CurrentWeeklyVideos = await svc!.GetCurrentWeeklyVideos()!;

			if (CurrentWeeklyVideos is not null)
			{
				Logger!.LogDebug($"...{nameof(CurrentWeeklyVideos)}.Count:{CurrentWeeklyVideos.Count}");
			}
			else
			{
				UserInterfaceMessage = $"{nameof(CurrentWeeklyVideos)} NOT FOUND";
				Toast!.ShowWarning(UserInterfaceMessage);
			}
		}
		catch (System.Exception ex)
		{
			UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
			LogExceptionMessage = string.Format("  Inside catch of {0}"
				, nameof(Videos) + "!" + nameof(OnInitializedAsync));
			Logger!.LogError(ex, LogExceptionMessage);
			Toast!.ShowError(UserInterfaceMessage);
		}
		finally
		{
			TurnSpinnerOff = true;
		}

	}

}
