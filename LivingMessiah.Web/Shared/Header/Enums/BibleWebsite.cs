using Ardalis.SmartEnum;
//using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Shared.Header.Enums;

public abstract class BibleWebsite : SmartEnum<BibleWebsite>
{
	#region  Declared Public Instances
	public static readonly BibleWebsite MyHebrewBible = new MyHebrewBibleSE();
	public static readonly BibleWebsite StepBible = new StepBibleSE();
	public static readonly BibleWebsite StepBibleSpanish = new StepBibleSpanishSE();
	#endregion

	private BibleWebsite(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	// "https://myhebrewbible.com/BookChapter/" + BibleBook.Title + "/" + chapter + "/slug",
	public abstract string Title { get; }
	public abstract string UrlBase { get; }
	public abstract string UrlTitle { get; }
	#endregion

	#region Private Instantiation
	private sealed class MyHebrewBibleSE : BibleWebsite
	{
		public MyHebrewBibleSE() : base("MyHebrewBible", 1) { }
		public override string Title => "My Hebrew Bible";
		public override string UrlBase => "https://myhebrewbible.com/BookChapter/";
		public override string UrlTitle => "MyHebrewbible.com/BookChapter/";
	}

	private sealed class StepBibleSE : BibleWebsite
	{
		public StepBibleSE() : base("StepBible", 2) { }
		public override string Title => "Step Bible";
		public override string UrlBase => "https://www.stepbible.org/";
		public override string UrlTitle => "StepBible.org";
	}

	// https://www.stepbible.org/?q=version=LBLA|version=ESV|reference=Gen.1&options=HNVUG
	// https://www.stepbible.org/?q=version=LBLA|reference=Gen.1&options=NVUGVH&display=INTERLEAVED
	private sealed class StepBibleSpanishSE : BibleWebsite
	{
		public StepBibleSpanishSE() : base("StepBibleSpanish", 3) { }
		public override string Title => "Step Bible Español";
		public override string UrlBase => "https://www.stepbible.org/";
		public override string UrlTitle => "StepBible.org";
	}

	#endregion
}
