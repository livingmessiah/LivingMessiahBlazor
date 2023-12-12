// Ignore Spelling: Yom Kippur

using Ardalis.SmartEnum;
using FeastDayType = LivingMessiah.Web.Features.Calendar.Enums.FeastDay;
using LivingMessiah.Web.Features.Calendar.FeastDayPlanner.Constants;
using System;

namespace LivingMessiah.Web.Features.Calendar.FeastDayPlanner.Enums;

public abstract class FeastDayDetail : SmartEnum<FeastDayDetail>
{
		#region Id's
		private static class Id
		{
				internal const int HanukkahDay1 = 1;
				internal const int HanukkahDay2 = 2;
				internal const int Purim = 3;
				internal const int UnleavenedBreadDay1 = 4;
				internal const int StartOmer = 5;
				internal const int UnleavenedBreadDay7 = 6;
				internal const int StopOmer = 7;
				internal const int Weeks = 8;
				internal const int Trumpets = 9;
				internal const int YomKippur = 10;
				internal const int SukkotDay1 = 11;
				internal const int SukkotDay8 = 12;
				internal const int SukkotCampTearDown = 13;
		}
		#endregion


		#region  Declared Public Instances
		public static readonly FeastDayDetail HanukkahDay1 = new HanukkahDay1SE();
		public static readonly FeastDayDetail HanukkahDay2 = new HanukkahDay2SE();

		public static readonly FeastDayDetail Purim = new PurimSE();

		public static readonly FeastDayDetail UnleavenedBreadDay1 = new UnleavenedBreadDay1SE();
		public static readonly FeastDayDetail StartOmer = new StartOmerSE();
		public static readonly FeastDayDetail UnleavenedBreadDay7 = new UnleavenedBreadDay7SE();

		public static readonly FeastDayDetail StopOmer = new StopOmerSE();
		public static readonly FeastDayDetail Weeks = new WeeksSE();

		public static readonly FeastDayDetail Trumpets = new TrumpetsSE();
		public static readonly FeastDayDetail YomKippur = new YomKippurSE();

		public static readonly FeastDayDetail SukkotDay1 = new SukkotDay1SE();
		public static readonly FeastDayDetail SukkotDay8 = new SukkotDay8SE();
		public static readonly FeastDayDetail SukkotCampTearDown = new SukkotCampTearDownSE();
		// SE=SmartEnum
		#endregion

