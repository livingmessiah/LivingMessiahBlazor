using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.SampleCode.BibleCascadingDDL;

public class IndexVM
{
	[Required(ErrorMessage = "Name is Required")]
	public string? Name { get; set; }

	[Required(ErrorMessage = "Bible Group is Required")]
	public int BibleGroupId { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "You must select a Book")]
	public int BookId { get; set; }
}

