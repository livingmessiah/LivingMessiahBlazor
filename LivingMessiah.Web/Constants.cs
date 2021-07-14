namespace LivingMessiah.Web
{
	public static class DateFormat
	{
		public const string ddd_mm_dd = "ddd, MM/dd";  //ddd, MM/dd/yyyy
		public const string mm_dd = "MM/dd";
		public const string dd = "dd";
		public const string dddd_dd_MMMM = "dddd, dd MMMM";
		public const string dddd_MMMM_dd = "dddd, MMMM dd ";
	}

	public static class Blobs
	{
		private const string root = "https://livingmessiahstorage.blob.core.windows.net/images/";
		private const string articles = "https://livingmessiahstorage.blob.core.windows.net/articles/";
		private const string maps = "https://livingmessiahstorage.blob.core.windows.net/images/events/";
		private const string other = "https://livingmessiahstorage.blob.core.windows.net/images/other/";
		private const string windmillRanch = "https://livingmessiahstorage.blob.core.windows.net/windmill-ranch/";
		private const string events = "https://livingmessiahstorage.blob.core.windows.net/images/events/";
		private const string godseconomy = "https://livingmessiahstorage.blob.core.windows.net/images/godseconomy/";
		private const string pdfs = "https://livingmessiahstorage.blob.core.windows.net/pdfs/";
		private const string persons = "https://livingmessiahstorage.blob.core.windows.net/images/persons/";
		private const string leadership = "https://livingmessiahstorage.blob.core.windows.net/leadership/";
		private const string redBloodMoons = "https://livingmessiahstorage.blob.core.windows.net/images/redbloodmoons/";
		private const string LearningHebrew = "https://livingmessiahstorage.blob.core.windows.net:443/pdfs/LearningHebrew.pdf";
		private const string HebrewHandouts = "https://livingmessiahstorage.blob.core.windows.net:443/hebrewhandouts/";
		private const string sukkot2017 = "https://livingmessiahstorage.blob.core.windows.net:443/sukkot2017/";
		private const string importantlinks = "https://livingmessiahstorage.blob.core.windows.net/images/importantlinks/";
		private const string shabbatService = "https://livingmessiahstorage.blob.core.windows.net/images/shabbatservice/";
		private const string weeklyAnnouncements = "https://livingmessiahstorage.blob.core.windows.net/weeklyannouncements/";

		public static string UrlSukkot2017(string blob)
		{
			return sukkot2017 + blob;
		}

		public static string UrlWeeklyAnnouncements(string blob)
		{
			return weeklyAnnouncements + blob;
		}


		public static string Url(string blob)
		{
			return maps + blob;
		}

		public static string UrlArticles(string blob)
		{
			return articles + blob;
		}

		public static string UrlRedBloodMoons(string blob)
		{
			return redBloodMoons + blob;
		}

		public static string UrlOther(string blob)
		{
			return other + blob;
		}

		public static string UrlEvents(string blob)
		{
			return events + blob;
		}

		public static string UrlGodsEconomy(string blob)
		{
			return godseconomy + blob;
		}

		public static string Persons(string blob)
		{
			return persons + blob;
		}

		public static string Leadership(string blob)
		{
			return leadership + blob;
		}

		public static string UrlRoot(string blob)
		{
			return root + blob;
		}

		public static string UrlShabbatService(string blob)
		{
			return shabbatService + blob;
		}

		public static string UrlImportantLinks(string blob)
		{
			return importantlinks + blob;
		}

		public static string UrlPdfs(string blob)
		{
			return pdfs + blob;
		}

		public static string UrlLearningHebrew()
		{
			return LearningHebrew;
		}

		public static string UrlHebrewHandouts(string blob)
		{
			return HebrewHandouts + blob;
		}

		public static string UrlWindmillRanch(string blob)
		{
			return windmillRanch + blob;
		}


	}

	// ToDo: this is redundant to LivingMessiah.Web.Data
	public static class Address 
	{
		public static string Name() { return "Living Messiah Ministries";	}
		public static string Street1() { return "19 North Robson #106"; }
		public static string City() { return "Mesa"; }
		public static string State() { return "AZ"; }
		public static string Zip() { return "85201"; }
		public static string LatLong() { return "33.415833, -111.836272"; }
		public static string Phone() { return "555.555.1212"; }
		public static string Email() { return "info@livingmessiah.com"; }
		public static string EmailHref() { return "mailto:info@livingmessiah.com"; }


		public static string EmailAnchor()
		{
			return $"<a href='{EmailHref()}'>{Email()}</a>";
		}	
	}

	/*
	public static class LatLongRange
	{
		public static string Lat { get; set; }
		public static string Long { get; set; }
		public static LatLongRange(string x= "33.415833", string y = "-111.836272")
		{
			(Lat, Long) = (x, y);
		}
	}
	*/
	//public static string LatLong() { return "33.415833, -111.836272"; }
	/*
	public class LatLongRange
	{
		public int Lat { get; set; }
		public int Long { get; set; }
		public LatLongRange(int x, int y) => (Lat, Long) = (x, y);
	}
	*/
}
