using Ardalis.SmartEnum;
using PageLink = LivingMessiah.Web.Pages.Parasha.LinkSmartEnums.ParashaLinks;

namespace LivingMessiah.Web.Pages.Parasha.LinkSmartEnums;

public abstract class Parasha : SmartEnum<Parasha>
{
	#region Id's
	private static class Id
	{
		internal const int Current = 1;  // default i.e. Index
		internal const int ListByBook = 2;
		internal const int ListByBookPrintVersion = 3;
		internal const int SeeAlsoTorahTuesday = 4;
		internal const int Archive = 5;
		/*
		internal const int ArchiveIndex = 6;
		internal const int ArchiveGenesis = 6;
		*/
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Parasha Current = new CurrentSE();
	public static readonly Parasha ListByBook = new ListByBookSE();
	public static readonly Parasha ListByBookPrintVersion = new ListByBookPrintVersionSE();
	public static readonly Parasha SeeAlsoTorahTuesday = new SeeAlsoTorahTuesdaySE();
	public static readonly Parasha Archive = new ArchiveSE();
	// SE=SmartEnum
	#endregion

	private Parasha(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Page { get; }
	public abstract string NavBarText { get; }
	public abstract string NavBarIcon { get; }
	public abstract string Title { get; }
	public abstract bool Display { get; }
	#endregion

	#region Private Instantiation
	private sealed class CurrentSE : Parasha
	{
		public CurrentSE() : base($"{nameof(Id.Current)}", Id.Current) { }
		public override string Page => PageLink.Index;
		public override string NavBarText => "Current Parasha"; 
		public override string NavBarIcon => PageLink.IconCurrent;
		public override string Title => "Current Parasha";
		public override bool Display => true;
	}

	private sealed class ListByBookSE : Parasha
	{
		public ListByBookSE() : base($"{nameof(Id.ListByBook)}", Id.ListByBook) { }
		public override string Page => PageLink.ListByBook.Index;
		public override string NavBarText => "Parasha by Book";
		public override string NavBarIcon => "fas fa-table";
		public override string Title => "";
		public override bool Display => true;
	}

	/*	*/
	private sealed class ListByBookPrintVersionSE : Parasha
	{
		public ListByBookPrintVersionSE() : base($"{nameof(Id.ListByBookPrintVersion)}", Id.ListByBookPrintVersion) { }
		public override string Page => "";  // Because Display => false;

		public override string NavBarText => PageLink.PrintTable.Title; 
		public override string NavBarIcon => PageLink.PrintTable.Icon;
		public override string Title => PageLink.PrintTable.Title;

		/*
		public override string NavBarText => ParashaPage.PrintTable.Title; //"Table by Book Print";
		public override string NavBarIcon => ParashaPage.PrintTable.Icon; 
		public override string Title => ParashaPage.PrintTable.Title;
		*/
		public override bool Display => false;
	}


	private sealed class SeeAlsoTorahTuesdaySE : Parasha
	{
		public SeeAlsoTorahTuesdaySE() : base($"{nameof(Id.SeeAlsoTorahTuesday)}", Id.SeeAlsoTorahTuesday) { }
		public override string Page => PageLink.TorahTuesday.Index;
		public override string NavBarText => PageLink.TorahTuesday.Title; 
		public override string NavBarIcon => PageLink.TorahTuesday.Icon;
		public override string Title => "";
		public override bool Display => true;
	}

	private sealed class ArchiveSE : Parasha
	{
		public ArchiveSE() : base($"{nameof(Id.Archive)}", Id.Archive) { }
		public override string Page => PageLink.Archive.Index;
		public override string NavBarText => "Archive";
		public override string NavBarIcon => PageLink.Archive.Icon;
		public override string Title => "";
		public override bool Display => true;
	}

	#endregion


}
