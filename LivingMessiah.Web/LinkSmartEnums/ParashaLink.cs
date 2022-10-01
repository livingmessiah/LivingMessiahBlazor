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
	public static readonly ParashaLink Current = new CurrentSE();
	public static readonly ParashaLink ListByBook = new ListByBookSE();
	public static readonly ParashaLink ListByBookPrintVersion = new ListByBookPrintVersionSE();
	public static readonly ParashaLink SeeAlsoTorahTuesday = new SeeAlsoTorahTuesdaySE();
	public static readonly ParashaLink Archive = new ArchiveSE();
	// SE=SmartEnum
	#endregion

	private ParashaLink(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Page { get; }
	public abstract string NavBarText { get; }
	public abstract string NavBarIcon { get; }
	public abstract string Title { get; }
	public abstract bool Display { get; }
	#endregion

	#region Private Instantiation
	private sealed class CurrentSE : ParashaLink
	{
		public CurrentSE() : base($"{nameof(Id.Current)}", Id.Current) { }
		public override string Page => ParashaPage.Index;
		public override string NavBarText => "Parasha"; 
		public override string NavBarIcon => ParashaPage.Icon;
		public override string Title => "Current Parasha";
		public override bool Display => true;
	}

	private sealed class ListByBookSE : ParashaLink
	{
		public ListByBookSE() : base($"{nameof(Id.ListByBook)}", Id.ListByBook) { }
		public override string Page => ParashaPage.ListByBook.Index;
		public override string NavBarText => "Parasha by Book";
		public override string NavBarIcon => "fas fa-table";
		public override string Title => "";
		public override bool Display => true;
	}

	/*	*/
	private sealed class ListByBookPrintVersionSE : ParashaLink
	{
		public ListByBookPrintVersionSE() : base($"{nameof(Id.ListByBookPrintVersion)}", Id.ListByBookPrintVersion) { }
		public override string Page => "";  // Because Display => false;
		public override string NavBarText => ParashaPage.PrintTable.Title; //"Table by Book Print";
		public override string NavBarIcon => ParashaPage.PrintTable.Icon; 
		public override string Title => ParashaPage.PrintTable.Title;
		public override bool Display => false;
	}


	private sealed class SeeAlsoTorahTuesdaySE : ParashaLink
	{
		public SeeAlsoTorahTuesdaySE() : base($"{nameof(Id.SeeAlsoTorahTuesday)}", Id.SeeAlsoTorahTuesday) { }
		public override string Page => ParashaPage.TorahTuesday.Index;
		public override string NavBarText => ParashaPage.TorahTuesday.Title; 
		public override string NavBarIcon => ParashaPage.TorahTuesday.Icon;
		public override string Title => "";
		public override bool Display => true;
	}

	private sealed class ArchiveSE : ParashaLink
	{
		public ArchiveSE() : base($"{nameof(Id.Archive)}", Id.Archive) { }
		public override string Page => ParashaPage.Archive.Index;
		public override string NavBarText => "Archive";
		public override string NavBarIcon => ParashaPage.Archive.Icon;
		public override string Title => "";
		public override bool Display => true;
	}

	#endregion


}
