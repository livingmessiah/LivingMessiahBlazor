using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using FeastDayType = LivingMessiah.Web.Features.Calendar.Enums.FeastDay;
using Page = LivingMessiah.Web.Links.Calendar.FeastPlanner;

namespace LivingMessiah.Web.Features.Calendar.FeastDayPlanner;

public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }

	public FeastDayType? CurrentFilter { get; set; }

	protected string inside = $"page {Page.Index}; class: {nameof(Index)}";

	protected override void OnInitialized()
	{
		base.OnInitialized();
		Logger!.LogDebug(string.Format("{0}; {1}", inside, nameof(OnInitialized)));

		//var todayPlus120 = DateOnly.FromDateTime(DateTime.Now.AddDays(120));
		var today = DateOnly.FromDateTime(DateTime.Now);

		CurrentFilter = FeastDayType.List
											.Where(w => DateOnly.FromDateTime(w.Date) >= today)
											.OrderBy(o => o.Date)
											.FirstOrDefault();

		if (CurrentFilter is null)
		{
			Logger!.LogDebug(string.Format("...{0} is null, setting to {1}"
			, nameof(CurrentFilter), nameof(FeastDayType.Hanukkah)));
			CurrentFilter = FeastDayType.Hanukkah;
		}

		Logger!.LogDebug(string.Format("...CurrentFilter.Name: {0}; todayPlus120: {1}"
			, CurrentFilter.Name, today.ToString("dd MMM yyyy")));


		GetSubTitle();
	}

	protected string GetSubTitle()
	{
		TimeSpan difference = CurrentFilter!.Date - DateTime.Now;
		int days = (int)difference.TotalDays;
		return $"{days} days until {CurrentFilter.Name}";
	}

	private void ReturnedFilter(FeastDayType filter)
	{
		CurrentFilter = filter;
	}

}
