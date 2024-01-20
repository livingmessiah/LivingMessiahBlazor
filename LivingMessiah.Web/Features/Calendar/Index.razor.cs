using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LivingMessiah.Web.Settings;
using Page = LivingMessiah.Web.Links.Calendar;

namespace LivingMessiah.Web.Features.Calendar;

public partial class Index
{
	[Inject] public IOptions<AppSettings>? AppSettings { get; set; }
	[Inject] public ILogger<Index>? Logger { get; set; }

	public int YearId { get; set; }

	private PrintedCalendarEnum printedCalendarEnum { get; set; }

	protected override void OnInitialized()
	{
		YearId = AppSettings!.Value.YearId;
		Logger!.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}, YearId:{2}"
			, Page.Index, nameof(Index) + "!" + nameof(OnInitializedAsync), YearId));

		printedCalendarEnum = PrintedCalendarEnum.OnlyShowCover;
	}

}
