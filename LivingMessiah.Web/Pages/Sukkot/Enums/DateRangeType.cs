using Ardalis.SmartEnum;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.Enums;

public abstract class DateRangeType : SmartEnum<DateRangeType>
{
	// ToDo: Create Sql CodeGen for this
	#region Id's
	private static class Id
	{
		internal const int Attendance = 1;
		internal const int Lodging = 2; // Not being used
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DateRangeType Attendance = new AttendanceSE();
	public static readonly DateRangeType Lodging = new LodgingSE();
	// SE=SmartEnum
	#endregion

	private DateRangeType(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract DateRange Range { get; }
	#endregion


	#region Private Instantiation

	private sealed class AttendanceSE : DateRangeType
	{
		public AttendanceSE() : base($"{nameof(Id.Attendance)}", Id.Attendance) { }
		public override string Title => "Attendance Date";
		public override DateRange Range => new DateRange(Convert.ToDateTime("2023-09-29"), Convert.ToDateTime("2023-10-07"));

	}

	private sealed class LodgingSE : DateRangeType
	{
		public LodgingSE() : base($"{nameof(Id.Lodging)}", Id.Lodging) { }
		public override string Title => "Lodging Date";
		public override DateRange Range => new DateRange(Convert.ToDateTime("2023-09-29"), Convert.ToDateTime("2023-10-07"));
	}

	#endregion
}

public record DateRange(DateTime Min, DateTime Max);

/*

SELECT
FROM Sukkot.AttendanceDate
*/