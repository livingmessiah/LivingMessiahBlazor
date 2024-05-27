namespace LivingMessiah.Web.Features.Admin.Video.GraphicFileIsNull;

public class YouTubeAndFileVM
{
	// IX_WeeklyVideo_Unique ON dbo.WeeklyVideo ShabbatWeekId ASC,	WeeklyVideoTypeId ASC
	public int ShabbatWeekId { get; set; }
	public int WeeklyVideoTypeId { get; set; }
	
	public string? YouTubeId { get; set; }
	public string? GraphicFile { get; set; }
}
