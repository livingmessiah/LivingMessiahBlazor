using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.UpcomingEvents.EditMarkdown;

public class EditMarkdownVM
{
		[Required]
		[Key]
		public int Id { get; set; }

		public string Title { get; set; }

		//[DataType(DataType.MultilineText)]
		//[Required(ErrorMessage = "A description is required")]
		//[MinLength(10, ErrorMessage = "Please enter at least 10 characters based on HTML.")]
		//[MaxLength(3000, ErrorMessage = "Please enter no more than at least 3,000... were not writing a novel.")]
		[DisplayName("Markdown Description")]
		public string Description { get; set; }
}
