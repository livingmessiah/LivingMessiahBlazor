using System;
using System.ComponentModel.DataAnnotations;
using LivingMessiah.Web.Pages.UpcomingEvents.Enums;

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin.Edit;
public class EditVM
{
	[Required]
	[Key]
	public int Id { get; set; }

	[Required(ErrorMessage = "The Start Date field is required.")]
	public DateTime? ShowBeginDate { get; set; }

	[Required(ErrorMessage = "The End Date field is required.")]
	public DateTime? ShowEndDate { get; set; }

	[Required]
	public DateTime EventDate { get; set; }

	public int SpecialEventTypeId { get; set; }

	public SpecialEventType? SpecialEventType { get; set; }

	[Required]
	public string? Title { get; set; }  // NOT NULL

	public string? SubTitle { get; set; }

	//[DataType(DataType.Upload)]

	//[DataType(DataType.ImageUrl)]
	public string? ImageUrl { get; set; }

	public string? YouTubeId { get; set; }

	[DataType(DataType.Url)]
	public string? WebsiteUrl { get; set; }

	public string? WebsiteDescr { get; set; }
	public string? Description { get; set; }  // ToDo: md?, probably going to be Component Body
}
