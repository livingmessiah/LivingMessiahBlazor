using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Home
{
	public partial class IntroductionAndWelcome
	{
		[Parameter]
		public MediaQueryEnum MediaQueryEnum { get; set; }

		public bool IsCollapsed { get; set; } = true;
		//protected string collapsed = " collapse "; // => _collapsed ? "collapse" : null;

		protected void Collapsed_ButtonClick()
		{
			IsCollapsed = !IsCollapsed;
		}
	}


}
