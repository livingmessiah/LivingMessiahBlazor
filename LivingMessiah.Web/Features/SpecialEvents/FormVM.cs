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

	// ToDo: Warning	CS8618	Non-nullable property 'Description' et. al. must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
	/*
	public FormVM()
	{
		SpecialEventTypeId = SpecialEventType.Other.Value;
		EventDate = DateTime.Now.AddDays(35);
		ShowBeginDate = DateTime.Now.AddMonths(1);
		ShowEndDate = DateTime.Now.AddDays(40);
	}
	*/
}

