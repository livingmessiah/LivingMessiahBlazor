using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.ThresholdCovenant.Enums;

public abstract class NavItem : SmartEnum<NavItem>
{
	#region Id's
	private static class Id
	{
		internal const int PrimativeFamilyAltar = 1;
		internal const int EarliestTempleAltar = 2;
		internal const int SacredBoundaryLine = 3;
		internal const int OriginOfTheRite = 4;
		internal const int HebrewPassOverOrCrossOver = 5;
		internal const int ChristianPassOver = 6;
		internal const int OutgrowthsAndPerversions = 7;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly NavItem PrimativeFamilyAltar = new PrimativeFamilyAltarSE();
	public static readonly NavItem EarliestTempleAltar = new EarliestTempleAltarSE();
	public static readonly NavItem SacredBoundaryLine = new SacredBoundaryLineSE();
	public static readonly NavItem OriginOfTheRite = new OriginOfTheRiteSE();
	public static readonly NavItem HebrewPassOverOrCrossOver = new HebrewPassOverOrCrossOverSE();
	public static readonly NavItem ChristianPassOver = new ChristianPassOverSE();
	public static readonly NavItem OutgrowthsAndPerversions = new OutgrowthsAndPerversionsSE();

	// SE=SmartEnum
	#endregion

	private NavItem(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string ButtonId { get; }
	public abstract string Target { get; }
	public abstract string Title { get; }
	public abstract string AriaControls { get; }
	#endregion

	#region Private Instantiation
	private sealed class PrimativeFamilyAltarSE : NavItem
	{
		public PrimativeFamilyAltarSE() : base($"{nameof(Id.PrimativeFamilyAltar)}", Id.PrimativeFamilyAltar) { }
		public override string ButtonId => $"{nameof(Id.PrimativeFamilyAltar).ToLower()}-tab";
		public override string Target => $"#{nameof(Id.PrimativeFamilyAltar).ToLower()}";
		public override string Title => "Primative Family Altar";
		public override string AriaControls => $"{nameof(Id.PrimativeFamilyAltar).ToLower()}";

	}

	private sealed class EarliestTempleAltarSE : NavItem
	{
		public EarliestTempleAltarSE() : base($"{nameof(Id.EarliestTempleAltar)}", Id.EarliestTempleAltar) { }
		public override string ButtonId => $"{nameof(Id.EarliestTempleAltar).ToLower()}-tab";
		public override string Target => $"#{nameof(Id.EarliestTempleAltar).ToLower()}";
		public override string Title => "Earliest Temple Altar";
		public override string AriaControls => $"{nameof(Id.EarliestTempleAltar).ToLower()}";
	}

	private sealed class SacredBoundaryLineSE : NavItem
	{
		public SacredBoundaryLineSE() : base($"{nameof(Id.SacredBoundaryLine)}", Id.SacredBoundaryLine) { }
		public override string ButtonId => $"{nameof(Id.SacredBoundaryLine).ToLower()}-tab";
		public override string Target => $"#{nameof(Id.SacredBoundaryLine).ToLower()}";
		public override string Title => "Sacred Boundary Line";
		public override string AriaControls => $"{nameof(Id.SacredBoundaryLine).ToLower()}";
	}

	private sealed class OriginOfTheRiteSE : NavItem
	{
		public OriginOfTheRiteSE() : base($"{nameof(Id.OriginOfTheRite)}", Id.OriginOfTheRite) { }
		public override string ButtonId => $"{nameof(Id.OriginOfTheRite).ToLower()}-tab";
		public override string Target => $"#{nameof(Id.OriginOfTheRite).ToLower()}";
		public override string Title => "Origin of the Rite";
		public override string AriaControls => $"{nameof(Id.OriginOfTheRite).ToLower()}";
	}

	private sealed class HebrewPassOverOrCrossOverSE : NavItem
	{
		public HebrewPassOverOrCrossOverSE() : base($"{nameof(Id.HebrewPassOverOrCrossOver)}", Id.HebrewPassOverOrCrossOver) { }
		public override string ButtonId => $"{nameof(Id.HebrewPassOverOrCrossOver).ToLower()}-tab";
		public override string Target => $"#{nameof(Id.HebrewPassOverOrCrossOver).ToLower()}";
		public override string Title => "Hebrew Pass-over or Cross-over";
		public override string AriaControls => $"{nameof(Id.HebrewPassOverOrCrossOver).ToLower()}";
	}

	private sealed class ChristianPassOverSE : NavItem
	{
		public ChristianPassOverSE() : base($"{nameof(Id.ChristianPassOver)}", Id.ChristianPassOver) { }
		public override string ButtonId => $"{nameof(Id.ChristianPassOver).ToLower()}-tab";
		public override string Target => $"#{nameof(Id.ChristianPassOver).ToLower()}";
		public override string Title => "Christian PassOver";
		public override string AriaControls => $"{nameof(Id.ChristianPassOver).ToLower()}";
	}

	private sealed class OutgrowthsAndPerversionsSE : NavItem
	{
		public OutgrowthsAndPerversionsSE() : base($"{nameof(Id.OutgrowthsAndPerversions)}", Id.OutgrowthsAndPerversions) { }
		public override string ButtonId => $"{nameof(Id.OutgrowthsAndPerversions).ToLower()}-tab";
		public override string Target => $"#{nameof(Id.OutgrowthsAndPerversions).ToLower()}";
		public override string Title => "Outgrowths and Perversions";
		public override string AriaControls => $"{nameof(Id.OutgrowthsAndPerversions).ToLower()}";
	}


	#endregion

	// Ignore Spelling: 
}
