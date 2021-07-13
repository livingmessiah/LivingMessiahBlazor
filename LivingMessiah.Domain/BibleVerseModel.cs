namespace LivingMessiah.Domain
{
	public class BibleVerseModel
	{
		private const string BaseUrl = "https://www.myhebrewbible.com/BookChapter";

		public string Book { get; set; }  // Genesis
		public int Chapter { get; set; }  // 32
		public string ChapterTitle { get; set; }  // Jacob Prepares to Meet Esau, Wrestles with God
		public string ChapterSlug { get; set; } // jacob-prepares-to-meet-esau-wrestles-with-god
		public int StartVerse { get; set; } = 0;

		public string BookChapter
		{
			get
			{
				return $"{Book} {Chapter}";
			}
		}

		//"https://www.myhebrewbible.com/BookChapter/John/16/Jesus-Promises-the-Holy-Spirit-Foretells-His-Death-and-Resurrection" },
		public string Url
		{
			get
			{
				return $"{BaseUrl}/{Book}/{Chapter}/{ChapterSlug}";
			}
		}


	}
}
