namespace LivingMessiah.Web.Pages.Admin.AudioVisual.Components;

using System;
using System.ComponentModel.DataAnnotations;
using LivingMessiah.Web.SmartEnums;

public class WeeklyVideoCrudGridVM
{
	public int? Id { get; set; }

	[Required]
	public WeeklyVideoTypeEnum WeeklyVideoTypeEnum { get; set; }

	public int ShabbatWeekId { get; set; }

	public string ShabbatWeekIdString { get; set; }

	[Required]
	[Display(Name = "YouTube Id")]
	[StringLength(11, MinimumLength = 3, ErrorMessage = "length {0} must be between {2} and {1}.")]
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


	public string MHBUrl()
	{
		if (Book != 0 && Chapter != 0)
		{
			if (BibleBook.TryFromValue(Book, out var se))
			{
				return $"https://myhebrewbible.com/BookChapter/{se.Name}/{Chapter}/Slug";
			}
			else
			{
				return "";
			}
		}
		else
		{
			return "";
		}
	}

	[Required]
	[Display(Name = "YouTube Title")]
	[StringLength(75, MinimumLength = 3, ErrorMessage = "length of {0} must be between {2} and {1}")]
	public string Title { get; set; }

	/*
	//[Required]
	public string GraphicFileRoot { get; set; } // File given by Ralphie

	public string NotesFileRoot { get; set; }   // File given by Mark
	*/

	// If I'm updating MainServiceEnglish or MainServiceSpanish, this fields can't be required
	//[Range(1, 66, ErrorMessage = "length of {0} must be between {1} and {2}")]
	public int Book { get; set; }

	//[Range(1, 150, ErrorMessage = "length of {0} must be between {1} and {2}")]
	public int Chapter { get; set; }

	public override string ToString()
	{
		return $@"  Id: {Id}; WeeklyVideoTypeEnum: {WeeklyVideoTypeEnum}; ShabbatWeekId: {ShabbatWeekId}; YouTubeId: {YouTubeId}";
	}

}
