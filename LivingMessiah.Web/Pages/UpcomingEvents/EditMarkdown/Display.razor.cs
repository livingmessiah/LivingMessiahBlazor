using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.UpcomingEvents.EditMarkdown
{
	public partial class Display
	{
		[Parameter]
		public int Id { get; set; }

		[Parameter]
		public string Title { get; set; }

		[Parameter]
		public string Description { get; set; }
	}
}
