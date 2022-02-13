namespace LivingMessiah.Web.LinkSmartEnums;
using Ardalis.SmartEnum;
using PageLink = LivingMessiah.Web.Links.Feast;

/*
Observations and what makes this LinkSmartEnums unique from the others
1. It's a annual event and therefore tied to KeyDate and Calendar
2. Currently, if you have advanced autorization you can to see these pages
		- Maybe that's not necessary i.e. going forward it can be open to the public 
		- If open to the public maybe have a PastDaysOld / FutureDaysUntil component
3. These have special attributes or...
		- string HomeFloatRightHebrew, string HomeTitleSuffix
		- bool SitemapUsage, bool HomeSidebarUsage, 
*/

public record class Hebrew
{
	public string FloatRightHebrew { get; set; }
	public string TitleSuffix { get; set; }
	public string Strongs { get; set; }
}

public abstract class Feast : SmartEnum<Feast>
{
	#region Id's
	private static class Id
	{
		internal const int Hanukkah = 1;
		internal const int Purim = 2;
		internal const int Passover = 3;
		internal const int Omer = 4;
		internal const int Weeks = 5;  
		internal const int Trumpets = 6;
		internal const int YomKippur = 7;
		internal const int Tabernacles = 8;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Feast Hanukkah = new HanukkahSE();
	public static readonly Feast Purim = new PurimSE();
	public static readonly Feast Passover = new PassoverSE();
	public static readonly Feast Omer = new OmerSE();
	public static readonly Feast Weeks = new WeeksSE();
	public static readonly Feast Trumpets = new TrumpetsSE();
	public static readonly Feast YomKippur = new YomKippurSE();
	public static readonly Feast Tabernacles = new TabernaclesSE();
	// SE=SmartEnum
	#endregion

	private Feast(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Page { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract Hebrew Hebrew { get; }
	#endregion

	#region Private Instantiation
	private sealed class HanukkahSE : Feast
	{
		public HanukkahSE() : base($"{nameof(Id.Hanukkah)}", Id.Hanukkah) { }
		public override string Page => PageLink.Hanukkah.Page;
		public override string Title => PageLink.Hanukkah.Title;
		public override string Icon => PageLink.Hanukkah.Icon;
		public override Hebrew Hebrew => new Hebrew { TitleSuffix= "Hanukkah", FloatRightHebrew = "חֲנֻכָּה", Strongs= "H2598" };
	}

	private sealed class PurimSE : Feast
	{
		public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
		public override string Page => PageLink.Purim.Page;
		public override string Title => PageLink.Purim.Title;
		public override string Icon => PageLink.Purim.Icon;
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Purim", FloatRightHebrew = "פּוּר", Strongs = "H6332" };
	}

	private sealed class PassoverSE : Feast
	{
		public PassoverSE() : base($"{nameof(Id.Passover)}", Id.Passover) { }
		public override string Page => PageLink.Passover.Page;
		public override string Title => PageLink.Passover.Title;
		public override string Icon => PageLink.Passover.Icon;
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Pesach", FloatRightHebrew = "פֶּסַח", Strongs = "H6453" };
	}

	private sealed class OmerSE : Feast
	{
		public OmerSE() : base($"{nameof(Id.Omer)}", Id.Omer) { }
		public override string Page => PageLink.Omer.Page;
		public override string Title => PageLink.Omer.Title;
		public override string Icon => PageLink.Omer.Icon;
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Omer", FloatRightHebrew = "עֹמֶר", Strongs = "H6016" };
	}

	private sealed class WeeksSE : Feast
	{
		public WeeksSE() : base($"{nameof(Id.Weeks)}", Id.Weeks) { }
		public override string Page => PageLink.Weeks.Page;
		public override string Title => PageLink.Weeks.Title;
		public override string Icon => PageLink.Weeks.Icon;
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Shavu'ot", FloatRightHebrew = "שָׁבוּעוֹת", Strongs = "H7620" };
	}

	private sealed class TrumpetsSE : Feast
	{
		public TrumpetsSE() : base($"{nameof(Id.Trumpets)}", Id.Trumpets) { }
		public override string Page => PageLink.Trumpets.Page;
		public override string Title => PageLink.Trumpets.Title;
		public override string Icon => PageLink.Trumpets.Icon;
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Yom Teruah", FloatRightHebrew = "יוֹם תְּרוּעָה", Strongs = "H8643" };
	}

	private sealed class YomKippurSE : Feast
	{
		public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
		public override string Page => PageLink.YomKippur.Page;
		public override string Title => PageLink.YomKippur.Title;
		public override string Icon => PageLink.YomKippur.Icon;
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Yom Kippur", FloatRightHebrew = "יוֹם כִּיפּוּר", Strongs = "H3725" };
	}

	private sealed class TabernaclesSE : Feast
	{
		public TabernaclesSE() : base($"{nameof(Id.Tabernacles)}", Id.Tabernacles) { }
		public override string Page => PageLink.Tabernacles.Page;
		public override string Title => PageLink.Tabernacles.Title;
		public override string Icon => PageLink.Tabernacles.Icon;
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Sukkot", FloatRightHebrew = "סֻּכּוֹת", Strongs = "H5523" };
	}

	#endregion

}

