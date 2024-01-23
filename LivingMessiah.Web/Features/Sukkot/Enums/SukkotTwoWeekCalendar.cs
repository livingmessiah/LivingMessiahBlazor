using Ardalis.SmartEnum;
using System;

namespace LivingMessiah.Web.Features.Sukkot.Enums;

public abstract class SukkotTwoWeekCalendar : SmartEnum<SukkotTwoWeekCalendar>
{
	#region Id's
	private static class Id
	{
		internal const int Sun_09_24 = 1;
		internal const int Mon_09_25 = 2;
		internal const int Tue_09_26 = 3;
		internal const int Wed_09_27 = 4;
		internal const int Thu_09_28 = 5;
		internal const int Fri_09_29 = 6;
		internal const int Sat_09_30 = 7;
		internal const int Sun_10_01 = 8;
		internal const int Mon_10_02 = 9;
		internal const int Tue_10_03 = 10;
		internal const int Wed_10_04 = 11;
		internal const int Thu_10_05 = 12;
		internal const int Fri_10_06 = 13;
		internal const int Sat_10_07 = 14;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly SukkotTwoWeekCalendar Sun_09_24 = new Sun_09_24_SE();
	public static readonly SukkotTwoWeekCalendar Mon_09_25 = new Mon_09_25_SE();
	public static readonly SukkotTwoWeekCalendar Tue_09_26 = new Tue_09_26_SE();
	public static readonly SukkotTwoWeekCalendar Wed_09_27 = new Wed_09_27_SE();
	public static readonly SukkotTwoWeekCalendar Thu_09_28 = new Thu_09_28_SE();
	public static readonly SukkotTwoWeekCalendar Fri_09_29 = new Fri_09_29_SE();
	public static readonly SukkotTwoWeekCalendar Sat_09_30 = new Sat_09_30_SE();
	public static readonly SukkotTwoWeekCalendar Sun_10_01 = new Sun_10_01_SE();
	public static readonly SukkotTwoWeekCalendar Mon_10_02 = new Mon_10_02_SE();
	public static readonly SukkotTwoWeekCalendar Tue_10_03 = new Tue_10_03_SE();
	public static readonly SukkotTwoWeekCalendar Wed_10_04 = new Wed_10_04_SE();
	public static readonly SukkotTwoWeekCalendar Thu_10_05 = new Thu_10_05_SE();
	public static readonly SukkotTwoWeekCalendar Fri_10_06 = new Fri_10_06_SE();
	public static readonly SukkotTwoWeekCalendar Sat_10_07 = new Sat_10_07_SE();
		// SE=SmartEnum
	#endregion

