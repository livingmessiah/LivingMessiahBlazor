namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public class WeeklyVideoUpdate
{
	public int Id { get; set; }
	public int WeeklyVideoTypeId { get; set; }
	public int ShabbatWeekId { get; set; }
	public string? YouTubeId { get; set; }
	public string? Title { get; set; }
	public int Book { get; set; }
	public int Chapter { get; set; }

	//public string GraphicFileRoot { get; set; } // File given by Ralphie
	//public string NotesFileRoot { get; set; }   // File given by Mark
}
