using System.Collections.Generic;
using LivingMessiah.Web.Domain;
using LivingMessiah.Web.Links;

using PageFeast = LivingMessiah.Web.Pages.Feasts.LinkSmartEnums.FeastLinks;
using PageParasha = LivingMessiah.Web.Pages.Parasha.LinkSmartEnums.ParashaLinks;
using PageWindmillRanch = LivingMessiah.Web.Pages.WindmillRanch.LinkSmartEnums.WindmillRanchLinks;

namespace LivingMessiah.Web.Data;

public interface ILinksFactory
{
	List<Link> GetLinks();
	List<Link> GetFeastLinks();
	List<LinkBasic> GetDashboardLinks();
	List<LinkBasic> GetVideoProductionLinks();
	List<LinkBasic> GetEldersLinks();
	//List<LinkBasic> GetMarkdownLinks();
}

public class LinksFactory : ILinksFactory
{

	public List<LinkBasic> GetVideoProductionLinks()
	{
		return new List<LinkBasic>
			{
				new LinkBasic {Index = Admin.Video.Index, Title = Admin.Video.Title, Icon = Admin.Video.Icon, },
				new LinkBasic {Index = Database.Error.Log, Title = Database.Error.Title, Icon = Database.Error.Icon, },
				new LinkBasic {Index = WeeklyVideos.Index, Title = WeeklyVideos.Title, Icon = WeeklyVideos.Icon, },
				new LinkBasic {Index = Admin.AudioVisual.Add.Index, Title = Admin.AudioVisual.Add.Title, Icon = Admin.AudioVisual.Add.Icon, },
				new LinkBasic {Index = KeyDatesEdit.Index, Title = KeyDatesEdit.Title, Icon = KeyDatesEdit.Icon, }
 			};
	}

	public List<LinkBasic> GetEldersLinks()
	{
		return new List<LinkBasic>
			{
				new LinkBasic {Index = PsalmsAndProverbs.Index, Title = PsalmsAndProverbs.Title, Icon = PsalmsAndProverbs.Icon, },
				new LinkBasic {Index = Contact.Index, Title = Contact.Title, Icon = Contact.Icon, },
				new LinkBasic {Index = Sukkot.SuperUser.Index, Title = Sukkot.SuperUser.Title, Icon = Sukkot.SuperUser.Icon, },
				new LinkBasic {Index = SpecialEvents.Index, Title = SpecialEvents.Title, Icon = SpecialEvents.Icon, },
 			};  
	}

	public List<LinkBasic> GetDashboardLinks()
	{
		return new List<LinkBasic>
			{
				new LinkBasic {Index = "/Admin/Dashboard/Index", Title = "Dashboard", Icon = "fas fa-tachometer-alt", },
				new LinkBasic {Index = "/Admin/Dashboard/Dump", Title = "Dump", Icon = "fas fa-truck-monster", },
				new LinkBasic {Index = "/Admin/Dashboard/FontAwesome", Title = "FontAwesome", Icon = "fab fa-font-awesome-flag", },
				new LinkBasic {Index = "/Admin/Dashboard/PerformanceCompliance", Title = "Performance Compliance", Icon = "fas fa-rocket", },
				new LinkBasic {Index = "/Admin/Dashboard/Routes", Title = "Routes", Icon = "fas fa-route", },
				new LinkBasic {Index = "/Admin/Dashboard/ThrowException", Title = "Throw Exception", Icon = "fas fa-bomb", },
 			};
	}

