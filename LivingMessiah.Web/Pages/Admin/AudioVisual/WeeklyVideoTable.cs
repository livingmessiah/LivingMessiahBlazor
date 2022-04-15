using System;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public class WeeklyVideoTable
{
	public int Id { get; set; }
	public int ShabbatWeekId { get; set; }
	public DateTime ShabbatDate { get; set; }
	public int WeeklyVideoTypeId { get; set; }
	public string WeeklyVideoTypeDescr { get; set; }
	public string YouTubeId { get; set; }
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
	public string Title { get; set; }
}

