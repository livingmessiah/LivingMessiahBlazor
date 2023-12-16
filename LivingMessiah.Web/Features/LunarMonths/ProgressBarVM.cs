namespace LivingMessiah.Web.Features.LunarMonths;

public class ProgressBarVM
{
	public string? BadgeColor { get; set; } // = "bg-warning-subtle";
	public int DaysDifferent { get; set; } // = 0;
	public string? DaysDifferentFormat { get; set; } // = "";
	public string? SuffixDescription { get; set; } // = "Passed, Present or Future?";
	public string? GregorianDate { get; set; }
	public string? HebrewDate { get; set; }
	public int PercentUntilNewMoon { get; set; }
	public int DaysOld { get; set; }
	
}
