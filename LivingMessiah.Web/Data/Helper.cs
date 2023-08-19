using System;

namespace LivingMessiah.Web.Data;

public static class Helper
{
	public static (string CompareDate, bool IsDayOfWeekSaturday) CurrentShabbatDate()
	{
		DateTime CompareDate = DateTime.Today;
		string sCompareDate = DateTime.Today.ToString("yyyy-MM-dd") + " 12:00:00 AM";
		bool isDayOfWeekSaturday = CompareDate.DayOfWeek == DayOfWeek.Saturday ? true : false;
		return (sCompareDate, isDayOfWeekSaturday);
	}

	public static string? Scrub(string? notes)
	{
		if (!string.IsNullOrEmpty(notes))
		{
			return notes.Replace("\"", string.Empty).Replace("'", string.Empty);
		}
		else
		{
			return string.Empty;
		}
	}
}
