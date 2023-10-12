using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Enums;

public abstract class NotesFilter : SmartEnum<NotesFilter>
{
	#region Id's
	private static class Id
	{
		internal const int Admin = 1;
		internal const int User = 2;
	}
	#endregion

	#region Declared Public Instances
	public static readonly NotesFilter Admin = new AdminSE();
	public static readonly NotesFilter User = new UserSE();
	// Note: SE=SmartEnum
	#endregion

	private NotesFilter(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string CssBgColor { get; }
	#endregion

	#region Private Instantiation
	private sealed class AdminSE : NotesFilter
	{
		public AdminSE() : base($"{nameof(Id.Admin)}", Id.Admin) { }
		public override string CssBgColor => "bg-warning"; 
	}

	private sealed class UserSE : NotesFilter
	{
		public UserSE() : base($"{nameof(Id.User)}", Id.User) { }
		public override string CssBgColor => "bg-info"; 
	}
	#endregion

}
