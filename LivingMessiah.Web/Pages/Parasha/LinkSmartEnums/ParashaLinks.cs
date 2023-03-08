namespace LivingMessiah.Web.Pages.Parasha.LinkSmartEnums;

public static class ParashaLinks
{
	public const string Index = "/Parasha";
	public const string Title = "Parasha";
	public const string NavBarText = "Current Parasha";
	public const string Icon = "fas fa-torah";
	public const string IconCurrent = "far fa-bookmark";

	public const string IndexPrint = "/Parasha/IndexPrint";  // Deprecate
	public const string BackToButtonText = "Back to Parasha"; // Deprecate

	public static class MyHebrewBible
	{
		private const string baseUrl = "https://myhebrewbible.com/Parasha/Triennial/LivingMessiah/";
		public static string ParashaUrl(int id, string slug)
		{
			return $"{baseUrl}/{id}?slug={slug}/";
		}
	}

	public static class TorahTuesday
	{
		public const string Index = Links.TorahTuesday.Index;
		public const string Title = Links.TorahTuesday.Title;
		public const string Icon = Links.TorahTuesday.Icon;
	}
	public static class ListByBook
	{
		public const string Index = "/Parasha/ListByBook";
		public const string Title = "Current Parasha Table";
		public const string Icon = "fas fa-table";
	}

	public static class Archive
	{
		public const string Index = "/Parasha/Archive";
		public const string Title = "Parashot Archive";
		public const string Icon = "fas fa-archive";
	}

	public static class PrintTable
	{
		public const string Title = "Parasha Table (Print)";
		public const string Icon = "fas fa-print";
	}

}

