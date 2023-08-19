using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using Cache = LivingMessiah.Web.Features.UpcomingEvents.Weekly;
//using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Shared.YouTube;

public partial class Card
{
	[Inject] public Cache.ICacheService? svc { get; set; }
	[Inject] public ILogger<Card>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter, EditorRequired] public int WeeklyVideoTypeValue { get; set; }

	public LivingMessiah.Web.Features.UpcomingEvents.Weekly.vwCurrentWeeklyVideo? CurrentWeeklyVideo;
	protected bool TurnSpinnerOff = false;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			Logger!.LogDebug(string.Format("Inside {0} WeeklyVideoType:{1}", nameof(Card) + "!" + nameof(OnInitialized), WeeklyVideoTypeValue));
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
		finally
		{
			TurnSpinnerOff = true;
		}
	}

}