		private FeastDayDetail(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract int ParentFeastDayId { get; }
		public abstract DateTime Date { get; }
		public abstract string Description { get; }
		public abstract string HebrewDate { get; }
		public abstract string HebrewBGColor { get; }
		public abstract string HebrewTextColor { get; }
		public abstract string PreHebrewDate { get; }
		#endregion

		#region Private Instantiation


		private sealed class HanukkahDay1SE : FeastDayDetail
		{
				public HanukkahDay1SE() : base($"{nameof(Id.HanukkahDay1)}", Id.HanukkahDay1) { }
				public override int ParentFeastDayId => FeastDayType.Hanukkah.Value;
				public override DateTime Date => FeastDayType.Hanukkah.Date;
				public override string Description => "Day 1";
				public override string HebrewDate => "Kislev 25th";
				public override string HebrewBGColor => BarColor.HebrewBGColor;
				public override string HebrewTextColor => BarColor.HebrewTextColor;
				public override string PreHebrewDate => "";
		}

		private sealed class HanukkahDay2SE : FeastDayDetail
		{
				public HanukkahDay2SE() : base($"{nameof(Id.HanukkahDay2)}", Id.HanukkahDay2) { }
				public override int ParentFeastDayId => FeastDayType.Hanukkah.Value;
				public override DateTime Date => FeastDayType.Hanukkah.Date.AddDays(8);
				public override string Description => "Day 8";
				public override string HebrewDate => "Tevet 2nd";
				public override string HebrewBGColor => BarColor.HebrewBGColor;
				public override string HebrewTextColor => BarColor.HebrewTextColor;
				public override string PreHebrewDate => "";
		}

		private sealed class PurimSE : FeastDayDetail
		{
				public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
				public override int ParentFeastDayId => FeastDayType.Purim.Value;
				public override DateTime Date => FeastDayType.Purim.Date;
				public override string Description => "Purim";
				public override string HebrewDate => HebrewYear.IsLeapYear ? "Adar II 14" : "Adar I 14";
				public override string HebrewBGColor => BarColor.HebrewBGColor;
				public override string HebrewTextColor => BarColor.HebrewTextColor;
				public override string PreHebrewDate => "";
		}

		private sealed class UnleavenedBreadDay1SE : FeastDayDetail
		{
				public UnleavenedBreadDay1SE() : base($"{nameof(Id.UnleavenedBreadDay1)}", Id.UnleavenedBreadDay1) { }
				public override int ParentFeastDayId => FeastDayType.Passover.Value;
				public override DateTime Date => FeastDayType.Passover.Date;
				public override string Description => "Unleavened Bread Day 1";
				public override string HebrewDate => "Nissan 15";
				public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
				public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
				public override string PreHebrewDate => "Seder";
		}

		private sealed class StartOmerSE : FeastDayDetail
		{
				public StartOmerSE() : base($"{nameof(Id.StartOmer)}", Id.StartOmer) { }
				public override int ParentFeastDayId => FeastDayType.Passover.Value;
				public override DateTime Date => FeastDayType.Passover.Date.AddDays(1);
				public override string Description => "Start Omer Count";
				public override string HebrewDate => "Nissan 16";
				public override string HebrewBGColor => BarColor.HebrewBGColor;
				public override string HebrewTextColor => BarColor.HebrewTextColor;
				public override string PreHebrewDate => "";
		}

		private sealed class UnleavenedBreadDay7SE : FeastDayDetail
		{
				public UnleavenedBreadDay7SE() : base($"{nameof(Id.UnleavenedBreadDay7)}", Id.UnleavenedBreadDay7) { }
				public override int ParentFeastDayId => FeastDayType.Passover.Value;
				public override DateTime Date => FeastDayType.Passover.Date.AddDays(7);
				public override string Description => "Unleavened Bread Day 7";
				public override string HebrewDate => "Nissan 21";
				public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
				public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
				public override string PreHebrewDate => "";
		}

		private sealed class StopOmerSE : FeastDayDetail
		{
				public StopOmerSE() : base($"{nameof(Id.StopOmer)}", Id.StopOmer) { }
				public override int ParentFeastDayId => FeastDayType.Weeks.Value;
				public override DateTime Date => FeastDayType.Weeks.Date.AddDays(-1);
				public override string Description => "Finish Omer Count";
				public override string HebrewDate => "Sivan 6";
				public override string HebrewBGColor => BarColor.HebrewBGColor;
				public override string HebrewTextColor => BarColor.HebrewTextColor;
				public override string PreHebrewDate => "Omer 49!";
		}

		private sealed class WeeksSE : FeastDayDetail
		{
				public WeeksSE() : base($"{nameof(Id.Weeks)}", Id.Weeks) { }
				public override int ParentFeastDayId => FeastDayType.Weeks.Value;
				public override DateTime Date => FeastDayType.Weeks.Date;
				public override string Description => "Weeks | Shavuot";
				public override string HebrewDate => "Sivan 7";
				public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
				public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
				public override string PreHebrewDate => "";
		}

		private sealed class TrumpetsSE : FeastDayDetail
		{
				public TrumpetsSE() : base($"{nameof(Id.Trumpets)}", Id.Trumpets) { }
				public override int ParentFeastDayId => FeastDayType.Trumpets.Value;
				public override DateTime Date => FeastDayType.Trumpets.Date;
				public override string Description => "Yom Teruah";
				public override string HebrewDate => "Tishrei 1";
				public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
				public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
				public override string PreHebrewDate => "";
		}

		private sealed class YomKippurSE : FeastDayDetail
		{
				public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
				public override int ParentFeastDayId => FeastDayType.YomKippur.Value;
				public override DateTime Date => FeastDayType.YomKippur.Date;
				public override string Description => "Day of Atonements";
				public override string HebrewDate => "Tishrei 10";
				public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
				public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
				public override string PreHebrewDate => "";
		}

		private sealed class SukkotDay1SE : FeastDayDetail
		{
				public SukkotDay1SE() : base($"{nameof(Id.SukkotDay1)}", Id.SukkotDay1) { }
				public override int ParentFeastDayId => FeastDayType.Tabernacles.Value;
				public override DateTime Date => FeastDayType.Tabernacles.Date;
				public override string Description => "Day 1";
				public override string HebrewDate => "Tishrei 15";
				public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
				public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
				public override string PreHebrewDate => "Prep Day";
		}

		private sealed class SukkotDay8SE : FeastDayDetail
		{
				public SukkotDay8SE() : base($"{nameof(Id.SukkotDay8)}", Id.SukkotDay8) { }
				public override int ParentFeastDayId => FeastDayType.Tabernacles.Value;
				public override DateTime Date => FeastDayType.Tabernacles.Date.AddDays(7);
				public override string Description => "Day 8";
				public override string HebrewDate => "Tishrei 22";
				public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
				public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
				public override string PreHebrewDate => "";
		}

		private sealed class SukkotCampTearDownSE : FeastDayDetail
		{
				public SukkotCampTearDownSE() : base($"{nameof(Id.SukkotCampTearDown)}", Id.SukkotCampTearDown) { }
				public override int ParentFeastDayId => FeastDayType.Tabernacles.Value;
				public override DateTime Date => FeastDayType.Tabernacles.Date.AddDays(8);
				public override string Description => "Camp Tear Down";
				public override string HebrewDate => "Tishrei 23";
				public override string HebrewBGColor => BarColor.HebrewBGColor;
				public override string HebrewTextColor => BarColor.HebrewTextColor;
				public override string PreHebrewDate => "";

		}

		#endregion
}



// Ignore Spelling: Descr
// Ignore Spelling: Erev
// Ignore Spelling: Sivan
// Ignore Spelling: Teruah
// Ignore Spelling: Tishrei
