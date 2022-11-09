using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Calendar.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.Calendar;

public partial class FeastDayCalendarDetails
{
	[Parameter] public FeastDay FeastDay { get; set; }
	[Parameter] public DateTime Date { get; set; }

	protected List<FeastDayDetail> FeastDayDetails { get; set; }

	protected override void OnInitialized()
	{
		if (FeastDay == FeastDay.Passover)
		{
			FeastDayDetails = FeastDayDetail.List.Where(w => w.ParentFeastDayId == FeastDay.Passover.Value).ToList();
		}
		else
		{
			FeastDayDetails = FeastDayDetail.List.Where(w => w.ParentFeastDayId == FeastDay.Tabernacles.Value).ToList();
		}
	}

	protected string NotesFDD(int addedDays) 
	{
		double d = (double)addedDays;
		return Date.AddDays(d).ToString(LivingMessiah.Web.DateFormat.ddd_mm_dd);
	}
}
