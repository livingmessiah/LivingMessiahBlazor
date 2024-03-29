﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using LivingMessiah.Web.Settings;

namespace LivingMessiah.Web.Links;

public interface ILinkService
{
	List<Link> GetSitemapLinks();
	List<Link> GetHomeSidebarLinks(bool isXsOrSm);
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

}

