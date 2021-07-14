namespace LivingMessiah.Web.Settings.Constants
{
	public static class Site
	{
		public const string BaseUrl = "https://livingmessiah.com/";
	}

	public static class PsalmsAndProverbsCache
	{
		public const string Key = "currentPsalmAndProverb";
		public const int AbsoluteExpirationInMinutes = 15;
		public const int SlidingExpirationInMinutes = 10;
	}

	public static class ParashaCache
	{
		public const string Key = "currentParasha";
		public const int AbsoluteExpirationInMinutes = 15;
		public const int SlidingExpirationInMinutes = 10;
	}

	public static class ParashaTorahBookCache
	{
		public const string Key = "parashaTorahBook";
		public const int AbsoluteExpirationInMinutes = 15;
		public const int SlidingExpirationInMinutes = 10;
	}

	public static class CurrentWeeklyVideosCache
	{
		public const string Key = "currentWeeklyVideos";
		public const int AbsoluteExpirationInMinutes = 15;
		public const int SlidingExpirationInMinutes = 10;
	}

	public static class KeyDatesCache
	{
		public const string Key = "keyDatesCache";
		public const int AbsoluteExpirationInMinutes = 15;
		public const int SlidingExpirationInMinutes = 10;
	}

	public static class HebrewYearAndChildrenCache
	{
		public const string Key = "keyHebrewYearAndChildrenCache";
		public const int AbsoluteExpirationInMinutes = 15;
		public const int SlidingExpirationInMinutes = 10;
	}
	
}
