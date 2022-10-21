using Ardalis.SmartEnum;
using LivingMessiah.Web.Enums;

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
	public abstract string VersionsNT { get; }
	public abstract string VersionsOT { get; }
	//public abstract BibleBook BibleBook { get; }
	//public abstract BookEnum BookEnum { get; }
	//public string Dump
	//{
	//	get
	//	{
	//		return $" {this.Value}-{this.Abrv}-{this.Name}-{this.BookGroupEnum}";
	//	}
	//}
	#endregion

	#region Private Instantiation
	private sealed class MyHebrewBibleSE : BibleWebsite
	{
		public MyHebrewBibleSE() : base("MyHebrewBible", 1) { }
		public override string UrlBase => "https://myhebrewbible.com/BookChapter/"; 
		public override string VersionsNT => "VersionsNT??";
		public override string VersionsOT => "VersionsOT??";
		//public override BibleBook BibleBook => BookGroupEnum.Torah;
		//public override BookEnum BookEnum => BookEnum.Genesis;
	}

	private sealed class StepBibleSE : BibleWebsite
	{
		public StepBibleSE() : base("StepBible", 2) { }
		public override string UrlBase => "https://www.stepbible.org/?q/";
		public override string VersionsNT => "VersionsNT??";
		public override string VersionsOT => "VersionsOT??";
		//public override BibleBook BibleBook => BookGroupEnum.Torah;
		//public override BookEnum BookEnum => BookEnum.Genesis;
	}
	#endregion
}
