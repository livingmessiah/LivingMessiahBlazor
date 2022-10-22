using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Enums;

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
	public static readonly BibleBook Joshua = new JoshuaSE();
	public static readonly BibleBook Judges = new JudgesSE();
	public static readonly BibleBook Ruth = new RuthSE();
	public static readonly BibleBook FirstSamuel = new FirstSamuelSE();
	public static readonly BibleBook SecondSamuel = new SecondSamuelSE();
	public static readonly BibleBook FirstKings = new FirstKingsSE();
	public static readonly BibleBook SecondKings = new SecondKingsSE();
	public static readonly BibleBook FirstChronicles = new FirstChroniclesSE();
	public static readonly BibleBook SecondChronicles = new SecondChroniclesSE();
	public static readonly BibleBook Ezra = new EzraSE();
	public static readonly BibleBook Nehemiah = new NehemiahSE();
	public static readonly BibleBook Esther = new EstherSE();
	public static readonly BibleBook Job = new JobSE();
	public static readonly BibleBook Psalms = new PsalmsSE();
	public static readonly BibleBook Proverbs = new ProverbsSE();
	public static readonly BibleBook Ecclesiastes = new EcclesiastesSE();
	public static readonly BibleBook SongofSolomon = new SongofSolomonSE();
	public static readonly BibleBook Isaiah = new IsaiahSE();
	public static readonly BibleBook Jeremiah = new JeremiahSE();
	public static readonly BibleBook Lamentations = new LamentationsSE();
	public static readonly BibleBook Ezekiel = new EzekielSE();
	public static readonly BibleBook Daniel = new DanielSE();
	public static readonly BibleBook Hosea = new HoseaSE();
	public static readonly BibleBook Joel = new JoelSE();
	public static readonly BibleBook Amos = new AmosSE();
	public static readonly BibleBook Obadiah = new ObadiahSE();
	public static readonly BibleBook Jonah = new JonahSE();
	public static readonly BibleBook Micah = new MicahSE();
	public static readonly BibleBook Nahum = new NahumSE();
	public static readonly BibleBook Habakkuk = new HabakkukSE();
	public static readonly BibleBook Zephaniah = new ZephaniahSE();
	public static readonly BibleBook Haggai = new HaggaiSE();
	public static readonly BibleBook Zechariah = new ZechariahSE();
	public static readonly BibleBook Malachi = new MalachiSE();
	public static readonly BibleBook Matthew = new MatthewSE();
	public static readonly BibleBook Mark = new MarkSE();
	public static readonly BibleBook Luke = new LukeSE();
	public static readonly BibleBook John = new JohnSE();
	public static readonly BibleBook Acts = new ActsSE();
	public static readonly BibleBook Romans = new RomansSE();
	public static readonly BibleBook FirstCorinthians = new FirstCorinthiansSE();
	public static readonly BibleBook SecondCorinthians = new SecondCorinthiansSE();
	public static readonly BibleBook Galatians = new GalatiansSE();
	public static readonly BibleBook Ephesians = new EphesiansSE();
	public static readonly BibleBook Philippians = new PhilippiansSE();
	public static readonly BibleBook Colossians = new ColossiansSE();
	public static readonly BibleBook FirstThessalonians = new FirstThessaloniansSE();
	public static readonly BibleBook SecondThessalonians = new SecondThessaloniansSE();
	public static readonly BibleBook FirstTimothy = new FirstTimothySE();
	public static readonly BibleBook SecondTimothy = new SecondTimothySE();
	public static readonly BibleBook Titus = new TitusSE();
	public static readonly BibleBook Philemon = new PhilemonSE();
	public static readonly BibleBook Hebrews = new HebrewsSE();
	public static readonly BibleBook James = new JamesSE();
	public static readonly BibleBook FirstPeter = new FirstPeterSE();
	public static readonly BibleBook SecondPeter = new SecondPeterSE();
	public static readonly BibleBook FirstJohn = new FirstJohnSE();
	public static readonly BibleBook SecondJohn = new SecondJohnSE();
	public static readonly BibleBook ThirdJohn = new ThirdJohnSE();
	public static readonly BibleBook Jude = new JudeSE();
	public static readonly BibleBook Revelation = new RevelationSE();
	// SE=SmartEnum
	#endregion

	private BibleBook(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string Abrv { get; }
	public abstract BookGroupEnum BookGroupEnum { get; }
	public abstract BookEnum BookEnum { get; }
	public abstract int LastChapter { get; }
	public abstract string TransliterationInHebrew { get; }
	public abstract string NameInHebrew { get; }
	public string Dump
	{
		get
		{
			return $" {this.Value}-{this.Abrv}-{this.Name}-{this.BookGroupEnum}";
		}
	}
	#endregion


	#region Private Instantiation
	private sealed class GenesisSE : BibleBook
	{
		public GenesisSE() : base("Genesis", 1) { }
		public override string Title => "Genesis";
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
		public override string Title => "Exodus";
		public override string Abrv => "Exo";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		public override BookEnum BookEnum => BookEnum.Exodus;
		public override int LastChapter => 40;
		public override string TransliterationInHebrew => "Shemoth";
		public override string NameInHebrew => "שְׁמֹות";
	}
	private sealed class LeviticusSE : BibleBook
	{
		public LeviticusSE() : base("Leviticus", 3) { }
		public override string Title => "Leviticus";
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
		public override string Title => "Numbers";
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
		public override string Title => "Deuteronomy";
		public override string Abrv => "Deu";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		public override BookEnum BookEnum => BookEnum.Deuteronomy;
		public override int LastChapter => 34;
		public override string TransliterationInHebrew => "Devarim";
		public override string NameInHebrew => "דְּבָרִים";
	}
	private sealed class JoshuaSE : BibleBook
	{
		public JoshuaSE() : base("Joshua", 6) { }
		public override string Title => "Joshua";
		public override string Abrv => "Jos";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.Joshua;
		public override int LastChapter => 24;
		public override string TransliterationInHebrew => "Yahoshua";
		public override string NameInHebrew => "יְהוֹשֻׁעַ";
	}
	private sealed class JudgesSE : BibleBook
	{
		public JudgesSE() : base("Judges", 7) { }
		public override string Title => "Judges";
		public override string Abrv => "Jdg";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.Judges;
		public override int LastChapter => 21;
		public override string TransliterationInHebrew => "Shophtim";
		public override string NameInHebrew => "שׁוֹפְטִים";
	}
	private sealed class RuthSE : BibleBook
	{
		public RuthSE() : base("Ruth", 8) { }
		public override string Title => "Ruth";
		public override string Abrv => "Rut";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.Ruth;
		public override int LastChapter => 4;
		public override string TransliterationInHebrew => "Root";
		public override string NameInHebrew => "רוּת";
	}
	private sealed class FirstSamuelSE : BibleBook
	{
		public FirstSamuelSE() : base("FirstSamuel", 9) { }
		public override string Title => "1Samuel";
		public override string Abrv => "1Sa";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.FirstSamuel;
		public override int LastChapter => 31;
		public override string TransliterationInHebrew => "Schmuel Alef";
		public override string NameInHebrew => "שְׁמוּאֵל א";
	}
	private sealed class SecondSamuelSE : BibleBook
	{
		public SecondSamuelSE() : base("SecondSamuel", 10) { }
		public override string Title => "2Samuel";
		public override string Abrv => "2Sa";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.SecondSamuel;
		public override int LastChapter => 24;
		public override string TransliterationInHebrew => "Schmuel Bet";
		public override string NameInHebrew => "שְׁמוּאֵל ב";
	}
	private sealed class FirstKingsSE : BibleBook
	{
		public FirstKingsSE() : base("FirstKings", 11) { }
		public override string Title => "1Kings";
		public override string Abrv => "1Ki";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.FirstKings;
		public override int LastChapter => 22;
		public override string TransliterationInHebrew => "Melechim Alef";
		public override string NameInHebrew => "מְלָכִים א";
	}
	private sealed class SecondKingsSE : BibleBook
	{
		public SecondKingsSE() : base("SecondKings", 12) { }
		public override string Title => "2Kings";
		public override string Abrv => "2Ki";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.SecondKings;
		public override int LastChapter => 25;
		public override string TransliterationInHebrew => "Melechim Bet";
		public override string NameInHebrew => "מְלָכִים ב";
	}
	private sealed class FirstChroniclesSE : BibleBook
	{
		public FirstChroniclesSE() : base("FirstChronicles", 13) { }
		public override string Title => "1Chronicles";
		public override string Abrv => "1Ch";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.FirstChronicles;
		public override int LastChapter => 29;
		public override string TransliterationInHebrew => "Divre HaYamim Alef";
		public override string NameInHebrew => "דִּבְרֵי הַיָּמִים א";
	}
	private sealed class SecondChroniclesSE : BibleBook
	{
		public SecondChroniclesSE() : base("SecondChronicles", 14) { }
		public override string Title => "2Chronicles";
		public override string Abrv => "2Ch";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.SecondChronicles;
		public override int LastChapter => 36;
		public override string TransliterationInHebrew => "Divre HaYamim Bet";
		public override string NameInHebrew => "דִּבְרֵי הַיָּמִים ב";
	}
	private sealed class EzraSE : BibleBook
	{
		public EzraSE() : base("Ezra", 15) { }
		public override string Title => "Ezra";
		public override string Abrv => "Ezr";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.Ezra;
		public override int LastChapter => 10;
		public override string TransliterationInHebrew => "Ezrah";
		public override string NameInHebrew => "עֶזְרָא";
	}
	private sealed class NehemiahSE : BibleBook
	{
		public NehemiahSE() : base("Nehemiah", 16) { }
		public override string Title => "Nehemiah";
		public override string Abrv => "Neh";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.Nehemiah;
		public override int LastChapter => 13;
		public override string TransliterationInHebrew => "Nechemyah";
		public override string NameInHebrew => "נְחֶמְיָה";
	}
	private sealed class EstherSE : BibleBook
	{
		public EstherSE() : base("Esther", 17) { }
		public override string Title => "Esther";
		public override string Abrv => "Est";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.History;
		public override BookEnum BookEnum => BookEnum.Esther;
		public override int LastChapter => 10;
		public override string TransliterationInHebrew => "Hadasah";
		public override string NameInHebrew => "אֶסְתֵּר";
	}
	private sealed class JobSE : BibleBook
	{
		public JobSE() : base("Job", 18) { }
		public override string Title => "Job";
		public override string Abrv => "Job";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Poetry;
		public override BookEnum BookEnum => BookEnum.Job;
		public override int LastChapter => 42;
		public override string TransliterationInHebrew => "Iyov";
		public override string NameInHebrew => "אִיּוֹב";
	}
	private sealed class PsalmsSE : BibleBook
	{
		public PsalmsSE() : base("Psalms", 19) { }
		public override string Title => "Psalms";
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
		public override string Title => "Proverbs";
		public override string Abrv => "Pro";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Poetry;
		public override BookEnum BookEnum => BookEnum.Proverbs;
		public override int LastChapter => 31;
		public override string TransliterationInHebrew => "Mishle";
		public override string NameInHebrew => "מִשְׁלֵי";
	}
	private sealed class EcclesiastesSE : BibleBook
	{
		public EcclesiastesSE() : base("Ecclesiastes", 21) { }
		public override string Title => "Ecclesiastes";
		public override string Abrv => "Ecc";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Poetry;
		public override BookEnum BookEnum => BookEnum.Ecclesiastes;
		public override int LastChapter => 12;
		public override string TransliterationInHebrew => "Koheleth";
		public override string NameInHebrew => "קֹהֶלֶת";
	}
	private sealed class SongofSolomonSE : BibleBook
	{
		public SongofSolomonSE() : base("SongofSolomon", 22) { }
		public override string Title => "SongofSolomon";
		public override string Abrv => "Song";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Poetry;
		public override BookEnum BookEnum => BookEnum.SongofSolomon;
		public override int LastChapter => 8;
		public override string TransliterationInHebrew => "Shir HaShirim";
		public override string NameInHebrew => "שִׁיר הַשִּׁירִים";
	}
	private sealed class IsaiahSE : BibleBook
	{
		public IsaiahSE() : base("Isaiah", 23) { }
		public override string Title => "Isaiah";
		public override string Abrv => "Isa";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MajorProphets;
		public override BookEnum BookEnum => BookEnum.Isaiah;
		public override int LastChapter => 66;
		public override string TransliterationInHebrew => "Yeshayahu";
		public override string NameInHebrew => "יְשַׁעְיָהוּ";
	}
	private sealed class JeremiahSE : BibleBook
	{
		public JeremiahSE() : base("Jeremiah", 24) { }
		public override string Title => "Jeremiah";
		public override string Abrv => "Jer";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MajorProphets;
		public override BookEnum BookEnum => BookEnum.Jeremiah;
		public override int LastChapter => 52;
		public override string TransliterationInHebrew => "Yirmeyahu";
		public override string NameInHebrew => "יִרְמְיָהוּ";
	}
	private sealed class LamentationsSE : BibleBook
	{
		public LamentationsSE() : base("Lamentations", 25) { }
		public override string Title => "Lamentations";
		public override string Abrv => "Lam";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MajorProphets;
		public override BookEnum BookEnum => BookEnum.Lamentations;
		public override int LastChapter => 5;
		public override string TransliterationInHebrew => "Echah";
		public override string NameInHebrew => "אֵיכָה";
	}
	private sealed class EzekielSE : BibleBook
	{
		public EzekielSE() : base("Ezekiel", 26) { }
		public override string Title => "Ezekiel";
		public override string Abrv => "Eze";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MajorProphets;
		public override BookEnum BookEnum => BookEnum.Ezekiel;
		public override int LastChapter => 48;
		public override string TransliterationInHebrew => "Yechezkel";
		public override string NameInHebrew => "יְחֶזְקֵאל";
	}
	private sealed class DanielSE : BibleBook
	{
		public DanielSE() : base("Daniel", 27) { }
		public override string Title => "Daniel";
		public override string Abrv => "Dan";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MajorProphets;
		public override BookEnum BookEnum => BookEnum.Daniel;
		public override int LastChapter => 12;
		public override string TransliterationInHebrew => "Daniyel";
		public override string NameInHebrew => "דָּנִיֵּאל";
	}
	private sealed class HoseaSE : BibleBook
	{
		public HoseaSE() : base("Hosea", 28) { }
		public override string Title => "Hosea";
		public override string Abrv => "Hos";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Hosea;
		public override int LastChapter => 14;
		public override string TransliterationInHebrew => "Hoshea";
		public override string NameInHebrew => "הוֹשֵׁעַ";
	}
	private sealed class JoelSE : BibleBook
	{
		public JoelSE() : base("Joel", 29) { }
		public override string Title => "Joel";
		public override string Abrv => "Joe";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Joel;
		public override int LastChapter => 3;
		public override string TransliterationInHebrew => "Yoel";
		public override string NameInHebrew => "יוֹאֵל";
	}
	private sealed class AmosSE : BibleBook
	{
		public AmosSE() : base("Amos", 30) { }
		public override string Title => "Amos";
		public override string Abrv => "Amo";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Amos;
		public override int LastChapter => 9;
		public override string TransliterationInHebrew => "Ahmos";
		public override string NameInHebrew => "עָמוֹס";
	}
	private sealed class ObadiahSE : BibleBook
	{
		public ObadiahSE() : base("Obadiah", 31) { }
		public override string Title => "Obadiah";
		public override string Abrv => "Oba";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Obadiah;
		public override int LastChapter => 1;
		public override string TransliterationInHebrew => "Ovadyah";
		public override string NameInHebrew => "עֹבַדְיָה";
	}
	private sealed class JonahSE : BibleBook
	{
		public JonahSE() : base("Jonah", 32) { }
		public override string Title => "Jonah";
		public override string Abrv => "Jon";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Jonah;
		public override int LastChapter => 4;
		public override string TransliterationInHebrew => "Yonah";
		public override string NameInHebrew => "יוֹנָה";
	}
	private sealed class MicahSE : BibleBook
	{
		public MicahSE() : base("Micah", 33) { }
		public override string Title => "Micah";
		public override string Abrv => "Mic";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Micah;
		public override int LastChapter => 7;
		public override string TransliterationInHebrew => "Micha";
		public override string NameInHebrew => "מִיכָה";
	}
	private sealed class NahumSE : BibleBook
	{
		public NahumSE() : base("Nahum", 34) { }
		public override string Title => "Nahum";
		public override string Abrv => "Nah";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Nahum;
		public override int LastChapter => 3;
		public override string TransliterationInHebrew => "Nachum";
		public override string NameInHebrew => "נַחוּם";
	}
	private sealed class HabakkukSE : BibleBook
	{
		public HabakkukSE() : base("Habakkuk", 35) { }
		public override string Title => "Habakkuk";
		public override string Abrv => "Hab";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Habakkuk;
		public override int LastChapter => 3;
		public override string TransliterationInHebrew => "Chabakook";
		public override string NameInHebrew => "חֲבַקּוּק";
	}
	private sealed class ZephaniahSE : BibleBook
	{
		public ZephaniahSE() : base("Zephaniah", 36) { }
		public override string Title => "Zephaniah";
		public override string Abrv => "Zep";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Zephaniah;
		public override int LastChapter => 3;
		public override string TransliterationInHebrew => "Tzephanyah";
		public override string NameInHebrew => "צְפַנְיָה";
	}
	private sealed class HaggaiSE : BibleBook
	{
		public HaggaiSE() : base("Haggai", 37) { }
		public override string Title => "Haggai";
		public override string Abrv => "Hag";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Haggai;
		public override int LastChapter => 2;
		public override string TransliterationInHebrew => "Chaggai";
		public override string NameInHebrew => "חַגַּי";
	}
	private sealed class ZechariahSE : BibleBook
	{
		public ZechariahSE() : base("Zechariah", 38) { }
		public override string Title => "Zechariah";
		public override string Abrv => "Zec";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Zechariah;
		public override int LastChapter => 14;
		public override string TransliterationInHebrew => "Zecharyah";
		public override string NameInHebrew => "זְכַרְיָה";
	}
	private sealed class MalachiSE : BibleBook
	{
		public MalachiSE() : base("Malachi", 39) { }
		public override string Title => "Malachi";
		public override string Abrv => "Mal";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.MinorProphets;
		public override BookEnum BookEnum => BookEnum.Malachi;
		public override int LastChapter => 4;
		public override string TransliterationInHebrew => "Malachi";
		public override string NameInHebrew => "מַלְאָכִי";
	}
	private sealed class MatthewSE : BibleBook
	{
		public MatthewSE() : base("Matthew", 40) { }
		public override string Title => "Matthew";
		public override string Abrv => "Mat";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Gospels;
		public override BookEnum BookEnum => BookEnum.Matthew;
		public override int LastChapter => 28;
		public override string TransliterationInHebrew => "Mattityahu";
		public override string NameInHebrew => "מַתִּתְיָהוּ";
	}
	private sealed class MarkSE : BibleBook
	{
		public MarkSE() : base("Mark", 41) { }
		public override string Title => "Mark";
		public override string Abrv => "Mar";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Gospels;
		public override BookEnum BookEnum => BookEnum.Mark;
		public override int LastChapter => 16;
		public override string TransliterationInHebrew => "Yochanan-Moshe";
		public override string NameInHebrew => "מַרְקוֹס";
	}
	private sealed class LukeSE : BibleBook
	{
		public LukeSE() : base("Luke", 42) { }
		public override string Title => "Luke";
		public override string Abrv => "Luk";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Gospels;
		public override BookEnum BookEnum => BookEnum.Luke;
		public override int LastChapter => 24;
		public override string TransliterationInHebrew => "Luka";
		public override string NameInHebrew => "לוּקָס";
	}
	private sealed class JohnSE : BibleBook
	{
		public JohnSE() : base("John", 43) { }
		public override string Title => "John";
		public override string Abrv => "Joh";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Gospels;
		public override BookEnum BookEnum => BookEnum.John;
		public override int LastChapter => 21;
		public override string TransliterationInHebrew => "Yochanan";
		public override string NameInHebrew => "יוֹחָנָן";
	}
	private sealed class ActsSE : BibleBook
	{
		public ActsSE() : base("Acts", 44) { }
		public override string Title => "Acts";
		public override string Abrv => "Act";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Gospels;
		public override BookEnum BookEnum => BookEnum.Acts;
		public override int LastChapter => 28;
		public override string TransliterationInHebrew => "Maaseh Shlichim";  // Emissaries Acts
		public override string NameInHebrew => "מַעֲשֶׂה שליחים";
	}
	private sealed class RomansSE : BibleBook
	{
		public RomansSE() : base("Romans", 45) { }
		public override string Title => "Romans";
		public override string Abrv => "Rom";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.Romans;
		public override int LastChapter => 16;
		public override string TransliterationInHebrew => "Romiyah";
		public override string NameInHebrew => "";
	}
	private sealed class FirstCorinthiansSE : BibleBook
	{
		public FirstCorinthiansSE() : base("FirstCorinthians", 46) { }
		public override string Title => "1Corinthians";
		public override string Abrv => "1Co";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.FirstCorinthians;
		public override int LastChapter => 16;
		public override string TransliterationInHebrew => "Qorintyah Alef";
		public override string NameInHebrew => "";
	}
	private sealed class SecondCorinthiansSE : BibleBook
	{
		public SecondCorinthiansSE() : base("SecondCorinthians", 47) { }
		public override string Title => "2Corinthians";
		public override string Abrv => "2Co";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.SecondCorinthians;
		public override int LastChapter => 13;
		public override string TransliterationInHebrew => "Qorintyah Bet";
		public override string NameInHebrew => "";
	}
	private sealed class GalatiansSE : BibleBook
	{
		public GalatiansSE() : base("Galatians", 48) { }
		public override string Title => "Galatians";
		public override string Abrv => "Gal";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.Galatians;
		public override int LastChapter => 6;
		public override string TransliterationInHebrew => "Galutyah";
		public override string NameInHebrew => "";
	}
	private sealed class EphesiansSE : BibleBook
	{
		public EphesiansSE() : base("Ephesians", 49) { }
		public override string Title => "Ephesians";
		public override string Abrv => "Eph";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.Ephesians;
		public override int LastChapter => 6;
		public override string TransliterationInHebrew => "Ephsiyah";
		public override string NameInHebrew => "";
	}
	private sealed class PhilippiansSE : BibleBook
	{
		public PhilippiansSE() : base("Philippians", 50) { }
		public override string Title => "Philippians";
		public override string Abrv => "Php";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.Philippians;
		public override int LastChapter => 4;
		public override string TransliterationInHebrew => "Phylypsiyah";
		public override string NameInHebrew => "";
	}
	private sealed class ColossiansSE : BibleBook
	{
		public ColossiansSE() : base("Colossians", 51) { }
		public override string Title => "Colossians";
		public override string Abrv => "Col";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.Colossians;
		public override int LastChapter => 4;
		public override string TransliterationInHebrew => "Qolesayah";
		public override string NameInHebrew => "";
	}
	private sealed class FirstThessaloniansSE : BibleBook
	{
		public FirstThessaloniansSE() : base("FirstThessalonians", 52) { }
		public override string Title => "1Thessalonians";
		public override string Abrv => "1Th";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.FirstThessalonians;
		public override int LastChapter => 5;
		public override string TransliterationInHebrew => "Tesloniqyah Alef";
		public override string NameInHebrew => "";
	}
	private sealed class SecondThessaloniansSE : BibleBook
	{
		public SecondThessaloniansSE() : base("SecondThessalonians", 53) { }
		public override string Title => "2Thessalonians";
		public override string Abrv => "2Th";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.SecondThessalonians;
		public override int LastChapter => 3;
		public override string TransliterationInHebrew => "Tesloniqyah Bet";
		public override string NameInHebrew => "";
	}
	private sealed class FirstTimothySE : BibleBook
	{
		public FirstTimothySE() : base("FirstTimothy", 54) { }
		public override string Title => "1Timothy";
		public override string Abrv => "1Ti";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.FirstTimothy;
		public override int LastChapter => 6;
		public override string TransliterationInHebrew => "Timtheous Alef";
		public override string NameInHebrew => "";
	}
	private sealed class SecondTimothySE : BibleBook
	{
		public SecondTimothySE() : base("SecondTimothy", 55) { }
		public override string Title => "2Timothy";
		public override string Abrv => "2Ti";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.SecondTimothy;
		public override int LastChapter => 4;
		public override string TransliterationInHebrew => "Timtheous Bet";
		public override string NameInHebrew => "";
	}
	private sealed class TitusSE : BibleBook
	{
		public TitusSE() : base("Titus", 56) { }
		public override string Title => "Titus";
		public override string Abrv => "Tit";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.Titus;
		public override int LastChapter => 3;
		public override string TransliterationInHebrew => "Teitus";
		public override string NameInHebrew => "";
	}
	private sealed class PhilemonSE : BibleBook
	{
		public PhilemonSE() : base("Philemon", 57) { }
		public override string Title => "Philemon";
		public override string Abrv => "Phm";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.PaulsEpistles;
		public override BookEnum BookEnum => BookEnum.Philemon;
		public override int LastChapter => 1;
		public override string TransliterationInHebrew => "Phileymon";
		public override string NameInHebrew => "";
	}
	private sealed class HebrewsSE : BibleBook
	{
		public HebrewsSE() : base("Hebrews", 58) { }
		public override string Title => "Hebrews";
		public override string Abrv => "Heb";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.GeneralEpistles;
		public override BookEnum BookEnum => BookEnum.Hebrews;
		public override int LastChapter => 13;
		public override string TransliterationInHebrew => "Ivrim";
		public override string NameInHebrew => "";
	}
	private sealed class JamesSE : BibleBook
	{
		public JamesSE() : base("James", 59) { }
		public override string Title => "James";
		public override string Abrv => "Jam";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.GeneralEpistles;
		public override BookEnum BookEnum => BookEnum.James;
		public override int LastChapter => 5;
		public override string TransliterationInHebrew => "Yaakov";
		public override string NameInHebrew => "";
	}
	private sealed class FirstPeterSE : BibleBook
	{
		public FirstPeterSE() : base("FirstPeter", 60) { }
		public override string Title => "1Peter";
		public override string Abrv => "1Pe";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.GeneralEpistles;
		public override BookEnum BookEnum => BookEnum.FirstPeter;
		public override int LastChapter => 5;
		public override string TransliterationInHebrew => "Kepha Alef";
		public override string NameInHebrew => "";
	}
	private sealed class SecondPeterSE : BibleBook
	{
		public SecondPeterSE() : base("SecondPeter", 61) { }
		public override string Title => "2Peter";
		public override string Abrv => "2Pe";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.GeneralEpistles;
		public override BookEnum BookEnum => BookEnum.SecondPeter;
		public override int LastChapter => 3;
		public override string TransliterationInHebrew => "Kepha Bet";
		public override string NameInHebrew => "";
	}
	private sealed class FirstJohnSE : BibleBook
	{
		public FirstJohnSE() : base("FirstJohn", 62) { }
		public override string Title => "1John";
		public override string Abrv => "1Jo";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.GeneralEpistles;
		public override BookEnum BookEnum => BookEnum.FirstJohn;
		public override int LastChapter => 5;
		public override string TransliterationInHebrew => "Yochanan Alef";
		public override string NameInHebrew => "";
	}
	private sealed class SecondJohnSE : BibleBook
	{
		public SecondJohnSE() : base("SecondJohn", 63) { }
		public override string Title => "2John";
		public override string Abrv => "2Jo";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.GeneralEpistles;
		public override BookEnum BookEnum => BookEnum.SecondJohn;
		public override int LastChapter => 1;
		public override string TransliterationInHebrew => "Yochanan Bet";
		public override string NameInHebrew => "";
	}
	private sealed class ThirdJohnSE : BibleBook
	{
		public ThirdJohnSE() : base("ThirdJohn", 64) { }
		public override string Title => "3John";
		public override string Abrv => "3Jo";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.GeneralEpistles;
		public override BookEnum BookEnum => BookEnum.ThirdJohn;
		public override int LastChapter => 1;
		public override string TransliterationInHebrew => "Yochanan Gimel";
		public override string NameInHebrew => "";
	}
	private sealed class JudeSE : BibleBook
	{
		public JudeSE() : base("Jude", 65) { }
		public override string Title => "Jude";
		public override string Abrv => "Jud";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.GeneralEpistles;
		public override BookEnum BookEnum => BookEnum.Jude;
		public override int LastChapter => 1;
		public override string TransliterationInHebrew => "Yahudah";
		public override string NameInHebrew => "";
	}
	private sealed class RevelationSE : BibleBook
	{
		public RevelationSE() : base("Revelation", 66) { }
		public override string Title => "Revelation";
		public override string Abrv => "Rev";
		public override BookGroupEnum BookGroupEnum => BookGroupEnum.Apocalypse;
		public override BookEnum BookEnum => BookEnum.Revelation;
		public override int LastChapter => 22;
		public override string TransliterationInHebrew => "Gilyahna";
		public override string NameInHebrew => "";
	}
	#endregion
}
