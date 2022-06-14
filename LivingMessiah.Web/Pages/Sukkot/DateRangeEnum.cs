using System;
using System.Collections.Generic;
using System.Linq;
using LivingMessiah.Web.Pages.Sukkot.Constants;

namespace LivingMessiah.Web.Pages.Sukkot;

public enum DateRangeEnum
{
	AttendanceDays = 1
}

public class DateRangeLocal
{
	public static List<DateRangeLocal> All { get; } = new List<DateRangeLocal>();

	public static DateRangeLocal AttendanceDays { get; } = new DateRangeLocal(
			DateRangeEnum.AttendanceDays
		, new DateRange(DateTime.Parse(AttendanceDateRange.Min), DateTime.Parse(AttendanceDateRange.Max))
		, "Select attendance days");

	public DateRangeEnum DateRangeEnum { get; private set; }
	public DateRange DateRange { get; set; }
	public string Placeholder { get; set; }

	private DateRangeLocal(DateRangeEnum crudEnum, DateRange dateRange, string placeholder)
	{
		DateRangeEnum = crudEnum;
		//Id = id;  , int id
		DateRange = dateRange;
		Placeholder = placeholder;
		All.Add(this);
	}

	public static DateRangeLocal FromEnum(DateRangeEnum enumValue)
	{
		return All.SingleOrDefault(r => r.DateRangeEnum == enumValue);
	}

}

public class DateRange
{
	public DateTime MinDate { get; set; }
	public DateTime MaxDate { get; set; }
	public DateRange(DateTime x, DateTime y) => (MinDate, MaxDate) = (x, y);
}


public static class DateFactory
{
	public static int GetAttendanceBitwise(DateTime dateTime)
	{
		bool found = _AttendanceDictionary.TryGetValue(dateTime, out int bitwise);
		if (found)
		{
			return bitwise;
		}
		else
		{
			return 0;
		}
	}

	static Dictionary<DateTime, int> _AttendanceDictionary = new Dictionary<DateTime, int>
		{
			{ Convert.ToDateTime("2022-10-09"), 1 },
			{ Convert.ToDateTime("2022-10-10"), 2 },
			{ Convert.ToDateTime("2022-10-11"), 4 },
			{ Convert.ToDateTime("2022-10-12"), 8 },
			{ Convert.ToDateTime("2022-10-13"), 16 },
			{ Convert.ToDateTime("2022-10-14"), 32 },
			{ Convert.ToDateTime("2022-10-15"), 64 },
			{ Convert.ToDateTime("2022-10-16"), 128 },
			{ Convert.ToDateTime("2022-10-17"), 256 },
			//{ Convert.ToDateTime("2022-10-18"), 512 },
		};


}
