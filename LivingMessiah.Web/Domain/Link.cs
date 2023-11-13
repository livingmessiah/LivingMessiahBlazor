namespace LivingMessiah.Web.Domain;

public class Link
{
	public string?  Index { get; set; }
	public string?  Title { get; set; }
	public string?  Icon { get; set; }

	public bool SitemapUsage { get; set; }
	public bool HomeSidebarUsage { get; set; }
	public string?  HomeFloatRightHebrew { get; set; }
	public string?  HomeTitleSuffix { get; set; } // shaMayim H8064"

	/*
	In `LinksFactory!GetFeastLinks` `FeastDayValue` is populated with...

	- Features.Calendar.Enums.FeastDay.Passover.Value
	- Features.Calendar.Enums.FeastDay.Weeks.Value
	- Features.Calendar.Enums.FeastDay.Tabernacles.Value

	Called by `LinkService.GetHomeSidebarLinks`
		- if (SukkotSettings.Value.SukkotIsOpen); Passover and Weeks are not used

	*/
	public int FeastDayValue { get; set; }
	
	public int SortOrder { get; set; }

}
