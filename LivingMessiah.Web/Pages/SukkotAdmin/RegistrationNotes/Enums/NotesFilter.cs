using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Enums;

public abstract class NotesFilter : SmartEnum<NotesFilter>
{
	#region Id's
	private static class Id
	{
		internal const int All = -1;  
		// 0=None
		internal const int Admin = 1;
		internal const int User = 2;
	}
	#endregion

	#region Declared Public Instances
	public static readonly NotesFilter All = new AllSE();
	public static readonly NotesFilter Admin = new AdminSE();
	public static readonly NotesFilter User = new UserSE();
	// Note: SE=SmartEnum
	#endregion

	private NotesFilter(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string CssBgColor { get; }
	public abstract string CssTextColor { get; }
	public abstract string SqlWhere { get; }
	public abstract string SqlOrder { get; }
	#endregion

	#region Private Instantiation
	private sealed class AllSE : NotesFilter
	{
		public AllSE() : base($"{nameof(Id.All)}", Id.All) { }
		public override string CssBgColor => "bg-warning";
		public override string CssTextColor => "btn-outline-success";
		public override string SqlWhere => @"
WHERE
	(AdminNotes IS NOT NULL OR TRIM(AdminNotes) <> '')
OR
	(Notes IS NOT NULL AND TRIM(Notes) <> '')
";
		public override string SqlOrder => " ORDER BY FamilyName";
	}
	
	private sealed class AdminSE : NotesFilter
	{
		public AdminSE() : base($"{nameof(Id.Admin)}", Id.Admin) { }
		public override string CssBgColor => "bg-warning";
		public override string CssTextColor => "btn-outline-warning";
		public override string SqlWhere => " WHERE AdminNotes IS NOT NULL AND TRIM(AdminNotes) <> ''";
		public override string SqlOrder => " ORDER BY FamilyName";
	}

	private sealed class UserSE : NotesFilter
	{
		public UserSE() : base($"{nameof(Id.User)}", Id.User) { }
		public override string CssBgColor => "bg-info";
		public override string CssTextColor => "btn-outline-primary";
		public override string SqlWhere => " WHERE Notes IS NOT NULL AND TRIM(Notes) <> ''";
		public override string SqlOrder => " ORDER BY FamilyName";
	}
	#endregion

}
// Ignore Spelling: Css