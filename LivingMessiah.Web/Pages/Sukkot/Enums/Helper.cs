using System;
using System.Linq;

namespace LivingMessiah.Web.Pages.Sukkot.Enums;

public class Helper
{
	public static (DateTime[] week1, DateTime[]? week2) GetAttendanceDatesArray(int attendanceBitwise)
	{
		if (!Enums.DateRangeType.Attendance.HasSecondMonth)
		{
			return (AttendanceDate.FromValue(attendanceBitwise).Select(s => s.Date).ToArray(), null);
		}
		else
		{
			return
				(AttendanceDate.FromValue(attendanceBitwise).Where(w => w.Week == 1).Select(s => s.Date).ToArray(),
				 AttendanceDate.FromValue(attendanceBitwise).Where(w => w.Week == 2).Select(s => s.Date).ToArray());
		}
	}

	public static int GetDaysBitwise(DateTime[] selectedDateArray, DateTime[] selectedDateArray2ndMonth, DateRangeType dateRangeType)
	{
		if (selectedDateArray is null || selectedDateArray.Length == 0) { return 0; }

		int bitwise = 0;
		AttendanceDate? attendanceDate;

		//if (dateRangeType == DateRangeType.Attendance)	{ 	}

		foreach (var item in selectedDateArray)
		{
			attendanceDate = AttendanceDate.List.Where(w => w.Date == item).SingleOrDefault();
			if (attendanceDate is not null)
			{
				bitwise += attendanceDate.Value;  // ToDo: .Bitwise is going away
			}
			else
			{
				//ExceptionMessage = $"...Acceptance Date:{item.ToShortDateString()} is out of range; range is {DateRangeType.Attendance.Range.Min.ToShortDateString()} to {DateRangeType.Attendance.Range.Max.ToShortDateString()}";
				//Logger.LogWarning(ExceptionMessage);
				//throw new RegistratationException(ExceptionMessage);
			}
		}

		if (dateRangeType.HasSecondMonth)
		{
			foreach (var item in selectedDateArray2ndMonth)
			{
				attendanceDate = AttendanceDate.List.Where(w => w.Date == item).SingleOrDefault();
				if (attendanceDate is not null)
				{
					bitwise += attendanceDate.Value;
				}
				else
				{
					//ExceptionMessage = $"...Acceptance Date:{item.ToShortDateString()} is out of range; range is {DateRangeType.Attendance.Range.Min.ToShortDateString()} to {DateRangeType.Attendance.Range.Max.ToShortDateString()}";
					//Logger.LogWarning(ExceptionMessage);
					//throw new RegistratationException(ExceptionMessage);
				}
			}
		}

		return bitwise;
	}

}
