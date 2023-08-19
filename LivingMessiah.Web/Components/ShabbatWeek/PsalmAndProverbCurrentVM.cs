using System;

namespace LivingMessiah.Web.Components.ShabbatWeek;

public class PsalmAndProverbCurrentVM
{
	public int ShabbatWeekId { get; set; }
	public DateTime ShabbatDate { get; set; }
	public string? ShabbatDateYMD { get; set; }

	public string? PsalmsBCV { get; set; }
	public int PsalmsChapter { get; set; }
	public int PsalmsVerseCount { get; set; }
	public bool IsWholeChapter { get; set; }
	public string? PsalmsKJVHtmlConcat { get; set; }
	public string? PsalmsUrl { get; set; }
	public string? PsalmsTitle { get; set; }

	public string? ProverbsBCV { get; set; }
	public int ProverbsVerseCount { get; set; }
	public int ProverbsChapter { get; set; }
	public string? ProverbsKJVHtmlConcat { get; set; }
	public string? ProverbsUrl { get; set; }

	public int TotalVerseCount { get; set; }
}

// Ignore Spelling: Concat