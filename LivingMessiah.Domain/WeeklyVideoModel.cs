using System.ComponentModel.DataAnnotations;

// ToDo: does this belong here?
// Moved from LivingMessiah.Web.Pages.Admin.WeeklyVideos because 
//   LivingMessiah.Data\ShabbatWeekRepository.cs needed it
namespace LivingMessiah.Domain;

public class WeeklyVideoModel
{
		public int Id { get; set; }
		public int WeeklyVideoTypeId { get; set; }
		public int ShabbatWeekId { get; set; }

		[Required]
		[Display(Name = "YouTube Id")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "length {0} must be between {2} and {1}.")]
		public string? YouTubeId { get; set; }

		/*
Unique every week
- 1 MS Eng:  The Spiritual State of Faithlessness
- 2 MS Esp:  El Estado Espiritual De Infidelidad

More static as it derived from the Book/Chapter
- 3 Indepth: SubTitle="<strike>Ralphie</strike> We continues the study in [<b>John</b> (<i>Yochanan</i>]
- 4 ToTu:    SubTitle="<strike>Mark</strike> We continue the study in [<b>Exodus</b> (<i>Shemot</i>] 
		*/
		[Required]
		[Display(Name = "YouTube Title")]
		[StringLength(150, MinimumLength = 3, ErrorMessage = "length of {0} must be between {2} and {1}")]
		public string? Title { get; set; }

		//[Required]
		public string? GraphicFileRoot { get; set; } // File given by Ralphie

		public string? NotesFileRoot { get; set; }   // File given by Mark

		[Range(1, 66, ErrorMessage = "length of {0} must be between {1} and {2}")]
		public int Book { get; set; }

		[Range(1, 150, ErrorMessage = "length of {0} must be between {1} and {2}")]
		public int Chapter { get; set; }

		//For TypeId = 3 use...		
		//public BookChapter TorahBookChapter { get; set; }
		//For TypeId = 4 use...		
		//public BookChapter GospelBookChapter { get; set; }


		public override string ToString()
		{
				return $@"  Id: {Id}; Type Id: {WeeklyVideoTypeId}; ShabbatWeekId: {ShabbatWeekId}; YouTubeId: {YouTubeId}";
		}
}
