using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Parasha
{
	public abstract class BaseButtonToggleVisibility : ComponentBase
	{
		[Parameter]
		public RenderFragment ChildContent { get; set; }

		[Parameter]
		public string CardCss { get; set; } = "border-primary my-3";

		[Parameter]
		public string HeaderBadgeColor { get; set; } = "badge-warning";

		[Parameter]
		public string Title { get; set; }

		public string ButtonText { get; set; } = "Details";
		public string ButtonChevron { get; set; } = " fas fa-chevron-down";

		public bool IsCollapsed { get; set; } = true;

		protected void ToggleButtonClick(bool isCollapsed)
		{
			IsCollapsed = !isCollapsed;
			ButtonText = IsCollapsed ? "Details" : "Hide";
			ButtonChevron = IsCollapsed ? "fas fa-chevron-down" : "fas fa-chevron-up";
		}

	}
}
