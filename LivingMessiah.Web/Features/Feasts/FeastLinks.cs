namespace LivingMessiah.Web.Features.Feasts;

public static class FeastLinks
{
	public const string Index = "/feasts/";
	public const string Title = "Feasts";
	public const string Icon = "fas fa-pizza-slice"; // <i class="fas fa-drumstick-bite"></i> <i class="fas fa-pizza-slice"></i>
	public const string Descr = "Landing page for Feasts of YHVH";

	public static class Shabbat
	{
		public const string Page = "/Shabbat";
		public const string Title = "Shabbat";
		public const string Icon = "far fa-hand-spock";
	}

	public static class LunarMonth
	{
		public const string Index = "/LunarMonth";
		public const string Title = "Lunar Month";
		public const string Icon = "far fa-moon";
	}

	public static class Hanukkah
	{
		public const string Page = "/Hanukkah";
		public const string Title = "Hanukkah";
		public const string Icon = "fas fa-hanukiah";
	}

	public static class Purim
	{
		public const string Page = "/Purim";
		public const string Title = "Purim";
		public const string Icon = "far fa-square";
	}

	public static class Passover
	{
		public const string Page = "/Passover";
		public const string Title = "Passover";
		public const string Icon = "fas fa-door-open";
	}


	public static class Omer
	{
		public const string Page = "/omer";
		public const string Title = "Omer";
		public const string Icon = "far fa-calendar";
	}

	public static class Weeks
	{
		public const string Page = "/Weeks"; // There's a component called OmerCount, but no page;  Pages\Shavuot\OmerCount.razor
		public const string Title = "Weeks";
		public const string Icon = "fab fa-creative-commons-zero";
	}

	public static class Trumpets
	{
		public const string Page = "/Trumpets";
		public const string Title = "Trumpets";
		public const string Icon = "fas fa-bullhorn";
	}

	public static class YomKippur
	{
		public const string Page = "/YomKippur";
		public const string Title = "Yom Kippur";
		public const string Icon = "fas fa-hands-helping";
	}

	public static class Tabernacles
	{
		public const string Page = "/Tabernacles";
		public const string Title = "Tabernacles";
		public const string Icon = "fas fa-campground";
	}

}
