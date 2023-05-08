using Blazored.Toast.Services;
using LivingMessiah.Domain;
using LivingMessiah.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Shared;

public partial class YouTubeWeeklyVideo
{
	[Inject] public IShabbatWeekCacheService? svc { get; set; }
	[Inject] public ILogger<YouTubeWeeklyVideo>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }
	[Parameter] public WeeklyVideoType? WeeklyVideoType { get; set; }

	public vwCurrentWeeklyVideo? CurrentWeeklyVideo;

	protected override async Task OnInitializedAsync()
	{
		int WeeklyVideoTypeValue = WeeklyVideoType is null ? 0 : (int)WeeklyVideoType;
		try
		{
			Logger!.LogDebug(string.Format("Inside {0} WeeklyVideoType:{1}", nameof(YouTubeWeeklyVideo) + "!" + nameof(OnInitialized), WeeklyVideoTypeValue));
			CurrentWeeklyVideo = await svc!.GetCurrentWeeklyVideoByTypeId(WeeklyVideoTypeValue)!;

			if (CurrentWeeklyVideo == null)
			{
				Toast!.ShowWarning($"{nameof(CurrentWeeklyVideo)} NOT FOUND, WeeklyVideoType:{WeeklyVideoTypeValue}");
			}
		}

		catch (System.Exception ex)
		{
			Logger!.LogError(ex, "Error reading database");
			Toast!.ShowError("An invalid operation occurred reading database, contact your administrator");
		}
	}

}