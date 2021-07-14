using LivingMessiah.Web.Data;
using LivingMessiah.Web.Domain;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Services
{
	public interface ILinkService
	{
		List<Link> GetSitemapLinks();
		List<Link> GetHomeSidebarLinks();
		
		/*
		List<Link> GetFeastLinks();
		List<LinkBasic> GetAdminLinks();
		List<LinkBasic> GetDashboardLinks();
		List<LinkBasic> GetMarkdownLinks();
		*/
	}

	public class LinkService : ILinkService
	{
		public List<Link> GetHomeSidebarLinks()
		{
			LinksFactory links = new LinksFactory();
			return links.GetLinks().Where(x => x.HomeSidebarUsage == true).ToList();
		}

		public List<Link> GetSitemapLinks()
		{
			LinksFactory links = new LinksFactory();
			return links.GetLinks().Where(x => x.SitemapUsage == true).ToList();
		}

		/*
		public List<LinkBasic> GetAdminLinks()
		{
			LinksFactory links = new LinksFactory();
			return links.GetDashboardLinks().Union(links.GetMarkdownLinks()).ToList();
		}

		public List<LinkBasic> GetDashboardLinks()
		{
			LinksFactory links = new LinksFactory();
			return links.GetDashboardLinks().ToList();
		}

		public List<LinkBasic> GetMarkdownLinks()
		{
			LinksFactory links = new LinksFactory();
			return links.GetMarkdownLinks().ToList();
		}

		public List<Link> GetFeastLinks()
		{
			LinksFactory links = new LinksFactory();
			return links.GetFeastLinks().ToList();
		}
		*/
	}
}

/*
This process causes this Startup error 'No process is associated with this object.'
private readonly LinksFactory _links;
public LinkService(LinksFactory links)
{
	_links = links;
}
...
return _links.GetLinks().Where(x => x.HomeSidebarUsage == true).ToList();
...
return _links.GetLinks().Where(x => x.SitemapUsage == true).ToList();
*/



/*

Link GetExternalLink();
Link GetExternalButtonLink();


		Simple = 1,
		InternalBtnSm = 2,
		InternalHome = 3,
		External = 4,
		ExternalButton = 5,
		Location = 6,
		ScrollSpyHeader = 7,
		ScrollSpyDetail = 8
 */
