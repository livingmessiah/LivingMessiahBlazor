using LivingMessiah.Web.Features.SpecialEvents.Enums;
using System;

namespace LivingMessiah.Web.Features.SpecialEvents;

public class FormVM
{
	public int Id { get; set; }										// [Required]
	public DateTime? ShowBeginDate { get; set; }	// [Required]
	public DateTime? ShowEndDate { get; set; }		// [Required]
	public DateTime EventDate { get; set; }				// [Required]
	public int SpecialEventTypeId { get; set; }   // [Required]
	public string? Title { get; set; }  
	public string? SubTitle { get; set; }
	public string? ImageUrl { get; set; }
	public string? YouTubeId { get; set; }
	public string? WebsiteUrl { get; set; }   // [DataType(DataType.Url)]
	public string? WebsiteDescr { get; set; }
	public string? Description { get; set; }  // ToDo: md?, probably going to be Component Body

	public bool ShowSecondRow()
	{
		/*
		Right now I've decided to ignore if YouTubeId, SubTitle etc.
		For know, this can be put in the SfRichTextEditor control
		*/
		return false;
	}

}

