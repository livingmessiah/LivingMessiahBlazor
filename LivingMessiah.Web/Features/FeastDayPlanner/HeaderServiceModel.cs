namespace LivingMessiah.Web.Features.FeastDayPlanner;

public class HeaderServiceModel
{
	public string? BadgeColor { get; set; } // = "bg-warning-subtle";
	public int DaysDifferent { get; set; } // = 0;
	public string? DaysDifferentFormat { get; set; } // = "";
	public string? SuffixDescription { get; set; } // = "Passed, Present or Future?";
	public string? GregorianDate { get; set; }
	public string? HebrewDate { get; set; }

}
