namespace LivingMessiah.Web.Links;

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
	public const string Url = "https://www.aboveallimages.net";  // .org
	public const string UrlSimple = "www.AboveAllImages.net";   // .org
}

public static class Account
{
	public const string Login = "/Account/Login";
	public const string Logout = "/Account/Logout";
	public const string LogoutAction = "Account/LogOut";
	public const string Profile = "/Account/Profile";

	public const string TitleAccessDenied = "Access Denied.";
	public const string TitleLogin = "Log in";
	public const string TitleLogout = "Log out";
	public const string TitleProfile = "Profile";

	public const string IconClaims = "fab fa-superpowers";
	public const string IconProfileVerified = "fas fa-check";
	public const string IconProfileNotVerified = "fas fa-question";
	public const string IconLogout = "fas fa-sign-out-alt";
	public const string IconLogin = "fas fa-sign-in-alt";
	public const string IconProfile = "fas fa-user";

	public static class PasswordChanged
	{
		public const string Index = "/account/PasswordChanged";
		public const string Title = " Password Changed Successfully";
		public const string PageTitle = " Password Changed";
		public const string Icon = "fas fa-key";
	}
}


public static class Admin
{
	public static class Video
	{
		public const string Index = "/Admin/Video/";
		public const string Redirect = "/Admin/Video";
		public const string Title = "Admin Video";
		public const string Icon = "fab fa-teamspeak";
	}
	public static class CascadingDropdownList
	{
		public const string Index = "/Admin/CascadingDropdownList/";
		public const string Title = "Cascading Dropdown List";
		public const string Icon = "fas fa-tachometer-alt";
	}

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

//ToDo use the link in WindmillRanch
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

public static class Calendar
{
	public const string Index = "/Calendar";
	public const string Title = "Calendar";
	public const string Icon = "far fa-calendar-alt";

	public static class FeastTable
	{
		public const string Index = "/FeastTable";
		public const string Title = "Feast Table";
		public const string Icon = "fas fa-glass-cheers";
	}

}

public static class Contact
{
	public const string Index = "/Contact";
	public const string Title = "Contact";
	public const string Descr = "Contacts";
	public const string Icon = "fas fa-user-friends";
}

public static class Community
{
	public const string Index = "/Community";
	public const string Title = "Community";
	public const string Descr = "Community Bulletin Board";
	public const string Icon = "fas fa-chalkboard";
	public const string Icon2 = "fas fa-city";
}

public static class DashBoard
{
	public const string Index = "/Admin/Dashboard/Index";
	public const string Title = "DashBoard";
	public const string Icon = "fas fa-tachometer-alt";
}

public static class Database
{
	public static class Error
	{
		public const string Log = "/Database/ErrorLog";
		public const string Title = "Error Log";
		public const string Icon = "fas fa-bomb";
		//public const string Title = "Database Error Log"; // append db name

		//public static class LivingMessiah
		//{
		//}
		//public static class Sukkot
		//{
		//}

	}


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
	public const string Title = "Living Messiah Home Page";
	public const string PageTitle = "Home | LMM";
	public const string Icon = "fas fa-home";
	public const string Error = "/Error";
}

public static class ImportantLinks
{
	public const string Index = "/ImportantLinks";
	public const string Title = "External Links";
	public const string Icon = "fas fa-external-link-square-alt";

	public static class HealThySelf
	{
		public const string Index = "/HealThySelf";
		public const string Title = "Heal Thy Self!";
		public const string Icon = "fas fa-heartbeat"; //
	}
}

public static class IndepthStudy
{
	public const string Index = "/IndepthStudy";
	public const string Title = "In-depth study";
	public const string Icon = "fas fa-graduation-cap";
	//public const string ImgUrl = Blobs.UrlOther("in-depth-book-of-john-1024-385.jpeg");
}

public static class IntroductionAndWelcome
{