	public List<Link> GetLinks()
	{
		return new List<Link>
			{
				new Link
				{
					Index = IntroductionAndWelcome.Index,
					Title = IntroductionAndWelcome.Title,
					Icon = IntroductionAndWelcome.Icon,
					HomeSidebarUsage=true,  // if XsOrSm == true 
					HomeFloatRightHebrew = "שָׁלוֹם",
					HomeTitleSuffix = " Shalom  H7695",
					SortOrder=1,  // Services!LinkService; const int IntroductionAndWelcomeSortOder=1; include only if isXsOrSm=true
					SitemapUsage=true
				},
				new Link
				{
					Index = Calendar.Index,
					Title = Calendar.Title,
					Icon = Calendar.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="חֳדָשִׁים",
					HomeTitleSuffix=" chôdeshim H2320",
					SortOrder=2,
					SitemapUsage=true
				},
				new Link
				{
					Index = PageWindmillRanch.Index,
					Title = PageWindmillRanch.Title,
					Icon = PageWindmillRanch.Icon,
					HomeSidebarUsage=true,
					SortOrder=3,
					SitemapUsage=true
				},
				new Link
				{
					Index = ShabbatService.Index,
					Title = ShabbatService.Title,
					Icon = ShabbatService.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="שַׁבָּת",
					HomeTitleSuffix=" Shabbat H7676",
					SortOrder=4,
					SitemapUsage=true
				},

				new Link
				{
					Index = UpcomingEvents.Index,
					Title = UpcomingEvents.Title,
					Icon = UpcomingEvents.Icon,
					HomeSidebarUsage=true,
					//Commented out not because it's wrong, but it's too wide
					//HomeFloatRightHebrew="שׁוֹפָר",
					//HomeTitleSuffix=" Shofar H7782",
					SortOrder=5,
					SitemapUsage=true
				},

				new Link
				{
					Index = HeavensDeclare.Index,
					Title = HeavensDeclare.Title,
					Icon = HeavensDeclare.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="שָּׁמַיִם",
					HomeTitleSuffix=" shaMayim H8064",
					SortOrder=6,
					SitemapUsage=true
				},

				new Link
				{
					Index = Podcast.Index,
					Title = Podcast.Title,
					Icon = Podcast.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="דָּבַר",
					HomeTitleSuffix=" Dabar H1696",
					SortOrder=7,
					SitemapUsage=true
				},
				new Link
				{
					Index = TorahTuesday.Index,
					Title = TorahTuesday.Title,
					Icon = TorahTuesday.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="תּוֹרָה",
					HomeTitleSuffix=" Torah H8451",
					SortOrder=8,
					SitemapUsage=true
				},
				new Link
				{
					Index = IndepthStudy.Index,
					Title = IndepthStudy.Title,
					Icon = IndepthStudy.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="תְּהוֹם",
					HomeTitleSuffix=" Tehom H8415",
					SortOrder=9,
					SitemapUsage=true
					// Location  Index = "/Location";  Title = "Map"; "fas fa-map-signs";
				},
				new Link
				{
					Index = About.Index,
					Title = About.Title,
					Icon = About.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="אודות",  // חָשַׁב
					HomeTitleSuffix=" Odot H182",  // chashav H2803
					SortOrder=10,
					SitemapUsage=true
				},
				new Link
				{
					Index = PageParasha.Index,
					Title = PageParasha.Title,
					Icon = PageParasha.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="פָּרָשַׁת",
					HomeTitleSuffix=" Parashat H6567",
					SortOrder=11,
					SitemapUsage=true
				},
				new Link
				{
					Index = PageParasha.Archive.Index,
					Title = PageParasha.Archive.Title,
					Icon = PageParasha.Archive.Icon,
					HomeSidebarUsage=false,
					HomeFloatRightHebrew="",
					HomeTitleSuffix="",
					SortOrder=12,
					SitemapUsage=true
				},
				new Link
				{
					Index = Leadership.Index,
					Title = Leadership.Title,
					Icon = Leadership.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="זָקֵן",
					HomeTitleSuffix=" zaken H2205",
					SortOrder=13,
					SitemapUsage=true
				},

				new Link
				{
					Index = PayPal.Donate.Index,
					Title = PayPal.Donate.Title,
					Icon = PayPal.Donate.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="מוֹעֲדָי",
					HomeTitleSuffix=" tzedakah H6666",
					SortOrder=14,
					SitemapUsage=true
				},
				new Link
				{
					Index = BibleSearch.Index,
					Title = BibleSearch.Title,
					Icon = BibleSearch.Icon,
					HomeSidebarUsage=false,
					HomeFloatRightHebrew="בָּקַר",
					HomeTitleSuffix=" bāqar H1239",
					SortOrder=15,
					SitemapUsage=true
				},
				new Link
				{
					Index = Location.Index,
					Title = Location.Title,
					Icon = Location.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="דֶּרֶךְ",
					HomeTitleSuffix=" derek H1870",
					SortOrder=16,
					SitemapUsage=true
				},
				new Link
				{
					Index = Sitemap.Index,
					Title = Sitemap.Title,
					Icon = Sitemap.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="נָהַל",
					HomeTitleSuffix=" nahal H5095",
					SortOrder=17,
					SitemapUsage=false // Don't show sitemap link on sitemap page
				},
				new Link
				{
					//Index = Store.Index, Title = Store.Title, Icon = Store.Icon,
					Index = Links.Store.Index, Title = Links.Store.Title, Icon = Links.Store.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="כֻּתֹּנֶת",
					HomeTitleSuffix=" Kuthoneth H3801",
					SortOrder=18,
					SitemapUsage=true
				},
				new Link
				{
					Index = BloodMoons.Index,
					Title = BloodMoons.Title,
					Icon = BloodMoons.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="יָרֵחַ",
					HomeTitleSuffix=" yareach H3394",
					SortOrder=19,
					SitemapUsage=true
				},
				new Link
				{
					Index = Articles.Index,
					Title = Articles.Title,
					Icon = Articles.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="כְּתֻבִים",
					HomeTitleSuffix=" Ketuvim H3789",
					SortOrder=20,
					SitemapUsage=true
				},
				new Link
				{
					Index = FurtherStudies.Index,
					Title = FurtherStudies.Title,
					Icon = FurtherStudies.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="סֵפֶר",
					HomeTitleSuffix=" sepher H5612",
					SortOrder=21,
					SitemapUsage=true
				},
				new Link
				{
					Index = ImportantLinks.Index,
					Title = ImportantLinks.Title,
					Icon = ImportantLinks.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="רָקַד",
					HomeTitleSuffix=" rakad H7540",
					SortOrder=22,
					SitemapUsage=true
				},
				new Link
				{
					Index = PageFeast.Index,
					Title = PageFeast.Title,
					Icon = PageFeast.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="מוֹעֵד",
					HomeTitleSuffix=" moed H4150",
					SortOrder=23,
					SitemapUsage=true
				},
				new Link
				{
					Index = AboveAllImages.Url,
					Title = AboveAllImages.Title,
					Icon = "fas fa-external-link-square-alt",
					HomeSidebarUsage=false,
					HomeFloatRightHebrew="",
					HomeTitleSuffix="",
					SortOrder=24,
					SitemapUsage=true
				},
				new Link
				{
					Index = BiblicalPermaculture.Index,
					Title = BiblicalPermaculture.Title,
					Icon = BiblicalPermaculture.Icon,
					HomeSidebarUsage=false,
					SortOrder=25,
					SitemapUsage=true
				},
				new Link
				{
					Index = Links.ShowLow.Index,
					Title = Links.ShowLow.Title,
					Icon = Links.ShowLow.Icon,
					HomeSidebarUsage=false,
					SortOrder=26,
					SitemapUsage=true
				},
				new Link
				{
					Index = Links.Mishpocha.Index,
					Title = Links.Mishpocha.Title,
					Icon = Links.Mishpocha.Icon,
					HomeSidebarUsage=false,
					SortOrder=27,
					SitemapUsage=true
				},
				new Link
				{
					Index = Links.Community.Index,
					Title = Links.Community.Title,
					Icon = Links.Community.Icon,
					HomeSidebarUsage=false,
					SortOrder=28,
					SitemapUsage=true
				},
				new Link
				{
					Index = Links.Gallery.Index,
					Title = Links.Gallery.Title,
					Icon = Links.Gallery.Icon,
					HomeSidebarUsage=false,
					SortOrder=29,
					SitemapUsage=true
				}

			};
	}

