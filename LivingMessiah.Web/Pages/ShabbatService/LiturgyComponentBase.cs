using LivingMessiah.Web.Services;
using LivingMessiah.RazorClassLibrary;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.ShabbatService
{
	public class LiturgyComponentBase : ComponentBase
	{
		[Parameter] public bool IsPrinterFriendly { get; set; }

		[Parameter]	public SectionEnum SectionEnum { get; set; }
		protected Section sec;

		[Inject]
		public ILiturgyService LiturgyService { get; set; }

		[Parameter]
		public RenderFragment ChildContent { get; set; }

		protected string _collapse;
		protected string BadgeColor = "badge-warning";
		protected List<ImageFile> AssetImages;

		protected override void OnInitialized()
		{
			sec = Section.FromEnum(SectionEnum);
			

			if (IsPrinterFriendly)
			{
				AssetImages = null;
				_collapse = "";
			}
			else
			{
				AssetImages = LiturgyService.GetImageBySectionEnum(SectionEnum);
				_collapse = " collapse";
			}
		}
	}
}
