using System;

namespace LivingMessiah.Web.Features.Admin.Video.GraphicFileIsNull;

public class GraphicFileIsNullVM
{
	// Unique Key
	public int ShabbatWeekId { get; set; }
	public int WeeklyVideoTypeId { get; set; } // 1==Eng. Parasha
	
	public DateTime ShabbatDate { get; set; }
	public string? YouTubeId { get; set; }
	public string? Torah { get; set; }

	public string MaxResDefaultImgSrc
	{
		get
		{
			return !String.IsNullOrEmpty(YouTubeId) ? $"http://img.youtube.com/vi/{YouTubeId}/{"maxresdefault.jpg"}" : "";
		}
	}
	
	public string ThumbnailImgSrc
	{
		get
		{
			return !String.IsNullOrEmpty(YouTubeId) ? $"http://img.youtube.com/vi/{YouTubeId}/{"default.jpg"}" : "";
		}
	}

	public string TorahFileName
	{
		get
		{
			return !String.IsNullOrEmpty(Torah) 
			? Torah!.Replace(" ", "-").Replace(":", "-") 
			: "";
		}
	}

	public string ShabbatWeekIdFileName
	{
		get
		{
			return ShabbatWeekId.ToString().PadLeft(3,'0');
		}
	}

	/*
 Deu 11:26-12:19 
 012_2021-12-18_S3wUFTd-TkM_Gen-12-and-13 
 010_2021-12-04_Uez59PFdyeQ_Gen-9-18-10-32
 */
	public string BlobFileName
	{
		get
		{
			return $"{ShabbatWeekIdFileName}_{ShabbatDate.ToString(DateFormat.YYYY_MM_DD)}_{YouTubeId!}_{TorahFileName}.jpg";
		}
	}


}

// Ignore Spelling: Img