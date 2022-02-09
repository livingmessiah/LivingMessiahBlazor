namespace LivingMessiah.Web.Domain;

public class Link
{
		public string Index { get; set; }
		public string Title { get; set; }
		public string Icon { get; set; }

		public bool SitemapUsage { get; set; }
		public bool HomeSidebarUsage { get; set; }
		public string HomeFloatRightHebrew { get; set; }
		public string HomeTitleSuffix { get; set; } // shaMayim H8064"

		public LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum FeastDay { get; set; }

		/*
		public string Descr { get; set; }
		public string FragmentId { get; set; }
		public string Href { get; set; }
		public string IndexPrint { get; set; }


				ParashaArchive!CurrentIndex
				Podcast.Title2
				Pesach.Title //  = "** dynamically created **";
		*/
}
