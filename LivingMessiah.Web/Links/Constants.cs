namespace LivingMessiah.Web.Links
{
	public static class About
	{
		public const string Index = "/About";
		public const string Title = "About";
		public const string Icon = "fas fa-info";
	}

	public static class AboutUs
	{
		public const string Title = "About Us";
		public const string FragmentId = "AboutUs";
		public const string Descr = "About Us";
	}

	public static class AboveAllImages
	{
		public const string Id = "AboveAllImages";
		public const string Title = "Above All Images";
		public const string Image = "AboveAllImages.jpg";
		public const string Url = "https://www.aboveallimages.org";
		public const string UrlSimple = "www.AboveAllImages.org";
	}

	public static class Account
	{
		public const string Login = "/Account/Login";
		public const string Logout = "/Account/Logout";
		public const string LogoutAction = "Account/LogOut";
		public const string Profile = "/Account/Profile";

		public const string TitleAccessDenied = "Access Denied.";
		public const string TitleLogin = "Login";
		public const string TitleLogout = "Logout";
		public const string TitleProfile = "Profile";

		public const string IconClaims = "fab fa-superpowers";
		public const string IconProfileVerified = "fas fa-check";
		public const string IconProfileNotVerified = "fas fa-question";
		public const string IconLogout = "fas fa-sign-out-alt";
		public const string IconLogin = "fas fa-sign-in-alt";
		public const string IconProfile = "fas fa-user";
	}

	public static class ArchivedVideos
	{
		public const string Index = "/ArchivedVideos/";
		public const string Title = "Archived Videos";
		public const string Icon = "fab fa-youtube";
	}

	public static class Articles
	{
		public const string Index = "/Articles";
		public const string Title = "Articles";
		public const string Icon = "fas fa-pencil-alt";

		public static class MakingChallahBread
		{
			public const string Index = "/articles/MakingChallahBread";
			public const string Title = "Challah Bread";
			public const string Icon = "fas fa-bread-slice";
		}

		public static class Pesach
		{
			public const string Index = "/articles/pesach";
			//public const string Title = "** dynamically created **";
			public const string Icon = "";

		}
		public static class Prepared
		{
			public const string Index = "/articles/Prepared";
			public const string Title = "Preparing";
			public const string Icon = "";
		}


	}
	public static class BiblicalPermaculture
	{
		public const string Index = "/WindmillRanch/Permaculture";
		public const string Title = "Biblical Permaculture";
		public const string Icon = "fas fa-tractor";
	}

	public static class BloodMoons
	{
		public const string Index = "/BloodMoons";
		public const string Title = "Blood Moons";
		public const string Icon = "far fa-moon";
	}

	public static class Community
	{
		public const string Title = "Community";
		public const string FragmentId = "Community";
		public const string Descr = "Community Bulletin Board";
	}

	public static class FurtherStudies
	{
		public const string Index = "/Further";
		public const string Title = "Further Studies";
		public const string Icon = "fab fa-leanpub";
	}

	public static class Gallery
	{
		public const string Index = "/Gallery";
		public const string Title = "Gallery";
		public const string Icon = "fas fa-image";
	}

	public static class HeavensDeclare
	{
		public const string Index = "/HeavensDeclare";
		public const string Title = "Heavens Declare";
		public const string Icon = "fas fa-cloud-moon";
	}

	public static class Home
	{
		public const string Index = "/";
		//public const string Title = "Home";
		//public const string Icon = "fas fa-home";
		public const string Error = "/Error";
	}

	public static class ImportantLinks
	{
		public const string Index = "/ImportantLinks";
		public const string Title = "External Links";
		public const string Icon = "fas fa-external-link-square-alt";
	}

	public static class IndepthStudy
	{
		public const string Index = "/IndepthStudy";
		public const string Title = "In-depth study";
		public const string Icon = "fas fa-graduation-cap";
		//public const string ImgUrl = Blobs.UrlOther("in-depth-book-of-john-1024-385.jpeg");
	}

	public static class KeyDates
	{
		public const string Index = "/KeyDates";
		public const string Title = "Key Dates";
		public const string Icon = "far fa-calendar-check";
	}

	public static class Leadership
	{
		public const string Index = "/Leadership";
		public const string Title = "Leadership";
		public const string Icon = "fas fa-user-friends";
		public static class Fragments
		{
			public const string Top = "Top";
			public const string Leadership = "Leadership";
		}
	}

	public static class Location
	{
		public const string Index = "/Location";
		public const string Title = "Location";
		public const string Icon = "fas fa-map-signs";
	}

	public static class Mishpocha
	{
		public const string Index = "/mishpocha";
		public const string Title = "Mishpocha means Family";
		public const string Icon = "fas fa-ellipsis-h";
		// "fas fa-ellipsis-h"
		public const string FragmentId = "Mishpocha";
		public const string Descr = "Mishpocha means Family";
	}

	public static class Parasha
	{
		public const string Index = "/Parasha";
		public const string Title = "Parasha";
		public const string Icon = "fas fa-torah";
		public const string IconCurrent = "far fa-bookmark";

		public const string IndexPrint = "/Parasha/IndexPrint";
		//public const string TitlePrint = "Parashot - Living Messiah"

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
		public const string Index = "/Parasha/Archive";
		public const string Icon = "fas fa-archive";
		public const string Title = "Parashot Archive";
		public const string CurrentIndex = Parasha.Index;
		public static class Fragments
		{
			public const string Leviticus = "leviticus";
			public const string Numbers = "numbers";
		}
	}

	public static class PayPal
	{
		public static class Donate
		{
			public const string Index = "/donate";
			public const string Title = "Donate";
			public const string TitleLMM = "Living Messiah Ministries";
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

	public static class Pesach
	{
		public const string Index = "/Pesach";
		public const string Title = "Pesach";
		public const string TitleEnglish = "Passover";
		public const string Icon = "fas fa-door-open";
		//public const string Icon = "fas fa-door-closed"
		//public const string Icon = "fas fa-frog";
	}

	public static class PsalmsAndProverbs
	{
		public const string Index = "/PsalmsAndProverbs";
		public const string Index2 = "/PandP";
		public const string Title = "Upcoming Psalms And Proverbs";
		public const string Icon = "fab fa-readme";
	}

	public static class Podcast
	{
		public const string Index = "/Podcast";
		public const string Title = "Podcast";
		public const string Title2 = "Podcasts";
		public const string Icon = "fas fa-podcast";
	}

	public static class ShabbatService
	{
		public const string Index = "/ShabbatService";
		public const string Title = "Shabbat Service";
		public const string Icon = "far fa-hand-spock";

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
		public const string Index = "/Shavuot";
		public const string Title = "Shavuot";
		public const string Icon = "fab fa-creative-commons-zero";
	}

	public static class ShowLow
	{
		public const string Index = "/ShowLow";
		public const string Title = "ShowLow";
		public const string Icon = "fas fa-tree"; //fas fa-ellipsis-h
		public const string FragmentId = "ShowLow";
		public const string Descr = "Show Low Arizona";
	}

	public static class Sitemap
	{
		public const string Index = "/Sitemap";
		public const string Title = "Sitemap";
		public const string Icon = "fas fa-sitemap";
	}

	public static class Store
	{
		public const string Index = "/Store";
		public const string Title = "Store";
		public const string Icon = "fas fa-shopping-cart";
		public static class Items
		{
			public const string TitleTShirt = "T-Shirt";
			public const string IconTShirt = "fas fa-tshirt";
			public const string TitleCalendar = "Calendar";
			public const string SubTitleCalendar = "LMM Designed Calendar";
			public const string IconCalendar = "far fa-calendar-alt";
		}
	}


	public static class Sukkot
	{
		public const string Index = "/Sukkot";
		public const string Title = "Sukkot";
		public const string Title2 = "Sukkot 2021";
		public const string Icon = "fas fa-campground";
		public const string RegistrationShell = "/Sukkot/RegistrationShell"; // See Startup.cs options.Conventions.AddPageRoute("/Sukkot/RegistrationShell", "/Sukkot/Registration");
		public const string CreateEdit = "/Sukkot/CreateEdit";
		public const string Details = "/Sukkot/Details";
		public const string DeleteConfirmation = "/Sukkot/DeleteConfirmation";
		public const string RegistrationList = "/SukkotAdmin/RegistrationList";
		public const string Notes = "/SukkotAdmin/Notes";
		public const string AttendanceAllFeastDays = "/SukkotAdmin/AttendanceAllFeastDays";
		public const string AttendanceChart = "/SukkotAdmin/AttendanceChart";
		public const string ReturnUrlSukkotRegistration = "/Sukkot/Registration";

		public static class Donations
		{
			public const string Index = "/SukkotAdmin/Donations/Index";
			public const string FormInsert = "/SukkotAdmin/Donations/FormInsert";
			public const string ByRegistration = "/SukkotAdmin/Donations/ByRegistration";
			public const string ByRegistrationId = "/SukkotAdmin/Donations/ByRegistrationId";
			public const string CreateDonation = "/SukkotAdmin/CreateDonation";
		}

		public static class LodgingDays
		{
			public const string All = "/SukkotAdmin/LodgingDaysAll";
			public const string Chart = "/SukkotAdmin/LodgingDaysChart";
			public const string PivotOnCampCode = "/SukkotAdmin/LodgingDaysPivotOnCampCode";
			public const string Details = "/SukkotAdmin/LodgingDetails";
		}

		public static class Errors
		{
			public const string LogErrorTest = "/SukkotAdmin/LogErrorTest";
			public const string ErrorLog = "/SukkotAdmin/ErrorLog";
			public const string ErrorLogEmpty = "/SukkotAdmin/ErrorLogEmpty";
		}

		public static class Meals
		{
			public const string Index = "/SukkotAdmin/Meals/Index";  // MealTickets = "/SukkotAdmin/Meals/Tickets";
			public const string TicketsPunched = "/SukkotAdmin/Meals/TicketsPunched";
			public const string PlannerReport = "/SukkotAdmin/Meals/PlannerReport";
			public const string ListMealPlanMenu = "/SukkotAdmin/Meals/ListMealPlanMenu";
		}

		public static class KitchenWork
		{
			public const string Index = "/SukkotAdmin/KitchenWork/Index";
			public const string Edit = "/SukkotAdmin/KitchenWork/Edit";
		}

		public static class Links2 
		{
			public const string EditMeals = "/Sukkot/EditMeals";
			public const string DetailsMealTicket = "/Sukkot/DetailsMealTicket";
			public const string DeleteConfirmation = "/Sukkot/DeleteConfirmation";
			public const string DeleteHandler = "delete";
			public const string Details = "/Sukkot/Details";
			public const string Payment = "/Sukkot/Payment";
			public const string KitchenWork = "/Sukkot/KitchenWork";
		}
		//public const string TitleMealPlanner = "Sukkot Meal Planner";
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
		public const string Index = "/TorahTuesday";
		public const string Title = "Torah Tuesday";
		public const string Title2 = "Tuesday Torah Study";
		public const string Icon = "fas fa-torah";
	}


	public static class UpcomingEvents
	{
		public const string Index = "/UpcomingEvents/";
		public const string Title = "Upcoming Events";
		public const string Icon = "far fa-clock";
	}


	public static class WeeklyVideos
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

	public static class WindmillRanch
	{
		public const string Index = "/windmillranch/";
		public const string Title = "Windmill Ranch";
		//public const string Icon = "fas fa-tractor";
		public const string Icon = "fas fa-dharmachakra";
		public const string Descr = "Landing page for the Windmill Ranch project";
		public const string IndexCodeOfConduct = "/CodeOfConduct/";
		public const string TitleCodeOfConduct = "Code Of Conduct";
		public const string IconCodeOfConduct = "fas fa-handshake";
	}
}
