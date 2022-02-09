using System;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.Sukkot;

public enum DateRangeEnum
{
		AttendanceDays = 1,
		LodgingDays = 2
}

public class DateRangeLocal
{
		public static List<DateRangeLocal> All { get; } = new List<DateRangeLocal>();

		public static DateRangeLocal AttendanceDays { get; } = new DateRangeLocal(
				DateRangeEnum.AttendanceDays
			, new DateRange(DateTime.Parse("10/20/2021"), DateTime.Parse("10/28/2021"))
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
			{ Convert.ToDateTime("2021-10-19"), 1 },
			{ Convert.ToDateTime("2021-10-20"), 2 },
			{ Convert.ToDateTime("2021-10-21"), 4 },
			{ Convert.ToDateTime("2021-10-22"), 8 },
			{ Convert.ToDateTime("2021-10-23"), 16 },
			{ Convert.ToDateTime("2021-10-24"), 32 },
			{ Convert.ToDateTime("2021-10-25"), 64 },
			{ Convert.ToDateTime("2021-10-26"), 128 },
			{ Convert.ToDateTime("2021-10-27"), 256 },
			{ Convert.ToDateTime("2021-10-28"), 512 },
		};

		public static int GetLodgingBitwise(DateTime dateTime)
		{
				bool found = _LodgingDictionary.TryGetValue(dateTime, out int bitwise);
				if (found)
				{
						return bitwise;
				}
				else
				{
						return 0;
				}
		}

		static Dictionary<DateTime, int> _LodgingDictionary = new Dictionary<DateTime, int>
		{
			{ Convert.ToDateTime("2021-10-18"), 1 },
			{ Convert.ToDateTime("2021-10-19"), 2 },
			{ Convert.ToDateTime("2021-10-20"), 4 },
			{ Convert.ToDateTime("2021-10-21"), 8 },
			{ Convert.ToDateTime("2021-10-22"), 16 },
			{ Convert.ToDateTime("2021-10-23"), 32 },
			{ Convert.ToDateTime("2021-10-24"), 64 },
			{ Convert.ToDateTime("2021-10-25"), 128 },
			{ Convert.ToDateTime("2021-10-26"), 256 },
			{ Convert.ToDateTime("2021-10-27"), 512 },
			{ Convert.ToDateTime("2021-10-28"), 1024 },
			{ Convert.ToDateTime("2021-10-29"), 2048 }
		};

}
