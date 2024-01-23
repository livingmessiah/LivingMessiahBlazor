using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Settings;

namespace LivingMessiah.Web.Features.Shavuot;

public partial class Index
{
	[Inject] public IOptions<AppSettings>? AppSettings { get; set; }

	protected int CurrentDay { get; set; }
	public int YearId { get; set; }

	protected override Task OnInitializedAsync()
	{
		YearId = AppSettings!.Value.YearId;
		// CurrentDay = GetCurrentDay("5/1/2020"); // Save for testing
		CurrentDay = GetCurrentDay();
		return base.OnInitializedAsync();
	}

	private static int GetCurrentDay(string overrideDate = "")
	{
		DateTime cur;
		DateTime curOverRide;
		if (DateTime.TryParse(overrideDate, out curOverRide))
		{
			cur = curOverRide;
		}
		else
		{
			cur = DateTime.Now;
		}

		DateTime start = Omer.Date;
		start = start.AddDays(-1);
		TimeSpan difference = cur - start;
		int days = (int)difference.TotalDays;
		return days;
	}

}
