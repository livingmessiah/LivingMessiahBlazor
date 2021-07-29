using System;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public enum DateRangeEnum
	{
		AttendanceDays=1,
		LodgingDays=2
	}

	public class DateRangeLocal
	{
		public static List<DateRangeLocal> All { get; } = new List<DateRangeLocal>();

		public static DateRangeLocal AttendanceDays { get; } = new DateRangeLocal(
				DateRangeEnum.AttendanceDays
			, new DateRange(DateTime.Parse("10/19/2021"), DateTime.Parse("10/28/2021"))
			, "Select attendance days");

		public static DateRangeLocal LodgingDays { get; } = new DateRangeLocal(
				DateRangeEnum.LodgingDays
			, new DateRange(DateTime.Parse("10/20/2021"), DateTime.Parse("10/28/2021"))
			, "Select lodging days");

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

		/*
		public static DateRangeLocal FromInt(int intValue)
		{
			return All.SingleOrDefault(r => r.Id == intValue);
		}


		public static DateRangeLocal FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}
		*/
	}

	public class DateRange
	{
		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }
		public DateRange(DateTime x, DateTime y) => (MinDate, MaxDate) = (x, y);
	}
}

/*

		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }

return $"{VersesBaseUrl}Verse/{book.Abrv}-{chapter}-{vr.BegVerse}-{vr.EndVerse}/Englishonly";

	public class VerseRange
	{
		public int BegVerse { get; set; }
		public int EndVerse { get; set; }
		public VerseRange(int x, int y) => (BegVerse, EndVerse) = (x, y);
	}
*/