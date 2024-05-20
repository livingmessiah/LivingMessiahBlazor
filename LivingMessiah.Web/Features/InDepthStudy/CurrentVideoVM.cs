using System;

namespace LivingMessiah.Web.Features.InDepthStudy;

public class CurrentVideoVM
{
	//public DateTime ShabbatDate { get; set; }
	// <td>@context.Date.ToString(DateFormat.ddd_mm_dd)</td>
	// model.GregorianDate = today.ToString(DateFormat.FeastDayPlanner);
	// public override string ParentDate => FeastDay.FromValue(ParentFeastDayId).Date.AddDays(AddDays).ToString(DateFormat.ddd_mm_dd);

	public string? Date { get; set; }
	public string? Title { get; set; }
	public string? YouTubeUrl { get; set; }

	public string? BookChapterLabel { get; set; }
	//public string? BookTitle { get; set; }	public string? Chapter { get; set; }

	public string? BiblicalUrlReference { get; set; }
	public string? GraphicFile { get; set; }
	//public string? IndepthInternalLink { get; set; }
	public string? Category { get; set; }
	public string? SubCategory { get; set; }
}
