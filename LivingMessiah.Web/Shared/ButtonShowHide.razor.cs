using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Shared
{
	public partial class ButtonShowHide
	{
		[Parameter]
		public RenderFragment ChildContent { get; set; }

		[Parameter]
		public string CardCss { get; set; } = "border-primary my-3";

		[Parameter]
		public string HeaderBadgeColor { get; set; } = "badge-warning";

		[Parameter]
		public string Title { get; set; }

		public string ButtonText { get; set; } = "Details ⬇️";
		// NO WORKY public string ButtonChevron { get; set; } = " fas fa-chevron-down";

		public bool IsCollapsed { get; set; } = true;

		protected void ToggleButtonClick(bool isCollapsed)
		{
			IsCollapsed = !isCollapsed;
			ButtonText = IsCollapsed ? "Details ⬇️" : "Hide ⬆️";
			
			// NO WORKY
			//ButtonText = IsCollapsed ? "Details" : "Hide";
			//ButtonChevron = IsCollapsed ? "fas fa-chevron-down" : "fas fa-chevron-up";
		}

	}
}
