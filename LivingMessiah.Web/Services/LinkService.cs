using LivingMessiah.Web.Data;
using LivingMessiah.Web.Domain;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using LivingMessiah.Web.Settings;

namespace LivingMessiah.Web.Services;

public interface ILinkService
{
	List<Link> GetSitemapLinks();
	List<Link> GetHomeSidebarLinks(bool isXsOrSm);
	//List<Link> GetFeastLinks();
	//List<LinkBasic> GetMarkdownLinks();
}

public class LinkService : ILinkService
{
	public IOptions<SukkotSettings> SukkotSettings { get; set; }
	public LinkService(IOptions<SukkotSettings> sukkotSettings)
	{
		SukkotSettings = sukkotSettings;
	}

	const int IntroductionAndWelcomeSortOder = 1;

	public List<Link> GetHomeSidebarLinks(bool isXsOrSm)
	{
		LinksFactory links = new LinksFactory();
		if (SukkotSettings.Value.SukkotIsOpen)
		{

			if (isXsOrSm)
			{
				return links.GetLinks()
					.Where(x => x.HomeSidebarUsage == true)
					.Union(links.GetFeastLinks().Where(z => z.FeastDayValue == Features.Calendar.Enums.FeastDay.Tabernacles))
					.OrderBy(x => x.SortOrder).ToList();
			}
			else
			{
				return links.GetLinks()
					.Where(x => x.HomeSidebarUsage == true & x.SortOrder != IntroductionAndWelcomeSortOder)
					.Union(links.GetFeastLinks().Where(z => z.FeastDayValue == Features.Calendar.Enums.FeastDay.Tabernacles))
					.OrderBy(x => x.SortOrder).ToList();
			}

		}
		else
		{
			if (isXsOrSm)
			{
				return links.GetLinks()
					.Where(x => x.HomeSidebarUsage == true).ToList();
			}
			else
			{
				return links.GetLinks().Where(x => x.HomeSidebarUsage == true & x.SortOrder != IntroductionAndWelcomeSortOder).ToList();
			}
			
		}

	}

	public List<Link> GetSitemapLinks()
	{
		LinksFactory links = new LinksFactory();
		return links.GetLinks().Where(x => x.SitemapUsage == true).ToList();
	}



	//public List<Link> GetFeastLinks()
	//{
	//	LinksFactory links = new LinksFactory();
	//	return links.GetFeastLinks().ToList();
	//}

	/*
	public List<LinkBasic> GetMarkdownLinks()
	{
		LinksFactory links = new LinksFactory();
		return links.GetMarkdownLinks().ToList();
	}
	*/
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
