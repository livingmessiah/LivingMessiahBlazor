using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using LivingMessiah.Web.Settings;

namespace LivingMessiah.Web.Features.UpcomingEvents;

public partial class Index
{
	[Inject] public IOptions<AppSettings>? AppSettings { get; set; }

	//During development this is usually set to false because it is slow
	protected bool ShowCurrentWeeklyVideos { get; set; }

	protected override void OnInitialized()
	{
		ShowCurrentWeeklyVideos = AppSettings!.Value.ShowCurrentWeeklyVideos;
	}
}