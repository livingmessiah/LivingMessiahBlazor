namespace LivingMessiah.Web.Helper
{
	public static class About
	{
		public static class Anchors
		{
			public const string Index = "/About";
			public const string Title = "About";
			public const string Icon = "fas fa-info";
		}
	}

	public static class AboutUs
	{
		public const string Title = "About Us";
		public const string FragmentId = "AboutUs";
		public const string Descr = "About Us";
	}

	public static class ArchivedVideos
	{
		public static class Anchors
		{
			public const string Index = "/ArchivedVideos/";
			public const string Title = "Archived Videos";
			public const string Icon = "fab fa-youtube";
		}
	}



	public static class Articles2
	{
		public static class Index
		{
			public static class Anchors
			{
				public const string Index = "/Articles";
				public const string Title = "Articles";
				public const string Icon = "fas fa-pencil-alt";
			}
		}
		public static class MakingChallahBread
		{
			public static class Anchors
			{
				public const string Index = "/articles/MakingChallahBread";
				public const string Title = "Challah Bread";
				public const string Icon = "fas fa-bread-slice";
			}
		}

		public static class Pesach
		{
			public static class Anchors
			{
				public const string Index = "/articles/pesach";
				//public const string Title = "** dynamically created **";
				public const string Icon = "";
			}
		}
		public static class Prepared
		{
			public static class Anchors
			{
				public const string Index = "/articles/Prepared";
				public const string Title = "Preparing";
				public const string Icon = "";
			}
		}

	}

	public static class BiblicalPermaculture
	{
		public static class Anchors
		{
			public const string Index = "/WindmillRanch/Permaculture";
			public const string Title = "Biblical Permaculture";
			public const string Icon = "fas fa-tree";
		}
	}

	public static class BloodMoons
	{
		public static class Anchors
		{
			public const string Index = "/BloodMoons";
			public const string Title = "Blood Moons";
			public const string Icon = "far fa-moon";
		}
	}

	public static class Community
	{
		public const string Title = "Community";
		public const string FragmentId = "Community";
		public const string Descr = "Community Bulletin Board";
	}

	public static class Donate
	{
		public static class Anchors
		{
			public const string Index = "/Donate/Index";
			public const string Title = "Donate";
			public const string Icon = "fab fa-paypal";
			public const string Href = "https://LivingMessiah.com/Donate";
		}
	}

	public static class ImportantLinks
	{
		public static class Anchors
		{
			public const string Index = "/ImportantLinks";
			public const string Title = "External Links";
			public const string Icon = "fas fa-external-link-square-alt";
		}
	}

	public static class FurtherStudies
	{
		public static class Anchors
		{
			public const string Index = "/Further";
			public const string Title = "Further Studies";
			public const string Icon = "fab fa-leanpub";
		}
	}

	public static class Gallery
	{
		public static class Anchors
		{
			public const string Index = "/Gallery";
			public const string Title = "Gallery";
			public const string Icon = "fas fa-image";
		}
	}

	public static class Location
	{
		public static class Anchors
		{
			public const string Index = "/Location";
			public const string Title = "Location";
			public const string Icon = "fas fa-map-signs";
		}
	}

	public static class Mishpocha
	{
		public const string Title = "Mishpocha means Family";
		//public const string SubTitle = "Mishpocha means Family";
		public const string FragmentId = "Mishpocha";
		public const string Descr = "Mishpocha means Family";
	}

	public static class NewsFlash
	{
		public const string Title = "NEWS FLASH!";
		public const string FragmentId = "NewsFlash";
		public const string Descr = "News Flash";
	}

	public static class Parasha
	{
		public static class Anchors
		{
			public const string Index = "/Parasha";
			public const string Title = "Parasha";

			public const string IndexPrint = "/Parasha/IndexPrint";
			//public const string TitlePrint = "Parashot - Living Messiah"

			public const string Icon = "fas fa-torah";
			public const string IconCurrent = "far fa-bookmark";
		}

		public static class MyHebrewBible
		{
			private const string baseUrl = "https://myhebrewbible.com/Parasha/Triennial/LivingMessiah/";
			public static string ParashaUrl(int id, string slug)
			{
				return $"{baseUrl}/{id}?slug={slug}/";
			}
		}

	}

	public static class ParashaArchive
	{
		public static class Anchors
		{
			public const string Index = "/Parasha/Archive";
			public const string Icon = "fas fa-archive";
			public const string Title = "Parashot Archive";
			public const string CurrentIndex = Parasha.Anchors.Index;
			public static class Fragments
			{
				public const string Leviticus = "leviticus";
				public const string Numbers = "numbers";
			}
		}

	}



	public static class PayPal
	{
		public static class Anchors
		{
			public static class Donate
			{
				public const string Index = "/donate";
				public const string Title = "Living Messiah Ministries";
				public const string Icon = "fab fa-paypal";
			}
			public static class CancelDonation
			{
				public const string Index = "/cancel_donation.html";
				public const string Title = "Donation Cancelation";
				public const string Icon = "fab fa-paypal";
			}
			public static class ConfirmDonation
			{
				public const string Index = "/confirm_donation.html";
				public const string Title = "Donation Confirmation";
				public const string Icon = "fab fa-paypal";
			}
		}
	}

	public static class Podcast
	{
		public static class Anchors
		{
			public const string Index = "/Podcast";
			public const string Title = "Podcast";
			public const string Title2 = "Podcasts";
			public const string Icon = "fas fa-podcast";
		}
	}

	public static class ShabbatService
	{
		//  Shofar H7782
		//<span class="hebrew">שַׁבָּת</span><br />

		// Shalom H7965
		//<span class="hebrew">שָׁלוֹם</span> <br />

		//ToDo: No references...delete.
		public const string LiveFeedTopId = "IndepthStudyTop";       // ToDo: Anchor Fragment's don't work in Blazor 3.1
		public const string LiveFeedEspTopId = "IndepthStudyEspTop"; // ToDo: Anchor Fragment's don't work in Blazor 3.1

		public static class Anchors
		{
			public const string Index = "/ShabbatService";
			public const string Title = "Shabbat Service";
			public const string Icon = "far fa-hand-spock";

		}

		//ToDo: used only by Components\Pages\ShabbatService\ShabbatService.razor
		public static class LiveFeed
		{
			public const string IndepthStudyTopId = "IndepthStudyTop";  // ToDo: Anchor Fragment's don't work in Blazor 3.1
			public const string IndepthStudyTitle = "In-depth Study";

			public const string ShabbatServiceTopId = "ShabbatServiceTop"; // ToDo: Anchor Fragment's don't work in Blazor 3.1
			public const string ShabbatServiceTitle = "Main Shabbat Service";

			public const string ShabbatServiceEspTopId = "ShabbatServiceEspTop"; // ToDo: Anchor Fragment's don't work in Blazor 3.1
			public const string ShabbatServiceEspTitle = "Servicio principal de Shabat";
		}
	}

	public static class Shavuot
	{
		public static class Anchors
		{
			public const string Index = "/Shavuot";
			public const string Title = "Shavuot";
			public const string Icon = "fab fa-creative-commons-zero";
		}
	}

	public static class Sitemap
	{
		public static class Anchors
		{
			public const string Index = "/Sitemap";
			public const string Title = "Sitemap";
			public const string Icon = "fas fa-sitemap";
		}
	}

	public static class ShowLow
	{
		public const string Title = "ShowLow";
		public const string FragmentId = "ShowLow";
		public const string Descr = "Show Low Arizona";
	}

	public static class Store
	{
		public static class Anchors
		{
			public const string Index = "/Store";
			public const string Title = "Store";
			public const string Icon = "fas fa-shopping-cart";
		}
		public static class Items
		{
			public const string TitleTShirt = "T-Shirt";
			public const string IconTShirt = "fas fa-tshirt";
			public const string TitleCalendar = "Calendar";
			public const string SubTitleCalendar = "LMM Designed Calendar";
			public const string IconCalendar = "far fa-calendar-alt";
		}

	}

	public static class ThankYou
	{
		public const string TopId = "ThankYouTop";
		public const string Title = "ThankYou";
		public const string FragmentId = "ThankYou";
		public const string Descr = "Thank You !!!";
	}

	public static class TorahTuesday
	{
		public static class Anchors
		{
			public const string Index = "/TorahTuesday/Index";
			public const string Title = "Torah";
			public const string Title2 = "Tuesday Torah Study";
			public const string Icon = "fas fa-torah";
		}

	}

	public static class UpcomingEvents
	{
		public static class Anchors
		{
			public const string Index = "/UpcomingEvents/";
			public const string Title = "Upcoming Events";
			public const string Icon = "far fa-clock";
		}
	}

	public static class WeeklyVideos
	{
		public static class Anchors
		{
			public const string Index = "/Admin/WeeklyVideos";
			public const string Title = "Weekly Videos";
			public const string Icon = "fab fa-youtube";

			public const string AddIcon = "fas fa-plus";
			public const string AddButtonColor = "btn btn-success";
			public const string AddText = "Add";
			public const string AddModalText = "Save";

			public const string EditIcon = "fas fa-pencil-alt";
			public const string EditButtonColor = "btn btn-primary";
			public const string EditText = "Edit";
			public const string EditModalText = "Update";

			public const string DeleteIcon = "fa fa-times";
			public const string DeleteButtonColor = "btn btn-danger";
			public const string DeleteText = "Delete";

			public const string SaveIcon = "fas fa-save";
			
			public const string CancelIcon = "fas fa-window-close"; //"far fa-window-close";

		}
	}

	public static class WindmillRanch
	{
		//public const string Title = "Windmill Ranch";
		public const string Descr = "Landing page for the Windmill Ranch project";
		public static class Anchors
		{
			public const string Index = "/windmillranch/";
			public const string Title = "Windmill Ranch";
			//public const string Icon = "fas fa-tractor";
			public const string Icon = "fas fa-dharmachakra";
			//<i class="fas fa-dharmachakra"></i>
			public const string IndexCodeOfConduct = "/CodeOfConduct/";
			public const string TitleCodeOfConduct = "Code Of Conduct";
			public const string IconCodeOfConduct = "fas fa-handshake";

		}

	}
}
