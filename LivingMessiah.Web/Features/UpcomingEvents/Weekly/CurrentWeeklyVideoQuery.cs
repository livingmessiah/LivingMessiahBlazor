using System;
using WVT_Enums = LivingMessiah.Web.Features.Admin.Video.Enums.WeeklyVideoType;

namespace LivingMessiah.Web.Features.UpcomingEvents.Weekly;

// public record WirecastQuery(int Id, DateTime ShabbatDate, string? WirecastLink);
public class CurrentWeeklyVideoQuery
{
	public int WeeklyVideoTypeId { get; set; }
	public DateTime ShabbatDate { get; set; }
	public string? YouTubeId { get; set; }
	public string? Title { get; set; }
	public string? BibleLinkLabel { get; set; } // could be parasha or book/chapter
	public string? BibleLinkUrl { get; set; }   // could be parasha or book/chapter

	public string? GraphicFile { get; set; }

	public string? NotesFile { get; set; }

	/*
	  public string? WvtDescr { get; set; }

			Used by: Card.razor 
			- Shared\YouTube\Card.razor
			- <span class="float-end">@CurrentWeeklyVideo!.WvtDescr</span>
	  
			Why not use this...
			- Web.Features.Admin.Video.Enums;  SmartEnum<WeeklyVideoType>
	*/


	/*


		Used by: Card.razor 
			if CurrentWeeklyVideo.WvtId > 2 {Not Eng/Sp Parasha}
				<a href="@CurrentWeeklyVideo.BiblicalUrlReference"
				<b>@CurrentWeeklyVideo.BookTitle 
					 @CurrentWeeklyVideo.Chapter</b> 
					 <i>@CurrentWeeklyVideo.BookChapterTitle</i>
	*/

	/*
		public override string ToString()
		{
			return $"Id: {Id}, WvtId: {WvtId}, ShabbatWeekId: {ShabbatWeekId}, YouTubeId: {YouTubeId ?? "NULL"}";
		}
	*/

	//public DateTime EventDate()
	//{
	//	if (WvtId == WVT_Enums.TorahTuesday.Value)
	//	{
	//		return ShabbatDate.AddDays(3);
	//	}
	//	else
	//	{
	//		return ShabbatDate;
	//	}
	//}
}
