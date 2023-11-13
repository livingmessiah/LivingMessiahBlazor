using System;

namespace LivingMessiah.Web.Pages.Feasts;

public partial class Omer
{
	protected int CurrentDay { get; set; }

	protected override void  OnInitialized()
	{
		// CurrentDay = GetCurrentDay("5/1/2020"); // Save for testing
		CurrentDay = GetCurrentDay();
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

		//using LivingMessiah.Web.Features.Calendar.ManageKeyDates.Constants;
		DateTime start = LivingMessiah.Web.Features.Calendar.ManageKeyDates.Constants.Omer.Date;
		start = start.AddDays(-1);
		TimeSpan difference = cur - start;
		int days = (int)difference.TotalDays;
		return days;
	}
}
