namespace LivingMessiah.Web.SmartEnums;
using Ardalis.SmartEnum;

public enum BookGroupEnum
{
	Torah = 1,
	History = 2,
	Poetry = 3,
	MajorProphets = 4,
	MinorProphets = 5,
	Gospels = 6,
	PaulsEpistles = 7,
	GeneralEpistles = 8,
	Apocalypse = 9,
}

public enum BookEnum
{
	Genesis = 1,
	Exodus = 2,
	Leviticus = 3,
	Numbers = 4,
	Deuteronomy = 5,
	Joshua = 6,
	Judges = 7,
	Ruth = 8,
	FirstSamuel = 9,
	SecondSamuel = 10,
	FirstKings = 11,
	SecondKings = 12,
	FirstChronicles = 13,
	SecondChronicles = 14,
	Ezra = 15,
	Nehemiah = 16,
	Esther = 17,
	Job = 18,
	Psalms = 19,
	Proverbs = 20,
	Ecclesiastes = 21,
	SongofSolomon = 22,
	Isaiah = 23,
	Jeremiah = 24,
	Lamentations = 25,
	Ezekiel = 26,
	Daniel = 27,
	Hosea = 28,
	Joel = 29,
	Amos = 30,
	Obadiah = 31,
	Jonah = 32,
	Micah = 33,
	Nahum = 34,
	Habakkuk = 35,
	Zephaniah = 36,
	Haggai = 37,
	Zechariah = 38,
	Malachi = 39,
	Matthew = 40,
	Mark = 41,
	Luke = 42,
	John = 43,
	Acts = 44,
	Romans = 45,
	FirstCorinthians = 46,
	SecondCorinthians = 47,
	Galatians = 48,
	Ephesians = 49,
	Philippians = 50,
	Colossians = 51,
	FirstThessalonians = 52,
	SecondThessalonians = 53,
	FirstTimothy = 54,
	SecondTimothy = 55,
	Titus = 56,
	Philemon = 57,
	Hebrews = 58,
	James = 59,
	FirstPeter = 60,
	SecondPeter = 61,
	FirstJohn = 62,
	SecondJohn = 63,
	ThirdJohn = 64,
	Jude = 65,
	Revelation = 66,
}

public abstract class BibleBook : SmartEnum<BibleBook>
{
	#region Id's
	// Use BookEnum
	#endregion


	#region  Declared Public Instances
	public static readonly BibleBook Genesis = new GenesisSE();
	public static readonly BibleBook Exodus = new ExodusSE();
	public static readonly BibleBook Leviticus = new LeviticusSE();
	public static readonly BibleBook Numbers = new NumbersSE();
	public static readonly BibleBook Deuteronomy = new DeuteronomySE();
	public static readonly BibleBook Psalms = new PsalmsSE();
	public static readonly BibleBook Proverbs = new ProverbsSE();
	public static readonly BibleBook Acts = new ActsSE();
	// SE=SmartEnum
	#endregion

	private BibleBook(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Abrv { get; }
	public abstract BookGroupEnum BookGroupEnum { get; }
	public abstract BookEnum BookEnum { get; }
	public abstract int LastChapter { get; }
	public abstract string TransliterationInHebrew { get; }
	public abstract string NameInHebrew { get; }

	#endregion


	#region Private Instantiation
	private sealed class GenesisSE : BibleBook
	{
		public GenesisSE() : base("Genesis", 1) { }
		public override string Abrv => "Gen";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		public override BookEnum BookEnum => BookEnum.Genesis;
		public override int LastChapter => 50;
		public override string TransliterationInHebrew => "Beresheeth";
		public override string NameInHebrew => "בְּרֵאשִׁית";
	}

	private sealed class ExodusSE : BibleBook
	{
		public ExodusSE() : base("Exodus", 2) { }
		public override string Abrv => "Exo";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		public override BookEnum BookEnum => BookEnum.Exodus;
		public override int LastChapter => 40;
		public override string TransliterationInHebrew => "Shemoth ";
		public override string NameInHebrew => "שְׁמֹות";
	}

	private sealed class LeviticusSE : BibleBook
	{
		public LeviticusSE() : base("Leviticus", 3) { }
		public override string Abrv => "Lev";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		public override BookEnum BookEnum => BookEnum.Leviticus;
		public override int LastChapter => 27;
		public override string TransliterationInHebrew => "Vayiqra";
		public override string NameInHebrew => "וַיִּקְרָא";
	}

	private sealed class NumbersSE : BibleBook
	{
		public NumbersSE() : base("Numbers", 4) { }
		public override string Abrv => "Num";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		public override BookEnum BookEnum => BookEnum.Numbers;
		public override int LastChapter => 36;
		public override string TransliterationInHebrew => "Bamidbar";
		public override string NameInHebrew => "בְּמִדְבַּר";
	}

	private sealed class DeuteronomySE : BibleBook
	{
		public DeuteronomySE() : base("Deuteronomy", 5) { }
		public override string Abrv => "Deu";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		public override BookEnum BookEnum => BookEnum.Deuteronomy;
		public override int LastChapter => 34;
		public override string TransliterationInHebrew => "Devarim";
		public override string NameInHebrew => "דְּבָרִים";

	}

	private sealed class PsalmsSE : BibleBook
	{
		public PsalmsSE() : base("Psalms", 19) { }
		public override string Abrv => "Psa";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Poetry;
		public override BookEnum BookEnum => BookEnum.Psalms;
		public override int LastChapter => 150;
		public override string TransliterationInHebrew => "Tehillim";
		public override string NameInHebrew => "תְּהִלִּים";

	}

	private sealed class ProverbsSE : BibleBook
	{
		public ProverbsSE() : base("Proverbs", 20) { }
		public override string Abrv => "Pro";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Poetry;
		public override BookEnum BookEnum => BookEnum.Proverbs;
		public override int LastChapter => 31;
		public override string TransliterationInHebrew => "Mishle";
		public override string NameInHebrew => "מִשְׁלֵי";

	}

	private sealed class ActsSE : BibleBook
	{
		public ActsSE() : base("Acts", 44) { }
		public override string Abrv => "Act";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Gospels;
		public override BookEnum BookEnum => BookEnum.Acts;
		public override int LastChapter => 28;
		public override string TransliterationInHebrew => "Maaseh Shlichim";
		public override string NameInHebrew => "";

	}
	#endregion

}
