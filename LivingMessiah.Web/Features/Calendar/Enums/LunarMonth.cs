using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.Calendar.Enums;

public abstract class LunarMonth : SmartEnum<LunarMonth>
{
	internal const string PGY = " Prev. Greg. Year";
	internal const string Adar2Suffix = " II";

	#region Id's
	private static class Id
	{
		internal const int HeshvanPrevGregYr = 1;
		internal const int KislevPrevGregYr = 2;
		internal const int TevetPrevGregYr = 3;
		internal const int Shevat = 4;
		internal const int Adar = 5;
		internal const int Nissan = 6;
		internal const int Iyar = 7;
		internal const int Sivan = 8;
		internal const int Tammuz = 9;
		internal const int Av = 10;
		internal const int Elul = 11;
		internal const int Tishri = 12;
		internal const int Heshvan = 13;
		internal const int Kislev = 14;
		internal const int Tevet = 15;
		internal const int Adar2 = 16;
	}
	#endregion

	public static readonly LunarMonth Nissan = new NissanMonth();
	public static readonly LunarMonth Iyar = new IyarMonth();
	public static readonly LunarMonth Sivan = new SivanMonth();
	public static readonly LunarMonth Tammuz = new TammuzMonth();
	public static readonly LunarMonth Av = new AvMonth();
	public static readonly LunarMonth Elul = new ElulMonth();
	public static readonly LunarMonth Tishri = new TishriMonth();
	public static readonly LunarMonth HeshvanPrevGregYr = new HeshvanPrevGregYrMonth();
	public static readonly LunarMonth KislevPrevGregYr = new KislevPrevGregYrMonth();
	public static readonly LunarMonth TevetPrevGregYr = new TevetPrevGregYrMonth();
	public static readonly LunarMonth Shevat = new ShevatMonth();
	public static readonly LunarMonth Adar = new AdarMonth();
	public static readonly LunarMonth Heshvan = new HeshvanMonth();
	public static readonly LunarMonth Kislev = new KislevMonth();
	public static readonly LunarMonth Tevet = new TevetMonth();
	public static readonly LunarMonth Adar2 = new Adar2Month();

	private LunarMonth(string name, int value) : base(name, value) { }

	//public abstract string Icon { get; }  //"far fa-moon" 

	#region Extra Fields
	public abstract string FullName { get; }
	public abstract string Hebrew { get; }
	public abstract string BiblicalName { get; }
	public abstract string BiblicalHebrew { get; }  // http://www.yashanet.com/library/hebrew-days-and-months.html
	public abstract bool IsPreviousYear { get; }
	#endregion

	#region Private Instantiation

	private sealed class NissanMonth : LunarMonth
	{
		public NissanMonth() : base($"{nameof(Id.Nissan)}", Id.Nissan) { }
		public override string Hebrew => "ניסן"; public override string BiblicalName => "Abib"; public override string BiblicalHebrew => "הָאָבִיב";
		public override string FullName => nameof(Id.Nissan);
		public override bool IsPreviousYear => false;
	}

	private sealed class IyarMonth : LunarMonth
	{
		public IyarMonth() : base($"{nameof(Id.Iyar)}", Id.Iyar) { }
		public override string Hebrew => "אייר"; public override string BiblicalName => "Ziv"; public override string BiblicalHebrew => "זִו";
		public override string FullName => nameof(Id.Iyar);
		public override bool IsPreviousYear => false;
	}

