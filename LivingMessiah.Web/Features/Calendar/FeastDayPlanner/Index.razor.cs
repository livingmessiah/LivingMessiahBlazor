using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using FeastDayType = LivingMessiah.Web.Features.Calendar.Enums.FeastDay;
using Page = LivingMessiah.Web.Links.Calendar.FeastPlanner;

namespace LivingMessiah.Web.Features.Calendar.FeastDayPlanner;

public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }

	protected override void OnInitialized()
	{
		base.OnInitialized();
		string inside = $"page {Page.Index}; class: {nameof(Index)}";
		Logger!.LogDebug(string.Format("{0}; {1}", inside, nameof(OnInitialized)));
		GetDefaultFeastDayType();
	}

	public FeastDayType? CurrentFilter { get; set; }

	private void GetDefaultFeastDayType()
	{
		var today = DateOnly.FromDateTime(DateTime.Now.AddDays(Constants.Test.AddDays).AddHours(Utc.ArizonaUtcMinus7));

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

		Logger!.LogDebug(string.Format("...CurrentFilter.Name: {0}; today: {1}"
			, CurrentFilter.Name, today.ToString("dd MMM yyyy")));
	}

	private void ReturnedFilter(FeastDayType filter)
	{
		Logger!.LogDebug(string.Format("...inside {0} filter: {1}"
			, nameof(ReturnedFilter), filter));
		CurrentFilter = filter;
	}

}
