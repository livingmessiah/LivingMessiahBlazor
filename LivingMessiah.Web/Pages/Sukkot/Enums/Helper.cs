using System;
using System.Linq;

namespace LivingMessiah.Web.Pages.Sukkot.Enums;

public class Helper
{
	public static (DateTime[]? week1, DateTime[]? week2) GetAttendanceDatesArray(int attendanceBitwise)
	{
		if (!Enums.DateRangeType.Attendance.HasSecondMonth)
		{
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
			if (AttendanceDate.FromValue(attendanceBitwise) == AttendanceDate.None)
			{
				return (null, null);
			}
			else
			{
				return (AttendanceDate.FromValue(attendanceBitwise).Select(s => s.Date).ToArray(), null);
			}
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
		}
		else
		{
			DateTime[]? wk1;
			DateTime[]? wk2;
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
			if (AttendanceDate.FromValue(attendanceBitwise) == AttendanceDate.None)
			{
				wk1 = null;
			}
			else
			{
				wk1 = AttendanceDate.FromValue(attendanceBitwise).Where(w => w.Week == 1).Select(s => s.Date).ToArray();
			}
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
			if (AttendanceDate.FromValue(attendanceBitwise) == AttendanceDate.None)
			{
				wk2 = null;
			}
			else
			{
				wk2 = AttendanceDate.FromValue(attendanceBitwise).Where(w => w.Week == 2).Select(s => s.Date).ToArray();
			}
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
			return (wk1, wk2);
		}
	}

	public static int GetDaysBitwise(DateTime[] selectedDateArray, DateTime[] selectedDateArray2ndMonth, DateRangeType dateRangeType)
	{
		int bitwise = 0;
		AttendanceDate? attendanceDate;

		//if (dateRangeType == DateRangeType.Attendance)	{ 	}

		if (selectedDateArray is null || selectedDateArray.Length == 0)
		{
		}
		else
		{
			foreach (var item in selectedDateArray!)
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
		}



		if (dateRangeType.HasSecondMonth)
		{
			if (selectedDateArray2ndMonth is null || selectedDateArray2ndMonth.Length == 0)
			{
			}
			else
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


		}

		return bitwise;
	}

}
