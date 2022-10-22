using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Enums;
using LivingMessiah.Web.Shared.Header.Enums;

namespace LivingMessiah.Web.Shared.Header;

public partial class BookChapterAnchorList
{
	[Parameter, EditorRequired] public BibleBook BibleBook { get; set; }
	[Parameter, EditorRequired] public BibleWebsite BibleWebsite { get; set; }

	protected string AnchorBookChapterUrl(int chapter)
	{
		if (BibleWebsite == BibleWebsite.MyHebrewBible)
		{
			return $"{BibleWebsite.UrlBase}{BibleBook.Title}/{chapter}{BibleWebsite.UrlSuffix}";
		}
		else
		{
			return $"{BibleWebsite.UrlBase} { (chapter < 40 ? $"{BibleWebsite.VersionsOT}" : $"{BibleWebsite.VersionsNT}" )}|reference={BibleBook.Abrv}.{chapter}{BibleWebsite.UrlSuffix}";
		}
	}

	protected string BookChapterTitle(int chapter)
	{
		if (BibleWebsite == BibleWebsite.MyHebrewBible)
		{
			return $"{BibleWebsite.UrlTitle}{BibleBook.Title}/{chapter}{BibleWebsite.UrlSuffix}";
		}
		else
		{
			return $"{BibleWebsite.UrlTitle} {BibleBook.Abrv}.{chapter}";
		}
	}
}
