using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.Admin.Video.BibleChapterCascadingDDL;

public class IndexVM
{
	[Required(ErrorMessage = "Bible is Required")]
	public int BookId { get; set; }
	//public string? BookId { get; set; }
	//public List<SelectListItem>? Books { get; set; }

	[Required(ErrorMessage = "Chapter is Required")]
	public int ChapterId { get; set; }
	//public string? ChapterId { get; set; }
	//public List<SelectListItem>? Chapters { get; set; }
}
