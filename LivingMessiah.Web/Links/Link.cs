﻿namespace LivingMessiah.Web.Links;

public class Link
{
	public string?  Index { get; set; }
	public string?  Title { get; set; }
	public string?  Icon { get; set; }

	public bool SitemapUsage { get; set; }
	public bool HomeSidebarUsage { get; set; }
	public string?  HomeFloatRightHebrew { get; set; }
	public string?  HomeTitleSuffix { get; set; } // shaMayim H8064"
	public int FeastDayValue { get; set; }
	
	public int SortOrder { get; set; }

}
