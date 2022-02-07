using System.Collections.Generic;
using LivingMessiah.Web.Domain;
using LivingMessiah.Web.Links;

namespace LivingMessiah.Web.Data
{
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
				new LinkBasic {Index = WeeklyVideos.Index, Title = WeeklyVideos.Title, Icon = WeeklyVideos.Icon, },
 			};
		}

		public List<LinkBasic> GetEldersLinks()
		{
			return new List<LinkBasic>
			{
				new LinkBasic {Index = PsalmsAndProverbs.Index, Title = PsalmsAndProverbs.Title, Icon = PsalmsAndProverbs.Icon, },
				new LinkBasic {Index = Contact.Index, Title = Contact.Title, Icon = Contact.Icon, },
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
					HomeSidebarUsage=false,
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
					SitemapUsage=true
				},
				new Link
				{
					Index = WindmillRanch.Index,
					Title = WindmillRanch.Title,
					Icon = WindmillRanch.Icon,
					HomeSidebarUsage=true,
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
					SitemapUsage=true
				},
				new Link
				{
					Index = Parasha.Index,
					Title = Parasha.Title,
					Icon = Parasha.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="פָּרָשַׁת",
					HomeTitleSuffix=" Parashat H6567",
					SitemapUsage=true
				},
				new Link
				{
					Index = ParashaArchive.Index,
					Title = ParashaArchive.Title,
					Icon = ParashaArchive.Icon,
					HomeSidebarUsage=false,
					HomeFloatRightHebrew="",
					HomeTitleSuffix="",
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
					SitemapUsage=false // Don't show sitemap link on sitemap page
				},
				new Link
				{
					Index = Store.Index,
					Title = Store.Title,
					Icon = Store.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="כֻּתֹּנֶת",
					HomeTitleSuffix=" Kuthoneth H3801",
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
					SitemapUsage=true
				},
				new Link
				{
					Index = BiblicalPermaculture.Index,
					Title = BiblicalPermaculture.Title,
					Icon = BiblicalPermaculture.Icon,
					HomeSidebarUsage=false,
					SitemapUsage=true
				},
				new Link
				{
					Index = Links.ShowLow.Index,
					Title = Links.ShowLow.Title,
					Icon = Links.ShowLow.Icon,
					HomeSidebarUsage=false,
					SitemapUsage=true
				},
				new Link
				{
					Index = Links.Mishpocha.Index,
					Title = Links.Mishpocha.Title,
					Icon = Links.Mishpocha.Icon,
					HomeSidebarUsage=false,
					SitemapUsage=true
				},
				new Link
				{
					Index = Links.Community.Index,
					Title = Links.Community.Title,
					Icon = Links.Community.Icon,
					HomeSidebarUsage=false,
					SitemapUsage=true
				},
				new Link
				{
					Index = Links.Gallery.Index,
					Title = Links.Gallery.Title,
					Icon = Links.Gallery.Icon,
					HomeSidebarUsage=false,
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
					HomeTitleSuffix=" shavuot H7620",
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
}
