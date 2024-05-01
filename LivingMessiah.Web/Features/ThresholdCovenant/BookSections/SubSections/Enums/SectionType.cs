using Ardalis.SmartEnum;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.ThresholdCovenant.BookSections.SubSections.Enums;

public abstract class SectionType : SmartEnum<SectionType>
{
	#region Id's
	private static class Id
	{
		internal const int Introduction = 0;
		internal const int NewMeaningOldRite = 1;
		internal const int WelcomeWithBlood = 2;
		internal const int BasonThreshold = 3;
		internal const int PassoverOrPassby = 4;
		internal const int MarriageJehovahIsrael = 5;
		internal const int EndNotes = 6;
	}
	#endregion

	#region Declared Public Instances
	public static readonly SectionType Introduction = new IntroductionSE();
	public static readonly SectionType NewMeaningOldRite = new NewMeaningOldRiteSE();
	public static readonly SectionType WelcomeWithBlood = new WelcomeWithBloodSE();
	public static readonly SectionType BasonThreshold = new BasonThresholdSE();
	public static readonly SectionType PassoverOrPassby = new PassoverOrPassbySE();
	public static readonly SectionType MarriageJehovahIsrael = new MarriageJehovahIsraelSE();
	public static readonly SectionType EndNotes = new EndNotesSE();
	// Note; SE=SmartEnum
	#endregion

	private SectionType(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract MarkupString TitleHtml { get; }
	public abstract bool IsStartOfEdge { get; }
	public abstract bool IsEndOfEdge { get; }
	#endregion


	#region Private Instantiation

	private sealed class IntroductionSE : SectionType
	{
		public IntroductionSE() : base($"{nameof(Id.Introduction)}", Id.Introduction) { }
		public override string Title => nameof(Id.Introduction);
		public override MarkupString TitleHtml => new MarkupString($"{Title}");
		public override bool IsStartOfEdge => true;
		public override bool IsEndOfEdge => false;
	}

	private sealed class NewMeaningOldRiteSE : SectionType
	{
		public NewMeaningOldRiteSE() : base($"{nameof(Id.NewMeaningOldRite)}", Id.NewMeaningOldRite) { }
		public override string Title => "New Meaning In An Old Rite";
		public override MarkupString TitleHtml => new MarkupString($"New Meaning <br /> in an <br /> Old Rite"); //  New Meaning in <br /> an Old Rite
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;
	}

	private sealed class WelcomeWithBloodSE : SectionType
	{
		public WelcomeWithBloodSE() : base($"{nameof(Id.WelcomeWithBlood)}", Id.WelcomeWithBlood) { }
		public override string Title => "A Welcome With Blood";
		public override MarkupString TitleHtml => new MarkupString($"A Welcome <br /> With Blood");
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;

	}

	private sealed class BasonThresholdSE : SectionType
	{
		public BasonThresholdSE() : base($"{nameof(Id.BasonThreshold)}", Id.BasonThreshold) { }
		public override string Title => "Bason Or Threshold";
		public override MarkupString TitleHtml => new MarkupString($"Bason <br /> or <br /> Threshold"); // Bason or <br /> Threshold
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;
	}

	private sealed class PassoverOrPassbySE : SectionType
	{
		public PassoverOrPassbySE() : base($"{nameof(Id.PassoverOrPassby)}", Id.PassoverOrPassby) { }
		public override string Title => "Pass-over Or Pass-by";
		public override MarkupString TitleHtml => new MarkupString($"Pass-over <br /> or <br /> Pass-by"); // Pass-over or <br /> Pass-by
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;
	}

	private sealed class MarriageJehovahIsraelSE : SectionType
	{
		public MarriageJehovahIsraelSE() : base($"{nameof(Id.MarriageJehovahIsrael)}", Id.MarriageJehovahIsrael) { }
		public override string Title => "Marriage of Jehovah with Israel";
		public override MarkupString TitleHtml => new MarkupString($"Marriage <br /> of Yehovah <br /> with Israel");  // Marriage of Yehovah <br /> with Israel
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;
	}

	private sealed class EndNotesSE : SectionType
	{
		public EndNotesSE() : base($"{nameof(Id.EndNotes)}", Id.EndNotes) { }
		public override string Title => "End Notes";
		public override MarkupString TitleHtml => new MarkupString($"End <br /> Notes");
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => true;

	}

	#endregion
}

// Ignore Spelling: Bason, Passby