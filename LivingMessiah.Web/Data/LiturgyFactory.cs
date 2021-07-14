using System.Collections.Generic;
using LivingMessiah.RazorClassLibrary;
using LivingMessiah.Web;

namespace LivingMessiah.Web.Data
{
	//ToDo: Consider refactoring this so that it's part of or similar to 
	//	LivingMessiah.Web\Components\Pages\ShabbatService\Section.cs
	// Task 656: Refactor Liturgy Service by adding the list of images to Section.cs
	public static class LiturgyFactory
	{
		public static List<ImageFile> OpeningAdorationImages()
		{
			return new List<ImageFile>
			{
			new ImageFile() { Url = Blobs.UrlShabbatService("opening-adoration-1-780-615.jpg") }, 
			new ImageFile() { Url = Blobs.UrlShabbatService("opening-adoration-2-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("opening-adoration-3-780-615.jpg") },
			};
		}

		public static List<ImageFile> CommunityPrayerImages()
		{
			return new List<ImageFile>
			{
			new ImageFile() { Url = Blobs.UrlShabbatService("community-prayer-1-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("community-prayer-2-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("community-prayer-3-780-615.jpg") },
			};
		}

		public static List<ImageFile> ChildrensBlessingImages()
		{
			return new List<ImageFile>
			{
			new ImageFile() { Url = Blobs.UrlShabbatService("childrens-blessing-1-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("childrens-blessing-2-780-615.jpg") },
			};
		}

		public static List<ImageFile> OsehShalomImages()
		{
			return new List<ImageFile>
			{
			new ImageFile() { Url = Blobs.UrlShabbatService("oseh-shalom-1-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("oseh-shalom-2-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("oseh-shalom-3-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("oseh-shalom-4-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("oseh-shalom-5-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("oseh-shalom-6-780-615.jpg") },
			};
		}

		public static List<ImageFile> EtzChaimImages()
		{
			return new List<ImageFile>
			{
			new ImageFile() { Url = Blobs.UrlShabbatService("etz-chaim-1-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("etz-chaim-2-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("etz-chaim-3-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("etz-chaim-4-780-615.jpg") },
			};
		}

		public static List<ImageFile> AvinuImages()
		{
			return new List<ImageFile>
			{
			new ImageFile() { Url = Blobs.UrlShabbatService("avinu-1-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("avinu-2-780-615.jpg") },
			};
		}

		public static List<ImageFile> AharonicBlessingImages()
		{
			return new List<ImageFile>
			{
			new ImageFile() { Url = Blobs.UrlShabbatService("aharonic-blessing-1-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("aharonic-blessing-2-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("aharonic-blessing-3-780-615.jpg") },
			};
		}

		public static List<ImageFile> WineAndBreadImages()
		{
			return new List<ImageFile>
			{
			new ImageFile() { Url = Blobs.UrlShabbatService("wine-and-bread-1-780-615.jpg") },
			new ImageFile() { Url = Blobs.UrlShabbatService("wine-and-bread-2-780-615.jpg") },
			};
		}
		//SectionEnum.EtzChaim
		//SectionEnum.Avinu
		//SectionEnum.AhronicBlessing
		//SectionEnum.WineAndBread
		//SiddurPrayer = 11 ???
	}
}