	private sealed class SivanMonth : LunarMonth
	{
		public SivanMonth() : base($"{nameof(Id.Sivan)}", Id.Sivan) { }
		public override string Hebrew => "סיון"; public override string BiblicalName => "3rd"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Sivan);
		public override bool IsPreviousYear => false;
	}

	private sealed class TammuzMonth : LunarMonth
	{
		public TammuzMonth() : base($"{nameof(Id.Tammuz)}", Id.Tammuz) { }
		public override string Hebrew => "תמוז"; public override string BiblicalName => "4th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Tammuz);
		public override bool IsPreviousYear => false;
	}

	private sealed class AvMonth : LunarMonth
	{
		public AvMonth() : base($"{nameof(Id.Av)}", Id.Av) { }
		public override string Hebrew => "אב"; public override string BiblicalName => "5th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Av);
		public override bool IsPreviousYear => false;
	}

	private sealed class ElulMonth : LunarMonth
	{
		public ElulMonth() : base($"{nameof(Id.Elul)}", Id.Elul) { }
		public override string Hebrew => "אלול"; public override string BiblicalName => "6th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Elul);
		public override bool IsPreviousYear => false;
	}

	private sealed class TishriMonth : LunarMonth
	{
		public TishriMonth() : base($"{nameof(Id.Tishri)}", Id.Tishri) { }
		public override string Hebrew => "תשרי"; public override string BiblicalName => "Ethanim"; public override string BiblicalHebrew => "הָאֵתָנִים";
		public override string FullName => nameof(Id.Tishri);
		public override bool IsPreviousYear => false;
	}


	private sealed class HeshvanPrevGregYrMonth : LunarMonth
	{
		public HeshvanPrevGregYrMonth() : base($"{nameof(Id.HeshvanPrevGregYr)}", Id.HeshvanPrevGregYr) { }
		public override string Hebrew => "חשון"; public override string BiblicalName => "Bul"; public override string BiblicalHebrew => "בּוּל";
		public override string FullName => nameof(Id.Heshvan) + PGY;
		public override bool IsPreviousYear => true;
	}

	private sealed class KislevPrevGregYrMonth : LunarMonth
	{
		public KislevPrevGregYrMonth() : base($"{nameof(Id.KislevPrevGregYr)}", Id.KislevPrevGregYr) { }
		public override string Hebrew => "כסלו"; public override string BiblicalName => "9th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Kislev) + PGY;
		public override bool IsPreviousYear => true;
	}

	private sealed class TevetPrevGregYrMonth : LunarMonth
	{
		public TevetPrevGregYrMonth() : base($"{nameof(Id.TevetPrevGregYr)}", Id.TevetPrevGregYr) { }
		public override string Hebrew => "טבת"; public override string BiblicalName => "10th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Tevet) + PGY;
		public override bool IsPreviousYear => true;
	}


	private sealed class HeshvanMonth : LunarMonth
	{
		public HeshvanMonth() : base($"{nameof(Id.Heshvan)}", Id.Heshvan) { }
		public override string Hebrew => "חשון"; public override string BiblicalName => "Bul"; public override string BiblicalHebrew => "בּוּל";
		public override string FullName => nameof(Id.Heshvan);
		public override bool IsPreviousYear => false;
	}

	private sealed class KislevMonth : LunarMonth
	{
		public KislevMonth() : base($"{nameof(Id.Kislev)}", Id.Kislev) { }
		public override string Hebrew => "כסלו"; public override string BiblicalName => "9th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Kislev);
		public override bool IsPreviousYear => false;
	}

	private sealed class TevetMonth : LunarMonth
	{
		public TevetMonth() : base($"{nameof(Id.Tevet)}", Id.Tevet) { }
		public override string Hebrew => "טבת"; public override string BiblicalName => "10th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Tevet);
		public override bool IsPreviousYear => false;
	}


	private sealed class ShevatMonth : LunarMonth
	{
		public ShevatMonth() : base($"{nameof(Id.Shevat)}", Id.Shevat) { }
		public override string Hebrew => "שבט"; public override string BiblicalName => "11th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Shevat);
		public override bool IsPreviousYear => false;
	}

	private sealed class AdarMonth : LunarMonth
	{
		public AdarMonth() : base($"{nameof(Id.Adar)}", Id.Adar) { }
		public override string Hebrew => "אדר א"; public override string BiblicalName => "12th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Adar);
		public override bool IsPreviousYear => false;
	}

	private sealed class Adar2Month : LunarMonth
	{
		public Adar2Month() : base($"{nameof(Id.Adar2)}", Id.Adar2) { }
		public override string Hebrew => "אדר ב"; public override string BiblicalName => "13th"; public override string BiblicalHebrew => "";
		public override string FullName => nameof(Id.Adar) + Adar2Suffix;
		public override bool IsPreviousYear => false;
	}
	#endregion
}

// Ignore Spelling: Nissan
// Ignore Spelling: Iyar
// Ignore Spelling: Sivan
// Ignore Spelling: Tammuz
// Ignore Spelling: Av
// Ignore Spelling: Elul
// Ignore Spelling: Tishri

// Ignore Spelling: Shevat
// Ignore Spelling: Adar
// Ignore Spelling: Heshvan
// Ignore Spelling: Kislev
// Ignore Spelling: Tevet
// Ignore Spelling: Adar2

/*

# This was how to do code generation for the old version of SmartEnum

SELECT 
'public static LunarMonthLocal ' + Month + ' { get; } = new LunarMonthLocal(LunarMonthEnum.' + Month + ', ' + CAST(EnumId AS varchar(30)) + 
', ' + QUOTENAME([Month], CHAR(34)) +
', ' + QUOTENAME(Hebrew, CHAR(34)) +
	', ' + QUOTENAME(BiblicalName, CHAR(34)) +
', ' + QUOTENAME(BiblicalHebrew, CHAR(34)) +
', ' + QUOTENAME('far fa-moon:', CHAR(34))+ ');'	
AS CodeGen
FROM KeyDate.LunarMonth 
WHERE  MONTH NOT LIKE '%(previous)%'
ORDER BY EnumId

--SELECT * 	FROM KeyDate.LunarMonth 



			internal const int Nissan = 1;
			internal const int Iyar = 2;
			internal const int Sivan = 3;
			internal const int Tammuz = 4;
			internal const int Av = 5;
			internal const int Elul = 6;
			internal const int Tishri = 7;
			internal const int HeshvanPrevGregYr = 8;
			internal const int KislevPrevGregYr = 9;
			internal const int TevetPrevGregYr = 10;
			internal const int Shevat = 11;
			internal const int Adar = 12;
			internal const int Heshvan = 13;
			internal const int Kislev = 14;
			internal const int Tevet = 15;
			internal const int Adar2 = 16;



*/
