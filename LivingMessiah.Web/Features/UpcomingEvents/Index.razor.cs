using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Settings;
using Page = LivingMessiah.Web.Links.UpcomingEvents;

namespace LivingMessiah.Web.Features.UpcomingEvents;

public partial class Index
{
	[Inject] public IOptions<AppSettings>? AppSettings { get; set; }
	[Inject] public ILogger<Index>? Logger { get; set; }

	//During development this is usually set to false because it is slow
	protected bool ShowCurrentWeeklyVideos { get; set; }

	readonly string inside = $"page {Page.Index}; class:{nameof(Index)}";

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0}!{1}", inside, nameof(OnInitialized)));
		ShowCurrentWeeklyVideos = AppSettings!.Value.ShowCurrentWeeklyVideos;
	}

}