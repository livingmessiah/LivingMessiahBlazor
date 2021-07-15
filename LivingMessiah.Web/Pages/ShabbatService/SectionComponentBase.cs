using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.ShabbatService
{
	public class SectionComponentBase : ComponentBase
	{
		[Parameter]
		public bool IsPrinterFriendly { get; set; }

		[Parameter]
		public SectionEnum SectionEnum { get; set; }

		[Parameter]
		public RenderFragment SubTitle { get; set; }

		[Parameter]
		public RenderFragment ChildContent { get; set; }

		[Parameter]
		public RenderFragment CurrentParashaButton { get; set; }

		protected string _collapse;

		protected Section sec;
		protected string HeaderIcon = "";
		protected string BadgeColor = "badge-warning";

		protected override void OnInitialized()
		{
			sec = Section.FromEnum(SectionEnum);
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
