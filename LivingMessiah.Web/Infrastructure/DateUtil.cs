using System;

namespace LivingMessiah.Web.Infrastructure
{
	public static class DateUtil
	{
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

		public static int GetNextShabbatWeek()
		{
			DateTime start = DateTime.Today;
			int daysToAdd = ((int)DayOfWeek.Saturday - (int)start.DayOfWeek + 7) % 7;
			start = start.AddDays(daysToAdd);
			return (start.Day - 1) / 7 + 1;
		}

		//https://stackoverflow.com/questions/2050805/getting-day-suffix-when-using-datetime-tostring
		private static string GetDaySuffix(int day)
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
}
