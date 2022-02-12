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

