using System;
using System.Globalization;
using System.Linq;
using CalendarEnums = LivingMessiah.Web.Features.Calendar.Enums;

namespace LivingMessiah.Web.Infrastructure;

public static class DateUtil
{
	public static string ToTransliteratedHebrewDateString(this DateTime date)
	{
		HebrewCalendar hc = new HebrewCalendar();
		CalendarEnums.LunarMonth? lm = CalendarEnums.LunarMonth.List
		.Where(w => w.HebrewSort == hc.GetMonth(date))
		.OrderBy(o => o.Date)
		.FirstOrDefault();

		string monthName = "???";
		if (lm != null)	
		{
			monthName = lm!.Name;
		}
		else
		{
			monthName = hc.GetMonth(date).ToString();
		}

		//string hebrewDate = $" {hc.ToFourDigitYear(hc.GetYear(date))} / {monthName} / {hc.GetDayOfMonth(date)}";
		string hebrewDate = $" {monthName} {hc.GetDayOfMonth(date)}, {hc.ToFourDigitYear(hc.GetYear(date))}";
		return hebrewDate; 
	}

	/*
	DateOnly nextShabbat = DateUtil.GetGregorianYmdFromHebrewYmd(5776, 2, 8); // 8 Cheshvan 5776
	This example produces the following output: 10/21/2015
	*/
	public static DateOnly GetGregorianYmdFromHebrewYmd(int hebrewYear, int hebrewMonth, int hebrewDay)
	{
		var hebrewCalendar = new System.Globalization.HebrewCalendar();
		var theDate = new DateOnly(hebrewYear, hebrewMonth, hebrewDay, hebrewCalendar); 
		return theDate;
	}

	/*
	So to get the value for "today or in the next 6 days":
	DateTime nextSaturday = GetNextWeekday(DateTime.Today, DayOfWeek.Saturday);

	To get the value for "the next Saturday excluding today"
	DateTime nextSaturdayExcludingToDay = GetNextWeekday(DateTime.Today.AddDays(1), DayOfWeek.Saturday);

	Source: https://stackoverflow.com/questions/6346119/datetime-get-next-tuesday
	*/
	public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
	{
		// The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
		int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
		return start.AddDays(daysToAdd);
	}

	public static DateTime GetDateTimeWithoutTime(DateTime dateTimeWithTime)
	{
		return dateTimeWithTime.Date;
	}


	public static int GetNextShabbatWeek()
	{
		DateTime start = DateTime.Today;
		int daysToAdd = ((int)DayOfWeek.Saturday - (int)start.DayOfWeek + 7) % 7;
		start = start.AddDays(daysToAdd);
		return (start.Day - 1) / 7 + 1;
	}

	//https://stackoverflow.com/questions/2050805/getting-day-suffix-when-using-datetime-tostring
	public static string GetDaySuffix(int day)
	{
		switch (day)
		{
			case 1:
			case 21:
			case 31:
				return "st";
			case 2:
			case 22:
				return "nd";
			case 3:
			case 23:
				return "rd";
			default:
				return "th";
		}
	}
}
