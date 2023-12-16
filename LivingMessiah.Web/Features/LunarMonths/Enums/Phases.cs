using Ardalis.SmartEnum;
using System;

namespace LivingMessiah.Web.Features.LunarMonths.Enums;

public abstract class Phases : SmartEnum<Phases>
{

	#region Id's
	private static class Id
	{

		internal const int New = 1; //🌑
		internal const int WaxingCrescent = 2;//🌒
		internal const int FirstQuarter = 3;//🌓
		internal const int WaxingGibbous = 4;//🌔
		internal const int Full = 5;//🌕
		internal const int WaningCrescent = 6;//🌘
		internal const int ThirdQuarter = 7;//🌗
		internal const int WaningGibbous = 8;//🌖
	}
	#endregion

	#region Declared Public Instances
	public static readonly Phases New = new NewSE();
	public static readonly Phases WaxingCrescent = new WaxingCrescentSE();
	public static readonly Phases FirstQuarter = new FirstQuarterSE();
	public static readonly Phases WaxingGibbous = new WaxingGibbousSE();
	public static readonly Phases Full = new FullSE();
	public static readonly Phases WaningCrescent = new WaningCrescentSE();
	public static readonly Phases ThirdQuarter = new ThirdQuarterSE();
	public static readonly Phases WaningGibbous = new WaningGibbousSE();
	#endregion

	private Phases(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	//public abstract string Icon { get; }
	public abstract string Emoji { get; }  // https://www.dotnetcatch.com/2019/06/04/visual-studio-quicktip-add-emoji-to-your-source-code/
	public abstract string Title { get; }
	public abstract int Day { get; }  // 3.635

	#endregion

	#region Private Instantiation
	private sealed class NewSE : Phases
	{
		public NewSE() : base($"{nameof(Id.New)}", Id.New) { }
		//public override string Icon => "fas fa-snowflake";
		public override string Emoji => "🌑";
		public override string Title => "New";
		public override int Day => 1;  // 3.635
	}

	private sealed class WaxingCrescentSE : Phases
	{
		public WaxingCrescentSE() : base($"{nameof(Id.WaxingCrescent)}", Id.WaxingCrescent) { }
		//public override string Icon => "fas fa-cloud-sun-rain";
		public override string Emoji => "🌒";
		public override string Title => "Waxing Crescent";
		public override int Day => 8;
	}

	private sealed class FirstQuarterSE : Phases
	{
		public FirstQuarterSE() : base($"{nameof(Id.FirstQuarter)}", Id.FirstQuarter) { }
		//public override string Icon => "far fa-sun";
		public override string Emoji => "🌓"; //"🌛";
		public override string Title => "First Quarter";
		public override int Day => 11;
	}

	private sealed class WaxingGibbousSE : Phases
	{
		public WaxingGibbousSE() : base($"{nameof(Id.WaxingGibbous)}", Id.WaxingGibbous) { }
		//public override string Icon => "fab fa-canadian-maple-leaf";
		public override string Emoji => "🌔";
		public override string Title => "Waxing Gibbous";
		public override int Day => 15;
	}

	private sealed class FullSE : Phases
	{
		public FullSE() : base($"{nameof(Id.Full)}", Id.Full) { }
		//public override string Icon => "fas fa-snowflake";
		public override string Emoji => "🌕";
		public override string Title => "Full";
		public override int Day => 18;
	}

	private sealed class WaningCrescentSE : Phases
	{
		public WaningCrescentSE() : base($"{nameof(Id.WaningCrescent)}", Id.WaningCrescent) { }
		//public override string Icon => "fas fa-cloud-sun-rain";
		public override string Emoji => "🌘";
		public override string Title => "Waning Crescent";
		public override int Day => 7;
	}

	private sealed class ThirdQuarterSE : Phases
	{
		public ThirdQuarterSE() : base($"{nameof(Id.ThirdQuarter)}", Id.ThirdQuarter) { }
		//public override string Icon => "far fa-sun";
		public override string Emoji => "🌗";
		public override string Title => "Third Quarter";
		public override int Day => 21;
	}

	private sealed class WaningGibbousSE : Phases
	{
		public WaningGibbousSE() : base($"{nameof(Id.WaningGibbous)}", Id.WaningGibbous) { }
		//public override string Icon => "fab fa-canadian-maple-leaf";
		public override string Emoji => "🌖";
		public override string Title => "Waning Gibbous";
		public override int Day => 25;
	}

	#endregion

}

/*
	The Moon has eight phases in a lunar month: four primary and four intermediate phases.
1 New Moon
2 Waxing Crescent Moon
3 First Quarter Moon
4 Waxing Gibbous Moon
5 Full Moon
6 Waning Gibbous Moon
6 Third Quarter Moon
8 Waning Crescent Moon

*/


// Ignore Spelling: Equilux