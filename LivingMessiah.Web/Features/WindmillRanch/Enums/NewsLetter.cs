using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.WindmillRanch.Enums;

public abstract class NewsLetter : SmartEnum<NewsLetter>
{

	#region Id's
	private static class Id
	{
		internal const int SwEnd = 1;
		internal const int ByWell = 2;
		internal const int Corner = 3;
		internal const int North = 4;
		internal const int Spillway = 5;
		internal const int SpillwayDamage1 = 6;
		internal const int SpillwayDamage2 = 7;
		internal const int FullSwale1 = 8;
		internal const int FullSwale2 = 9;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly NewsLetter SwEnd = new SwEndSE();
	public static readonly NewsLetter ByWell = new ByWellSE();
	public static readonly NewsLetter Corner = new CornerSE();
	public static readonly NewsLetter North = new NorthSE();
	public static readonly NewsLetter Spillway = new SpillwaySE();
	public static readonly NewsLetter SpillwayDamage1 = new SpillwayDamage1SE();
	public static readonly NewsLetter SpillwayDamage2 = new SpillwayDamage2SE();
	public static readonly NewsLetter FullSwale1 = new FullSwale1SE();
	public static readonly NewsLetter FullSwale2 = new FullSwale2SE();
	// SE=SmartEnum
	#endregion

	private NewsLetter(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields

	public abstract string Image { get; }
	public abstract string Caption { get; }
	public abstract bool IsVideo { get; }
	public abstract string Video { get; }
	#endregion

	#region Private Instantiation

	private sealed class SwEndSE : NewsLetter
	{
		public SwEndSE() : base($"{nameof(Id.SwEnd)}", Id.SwEnd) { }
		public override string Image => "IMG_20230913_0800-SW-End.jpg";
		public override string Caption => "S.W. End looking north";
		public override bool IsVideo => true;
		public override string Video => "VID_20230913_0800-SW-End.mp4";
	}

	private sealed class ByWellSE : NewsLetter
	{
		public ByWellSE() : base($"{nameof(Id.ByWell)}", Id.ByWell) { }
		public override string Image => "IMG_20230913_0802-by-Well.jpg";
		public override string Caption => "By the well";
		public override bool IsVideo => true;
		public override string Video => "VID_20230913_0802-by-Well.mp4";
	}

	private sealed class CornerSE : NewsLetter
	{
		public CornerSE() : base($"{nameof(Id.Corner)}", Id.Corner) { }
		public override string Image => "IMG_20230913_0804-corner.jpg";
		public override string Caption => "Corner of Swale West";
		public override bool IsVideo => true;
		public override string Video => "VID_20230913_0804-corner.mp4";
	}

	private sealed class NorthSE : NewsLetter
	{
		public NorthSE() : base($"{nameof(Id.North)}", Id.North) { }
		public override string Image => "IMG_20230913_0805-North.jpg";
		public override string Caption => "North section of Swale";
		public override bool IsVideo => true;
		public override string Video => "VID_20230913_0805-North.mp4";
	}

	private sealed class SpillwaySE : NewsLetter
	{
		public SpillwaySE() : base($"{nameof(Id.Spillway)}", Id.Spillway) { }
		public override string Image => "IMG_20230913_0820-Spillway.jpg";
		public override string Caption => "Spillway Off Ramp";
		public override bool IsVideo => true;
		public override string Video => "VID_20230913_0820-Spillway.mp4";
	}

	private sealed class SpillwayDamage1SE : NewsLetter
	{
		public SpillwayDamage1SE() : base($"{nameof(Id.SpillwayDamage1)}", Id.SpillwayDamage1) { }
		public override string Image => "IMG_20230913_0823_Spillway-damage-1-1200x900.jpg";
		public override string Caption => "Damaged Spillway Close-up";
		public override bool IsVideo => false;
		public override string Video => "";
	}

	private sealed class SpillwayDamage2SE : NewsLetter
	{
		public SpillwayDamage2SE() : base($"{nameof(Id.SpillwayDamage2)}", Id.SpillwayDamage2) { }
		public override string Image => "IMG_20230913_0824_Spillway-damage-2-900x1200.jpg";
		public override string Caption => "Damaged Spillway Looking West";
		public override bool IsVideo => false;
		public override string Video => "";
	}

	private sealed class FullSwale1SE : NewsLetter
	{
		public FullSwale1SE() : base($"{nameof(Id.FullSwale1)}", Id.FullSwale1) { }
		public override string Image => "IMG_20230913_0826_Full-Swale-1-1200x900.jpg";
		public override string Caption => "Looking S.W.";
		public override bool IsVideo => false;
		public override string Video => "";
	}

	private sealed class FullSwale2SE : NewsLetter
	{
		public FullSwale2SE() : base($"{nameof(Id.FullSwale2)}", Id.FullSwale2) { }
		public override string Image => "IMG_20230913_0826_Full-Swale-2-1200x900.jpg";
		public override string Caption => "Looking South";
		public override bool IsVideo => false;
		public override string Video => "";
	}

	#endregion

}

