using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.ThresholdCovenant;

public static class DynamicComponentPaths
{
	public static string BookSectionCards = "LivingMessiah.Web.Features.ThresholdCovenant.BookSections.";
}

public static class Strongs
{
	public static MarkupString H(string number)
	{
		return (MarkupString)$"<sup><a href='https://www.blueletterbible.org/lexicon/{number}/kjv/wlc/0-1/' target='blank' title='Blue Letter Bible WLC Lexicon {number}'>{number}</a></sup>";
	}

	public static MarkupString G(string number)
	{
		return (MarkupString)$"<sup><a href='https://www.blueletterbible.org/lexicon/{number}/kjv/tr/0-1/>' target='blank' title='Blue Letter Bible TR Lexicon {number}'{number}</a></sup>";
	}

}

// Ignore Spelling: Strongs, kjv, wlc