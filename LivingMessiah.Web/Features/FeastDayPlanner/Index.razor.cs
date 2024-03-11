using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using FeastDayType = LivingMessiah.Web.Features.Calendar.Enums.FeastDay;
using LivingMessiah.Web.Infrastructure;

namespace LivingMessiah.Web.Features.FeastDayPlanner;

public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }

	protected override void OnInitialized()
	{
		base.OnInitialized();
		Logger!.LogDebug("Inside: {Class}!{Method}", nameof(Index), nameof(OnInitialized));
		GetDefaultFeastDayType();
	}

	public FeastDayType? CurrentFilter { get; set; }

	private void GetDefaultFeastDayType()
	{
		DateTime dateTimeWithoutTime = DateUtil.GetDateTimeWithoutTime(DateTime.Now.AddDays(Constants.Test.AddDays).AddHours(Utc.ArizonaUtcMinus7));

		CurrentFilter = FeastDayType.List
											.Where(w => w.Range.Max >= dateTimeWithoutTime)
											.OrderBy(o => o.Date)
											.FirstOrDefault();

		if (CurrentFilter is null)
		{
			Logger!.LogDebug("...{CurrentFilter} is null, setting to {DefaultFilter}"
				, nameof(CurrentFilter), nameof(FeastDayType.Hanukkah));
			CurrentFilter = FeastDayType.Hanukkah;
		}
	
		Logger!.LogDebug("...CurrentFilter.Name: {CurrentFilter}; CurrentDate: {CurrentDate}; FirstAndLastDates: {FirstAndLastDates}"
			, CurrentFilter.Name, dateTimeWithoutTime.ToString("dd MMM yyyy HH"), CurrentFilter.FirstAndLastDates );
	}

	private void ReturnedFilter(FeastDayType filter)
	{
		Logger!.LogDebug("...inside {Class}!{Method}; filter: {Filter}", nameof(Index), nameof(ReturnedFilter), filter);
		CurrentFilter = filter;
		Logger!.LogDebug("......Calling {Method} CurrentFilter: {CurrentFilter}", nameof(StateHasChanged), CurrentFilter);
		StateHasChanged();
	}

}
