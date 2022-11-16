using System;

namespace LivingMessiah.Web.Pages.SpecialEvents;

public class DateRange
{
	public DateTime DateBegin { get; set; }
	public DateTime DateEnd { get; set; }

	public override string ToString()
	{
		return $" Date Range: {DateBegin.ToString("yyyy-MM-dd")} to {DateEnd.ToString("yyyy-MM-dd")}";
	}
}
