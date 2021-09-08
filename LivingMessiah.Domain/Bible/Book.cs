using Ardalis.SmartEnum;

namespace LivingMessiah.Domain.Bible
{
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

	public abstract class Book : SmartEnum<Book>
	{
		public static readonly Book Genesis = new GenesisBook();
		public static readonly Book Exodus = new ExodusBook();
		public static readonly Book Leviticus = new LeviticusBook();
		public static readonly Book Numbers = new NumbersBook();
		public static readonly Book Deuteronomy = new DeuteronomyBook();
		public static readonly Book Psalms = new PsalmsBook();
		public static readonly Book Proverbs = new ProverbsBook();

		private Book(string name, int value) : base(name, value)  //, ushort value
		{
		}

		public abstract string Abrv { get; }
		public abstract BookGroupEnum BookGroupEnum { get; }

		public string Dump 
		{
			get {
				return  $" {this.Value}-{this.Abrv}-{this.Name}-{this.BookGroupEnum}";
			}
		}

		/*
		Extra attributes I could add if I want to
		public abstract string EnglishTitle { get; }
		public abstract string HebrewTitle { get; }
		public abstract string HebrewName { get; }
		*/

		private sealed class GenesisBook : Book
		{
			public GenesisBook() : base("Genesis", 1) { }
			public override string Abrv => "Gen";
			public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		}

		private sealed class ExodusBook : Book
		{
			public ExodusBook() : base("Exodus", 2) { }
			public override string Abrv => "Exo";
			public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		}

		private sealed class LeviticusBook : Book
		{
			public LeviticusBook() : base("Leviticus", 3) { }
			public override string Abrv => "Lev";
			public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		}

		private sealed class NumbersBook : Book
		{
			public NumbersBook() : base("Numbers", 4) { }
			public override string Abrv => "Num";
			public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		}

		private sealed class DeuteronomyBook : Book
		{
			public DeuteronomyBook() : base("Deuteronomy", 5) { }
			public override string Abrv => "Deu";
			public override BookGroupEnum BookGroupEnum => BookGroupEnum.Torah;
		}

		private sealed class PsalmsBook : Book
		{
			public PsalmsBook() : base("Psalms", 19) { }
			public override string Abrv => "Psa";
			public override BookGroupEnum BookGroupEnum => BookGroupEnum.Poetry;
		}

		private sealed class ProverbsBook : Book
		{
			public ProverbsBook() : base("Proverbs", 20) { }
			public override string Abrv => "Pro";
			public override BookGroupEnum BookGroupEnum => BookGroupEnum.Poetry;
		}

	}
}

/*
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

*/