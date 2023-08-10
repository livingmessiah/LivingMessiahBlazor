using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Enums;

public abstract class BibleGroup : SmartEnum<BibleGroup>
{

	#region Id's
	private static class Id
	{
		internal const int Torah = 1;
		internal const int History = 2;
		internal const int Poetry = 3;
		internal const int MajorProphets = 4;
		internal const int MinorProphets = 5;
		internal const int Gospels = 6;
		internal const int PaulsEpistles = 7;
		internal const int GeneralEpistles = 8;
		internal const int Apocalypse = 9;
	}
	#endregion


	#region  Declared Public Instances
	public static readonly BibleGroup Torah = new TorahSE();
	public static readonly BibleGroup History = new HistorySE();
	public static readonly BibleGroup Poetry = new PoetrySE();
	public static readonly BibleGroup MajorProphets = new MajorProphetsSE();
	public static readonly BibleGroup MinorProphets = new MinorProphetsSE();
	public static readonly BibleGroup Gospels = new GospelsSE();
	public static readonly BibleGroup PaulsEpistles = new PaulsEpistlesSE();
	public static readonly BibleGroup GeneralEpistles = new GeneralEpistlesSE();
	public static readonly BibleGroup Apocalypse = new ApocalypseSE();
	// SE=SmartEnum
	#endregion

	private BibleGroup(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	//public abstract string Title { get; }
	#endregion

	#region Private Instantiation

	private sealed class TorahSE : BibleGroup
	{
		public TorahSE() : base("Torah", Id.Torah) { }
		//public override string Title => "Genesis";
	}

	private sealed class HistorySE : BibleGroup
	{
		public HistorySE() : base("History", Id.History) { }
	}

	private sealed class PoetrySE : BibleGroup
	{
		public PoetrySE() : base("Poetry", Id.Poetry) { }
	}

	private sealed class MajorProphetsSE : BibleGroup
	{
		public MajorProphetsSE() : base("MajorProphets", Id.MajorProphets) { }
	}

	private sealed class MinorProphetsSE : BibleGroup
	{
		public MinorProphetsSE() : base("MinorProphets", Id.MinorProphets) { }
	}

	private sealed class GospelsSE : BibleGroup
	{
		public GospelsSE() : base("Gospels", Id.Gospels) { }
	}

	private sealed class PaulsEpistlesSE : BibleGroup
	{
		public PaulsEpistlesSE() : base("PaulsEpistles", Id.PaulsEpistles) { }
	}

	private sealed class GeneralEpistlesSE : BibleGroup
	{
		public GeneralEpistlesSE() : base("GeneralEpistles", Id.GeneralEpistles) { }
	}

	private sealed class ApocalypseSE : BibleGroup
	{
		public ApocalypseSE() : base("Apocalypse", Id.Apocalypse) { }
	}

	#endregion
}


// Ignore Spelling: Pauls