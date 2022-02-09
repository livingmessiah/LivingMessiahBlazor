
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.OtherPages;

public partial class FurtherStudies
{
		public bool IsCollapsed { get; set; } = true;
		public int VerseId { get; set; }
		public string ButtonText { get; set; } = "Details";
		public string ButtonChevron { get; set; } = " fas fa-chevron-down";

		protected void ToggleVersePopupClick(bool isCollapsed, int verseId)
		{
				IsCollapsed = !isCollapsed;
				VerseId = verseId;
				ButtonText = IsCollapsed ? "Details" : "Hide";
				ButtonChevron = IsCollapsed ? "fas fa-chevron-down" : "fas fa-chevron-up";
		}
}
