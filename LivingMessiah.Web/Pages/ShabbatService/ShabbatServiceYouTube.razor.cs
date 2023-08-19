using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using static LivingMessiah.Web.Links.ShabbatService.LiveFeed;

using WVT_Enums = LivingMessiah.Web.Pages.Admin.Video.Enums.WeeklyVideoType;

namespace LivingMessiah.Web.Pages.ShabbatService;

public partial class ShabbatServiceYouTube
{
	[Inject] public LivingMessiah.Web.Features.UpcomingEvents.Weekly.ICacheService? svc { get; set; }

	[Inject] public ILogger<ShabbatServiceYouTube>? Logger { get; set; }

	[Parameter]	public bool ShowSpanish { get; set; }
	[Parameter] public bool LoadQuickly { get; set; } = false;
	[Parameter]	public bool IsPrinterFriendly { get; set; }

	protected const string SubTitle = "Watch the video when it is live";

	protected string? Url { get; set; }
	protected string? Heading;
	protected bool LoadFailed = false;
	public LivingMessiah.Web.Features.UpcomingEvents.Weekly.vwCurrentWeeklyVideo? CurrentWeeklyVideo;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			LoadFailed = false;
			Logger!.LogDebug($"Inside {nameof(ShabbatServiceYouTube)}!{nameof(OnInitializedAsync)}, ShowSpanish:{ShowSpanish} ");
			#pragma warning disable CS8602 // Dereference of a possibly null reference.
						CurrentWeeklyVideo = await svc!.GetCurrentWeeklyVideoByTypeId(
											(ShowSpanish) ?
											WVT_Enums.MainServiceSpanish.Value :
											WVT_Enums.MainServiceEnglish.Value);
			#pragma warning restore CS8602 // Dereference of a possibly null reference.


			if (CurrentWeeklyVideo != null)
			{
				SetUrl(CurrentWeeklyVideo.YouTubeId!);
				SetHeader();
			}
			else
			{
				LoadFailed = true;
			}
		}

		catch (System.Exception ex)
		{
			LoadFailed = true;
			Logger!.LogError(ex, $"<br /><br /> {nameof(OnInitializedAsync)}");
		}

	}

	private void SetHeader()
	{
		const string Icon = "<i class='fas fa-satellite'></i>";
		string Title =
			(ShowSpanish) ?
			ShabbatServiceEspTitle :
			ShabbatServiceTitle;

		if (!IsPrinterFriendly)
		{
			Heading = $"<h2 id='@TopId'><span class='badge bg-danger'>{Icon} {Title}</span></h2>";
		}
		else
		{
			Heading = $@"<p>See this YouTube link: <b> {Url} </b></p>";
		}
	}

	private void SetUrl(string youTubeId)
	{
		const string BaseUrl = "https://www.youtube.com/embed/";
		const string BasePrinterFriendlyUrl = "https://youtu.be/";

		if (IsPrinterFriendly)
		{
			Url = BasePrinterFriendlyUrl + youTubeId;
		}
		else
		{
			Url = BaseUrl + youTubeId + "?rel=0";
		}
	}

}
