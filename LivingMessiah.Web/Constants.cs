using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web;

public static class Global
{
	public const string ToastShowError = "An invalid operation occurred, contact your administrator";
}

public static class DateFormat
{
	public const string ddd_mm_dd = "ddd, MM/dd";  //ddd, MM/dd/yyyy
	public const string mm_dd = "MM/dd";
	public const string MM_dd_HH_mm = "MM/dd HH:mm";
	public const string MM_dd_hh_mm = "MM/dd hh:mm";
	public const string dd = "dd";
	public const string dddd_dd_MMMM = "dddd, dd MMMM";
	public const string dddd_MMMM_dd = "dddd, MMMM dd ";
	public const string ddd_MMMM_dd_YYYY = "ddd, MMMM dd, yyyy";
}

public static class CurrencyFormat
{
	public const string NoCents = "{0:C0}"; // doesn't work use a property like below
	/*
		public string AmountNoCents { get { return String.Format("{0:C0}", Amount); }	}
	*/
}

public static class Blobs
{
	private const string root = "https://livingmessiahstorage.blob.core.windows.net/images/";
	private const string articles = "https://livingmessiahstorage.blob.core.windows.net/articles/";
	private const string maps = "https://livingmessiahstorage.blob.core.windows.net/images/events/";
	private const string other = "https://livingmessiahstorage.blob.core.windows.net/images/other/";
	private const string windmillRanch = "https://livingmessiahstorage.blob.core.windows.net/windmill-ranch/";
	private const string windmillRanchBulldozer = "https://livingmessiahstorage.blob.core.windows.net/windmill-ranch/Bulldozer/";
	private const string windmillRanchGarden = "https://livingmessiahstorage.blob.core.windows.net/windmill-ranch/Garden/";
	private const string windmillRanchSwaleRainEvent = "https://livingmessiahstorage.blob.core.windows.net/windmill-ranch/Swale-2022-10-07-Rain-Event/";
	private const string windmillRanch2023_09_13_Swale_Report = "https://livingmessiahstorage.blob.core.windows.net/windmill-ranch/2023-09-13-Swale-Report/";

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
	private const string calendar = "https://livingmessiahstorage.blob.core.windows.net/images/calendar/";
	private const string ruth_omer_count = "https://livingmessiahstorage.blob.core.windows.net/ruth-omer-count/";

	public static string UrlSukkot2017(string blob)
	{
		return sukkot2017 + blob;
	}

	public static string UrlCalendar(string blob)
	{
		return calendar + blob;
	}

	public static string RuthOmerCount(string blob)
	{
		return ruth_omer_count + blob;
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

	public static string UrlWindmillRanchBulldozer(string blob)
	{
		return windmillRanchBulldozer + blob;
	}

	public static string UrlWindmillRanchGarden(string blob)
	{
		return windmillRanchGarden + blob;
	}

	public static string UrlWindmillRanchSwaleRainEvent(string blob)
	{
		return windmillRanchSwaleRainEvent + blob;
	}

	public static string NewsLetter(string blob)
	{
		return windmillRanch2023_09_13_Swale_Report + blob;
	}


}

public static class Emails
{
	public static class Donations
	{
		public static string Email() { return "donations@livingmessiah.com"; }
	}

	public static class Info
	{
		public static string Email() { return "info@livingmessiah.com"; }
		public static string Subject = "?Subject=Questions";
	}

}

public static class SocialMedia
{
	public static class YouTube
	{
		private const string _channelId = "UCz_q3-dBtU_sSbEojRP57OQ";
		private const string _baseFeedUrl = "https://www.youtube.com/feeds/videos.xml?channel_id=";
		private const string _baseNormalUrl = "https://www.youtube.com/channel/";
		private const string _baseSearchUrl = "https://www.youtube.com/results?search_query=living+messiah";
		public static string YouTubeFeed()
		{
			return _baseFeedUrl + _channelId;
		}

		public static string YouTubeNormal()
		{
			return _baseNormalUrl + _channelId;
		}

		public static string YouTubeFeatured()
		{
			return _baseNormalUrl + _channelId + "/featured";
		}

		public static string YouTubeSearch()
		{
			return _baseSearchUrl;
		}

	}
}