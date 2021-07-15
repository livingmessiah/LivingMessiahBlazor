using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Shared
{
	public class ThankYouBase : ComponentBase
	{
		[Parameter]
		public bool IsPrinterFriendly { get; set; }
		protected string _collapse;

		protected override void OnInitialized()
		{

			if (IsPrinterFriendly)
			{
				_collapse = "";
			}
			else
			{
				_collapse = " collapse";
			}
		}
	}
}
