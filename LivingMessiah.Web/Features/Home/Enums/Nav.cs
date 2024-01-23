using Ardalis.SmartEnum;

using static LivingMessiah.Web.Links.Admin;
// using LivingMessiah.Web.Links; not used, I think cuz i'm trying to make this be the source of  "Page Link" truth

namespace LivingMessiah.Web.Features.Home.Enums;

public abstract class Nav : SmartEnum<Nav>
{
	#region Id's
	private static class Id
	{
		//internal const int Admin = 0; // 
		internal const int Video = 1; // Admin Video
		internal const int Wirecast = 2;  // AudioVisual
		internal const int KeyDates = 3;
		internal const int Sukkot = 4;  // Manage Registration
		internal const int Contact = 5;
		internal const int SpecialEvents = 6;
		internal const int DatabaseError = 7;
		internal const int ParashaCalendar = 8;
		internal const int CalendarHealthCheck = 9;
		internal const int Dashboard = 10;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Nav Video = new VideoSE();
	public static readonly Nav Wirecast = new WirecastSE();
	public static readonly Nav KeyDates = new KeyDatesSE();
	public static readonly Nav Sukkot = new SukkotSE();
	public static readonly Nav Contact = new ContactSE();
	public static readonly Nav SpecialEvents = new SpecialEventsSE();
	public static readonly Nav DatabaseError = new DatabaseErrorSE();
	public static readonly Nav ParashaCalendar = new ParashaCalendarSE();
	public static readonly Nav CalendarHealthCheck = new CalendarHealthCheckSE();
	public static readonly Nav Dashboard = new DashboardSE();
	#endregion

	private Nav(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Text { get; }
	public abstract string Icon { get; }
	public abstract string Role { get; }
	public abstract int Sort { get; }
	#endregion

	#region Private Instantiation

	private sealed class VideoSE : Nav
	{
		public VideoSE() : base($"{nameof(Id.Video)}", Id.Video) { }
		public override string Index => "/Admin/Video/";
		//public const string Redirect = "/Admin/Video";
		public override string Text => "Admin Video";
		public override string Icon => "fab fa-teamspeak";
		public override string Role => "Admin";
		public override int Sort => 1;
	}

	private sealed class WirecastSE : Nav
	{
		public WirecastSE() : base($"{nameof(Id.Wirecast)}", Id.Wirecast) { }
		public override string Index => "/Wirecast/Edit";
		public override string Text => "Wirecast Edit";
		public override string Icon => "fas fa-pencil-alt";
		public override string Role => "audiovisual";
		public override int Sort => 2;
	}

	private sealed class KeyDatesSE : Nav
	{
		public KeyDatesSE() : base($"{nameof(Id.KeyDates)}", Id.KeyDates) { }
		public override string Index => "/Admin/KeyDatesEdit";
		public override string Text => "Key Dates Edit";
		public override string Icon => "far fa-calendar-check";
		public override string Role => "keydates";
		public override int Sort => 3;
	}

	private sealed class SukkotSE : Nav
	{
		public SukkotSE() : base($"{nameof(Id.Sukkot)}", Id.Sukkot) { }
		public override string Index => Links.Sukkot.ManageRegistration.Index;
		public override string Text => Links.Sukkot.ManageRegistration.Title;
		public override string Icon => Links.Sukkot.ManageRegistration.Icon;
		public override string Role => "sukkot";  //AdminOrSukkotOrElder
		public override int Sort => 4;
	}

	private sealed class ContactSE : Nav
	{
		public ContactSE() : base($"{nameof(Id.Contact)}", Id.Contact) { }
		public override string Index => "/Contact";
		public override string Text => "Contact";
		public override string Icon => "fas fa-user-friends";
		public override string Role => "elder";
		public override int Sort => 5;
	}

	private sealed class SpecialEventsSE : Nav
	{
		public SpecialEventsSE() : base($"{nameof(Id.SpecialEvents)}", Id.SpecialEvents) { }
		public override string Index => "/SpecialEvents";
		public override string Text => "Special Events";
		public override string Icon => "far fa-clock";
		public override string Role => "admin";
		public override int Sort => 6;
	}

	private sealed class DatabaseErrorSE : Nav
	{
		public DatabaseErrorSE() : base($"{nameof(Id.DatabaseError)}", Id.DatabaseError) { }
		public override string Index => "/Database/ErrorLog";
		public override string Text => "Database Error Log";
		public override string Icon => "fas fa-bomb";
		public override string Role => "admin";
		public override int Sort => 7;
	}

	private sealed class ParashaCalendarSE : Nav
	{
		public ParashaCalendarSE() : base($"{nameof(Id.ParashaCalendar)}", Id.ParashaCalendar) { }
		public override string Index =>  "/ParashaCalendar"; //Links.ParashaCalendar.Index;
		public override string Text => "Parasha Calendar"; //public const string Title = "Parasha Calendar";
		public override string Icon => "fas fa-file-csv"; // far fa-file-excel 
		
		public override string Role => "admin";
		public override int Sort => 8;
	}

	private sealed class CalendarHealthCheckSE : Nav
	{
		public CalendarHealthCheckSE() : base($"{nameof(Id.CalendarHealthCheck)}", Id.CalendarHealthCheck) { }
		public override string Index => "/Calendar/HealthCheck"; 
		public override string Text => "Calendar Health Check"; 
		public override string Icon => "fas fa-heartbeat"; 

		public override string Role => "admin";
		public override int Sort => 9;
	}

	//

	private sealed class DashboardSE : Nav
	{
		public DashboardSE() : base($"{nameof(Id.Dashboard)}", Id.Dashboard) { }
		public override string Index => "/Admin/Dashboard"; 

		public override string Text => "Dashboard";

		public override string Icon => "fas fa-tachometer-alt"; 

		public override string Role => "admin";
		public override int Sort => 10;
	}

	
	#endregion
}
