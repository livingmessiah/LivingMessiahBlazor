using System;

namespace LivingMessiah.Web.Pages.PsalmsAndProverbs;

public class vwPsalmsAndProverbs
{
	public int ShabbatWeekId { get; set; }
	public DateTime ShabbatDate { get; set; }
	public string? ShabbatDateYMD { get; set; }

	public string? PsalmsBCV { get; set; }
	public int PsalmsChapter { get; set; }
	//public int PsalmsBegVerse { get; set; }
	//public int PsalmsEndVerse { get; set; }
	public int PslamsVerseCount { get; set; }
	public bool IsWholeChapter { get; set; }
	//public string? PsalmsKJVHtmlConcat { get; set; }
	public string? PsalmsUrl { get; set; }
	public string? PsalmsTitle { get; set; }

	public string? ProverbsBCV { get; set; }
	public int ProverbsVerseCount { get; set; }
	public int ProverbsChapter { get; set; }
	//public int ProverbsBegVerse { get; set; }
	//public int ProverbsEndVerse { get; set; }
	//public string? ProverbsKJVHtmlConcat { get; set; }
	public string? ProverbsUrl { get; set; }

	public int TotalVerseCount { get; set; }
}
