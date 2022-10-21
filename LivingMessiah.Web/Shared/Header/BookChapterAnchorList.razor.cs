using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Shared.Header;

public partial class BookChapterAnchorList
{
	[Parameter, EditorRequired] public BibleBook BibleBook { get; set; }

	[Parameter, EditorRequired] public BibleSource BibleSource { get; set; }

	private const string VersionOT = "version=THOT|version=NASB2020|version=LXX";
	private const string VersionNT = "version=THOT|version=NASB2020|version=LXX";

	protected string AnchorBookChapterUrl(int chapter)
	{
		return BibleSource
			switch
			{
				BibleSource.MyHebrewBible => "https://myhebrewbible.com/BookChapter/" + BibleBook.Title + "/" + chapter + "/slug",
				BibleSource.StepBible => "https://www.stepbible.org/?q=version=KJVA|version=THOT|version=NASB2020|version=LXX|reference=" + BibleBook.Abrv + "." + chapter + "&options=UVNGHV&display=INTERLEAVED",
				_ => "https://myhebrewbible.com/BookChapter/" + BibleBook.Title + "/" + chapter + "/slug",
			};
	}

	protected string BookChapterTitle(int chapter)
	{
		return BibleSource
	switch
		{
			BibleSource.MyHebrewBible => "MyHebrewBible.com/BookChapter/" + BibleBook.Title + "/" + chapter,
			BibleSource.StepBible => "https://www.stepbible.org/ KJVA, THOT, NASB2020, LXX" + BibleBook.Abrv + "." + chapter,
			_ => "MyHebrewBible.com/BookChapter/" + BibleBook.Title + "/" + chapter,
		};

	}
}

public enum BibleSource
{
	MyHebrewBible = 1,
	StepBible = 2  // https://www.stepbible.org/?q=version=KJVA|version=THOT|version=NASB2020|version=THGNT|reference=Acts.28&options=UVNGVH&display=INTERLEAVED
}

