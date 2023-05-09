namespace LivingMessiah.Web.Pages.Admin.AudioVisual.Services;
using System;

public class YouTubeFeedModel
{
	public int? Id { get; set; }
	public string? YouTubeId { get; set; }
	public string? Title { get; set; }
	public DateTimeOffset PublishDate { get; set; }
}
