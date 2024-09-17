using Ardalis.SmartEnum;
using System;

namespace LivingMessiah.Web.Features.Sukkot.Enums;

public abstract class AttendanceDate : SmartFlagEnum<AttendanceDate>
{
	#region Id's
	// This is a SmartEnum the leverages Bitwise, therefor all the Id values need to be powers of two
	private static class BitwiseId 
	{
		//internal const int All = -1;
		internal const int None = 0;
		internal const int Wed_10_16 = 1;   
		internal const int Thu_10_17 = 2;
		internal const int Fri_10_18 = 4;
		internal const int Sat_10_19 = 8;
		internal const int Sun_10_20 = 16;
		internal const int Mon_10_21 = 32;
		internal const int Tue_10_22 = 64;
		internal const int Wed_10_23 = 128;
		internal const int Thu_10_24 = 256;
		internal const int Fri_10_25 = 512;
	}
	#endregion


	#region  Declared Public Instances
	//public static readonly AttendanceDate All = new AllSE();
	public static readonly AttendanceDate None = new NoneSE();

	public static readonly AttendanceDate Wed_10_16 = new Wed_10_16_SE();
	public static readonly AttendanceDate Thu_10_17 = new Thu_10_17_SE();
	public static readonly AttendanceDate Fri_10_18 = new Fri_10_18_SE();
	public static readonly AttendanceDate Sat_10_19 = new Sat_10_19_SE();
	public static readonly AttendanceDate Sun_10_20 = new Sun_10_20_SE();
	public static readonly AttendanceDate Mon_10_21 = new Mon_10_21_SE();
	public static readonly AttendanceDate Tue_10_22 = new Tue_10_22_SE();
	public static readonly AttendanceDate Wed_10_23 = new Wed_10_23_SE();
	public static readonly AttendanceDate Thu_10_24 = new Thu_10_24_SE();
	public static readonly AttendanceDate Fri_10_25 = new Fri_10_25_SE();
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
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1; // N/A
	}
	*/


	private sealed class NoneSE : AttendanceDate
	{
		public NoneSE() : base($"{nameof(BitwiseId.None)}", BitwiseId.None) { }
		public override string Title => "None";
		public override DateTime Date => DateTime.MinValue;
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1; // N/A
	}


	private sealed class Wed_10_16_SE : AttendanceDate
	{
		public Wed_10_16_SE() : base($"{nameof(BitwiseId.Wed_10_16)}", BitwiseId.Wed_10_16) { }
		public override string Title => "Wed 10/16";
		public override DateTime Date => Convert.ToDateTime("2024-10-16");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Thu_10_17_SE : AttendanceDate
	{
		public Thu_10_17_SE() : base($"{nameof(BitwiseId.Thu_10_17)}", BitwiseId.Thu_10_17) { }
		public override string Title => "Thu 10/17";
		public override DateTime Date => Convert.ToDateTime("2024-10-17");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Fri_10_18_SE : AttendanceDate
	{
		public Fri_10_18_SE() : base($"{nameof(BitwiseId.Fri_10_18)}", BitwiseId.Fri_10_18) { }
		public override string Title => "Fri 10/18";
		public override DateTime Date => Convert.ToDateTime("2024-10-18");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Sat_10_19_SE : AttendanceDate
	{
		public Sat_10_19_SE() : base($"{nameof(BitwiseId.Sat_10_19)}", BitwiseId.Sat_10_19) { }
		public override string Title => "Sat 10/19";
		public override DateTime Date => Convert.ToDateTime("2024-10-19");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Sun_10_20_SE : AttendanceDate
	{
		public Sun_10_20_SE() : base($"{nameof(BitwiseId.Sun_10_20)}", BitwiseId.Sun_10_20) { }
		public override string Title => "Sun 10/20";
		public override DateTime Date => Convert.ToDateTime("2024-10-20");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Mon_10_21_SE : AttendanceDate
	{
		public Mon_10_21_SE() : base($"{nameof(BitwiseId.Mon_10_21)}", BitwiseId.Mon_10_21) { }
		public override string Title => "Mon 10/21";
		public override DateTime Date => Convert.ToDateTime("2024-10-21");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Tue_10_22_SE : AttendanceDate
	{
		public Tue_10_22_SE() : base($"{nameof(BitwiseId.Tue_10_22)}", BitwiseId.Tue_10_22) { }
		public override string Title => "Tue 10/22";
		public override DateTime Date => Convert.ToDateTime("2024-10-22");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Wed_10_23_SE : AttendanceDate
	{
		public Wed_10_23_SE() : base($"{nameof(BitwiseId.Wed_10_23)}", BitwiseId.Wed_10_23) { }
		public override string Title => "Wed 10/23";
		public override DateTime Date => Convert.ToDateTime("2024-10-23");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Thu_10_24_SE : AttendanceDate
	{
		public Thu_10_24_SE() : base($"{nameof(BitwiseId.Thu_10_24)}", BitwiseId.Thu_10_24) { }
		public override string Title => "Thu 10/24";
		public override DateTime Date => Convert.ToDateTime("2024-10-24");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Fri_10_25_SE : AttendanceDate
	{
		public Fri_10_25_SE() : base($"{nameof(BitwiseId.Fri_10_25)}", BitwiseId.Fri_10_25) { }
		public override string Title => "Fri 10/25";
		public override DateTime Date => Convert.ToDateTime("2024-10-25");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
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