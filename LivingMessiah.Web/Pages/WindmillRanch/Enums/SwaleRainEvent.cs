using Ardalis.SmartEnum;

// LivingMessiah.Web.SmartEnums.MediaQuery;

namespace LivingMessiah.Web.Pages.WindmillRanch.Enums;

public abstract class SwaleRainEvent : SmartEnum<SwaleRainEvent>
{

	#region Id's
	private static class Id
	{
		internal const int RampErosion = 1;
		internal const int CheckDam = 2;
		internal const int CornerLookingWest = 3;
		internal const int CornerLookingSouth = 4;
		internal const int SpillwayOffRamp = 5;
		internal const int SpillwayOffRampCloseUp = 6;
		internal const int GroomedBerm = 7;
		internal const int PuddleOnWindmillRd = 8;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly SwaleRainEvent RampErosion = new RampErosionSE();
	public static readonly SwaleRainEvent CheckDam = new CheckDamSE();
	public static readonly SwaleRainEvent CornerLookingWest = new CornerLookingWestSE();
	public static readonly SwaleRainEvent CornerLookingSouth = new CornerLookingSouthSE();
	public static readonly SwaleRainEvent SpillwayOffRamp = new SpillwayOffRampSE();
	public static readonly SwaleRainEvent SpillwayOffRampCloseUp = new SpillwayOffRampCloseUpSE();
	public static readonly SwaleRainEvent GroomedBerm = new GroomedBermSE();
	public static readonly SwaleRainEvent PuddleOnWindmillRd = new PuddleOnWindmillRdSE();
	// SE=SmartEnum
	#endregion

	private SwaleRainEvent(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields

	public abstract string Image { get; }
	public abstract string ImageFullSize { get; }
	public abstract string Caption { get; }
	#endregion

	#region Private Instantiation

	private sealed class RampErosionSE : SwaleRainEvent
	{
		public RampErosionSE() : base($"{nameof(Id.RampErosion)}", Id.RampErosion) { }
		public override string Image => "IMG_20221008_01_on-ramp-erosion-20pct.jpg";
		public override string ImageFullSize => "IMG_20221008_01_on-ramp-erosion.jpg";
		public override string Caption => "On ramp erosion";
	}

	private sealed class CheckDamSE : SwaleRainEvent
	{
		public CheckDamSE() : base($"{nameof(Id.CheckDam)}", Id.CheckDam) { }
		public override string Image => "IMG_20221008_02_check-dam-20pct.jpg";
		public override string ImageFullSize => "IMG_20221008_02_check-dam.jpg";
		public override string Caption => "Check Dam";
	}

	private sealed class CornerLookingWestSE : SwaleRainEvent
	{
		public CornerLookingWestSE() : base($"{nameof(Id.CornerLookingWest)}", Id.CornerLookingWest) { }
		public override string Image => "IMG_20221008_03_corner-looking-W-20pct.jpg";
		public override string ImageFullSize => "IMG_20221008_03_corner-looking-W.jpg";
		public override string Caption => "Corner of Swale Looking West";
	}

	private sealed class CornerLookingSouthSE : SwaleRainEvent
	{
		public CornerLookingSouthSE() : base($"{nameof(Id.CornerLookingSouth)}", Id.CornerLookingSouth) { }
		public override string Image => "IMG_20221008_04_corner-looking-S-20pct.jpg";
		public override string ImageFullSize => "IMG_20221008_04_corner-looking-S.jpg";
		public override string Caption => "Corner of Swale Looking South";
	}

	private sealed class SpillwayOffRampSE : SwaleRainEvent
	{
		public SpillwayOffRampSE() : base($"{nameof(Id.SpillwayOffRamp)}", Id.SpillwayOffRamp) { }
		public override string Image => "IMG_20221008_05_spillway-off-ramp-20pct.jpg";
		public override string ImageFullSize => "IMG_20221008_05_spillway-off-ramp.jpg";
		public override string Caption => "Spillway Off Ramp";
	}

	private sealed class SpillwayOffRampCloseUpSE : SwaleRainEvent
	{
		public SpillwayOffRampCloseUpSE() : base($"{nameof(Id.SpillwayOffRampCloseUp)}", Id.SpillwayOffRampCloseUp) { }
		public override string Image => "IMG_20221008_06_spillway-closeup-20pct.jpg";
		public override string ImageFullSize => "IMG_20221008_06_spillway-closeup.jpg";
		public override string Caption => "Spillway Close-up";
	}

	private sealed class GroomedBermSE : SwaleRainEvent
	{
		public GroomedBermSE() : base($"{nameof(Id.GroomedBerm)}", Id.GroomedBerm) { }
		public override string Image => "IMG_20221008_07_groomed-berm-20pct.jpg";
		public override string ImageFullSize => "IMG_20221008_07_groomed-berm.jpg";
		public override string Caption => "Groomed Berm";
	}

	private sealed class PuddleOnWindmillRdSE : SwaleRainEvent
	{
		public PuddleOnWindmillRdSE() : base($"{nameof(Id.PuddleOnWindmillRd)}", Id.PuddleOnWindmillRd) { }
		public override string Image => "IMG_20221008_08_puddle-on-windmill-rd-20pct.jpg";
		public override string ImageFullSize => "IMG_20221008_08_puddle-on-windmill-rd.jpg";
		public override string Caption => "Puddle on Windmill Road";
	}

	#endregion

}

