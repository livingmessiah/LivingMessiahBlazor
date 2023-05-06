using Ardalis.SmartEnum;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.Enums;

public abstract class AttendanceDate : SmartFlagEnum<AttendanceDate>
{
	#region Id's
	private static class Id
	{
		//internal const int All = -1;
		//internal const int None = 0;
		internal const int Fri_09_29 = 1;
		internal const int Sat_09_30 = 2;
		internal const int Sun_10_01 = 4;
		internal const int Mon_10_02 = 8;
		internal const int Tue_10_03 = 16;
		internal const int Wed_10_04 = 32;
		internal const int Thu_10_05 = 64;
		internal const int Fri_10_06 = 128;
		internal const int Sat_10_07 = 256;
	}
	#endregion


	#region  Declared Public Instances
	//public static readonly AttendanceDate All = new AllSE();
	//public static readonly AttendanceDate None = new NoneSE();

	public static readonly AttendanceDate Fri_09_29 = new Fri_09_29_SE();
	public static readonly AttendanceDate Sat_09_30 = new Sat_09_30_SE();
	public static readonly AttendanceDate Sun_10_01 = new Sun_10_01_SE();
	public static readonly AttendanceDate Mon_10_02 = new Mon_10_02_SE();
	public static readonly AttendanceDate Tue_10_03 = new Tue_10_03_SE();
	public static readonly AttendanceDate Wed_10_04 = new Wed_10_04_SE();
	public static readonly AttendanceDate Thu_10_05 = new Thu_10_05_SE();
	public static readonly AttendanceDate Fri_10_06 = new Fri_10_06_SE();
	public static readonly AttendanceDate Sat_10_07 = new Sat_10_07_SE();
	// SE=SmartEnum
	#endregion

	private AttendanceDate(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract DateTime Date { get; }
	public abstract DateRangeType DateRangeType { get; }
	public abstract int Week { get; }
	#endregion


	#region Private Instantiation

	/*
	private sealed class AllSE : AttendanceDate
	{
		public AllSE() : base($"{nameof(Id.All)}", Id.All) { }
		public override string Title => "All";
		public override DateTime Date => DateTime.MaxValue;
		public override int Bitwise => Id.All;
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1; // N/A
	}

	private sealed class NoneSE : AttendanceDate
	{
		public NoneSE() : base($"{nameof(Id.None)}", Id.None) { }
		public override string Title => "None";
		public override DateTime Date => DateTime.MinValue;
		public override int Bitwise => Id.None;
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1; // N/A
	}
	*/

	private sealed class Fri_09_29_SE : AttendanceDate
	{
		public Fri_09_29_SE() : base($"{nameof(Id.Fri_09_29)}", Id.Fri_09_29) { }
		public override string Title => "Fri 09/29";
		public override DateTime Date => Convert.ToDateTime("2023-09-29");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Sat_09_30_SE : AttendanceDate
	{
		public Sat_09_30_SE() : base($"{nameof(Id.Sat_09_30)}", Id.Sat_09_30) { }
		public override string Title => "Sat 09/30";
		public override DateTime Date => Convert.ToDateTime("2023-09-30");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Sun_10_01_SE : AttendanceDate
	{
		public Sun_10_01_SE() : base($"{nameof(Id.Sun_10_01)}", Id.Sun_10_01) { }
		public override string Title => "Sun 10/01";
		public override DateTime Date => Convert.ToDateTime("2023-10-01");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}
	private sealed class Mon_10_02_SE : AttendanceDate
	{
		public Mon_10_02_SE() : base($"{nameof(Id.Mon_10_02)}", Id.Mon_10_02) { }
		public override string Title => "Mon 10/02";
		public override DateTime Date => Convert.ToDateTime("2023-10-02");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}
	private sealed class Tue_10_03_SE : AttendanceDate
	{
		public Tue_10_03_SE() : base($"{nameof(Id.Tue_10_03)}", Id.Tue_10_03) { }
		public override string Title => "Tue 10/03";
		public override DateTime Date => Convert.ToDateTime("2023-10-03");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}
	private sealed class Wed_10_04_SE : AttendanceDate
	{
		public Wed_10_04_SE() : base($"{nameof(Id.Wed_10_04)}", Id.Wed_10_04) { }
		public override string Title => "Wed 10/04";
		public override DateTime Date => Convert.ToDateTime("2023-10-04");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}
	private sealed class Thu_10_05_SE : AttendanceDate
	{
		public Thu_10_05_SE() : base($"{nameof(Id.Thu_10_05)}", Id.Thu_10_05) { }
		public override string Title => "Thu 10/05";
		public override DateTime Date => Convert.ToDateTime("2023-10-05");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}
	private sealed class Fri_10_06_SE : AttendanceDate
	{
		public Fri_10_06_SE() : base($"{nameof(Id.Fri_10_06)}", Id.Fri_10_06) { }
		public override string Title => "Fri 10/06";
		public override DateTime Date => Convert.ToDateTime("2023-10-06");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}
	private sealed class Sat_10_07_SE : AttendanceDate
	{
		public Sat_10_07_SE() : base($"{nameof(Id.Sat_10_07)}", Id.Sat_10_07) { }
		public override string Title => "Sat 10/07";
		public override DateTime Date => Convert.ToDateTime("2023-10-07");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}

	#endregion
}


/*
	DECLARE @RC int
	EXEC @RC = dbo.stpAttendanceDateCodeGen 

SELECT Id, Date
, 'public override int Bitwise => ' + CAST(Bitwise AS VARCHAR(20)) + ';' AS Bitwise
, REPLACE( REPLACE(FORMAT([Date], N'ddd MM/dd'),' ','_'),'/','_') AS Value
, '		public override string Title => "' + FORMAT([Date], N'ddd MM/dd') + '";' AS Title
, '		public override DateTime Date => ' + 'Convert.ToDateTime("' + FORMAT([Date], 'yyyy-MM-dd') + '");' AS CodeGenDate
FROM Sukkot.AttendanceDate
*/