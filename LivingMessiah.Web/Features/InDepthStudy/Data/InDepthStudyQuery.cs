using System;

namespace LivingMessiah.Web.Features.InDepthStudy.Data;

public class InDepthStudyQuery
{
	public int Id { get; set; }
	public int ShabbatWeekId { get; set; }
	public DateTime ShabbatDate { get; set; }
	public string? YouTubeId { get; set; }
	public string? YouTubeUrl { get; set; }
	public string? Title { get; set; }
	public string? GraphicFile { get; set; }
	public string? NotesFile { get; set; }
	public string? BookChapterTitle { get; set; }
	public string? Chapter { get; set; }
	public string? BookTitle { get; set; }
	//public string? HebrewTitle { get; set; }
	//public string? HebrewName { get; set; }
	public string? BiblicalUrlReference { get; set; }
	public string? Category { get; set; }
	public string? SubCategory { get; set; }

	public override string ToString()
	{
		return $"Id: {Id}, ShabbatWeekId: {ShabbatWeekId}, YouTubeId: {YouTubeId ?? "NULL"}";
	}

	public DateTime EventDate()
	{
		return ShabbatDate;
	}
}

// 