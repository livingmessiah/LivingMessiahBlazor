namespace LivingMessiah.Web.Domain;

LinkBasic:			class LinkBasic Index, Title, Icon  {should be can abstract class and called BaseLink}

Link : BaseLink 
	class 
		bool SitemapUsage, bool HomeSidebarUsage, string HomeFloatRightHebrew, string HomeTitleSuffix
		Pages.KeyDates.Enums, enum FeastDayEnum 
			{	Hanukkah = 1,	Purim = 2, Passover = 3,	Weeks = 4, Trumpets = 5,	YomKippur = 6, Tabernacles = 7, HanukkahEOY = 8}
    

		List<Link> GetLinks() is used by LinkService...
			- GetHomeSidebarLinks {}
				- If SukkotIsOpen
						return links.GetLinks()
							.Where(x => x.HomeSidebarUsage == true)
							.Union(links.GetFeastLinks().Where(z => z.FeastDay == LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Tabernacles)).ToList();
					Else 
						return new LinksFactory().GetLinks().Where(x => x.HomeSidebarUsage == true).ToList();
			
			- GetSitemapLinks
				return links.GetLinks().Where(x => x.SitemapUsage == true).ToList();

		List<Link> GetFeastLinks()  is used by LinkService...
			public List<Link> GetFeastLinks()
			{
					return LinksFactory.GetFeastLinks().ToList();
			}

		List<LinkBasic> GetDashboardLinks();  // NOT USED
		List<LinkBasic> GetVideoProductionLinks();
		List<LinkBasic> GetEldersLinks();

1 only used by DetailKeyDate.razor (Pages\UpcomingEvents\DetailKeyDate.razor)
2 LinkBasic Used by LinkFacotry, LinkService, Home!Admin.razor 
3 Link			Used by LinkFacotry, LinkService, Home!SidebarButtons.razor.cs, Home!Sitemap.razor.cs, Parasha!Index.razor.cs







public static class Feast
{
	public const string Index = "/feast/";
	public const string Title = "Feasts";
	public const string Icon = "fas fa-pizza-slice"; // <i class="fas fa-drumstick-bite"></i> <i class="fas fa-pizza-slice"></i>
	public const string Descr = "Landing page for Feasts of YHVH";

	public static class Hanukkah
	{
		public const string Page = "/feast/Hanukkah";
		public const string Title = "Hanukkah";
	}
}







public static class Shavuot
{
	public const LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum FeastDay = LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Weeks;
	public const string Index = "/Shavuot";
	public const string Title = "Shavuot";
	public const string Icon = "fab fa-creative-commons-zero";
}

public static class Sukkot
{
	public const LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum FeastDay = LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Tabernacles;
	public const string Index = "/Sukkot";
	public const string Title = "Sukkot";
	public const string Title2 = "Sukkot 2021";
	public const string Icon = "fas fa-campground";
	public const string RegistrationShell = "/Sukkot/RegistrationShell"; // See Startup.cs options.Conventions.AddPageRoute("/Sukkot/RegistrationShell", "/Sukkot/Registration");
}