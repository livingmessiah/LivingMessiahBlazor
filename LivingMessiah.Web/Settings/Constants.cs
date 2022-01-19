namespace LivingMessiah.Web.Settings.Constants
{
	public static class Site
	{
		public const string BaseUrl = "https://livingmessiah.com/";
	}

	public static class CalendarCache
	{
		public const string Key = "CalendarEntries";
		public const int FromMinutes = 30;
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
		public const int FromMinutes = 45; 

		// ToDo: Remove this
		public const int AbsoluteExpirationInMinutes = 15;
		public const int SlidingExpirationInMinutes = 10;
	}

	public static class ParashaPrintTupleCache
	{
		public const int FromMinutes = 40;
		public static class Keys
		{
			public const string Item1BibleBook = "ParashaPrintTupleCacheBibleBook";
			public const string Item2ParashaList = "ParashaPrintTupleCacheParashaList";
		}
	}


	public static class ParashaIndexTableTupleCache
	{
		public const string Key = "ParashaIndexTableTupleCache";
		public const int FromMinutes = 40; 
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
