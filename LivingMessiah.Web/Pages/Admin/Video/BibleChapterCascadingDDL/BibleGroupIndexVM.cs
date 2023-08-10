using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.Admin.Video.BibleChapterCascadingDDL;

public class BibleGroupIndexVM
{
	[Required]
	public string? Name { get; set; }

	[Required(ErrorMessage = "Bible Group is Required")]
	public int BibleGroupId { get; set; }


	//public string? BookId { get; set; }
	//public List<SelectListItem>? Books { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "You must select a Book")]
	public int BookId { get; set; }

	//[Required(ErrorMessage = "Bible is Required")]
	//public int BookId { get; set; }
}
