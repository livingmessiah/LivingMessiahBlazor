using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Features.Calendar.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Features.Calendar;

public partial class FeastDayCalendarDetails
{
	[Parameter] public FeastDay? FeastDay { get; set; }
	[Parameter] public DateTime Date { get; set; }

	protected List<FeastDayDetail>? FeastDayDetails { get; set; }

	protected override void OnInitialized()
	{
		if (FeastDay == FeastDay.Passover)
		{
			FeastDayDetails = FeastDayDetail.List.Where(w => w.ParentFeastDayId == FeastDay.Passover.Value).OrderBy(o => o.AddDays).ToList();
		}
		else
		{
			if (FeastDay == FeastDay.Trumpets)
			{
				FeastDayDetails = FeastDayDetail.List.Where(w => w.ParentFeastDayId == FeastDay.Trumpets.Value).OrderBy(o => o.AddDays).ToList();
			}
			else
			{
				if (FeastDay == FeastDay.YomKippur)
				{
					FeastDayDetails = FeastDayDetail.List.Where(w => w.ParentFeastDayId == FeastDay.YomKippur.Value).OrderBy(o => o.AddDays).ToList();
				}
				else
				{
					if (FeastDay == FeastDay.Tabernacles)
					{
					FeastDayDetails = FeastDayDetail.List.Where(w => w.ParentFeastDayId == FeastDay.Tabernacles.Value).OrderBy(o => o.AddDays).ToList();
					}
					else
					{
						if (FeastDay == FeastDay.Hanukkah) 
						{
							FeastDayDetails = FeastDayDetail.List.Where(w => w.ParentFeastDayId == FeastDay.Hanukkah.Value).OrderBy(o => o.AddDays).ToList();
						}
						else
						{
							FeastDayDetails = FeastDayDetail.List.Where(w => w.ParentFeastDayId == FeastDay.Purim.Value).OrderBy(o => o.AddDays).ToList();
						}
					}
				}
			}
		}
	}

	protected string NotesFDD(int addedDays) 
	{
		double d = (double)addedDays;
		return Date.AddDays(d).ToString(LivingMessiah.Web.DateFormat.ddd_mm_dd);
	}
}
