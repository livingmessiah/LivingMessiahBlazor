using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class ButtonToggleVisibility
	{
		[Parameter]
		public RenderFragment ChildContent { get; set; }

		[Parameter]
		public string CardCss { get; set; } = "border-primary my-3";

		[Parameter]
		public string HeaderBadgeColor { get; set; } = "badge-warning";

		[Parameter]
		public string Title { get; set; }

		public bool IsCollapsed { get; set; } = true;

		protected void Collapsed_ButtonClick()
		{
			IsCollapsed = !IsCollapsed;
		}

	}
}