	public const string Index = "/IntroductionAndWelcome";
	public const string Title = "Welcome";
	public const string Icon = "far fa-handshake";
	/*
	https://github.com/anton-bot/Full-Emoji-List/blob/master/Emoji.cs
	public const string Title2 = "Welcome 😄";
	*/
}

public static class KeyDatesEdit
{
	public const string Index = "/Admin/KeyDatesEdit";
	public const string Title = "Key Dates Edit";
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

public static class NavigationSearch
{
	public const string Index = "/NavigationSearch";
	public const string Title = "Navigation Search";
	public const string Icon = "fas fa-compass"; //  fas fa-search
}

public static class PayPal
{
	public static class Donate
	{
		public const string Index = "/donate";
		public const string Title = "Donate";
		public const string TitleLMM = "Living Messiah Ministries";
		public const string Icon = "fas fa-donate";
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

// ToDo maybe make an SmartEnum of Articles
public static class Pesach
{
	public const string Index = "/Articles/Pesach";
	//public const string Index = "/Pesach";
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

public static class Psalms
{
	public const string Index = "/Psalms";
	public const string Title = "Psalms";
	public const string Icon = "fab fa-readme";
}

public static class Podcast
{
	public const string Index = "/Podcast";
	public const string Title = "Podcast";
	public const string Title2 = "Podcasts";
	public const string Icon = "fas fa-podcast";
}

public static class SampleCode
{
	public const string Index = "/SampleCode";
	public const string Title = "Sample Code";
	public const string Icon = "fas fa-vial";

	public static class BibleCascadingDDL
	{
		public const string Index = "/BibleCascadingDDL/";
		public const string Title = "Bible Cascading Dropdown List";
		public const string Icon = "fas fa-tachometer-alt";
	}

	public static class BibleSearch
	{
		public const string Index = "/BibleSearch";
		public const string Title = "Bible Search";
		public const string Icon = "fas fa-search";  //fas fa-bible
	}

	public static class SyncfusionSfDropDownList
	{
		public const string Index = "/BBCP";
		public const string Title = "Bible Book Chapter | Syncfusion DDL";
		public const string Icon = "fas fa-search";
	}

	public static class BibleBooks
	{
		public const string Index = "/BibleBooks/";  // /SmartEnums/BibleBooks
		public const string Title = "Bible Books";  // <PageTitle>SmartEnums | BibleBooks</PageTitle>
		public const string Icon = "fas fa-tachometer-alt";
	}

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
	public const LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum FeastDay = LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Weeks;
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
	public static class RegistrationSteps
	{
		public const string Index = "/Sukkot/RegistrationSteps";
		public const string Title = "Registration Steps";
		public const string Icon = "fas fa-campground";
		public const string StartButtonText = "Begin Registration Steps";
		public const string StartButtonIcon = "fas fa-caret-right";
		public const string BackToButtonText = "Back to Registration Steps";
		public const string BackToButtonIcon = "fas fa-campground";
	}

	public static class SuperUser
	{
		public const string Index = "Sukkot/SuperUser";
		public const string Title = "Super User Registration";
		public const string IconText = "Super User";
		public const string Icon = "fas fa-mask";
	}


	public const LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum FeastDay = LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Tabernacles;
	public const string Index = "/Sukkot";
	public const string Title = "Sukkot";
	public const string Icon = "fas fa-campground";

	public const string Details = "/Sukkot/Details";
	public const string DetailsTitle = "Sukkot Details";

	public const string DeleteConfirmation = "/Sukkot/DeleteConfirmation";
	public const string DeleteConfirmationTitle = "Delete Sukkot Registration?";
	public const string DeleteConfirmationSubTitle = "Delete Registration? | ";

	public const string AdminIndex = "/SukkotAdmin";
	public const string AdminIndexTitle = "Sukkot Admin";

	public const string RegistrationList = "/SukkotAdmin/RegistrationList";
	public const string RegistrationListTitle = "Sukkot Admin Registration List";


	public const string Notes = "/SukkotAdmin/Notes";
	public const string NotesTitle = "Sukkot Admin Registration Notes";

