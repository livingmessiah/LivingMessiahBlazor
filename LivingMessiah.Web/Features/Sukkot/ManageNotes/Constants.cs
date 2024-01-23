namespace LivingMessiah.Web.Features.Sukkot.ManageNotes;

public static class Constants
{
	public static Enums.Filter DefaultFilter = Enums.Filter.Admin;
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

