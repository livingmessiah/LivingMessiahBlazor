using LivingMessiah.Domain;
using LivingMessiah.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Shared
{
	public partial class YouTubeWeeklyVideo
	{
		[Inject]
		public IShabbatWeekCacheService svc { get; set; }

		[Inject]
		public ILogger<YouTubeWeeklyVideo> Logger { get; set; }

		[Parameter]
		public WeeklyVideoType WeeklyVideoType { get; set; }

		protected bool LoadFailed = false;
		public vwCurrentWeeklyVideo CurrentWeeklyVideo;

		protected override async Task OnInitializedAsync()
		{
			try
			{
				LoadFailed = false;
				Logger.LogDebug($"Inside {nameof(YouTubeWeeklyVideo)}!{nameof(OnInitializedAsync)}; weeklyVideoType:{(int)WeeklyVideoType}");

				CurrentWeeklyVideo = await svc.GetCurrentWeeklyVideoByTypeId((int)WeeklyVideoType);

				if (CurrentWeeklyVideo == null)
				{
					LoadFailed = true;
				}
			}

			catch (System.Exception ex)
			{
				LoadFailed = true;
				Logger.LogError(ex, $"<br /><br /> {nameof(OnInitializedAsync)}");
			}

		}

	}
}
