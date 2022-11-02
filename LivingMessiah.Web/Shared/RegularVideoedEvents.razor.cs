using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain;
using LivingMessiah.Web.Settings;
using Microsoft.Extensions.Options;
using Blazored.Toast.Services;
using System;

namespace LivingMessiah.Web.Shared;

public partial class RegularVideoedEvents
{
	[Inject]
	public IOptions<AppSettings> AppSettings { get; set; }

	[Inject]
	public IShabbatWeekCacheService svc { get; set; }

	[Inject] public ILogger<RegularVideoedEvents> Logger { get; set; }
	[Inject] public IToastService Toast { get; set; }

	protected bool ShowCurrentWeeklyVideos { get; set; }
	private string UserInterfaceMessage = "";
	private string LogExceptionMessage = "";
	protected bool TurnSpinnerOff = false;

	public DateTime MessageExpiration { get; set; } = new System.DateTime(2022, 01, 01);

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
					UserInterfaceMessage = $"{nameof(CurrentWeeklyVideos)} NOT FOUND";
					Toast.ShowWarning(UserInterfaceMessage);
				}
			}
			catch (System.Exception ex)
			{
				UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
				LogExceptionMessage = string.Format("  Inside catch of {0}"
					, nameof(RegularVideoedEvents) + "!" + nameof(OnInitializedAsync));
				Logger.LogError(ex, LogExceptionMessage);
				Toast.ShowError(UserInterfaceMessage);
			}
			finally
			{
				TurnSpinnerOff = true;
			}
		}
	}

}
