using LivingMessiah.Web.Data;
using LivingMessiah.Web.Components.Pages.ShabbatService;
using LivingMessiah.RazorClassLibrary;
using System;
using System.Collections.Generic;

namespace LivingMessiah.Web.Services
{
	public interface ILiturgyService
	{
		List<ImageFile> GetImageBySectionEnum(SectionEnum sectionEnum);
	}

	public class LiturgyService : ILiturgyService
	{
		public List<ImageFile> GetImageBySectionEnum(SectionEnum sectionEnum)
		{

			switch (sectionEnum)
			{
				case SectionEnum.CallToService:
					break;

				case SectionEnum.OpeningAdoration:
					return LiturgyFactory.OpeningAdorationImages();

				case SectionEnum.PraiseAndWorship:
					break;

				case SectionEnum.CommunityPrayer:
					return LiturgyFactory.CommunityPrayerImages();

				case SectionEnum.ChildrensBlessing:
					return LiturgyFactory.ChildrensBlessingImages();

				case SectionEnum.OsehShalom:
					return LiturgyFactory.OsehShalomImages();

				case SectionEnum.Shema:
					break;

				case SectionEnum.Vahavta:
					break;

				case SectionEnum.Announcements:
					break;

				case SectionEnum.EtzChaim:
					return LiturgyFactory.EtzChaimImages();

				case SectionEnum.TorahParahsa:
					break;

				case SectionEnum.Midrash:
					break;

				case SectionEnum.Avinu:
					return LiturgyFactory.AvinuImages();

				case SectionEnum.AhronicBlessing:
					return LiturgyFactory.AharonicBlessingImages();

				case SectionEnum.WineAndBread:
					return LiturgyFactory.WineAndBreadImages();

				case SectionEnum.NewsFlash:
					break;

				case SectionEnum.PsalmsAndProverbs:
					break;

				default:
					break;
			}
			throw new NotImplementedException();
		}

	}
}
