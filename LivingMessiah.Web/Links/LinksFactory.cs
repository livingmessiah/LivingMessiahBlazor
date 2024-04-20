using System.Collections.Generic;
using PageFeast = LivingMessiah.Web.Features.Feasts.FeastLinks;
using PageParasha = LivingMessiah.Web.Features.Parasha.Enums.ParashaLinks;
using PageWindmillRanch = LivingMessiah.Web.Features.WindmillRanch.Enums.WindmillRanchLinks;

namespace LivingMessiah.Web.Links;

public interface ILinksFactory
{
	List<Link> GetLinks();
	List<Link> GetFeastLinks();
	List<LinkBasic> GetVideoProductionLinks();
}

public class LinksFactory : ILinksFactory
{

	public List<LinkBasic> GetVideoProductionLinks()
	{
		return new List<LinkBasic>
			{
				new LinkBasic {Index = Admin.Video.Index, Title = Admin.Video.Title, Icon = Admin.Video.Icon, },
				new LinkBasic {Index = Wirecast.Admin.Index, Title = Wirecast.Admin.Title, Icon = Wirecast.Admin.Icon, },
				new LinkBasic {Index = Database.Error.Index, Title = Database.Error.Title, Icon = Database.Error.Icon, },
				new LinkBasic {Index = KeyDatesEdit.Index, Title = KeyDatesEdit.Title, Icon = KeyDatesEdit.Icon, }
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
					Index = Donate.Index,
					Title = Donate.Title,
					Icon = Donate.Icon,
					HomeSidebarUsage=true,
					HomeFloatRightHebrew="מוֹעֲדָי",
					HomeTitleSuffix=" tzedakah H6666",
					SortOrder=14,
					SitemapUsage=true
				},
				new Link
				{
					Index = SampleCode.BibleSearch.Index,
					Title = SampleCode.BibleSearch.Title,
					Icon = SampleCode.BibleSearch.Icon,
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
					Index = Store.Index, Title = Store.Title, Icon = Store.Icon,
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
					Index = SampleCode.Index,
					Title = SampleCode.Title,
					Icon = SampleCode.Icon,
					HomeSidebarUsage=false,
					SortOrder=26,
					SitemapUsage=true
				},
				new Link
				{
					Index = Mishpocha.Index,
					Title = Mishpocha.Title,
					Icon = Mishpocha.Icon,
					HomeSidebarUsage=false,
					SortOrder=28,
					SitemapUsage=true
				},
				new Link
				{
					Index = Community.Index,
					Title = Community.Title,
					Icon = Community.Icon,
					HomeSidebarUsage=false,
					SortOrder=29,
					SitemapUsage=true
				},
				new Link
				{
					Index = Gallery.Index,
					Title = Gallery.Title,
					Icon = Gallery.Icon,
					HomeSidebarUsage=false,
					SortOrder=30,
					SitemapUsage=true
				},

				new Link
				{
					Index = ThresholdCovenant.Index,
					Title = ThresholdCovenant.Title,
					Icon = ThresholdCovenant.Icon,
					HomeSidebarUsage=false,
					HomeFloatRightHebrew="סַף",
					HomeTitleSuffix=" saph H5592",
					SortOrder=31,
					SitemapUsage=true
				}

			};
	}

	public List<Link> GetFeastLinks()
	{
		return new List<Link>
			{
				new Link
				{
					FeastDayValue = Features.Calendar.Enums.FeastDay.Passover.Value,
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
					FeastDayValue = Features.Calendar.Enums.FeastDay.Weeks.Value,
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
					FeastDayValue = Features.Calendar.Enums.FeastDay.Tabernacles.Value,
					Index = Sukkot.Index,
					Title = Sukkot.Title,
					Icon = Sukkot.Icon,
					HomeSidebarUsage=false,
					HomeFloatRightHebrew="סֻכּוֹת",
					HomeTitleSuffix=" Sukkot H5523",
					SortOrder = 0,
					SitemapUsage=false
				},
			};
	}


}
