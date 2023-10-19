namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes;

public static class Constants
{
	public static Enums.NotesFilter DefaultFilter = Enums.NotesFilter.Admin;
	public static bool DefaultShowDetailCard = false;

	public static class Effects
	{
		public const string ResponseMessageFailure = "An invalid operation occurred, contact your administrator";
	}

	public static class FluxorStores
	{
		public const string Index = "RegNotes_List";
	}
}

