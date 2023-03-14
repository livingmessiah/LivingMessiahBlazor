using System;

namespace LivingMessiah.Domain;

public class PsalmsVM
{
	public int Id { get; set; }
	public int BegVerse { get; set; }
	public int EndVerse { get; set; }
	public int VerseCount { get; set; }
	public bool IsWholeChapter { get; set; }
	public string BCV { get; set; }
	public int Chapter { get; set; }
	public string? KJVHtmlConcat { get; set; }
	public int? ShabbatWeekId { get; set; }
	public string? ShabbatDateYMD { get; set; }

	public string ShabbatWeek()
	{
		return $"{(ShabbatWeekId is not null ? ShabbatWeekId.ToString() : "")} / {ShabbatDateYMD ?? ""}";
	}

	public string IsWholeChapterIcon() //MarkupString
	{
		//new MarkupString($"<a href='mailto:{Emails.Info.Email()}{Emails.Info.Subject}'>{Emails.Info.Email()}</a>");
		return $"{(IsWholeChapter ? "<i class='fas fa-check'></i>" : "<i class='far fa-square'></i>")}";
	}


}
