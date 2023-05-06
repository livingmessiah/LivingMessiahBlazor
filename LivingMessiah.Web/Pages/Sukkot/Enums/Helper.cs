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
}
