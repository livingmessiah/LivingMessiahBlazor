using Microsoft.AspNetCore.Components;
//using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Shared
{
	public partial class PageHeader
	{
		//[Parameter] public LinkLocal LinkLocal { get; set; }

		[Parameter]
		public string Title { get; set; }

		[Parameter]
		public string Icon { get; set; }

		[Parameter]
		public bool AddBreak { get; set; } = false;

		[Parameter]
		public bool AddBorderBottom { get; set; } = true;

	}
}
