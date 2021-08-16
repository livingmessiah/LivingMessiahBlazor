using LivingMessiah.Web.Services;
using LivingMessiah.RazorClassLibrary;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.ShabbatService
{
	public partial class Liturgy : BaseSection
	{
		[Inject]
		public ILiturgyService LiturgyService { get; set; }

		protected List<ImageFile> AssetImages;

		public bool ShowCarousel { get; set; } = false;

		/*
		protected override void OnInitialized()
		{
			if (base.IsPrinterFriendly)
			{
				AssetImages = null;
			}
			else
			{
				AssetImages = LiturgyService.GetImageBySectionEnum(SectionEnum);
			}
		}
		*/
	}
}
