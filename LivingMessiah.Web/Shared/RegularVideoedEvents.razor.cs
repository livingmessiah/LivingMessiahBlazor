using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain;
using LivingMessiah.Web.Settings;
using Microsoft.Extensions.Options;

namespace LivingMessiah.Web.Shared
{
	public partial class RegularVideoedEvents
	{
		[Inject]
		public IOptions<AppSettings> AppSettings { get; set; }

		[Inject]
		public IShabbatWeekCacheService svc { get; set; }

		[Inject]
		public ILogger<RegularVideoedEvents> Logger { get; set; }

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		protected bool ShowCurrentWeeklyVideos { get; set; }

		protected IReadOnlyList<vwCurrentWeeklyVideo> CurrentWeeklyVideos;

		protected override async Task OnInitializedAsync()
		{
			ShowCurrentWeeklyVideos = AppSettings.Value.ShowCurrentWeeklyVideos;
			Logger.LogDebug($"Inside {nameof(RegularVideoedEvents)}!{nameof(OnInitializedAsync)}; ShowCurrentWeeklyVideos:{ShowCurrentWeeklyVideos}");
			if (ShowCurrentWeeklyVideos)
			{
				try
				{
					CurrentWeeklyVideos = await svc.GetCurrentWeeklyVideos();

					if (CurrentWeeklyVideos is not null)
					{
						Logger.LogDebug($"...{nameof(CurrentWeeklyVideos)}.Count:{CurrentWeeklyVideos.Count}");
					}
					else
					{
						DatabaseWarning = true;
						DatabaseWarningMsg = $"{nameof(CurrentWeeklyVideos)} NOT FOUND";
						//Logger.LogDebug($"{nameof(CurrentWeeklyVideos)} is null, Sql:{db.BaseSqlDump}");
					}
				}
				catch (System.Exception ex)
				{
					DatabaseError = true;
					DatabaseErrorMsg = $"Error reading database";
					Logger.LogError(ex, $"...{DatabaseErrorMsg}");
				}
			}
		}

	}
}