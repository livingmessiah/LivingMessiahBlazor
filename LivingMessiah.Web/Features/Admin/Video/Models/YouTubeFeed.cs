using System;

namespace LivingMessiah.Web.Features.Admin.Video.Models;

public class YouTubeFeed
{
	public int? Id { get; set; }
	public string? YouTubeId { get; set; }
	public string? Title { get; set; }
	public DateTimeOffset PublishDate { get; set; }
	public int Id_Zero_If_Null => Id ?? 0;
	public bool IsAddMode => Id is null ? true : false;
}
