using Ardalis.SmartEnum;
using ParashaPage = LivingMessiah.Web.Links.Parasha;

namespace LivingMessiah.Web.LinkSmartEnums;


public abstract class ParashaLink : SmartEnum<ParashaLink>
{
	#region Id's
	private static class Id
	{
		internal const int Current = 1;  // default i.e. Index
		internal const int ListByBook = 2;
	//	internal const int ListByBookPrintVersion = 3;
		internal const int SeeAlsoTorahTuesday = 4;
		internal const int Archive = 5;
		/*
		internal const int ArchiveIndex = 6;
		internal const int ArchiveGenesis = 6;
		*/
	}
	#endregion

	#region  Declared Public Instances
	public static readonly ParashaLink Current = new CurrentSE();
	public static readonly ParashaLink ListByBook = new ListByBookSE();
	//public static readonly Parasha ListByBookPrintVersion = new ListByBookPrintVersionSE();
	public static readonly ParashaLink SeeAlsoTorahTuesday = new SeeAlsoTorahTuesdaySE();
	public static readonly ParashaLink Archive = new ArchiveSE();
	// SE=SmartEnum
	#endregion

	private ParashaLink(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Color { get; }  // badge bg-dark
	public abstract string Page { get; }
	public abstract string Title { get; }
	//public abstract string Icon { get; }
	#endregion

	#region Private Instantiation
	private sealed class CurrentSE : ParashaLink
	{
		public CurrentSE() : base($"{nameof(Id.Current)}", Id.Current) { }
		public override string Color => "dark";
		public override string Page => ParashaPage.Index;
		public override string Title => "Current Parasha"; // ParashaPage.Title;
		//public override string Icon => ParashaPage.Icon;
	}

	private sealed class ListByBookSE : ParashaLink
	{
		public ListByBookSE() : base($"{nameof(Id.ListByBook)}", Id.ListByBook) { }
		public override string Color => "success";
		public override string Page => ParashaPage.ListByBook.Index;
		public override string Title => ParashaPage.ListByBook.Title;
	}

	/*
	private sealed class ListByBookPrintVersionSE : Parasha
	{
		public ListByBookPrintVersionSE() : base($"{nameof(Id.ListByBookPrintVersion)}", Id.ListByBookPrintVersion) { }
		public override string Color => "warning";
		public override string Page => ParashaPage.ListByBookPrint.Index;
		public override string Title => ParashaPage.ListByBookPrint.Title;
	}
	*/

	private sealed class SeeAlsoTorahTuesdaySE : ParashaLink
	{
		public SeeAlsoTorahTuesdaySE() : base($"{nameof(Id.SeeAlsoTorahTuesday)}", Id.SeeAlsoTorahTuesday) { }
		public override string Color => "info";
		public override string Page => ParashaPage.SeeAlsoSeeAlsoTorahTuesday.Index;
		public override string Title => ParashaPage.SeeAlsoSeeAlsoTorahTuesday.Title;
	}

	private sealed class ArchiveSE : ParashaLink
	{
		public ArchiveSE() : base($"{nameof(Id.Archive)}", Id.Archive) { }
		public override string Color => "danger";
		public override string Page => ParashaPage.Archive.Index;
		public override string Title => ParashaPage.Archive.Title;
	}

	#endregion


}
