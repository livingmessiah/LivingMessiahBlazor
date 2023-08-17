using System;

namespace LivingMessiah.Web.Pages.ArchivedVideos;

public class WeeklyVideoIndex
{
	public int TypeId { get; set; }
	public string? Descr { get; set; }
	public int ShabbatWeekId { get; set; }
	public int RowNum { get; set; }
	public DateTime ShabbatDate { get; set; }
	public int? WeeklyVideoId { get; set; }
	public string? YouTubeId { get; set; }
	public string? Title { get; set; }
	public string? GraphicFile { get; set; }
	public string? NotesFile { get; set; }
	public int Book { get; set; }
	public int Chapter { get; set; }

	public override string ToString()
	{
		return $"Type Id: {TypeId}, ShabbatWeekId: {ShabbatWeekId}, YouTubeId: {YouTubeId}"; //, WeeklyVideoType: {WeeklyVideoType}
	}

	public string Url()
	{
		if (YouTubeId != null)
		{
			return $"https://www.youtube.com/watch?v={YouTubeId}";
		}
		else
		{
			return "";
		}
	}


}
