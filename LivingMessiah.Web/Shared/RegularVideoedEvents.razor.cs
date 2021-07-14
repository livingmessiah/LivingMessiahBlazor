using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain;

namespace LivingMessiah.Web.Shared
{
	public partial class RegularVideoedEvents
	{
		[Inject]
		public IShabbatWeekCacheService svc { get; set; }

		[Inject]
		public ILogger<RegularVideoedEvents> Logger { get; set; }

		protected bool ReadOperationFailed = false;
		protected IReadOnlyList<vwCurrentWeeklyVideo> CurrentWeeklyVideos;

		protected override async Task OnInitializedAsync()
		{
			try
			{
				ReadOperationFailed = false;
				CurrentWeeklyVideos = await svc.GetCurrentWeeklyVideos();
			}

			catch (System.Exception ex)
			{
				ReadOperationFailed = true;
				Logger.LogError(ex, $"<br /><br /> {nameof(OnInitializedAsync)}");
			}
		}

	}
}