using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.Home.Enums;

// - LivingMessiah.Web\Services\Auth0.cs
//		public override BibleGroup BibleGroup => BibleGroup.Torah;

public abstract class Roles : SmartFlagEnum<Roles>
{
	#region Id's
	private static class Id
	{
		internal const int All = -1;  // admin
		internal const int None = 0;  // user
		internal const int ManageRegistration = 1;
		internal const int Sukkot = 2;
		internal const int KeyDates = 4;
		internal const int Elder = 8;
		internal const int AudioVisual = 16;
		internal const int Announcements = 32;
		internal const int Admin = 64;

		/*
		AdminOrElder = "admin, elder";
		AdminOrSukkot = "admin, sukkot";
		AdminOrAnnouncements = "admin, announcements";
		AdminOrSukkotOrElder = "admin, sukkot, elder";
		AdminOrAudiovisual = "admin, audiovisual";
		AdminOrKeyDates = "admin, keydates";
		SukkotMenuBar = Elder + "," + Admin + "," + ManageRegistration + "," + Sukkot;
		*/
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Roles All = new AllSE();
	public static readonly Roles None = new NoneSE();
	public static readonly Roles ManageRegistration = new ManageRegistrationSE();
	public static readonly Roles Sukkot = new SukkotSE();
	public static readonly Roles KeyDates = new KeyDatesSE();
	public static readonly Roles Elder = new ElderSE();
	public static readonly Roles AudioVisual = new AudioVisualSE();
	public static readonly Roles Announcements = new AnnouncementsSE();
	public static readonly Roles Admin = new AdminSE();

	//public static readonly Roles SupperUserAndSukkot = new Roles(nameof(SupperUserAndSukkot), 3); // Explicit `Combination` value
	/*
	public static readonly SmartFlagTestEnum CardAndCash = new SmartFlagTestEnum(nameof(CardAndCash), 3);
	*/

	// SE=SmartEnum
	#endregion

	private Roles(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	//public abstract string Title { get; }
	#endregion

	#region Private Instantiation
	private sealed class AllSE : Roles
	{
		public AllSE() : base($"{nameof(Id.All)}", Id.All) { }
		//public override string Title => "All";
	}

	private sealed class NoneSE : Roles
	{
		public NoneSE() : base($"{nameof(Id.None)}", Id.None) { }
	}

	private sealed class ManageRegistrationSE : Roles
	{
		public ManageRegistrationSE() : base($"{nameof(Id.ManageRegistration)}", Id.ManageRegistration) { }
	}

	private sealed class SukkotSE : Roles
	{
		public SukkotSE() : base($"{nameof(Id.Sukkot)}", Id.Sukkot) { }
	}

	private sealed class KeyDatesSE : Roles
	{
		public KeyDatesSE() : base($"{nameof(Id.KeyDates)}", Id.KeyDates) { }
	}

	private sealed class ElderSE : Roles
	{
		public ElderSE() : base($"{nameof(Id.Elder)}", Id.Elder) { }
	}

	private sealed class AudioVisualSE : Roles
	{
		public AudioVisualSE() : base($"{nameof(Id.AudioVisual)}", Id.AudioVisual) { }
	}

	private sealed class AnnouncementsSE : Roles
	{
		public AnnouncementsSE() : base($"{nameof(Id.Announcements)}", Id.Announcements) { }
	}

	private sealed class AdminSE : Roles
	{
		public AdminSE() : base($"{nameof(Id.Admin)}", Id.Admin) { }
	}

	#endregion
}