	/**/
	public List<Link> GetFeastLinks()
	{
		return new List<Link>
			{
				new Link
				{
					FeastDay = LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Passover,
					Index = Pesach.Index,
					Title = Pesach.Title,
					Icon = Pesach.Icon,
					HomeSidebarUsage=false,
					HomeFloatRightHebrew="פֶסַח",
					HomeTitleSuffix=" pesach H6453",
					SortOrder = 0,
					SitemapUsage=false
				},
				new Link
				{
					FeastDay = LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Weeks,
					Index = Shavuot.Index,
					Title = Shavuot.Title,
					Icon = Shavuot.Icon,
					HomeSidebarUsage=false,
					HomeFloatRightHebrew="שָׁבוּעוֹת",
					HomeTitleSuffix=" shavuot H7620",
					SortOrder = 0,
					SitemapUsage=false
				},
				new Link
				{
					FeastDay = LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Tabernacles,
					Index = Links.Sukkot.Index,
					Title = Links.Sukkot.Title,
					Icon = Links.Sukkot.Icon,
					HomeSidebarUsage=false,
					HomeFloatRightHebrew="סֻכּוֹת",
					HomeTitleSuffix=" Sukkot H5523",
					SortOrder = 0,
					SitemapUsage=false
				},
			};
	}


	/*
	public List<LinkBasic> GetMarkdownLinks()
	{
		return new List<LinkBasic>
		{
			new LinkBasic {Index = "/Admin/Markdown/Index", Title = "Markdown", Icon = "fas fa-tachometer-alt", },
			new LinkBasic {Index = "/Admin/Markdown/BlogUrl", Title = "BlogUrl", Icon = "fas fa-globe-americas", },
			new LinkBasic {Index = "/Admin/Markdown/EmbededStaticRazorView", Title = "Embeded Static Razor View", Icon = "fab fa-font-awesome-flag", },
			new LinkBasic {Index = "/Admin/Markdown/LocalFilename", Title = "Local Filename", Icon = "fas fa-satellite", },
			new LinkBasic {Index = "/Admin/Markdown/Parse", Title = "Parse", Icon = "fas fa-horse-head", },
			new LinkBasic {Index = "/Admin/Markdown/Verse_Mat_5_17_to_20", Title = "Verse Mat:517-20", Icon = "fas fa-bible", },
		};
	}
	*/


}