	private SukkotTwoWeekCalendar(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract DateTime Date { get; }
	public abstract int Week { get; }
	public abstract DateTime? DateAttendance { get; }

	//public abstract string WeekTitle { get; }
	//public abstract int DayNumber { get; }
	//public abstract string Font { get; }  -- fas fa-times
	#endregion

	#region Private Instantiation

	private sealed class Sun_09_24_SE : SukkotTwoWeekCalendar
	{
		public Sun_09_24_SE() : base($"{nameof(Id.Sun_09_24)}", Id.Sun_09_24) { }
		public override string Title => "Sun 09/24";
		public override DateTime Date => Convert.ToDateTime("2023-09-24");
		public override DateTime? DateAttendance => null;
		public override int Week => 1;
	}
	private sealed class Mon_09_25_SE : SukkotTwoWeekCalendar
	{
		public Mon_09_25_SE() : base($"{nameof(Id.Mon_09_25)}", Id.Mon_09_25) { }
		public override string Title => "Mon 09/25";
		public override DateTime Date => Convert.ToDateTime("2023-09-25");
		public override DateTime? DateAttendance => null;
		public override int Week => 1;
	}
	private sealed class Tue_09_26_SE : SukkotTwoWeekCalendar
	{
		public Tue_09_26_SE() : base($"{nameof(Id.Tue_09_26)}", Id.Tue_09_26) { }
		public override string Title => "Tue 09/26";
		public override DateTime Date => Convert.ToDateTime("2023-09-26");
		public override DateTime? DateAttendance => null;
		public override int Week => 1;
	}
	private sealed class Wed_09_27_SE : SukkotTwoWeekCalendar
	{
		public Wed_09_27_SE() : base($"{nameof(Id.Wed_09_27)}", Id.Wed_09_27) { }
		public override string Title => "Wed 09/27";
		public override DateTime Date => Convert.ToDateTime("2023-09-27");
		public override DateTime? DateAttendance => null;
		public override int Week => 1;
	}
	private sealed class Thu_09_28_SE : SukkotTwoWeekCalendar
	{
		public Thu_09_28_SE() : base($"{nameof(Id.Thu_09_28)}", Id.Thu_09_28) { }
		public override string Title => "Thu 09/28";
		public override DateTime Date => Convert.ToDateTime("2023-09-28");
		public override DateTime? DateAttendance => null;
		public override int Week => 1;
	}
	private sealed class Fri_09_29_SE : SukkotTwoWeekCalendar
	{
		public Fri_09_29_SE() : base($"{nameof(Id.Fri_09_29)}", Id.Fri_09_29) { }
		public override string Title => "Fri 09/29";
		public override DateTime Date => Convert.ToDateTime("2023-09-29");
		public override DateTime? DateAttendance => Convert.ToDateTime("2023-09-29");
		public override int Week => 1;
	}
	private sealed class Sat_09_30_SE : SukkotTwoWeekCalendar
	{
		public Sat_09_30_SE() : base($"{nameof(Id.Sat_09_30)}", Id.Sat_09_30) { }
		public override string Title => "Sat 09/30";
		public override DateTime Date => Convert.ToDateTime("2023-09-30");
		public override DateTime? DateAttendance => Convert.ToDateTime("2023-09-30");
		public override int Week => 1;
	}
	private sealed class Sun_10_01_SE : SukkotTwoWeekCalendar
	{
		public Sun_10_01_SE() : base($"{nameof(Id.Sun_10_01)}", Id.Sun_10_01) { }
		public override string Title => "Sun 10/01";
		public override DateTime Date => Convert.ToDateTime("2023-10-01");
		public override DateTime? DateAttendance => Convert.ToDateTime("2023-10-01");
		public override int Week => 2;
	}
	private sealed class Mon_10_02_SE : SukkotTwoWeekCalendar
	{
		public Mon_10_02_SE() : base($"{nameof(Id.Mon_10_02)}", Id.Mon_10_02) { }
		public override string Title => "Mon 10/02";
		public override DateTime Date => Convert.ToDateTime("2023-10-02");
		public override DateTime? DateAttendance => Convert.ToDateTime("2023-10-02");
		public override int Week => 2;
	}
	private sealed class Tue_10_03_SE : SukkotTwoWeekCalendar
	{
		public Tue_10_03_SE() : base($"{nameof(Id.Tue_10_03)}", Id.Tue_10_03) { }
		public override string Title => "Tue 10/03";
		public override DateTime Date => Convert.ToDateTime("2023-10-03");
		public override DateTime? DateAttendance => Convert.ToDateTime("2023-10-03");
		public override int Week => 2;
	}
	private sealed class Wed_10_04_SE : SukkotTwoWeekCalendar
	{
		public Wed_10_04_SE() : base($"{nameof(Id.Wed_10_04)}", Id.Wed_10_04) { }
		public override string Title => "Wed 10/04";
		public override DateTime Date => Convert.ToDateTime("2023-10-04");
		public override DateTime? DateAttendance => Convert.ToDateTime("2023-10-04");
		public override int Week => 2;
	}
	private sealed class Thu_10_05_SE : SukkotTwoWeekCalendar
	{
		public Thu_10_05_SE() : base($"{nameof(Id.Thu_10_05)}", Id.Thu_10_05) { }
		public override string Title => "Thu 10/05";
		public override DateTime Date => Convert.ToDateTime("2023-10-05");
		public override DateTime? DateAttendance => Convert.ToDateTime("2023-10-05");
		public override int Week => 2;
	}
	private sealed class Fri_10_06_SE : SukkotTwoWeekCalendar
	{
		public Fri_10_06_SE() : base($"{nameof(Id.Fri_10_06)}", Id.Fri_10_06) { }
		public override string Title => "Fri 10/06";
		public override DateTime Date => Convert.ToDateTime("2023-10-06");
		public override DateTime? DateAttendance => Convert.ToDateTime("2023-10-06");
		public override int Week => 2;
	}
	private sealed class Sat_10_07_SE : SukkotTwoWeekCalendar
	{
		public Sat_10_07_SE() : base($"{nameof(Id.Sat_10_07)}", Id.Sat_10_07) { }
		public override string Title => "Sat 10/07";
		public override DateTime Date => Convert.ToDateTime("2023-10-07");
		public override DateTime? DateAttendance => Convert.ToDateTime("2023-10-07");
		public override int Week => 2;
	}
	#endregion
}

/*
	DECLARE @RC int
	EXEC @RC = dbo.stpSukkotTwoWeekCalendarCodeGen 
*/