	public const string AttendanceAllFeastDays = "/SukkotAdmin/AttendanceAllFeastDays";
	public const string AttendanceChart = "/SukkotAdmin/AttendanceChart";
	public const string ReturnUrlSukkotRegistration = "/Sukkot/Registration";

	public static class Donations
	{
		public const string Grid = "/SukkotAdmin/DonationsGrid";
		public const string GridTitle = "Sukkot Admin DonationsGrid";
		public const string CreateDonation = "/SukkotAdmin/CreateDonation";
	}

	public static class Errors
	{
		public const string ErrorLog = "/SukkotAdmin/ErrorLog";
		public const string ErrorLogTitle = "Sukkot Admin ErrorLog";
		//public const string LogErrorTest = "/SukkotAdmin/LogErrorTest";
		//public const string ErrorLogEmpty = "/SukkotAdmin/ErrorLogEmpty";
	}

	public static class LegalAgreementVerbiage
	{
		public const string Index = "/SukkotAdmin/LegalAgreementVerbiage";
		public const string Title = "Legal Agreement Verbiage";
		public const string Icon = "fas fa-balance-scale";  // "fas fa-handshake" "far fa-handshake"
	}

	public static class Links2
	{
		public const string Details = "/Sukkot/Details";
		public const string Payment = "/Sukkot/Payment";
		public const string PaymentTitle = "Donations Earmarked for Sukkot";
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
	public const string Index = "/TorahTuesday";
	public const string Title = "Torah Tuesday";
	public const string Title2 = "Tuesday Torah Study";
	public const string Icon = "fas fa-torah";
}


public static class SpecialEvents
{
	public const string Index = "/SpecialEvents/";
	public const string Title = "Special Events";
	public const string Icon = "far fa-clock";
}


public static class UpcomingEvents
{
	public const string Index = "/UpcomingEvents/";
	public const string Title = "Upcoming Events";
	public const string Icon = "far fa-clock";
}

public static class UpcomingEventsAdmin
{

	public static class Edit
	{
		public const string Page = "/UpcomingEventsAdminEdit";
		public const string Title = "Upcoming Events Edit";
		public const string Icon = "fas fa-pencil-alt";
	}


	public static class Form
	{
		public const string Page = "/UpcomingEventsAdminForm";  // was UpcomingEventsCRUD
		public const string Title = "Upcoming Events Form";
		public const string Icon = "fas fa-tablet-alt";
	}

	public static class Grid
	{
		public const string Page = "/UpcomingEventsAdminGrid"; // SimpleGrid.razor
		public const string Title = "Upcoming Events Grid";
		public const string Icon = "far fa-clock";
	}

	public static class Table
	{
		public const string Page = "/UpcomingEventsAdminTable";
		public const string Title = "Upcoming Events Table";
		public const string Icon = "fas fa-table";
	}

	public static class EditMarkdown
	{
		public const string Page = "/UpcomingEventsAdminEditMarkdown"; // ToDo: URL doesn't work but the class is referenced ???
		public const string Title = "Edit Upcoming Events Markdown ";
		public const string Icon = "fab fa-markdown";
		public const string Icon2 = "fas fa-pencil-alt";
	}

	public static class UploadImage
	{
		public const string Page = "/UpcomingEventsAdminUploadImage"; // URL Doesn't work but the class is referenced ???
		public const string Title = "Upload Upcoming Events Image ";
		public const string Icon = "fas fa-cloud-upload-alt";
		public const string Icon2 = "fas fa-image";
	}

}


public static class Wirecast
{
	public const string Icon = "fas fa-podcast";
	public const string Index = "/Wirecast";
	public const string Title = "Wirecast Link for Translators";

	public static class Admin
	{
		public const string Index = "/Wirecast/Edit";
		public const string Title = "Wirecast Edit";
		public const string Icon = "fas fa-pencil-alt ";  // fa-pencil-alt-square-o
	}

}
