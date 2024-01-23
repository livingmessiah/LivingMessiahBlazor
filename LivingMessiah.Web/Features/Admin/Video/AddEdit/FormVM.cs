namespace LivingMessiah.Web.Features.Admin.Video.AddEdit;

public class FormVM
{
	public int Id { get; set; }
	public int WeeklyVideoTypeId { get; set; }
	public int ShabbatWeekId { get; set; }
	public string? YouTubeId { get; set; }
	public string? Title { get; set; }
	public int Book { get; set; } //= 0;
	public int Chapter { get; set; } // = 0;
}
