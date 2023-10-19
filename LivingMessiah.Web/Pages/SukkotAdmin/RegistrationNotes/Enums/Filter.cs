using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Enums;

// ToDo Rename as Filter
public abstract class Filter : SmartEnum<Filter>
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
	public static readonly Filter All = new AllSE();
	public static readonly Filter Admin = new AdminSE();
	public static readonly Filter User = new UserSE();
	// Note: SE=SmartEnum
	#endregion

	private Filter(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string CssBgColor { get; }
	public abstract string CssTextColor { get; }
	public abstract string SqlWhere { get; }
	public abstract string SqlOrder { get; }
	#endregion

	#region Private Instantiation
	private sealed class AllSE : Filter
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
		public override string SqlOrder => " ORDER BY FirstName";
	}
	
	private sealed class AdminSE : Filter
	{
		public AdminSE() : base($"{nameof(Id.Admin)}", Id.Admin) { }
		public override string CssBgColor => "bg-warning";
		public override string CssTextColor => "btn-outline-warning";
		public override string SqlWhere => " WHERE AdminNotes IS NOT NULL AND TRIM(AdminNotes) <> ''";
		public override string SqlOrder => " ORDER BY FirstName";
	}

	private sealed class UserSE : Filter
	{
		public UserSE() : base($"{nameof(Id.User)}", Id.User) { }
		public override string CssBgColor => "bg-info";
		public override string CssTextColor => "btn-outline-primary";
		public override string SqlWhere => " WHERE Notes IS NOT NULL AND TRIM(Notes) <> ''";
		public override string SqlOrder => " ORDER BY FirstName";
	}
	#endregion

}
// Ignore Spelling: Css