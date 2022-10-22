using Ardalis.SmartEnum;
//using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Shared.Header.Enums;

public abstract class BibleWebsite : SmartEnum<BibleWebsite>
{
	#region  Declared Public Instances
	public static readonly BibleWebsite MyHebrewBible = new MyHebrewBibleSE();
	public static readonly BibleWebsite StepBible = new StepBibleSE();
	#endregion

	private BibleWebsite(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	// "https://myhebrewbible.com/BookChapter/" + BibleBook.Title + "/" + chapter + "/slug",
	public abstract string UrlBase { get; }
	public abstract string VersionsOT { get; }
	public abstract string VersionsNT { get; }
	public abstract string UrlSuffix { get; }
	public abstract string UrlTitle { get; }
	#endregion

	#region Private Instantiation
	private sealed class MyHebrewBibleSE : BibleWebsite
	{
		public MyHebrewBibleSE() : base("MyHebrewBible", 1) { }
		public override string UrlBase => "https://myhebrewbible.com/BookChapter/";
		public override string VersionsOT => "";
		public override string VersionsNT => "";
		public override string UrlSuffix => "/slug";
		//public override string FullUrl => $"{UrlBase}{BibleBook.Title}/{BibleBook.chapter}{UrlSuffix}";
		public override string UrlTitle => "MyHebrewbible.com/BookChapter/";
	}

	
	private sealed class StepBibleSE : BibleWebsite
	{
		public StepBibleSE() : base("StepBible", 2) { }
		public override string UrlBase => "https://www.stepbible.org/?q=";
		public override string VersionsOT => "?q=version=KJVA|version=THOT|version=LXX";  // |version=NASB2020
		public override string VersionsNT => "?q=version=KJVA|version=THGNT";
		public override string UrlSuffix => "&options=UVNGVH&display=INTERLEAVED";
		/*
		public override string FullUrlOT => $"{UrlBase}{VersionsOT}|reference={BibleBook.Abrv}.{BibleBook.chapter}{UrlSuffix}";  e.g. Exo.19
		public override string FullUrlNT => $"{UrlBase}{VersionsNT}|reference={BibleBook.Abrv}.{BibleBook.chapter}{UrlSuffix}";  e.g. Acts.28
		*/
		public override string UrlTitle => "StepBible.org";
	}
	#endregion
}

