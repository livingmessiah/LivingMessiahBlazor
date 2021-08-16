using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.ShabbatService
{
	public enum SectionEnum
	{
		CallToService = 1,
		OpeningAdoration = 2,
		PraiseAndWorship = 3,
		CommunityPrayer = 4,
		ChildrensBlessing = 5,
		OsehShalom = 6,
		Shema = 7,  // NOT SPLIT OUT
		Vahavta = 8,   // NOT SPLIT OUT
		Announcements = 9,
		// PsalmsAndProverbs Ported to Components\Pages\PsaPro\Index.razor
		EtzChaim = 10,
		//SiddurPrayer = 11,
		TorahParahsa = 12,
		Midrash = 13,  // No Graphic
		Avinu = 14,
		AhronicBlessing = 15,
		WineAndBread = 16,
		NewsFlash = 17,
		PsalmsAndProverbs = 18,
		ThankYou = 19,
	}

	public class Section
	{
		public static List<Section> All { get; } = new List<Section>();

		public static Section CallToService { get; } = new Section(
			SectionEnum.CallToService, "shofar-call-to-service-with-time-1024-385.jpeg", "Call to Service", "Toggle01", "Carousel01", "3:00 pm");

		public static Section OpeningAdoration { get; } = new Section(
			SectionEnum.OpeningAdoration, "blessed-be-he-who-spoke-1024-385.jpeg", "Opening Adoration", "Toggle02", "Carousel02", "3:03 pm");

		public static Section PraiseAndWorship { get; } = new Section(
			SectionEnum.PraiseAndWorship, "praise-and-worship-1024-385.jpeg", "Praise and Worship", "Toggle03", "Carousel03", "3:10 pm");

		public static Section CommunityPrayer { get; } = new Section(
			SectionEnum.CommunityPrayer, "community-prayer-1024-385.jpeg", "Community Prayer", "Toggle04", "Carousel04", "3:22 pm");

		public static Section ChildrensBlessing { get; } = new Section(
			SectionEnum.ChildrensBlessing, "childrens-blessing-1024-385.jpeg", "Children's Blessing", "Toggle05", "Carousel05", "3:34 pm");

		public static Section OsehShalom { get; } = new Section(
			SectionEnum.OsehShalom, "oseh-shalom-1024-385.jpeg", "Oseh Shalom", "Toggle06", "Carousel06", "3:36 pm");

		//7 shema-yisrael-brown-1024-385.jpeg shema-yisrael-pink-1024-385.jpeg
		//8 vahavta-1024-385.jpeg

		public static Section Announcements { get; } = new Section(
			SectionEnum.Announcements, "announcements-1024-385.jpeg", "Announcements", "Toggle09", "Carousel09", " 3:38 pm");

		public static Section EtzChaim { get; } = new Section(
			SectionEnum.EtzChaim, "etz-chaim-1024-385.jpeg", "Etz Chaim", "Toggle10", "Carousel10", "4:00 pm");

		//public static Section SiddurPrayer { get; } = new Section(
		//	SectionEnum.SiddurPrayer, "", "SiddurPrayer", "Toggle11", "Carousel11", "4:01 pm");

		public static Section TorahParahsa { get; } = new Section(
			SectionEnum.TorahParahsa, "torah-parahsa-green-1024-385.jpeg", "Torah Parahsa", "Toggle12", "Carousel12", "4:02 pm");  //torah-parahsa-purple-1024-385.jpeg

		//public static Section Midrash { get; } = new Section(
		//	SectionEnum.Midrash, "midrash-1024-385.jpeg", "Midrash", "Toggle13", "Carousel13", "4:03 pm"); 

		public static Section Avinu { get; } = new Section(
			SectionEnum.Avinu, "avinu-1024-385.jpeg", "Avinu Prayer", "Toggle14", "Carousel14", "4:10 pm");

		public static Section AhronicBlessing { get; } = new Section(
			SectionEnum.AhronicBlessing, "ahronic-blessing-1024-385.jpeg", "Aaronic Blessing", "Toggle15", "Carousel15", "5:02 pm");

		public static Section WineAndBread { get; } = new Section(
			SectionEnum.WineAndBread, "wine-and-bread-1024-385.jpeg", "Wine and Bread", "Toggle16", "Carousel16", "5:10 pm");

		public static Section NewsFlash { get; } = new Section(
			SectionEnum.NewsFlash, "news-flash-1039-398.jpeg", "News Flash", "Toggle17", "", "");

		public static Section PsalmsAndProverbs { get; } = new Section(
			SectionEnum.PsalmsAndProverbs, "psalms-and-proverbs-1024-385.jpeg", "Psalms and Proverbs", "Toggle18", "", "");

		public static Section ThankYou { get; } = new Section(
			SectionEnum.ThankYou, "thank-you-1024-385.jpeg", "Thank You!!!", "Toggle19", "", "");

		public SectionEnum SectionEnum { get; private set; }
		public string GraphicUrl { get; private set; }
		public string Title { get; private set; }
		public string ToggleId { get; private set; }
		public string CarouselId { get; private set; }
		public string Time { get; private set; }

		private Section(SectionEnum sectionEnum, string graphicUrl, string title, string toggleId, string carouselId, string time)
		{
			SectionEnum = sectionEnum;
			GraphicUrl = graphicUrl;
			Title = title;
			ToggleId = toggleId;
			CarouselId = carouselId;
			Time = time;
			All.Add(this);
		}

		public static Section FromEnum(SectionEnum enumValue)
		{
			return All.SingleOrDefault(r => r.SectionEnum == enumValue);
		}
	}
	//ToDo Create a Carousel.cs with list of pages

}
