using System;
using System.Collections.Generic;
using System.Text;

namespace LivingMessiah.Domain
{
	public class PsalmAndProverb
	{
		public int ShabbatWeekId { get; set; }
		public DateTime ShabbatDate { get; set; }
		public string PsalmsBCV { get; set; }
		public int PsalmsChapter { get; set; }
		public string PsalmsKJVHtmlConcat { get; set; }
		public string PsalmsUrl { get; set; }
		public string PsalmsTitle { get; set; }
		public string ProverbsBCV { get; set; }
		public int ProverbsChapter { get; set; }
		public string ProverbsKJVHtmlConcat { get; set; }
		public string ProverbsUrl { get; set; }
	}
}
