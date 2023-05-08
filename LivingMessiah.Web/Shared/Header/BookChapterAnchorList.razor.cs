using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Shared.Header.Enums;
using LivingMessiah.Web.Shared.Header.Store;

namespace LivingMessiah.Web.Shared.Header;

public partial class BookChapterAnchorList
{
	[Inject] private IState<ToolbarState>? ToolbarState { get; set; }

	protected string AnchorBookChapterUrl(int chapter)
	{
		if (ToolbarState!.Value.BibleWebsite == BibleWebsite.MyHebrewBible)
		{
			return $"{BibleWebsite.MyHebrewBible.UrlBase}{ToolbarState.Value.BibleBook!.Title}/{chapter}{UrlSuffix(true)}";
		}
		else
		{
			// https://www.stepbible.org/?q=version=LBLA|reference=Gen.1&options=NVUGVH&display=INTERLEAVED
			//https://www.stepbible.org/?q=version=LBLA|reference=Matt.3&options=HNVUG
			if (ToolbarState.Value.BibleWebsite == BibleWebsite.StepBibleSpanish)
			{
				if (ToolbarState.Value.BibleBook!.Value < 40)
				{ 
					return $"{BibleWebsite.StepBibleSpanish.UrlBase}?q=version=LBLA|reference={ToolbarState.Value.BibleBook.Abrv}.{chapter}&options=NVUGVH&display=INTERLEAVED"; 
				}
				else
				{
					return $"{BibleWebsite.StepBibleSpanish.UrlBase}?q=version=LBLA|reference={ToolbarState.Value.BibleBook.Abrv}.{chapter}&options=HNVUG";
				}
					
			}
			else
			{
				if (ToolbarState.Value.BibleBook!.Value < 40)
				{
					return $"{BibleWebsite.StepBible.UrlBase}{Versions(true)}|reference={ToolbarState.Value.BibleBook.Abrv}.{chapter}{UrlSuffix(true)}";
				}
				else
				{
					return $"{BibleWebsite.StepBible.UrlBase}{Versions(false)}|reference={ToolbarState.Value.BibleBook.Abrv}.{chapter}{UrlSuffix(false)}";
				}
			}

		}
	}

	protected string BookChapterTitle(int chapter)
	{
		if (ToolbarState!.Value.BibleWebsite == BibleWebsite.MyHebrewBible)
		{
			return $"{BibleWebsite.MyHebrewBible.UrlTitle}{ToolbarState.Value.BibleBook!.Title}/{chapter}{UrlSuffix(true)}";
		}
		else
		{
			if (ToolbarState.Value.BibleBook!.Value < 40)
			{
				return $"{BibleWebsite.StepBible.UrlTitle} {ToolbarState.Value.BibleBook.Abrv}.{chapter} OT";
			}
			else
			{
				return $"{BibleWebsite.StepBible.UrlTitle} {ToolbarState.Value.BibleBook.Abrv}.{chapter} NT";
			}
			
		}
	}

	private string Versions(bool isOT) 
	{
		if (ToolbarState!.Value.BibleWebsite == BibleWebsite.MyHebrewBible)
		{
			return "";
		}
		else
		{
			if (isOT)
			{
				return "?q=version=KJVA|version=THOT|version=LXX";
			}
			else
			{
				return "?q=version=KJVA|version=THGNT";
			}
		}
	}

	private string UrlSuffix(bool isOT)
	{
		if (ToolbarState!.Value.BibleWebsite == BibleWebsite.MyHebrewBible)
		{
			return "/slug";
		}
		else
		{
			if (isOT)
			{
				return "&options=UVNGVH&display=INTERLEAVED";
			}
			else
			{
				return "&options=GVUNH";
			}
		}
	}

	//{ (ToolbarState.Value.BibleBook.Value < 40 ? $"{BibleWebsite.StepBible.VersionsOT}" : $"{BibleWebsite.StepBible.VersionsNT}" )}

}
