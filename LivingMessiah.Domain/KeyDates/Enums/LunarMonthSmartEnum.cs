using Ardalis.SmartEnum;

namespace LivingMessiah.Domain.KeyDates.Enums
{
	public abstract class LunarMonthSmartEnum : SmartEnum<LunarMonthSmartEnum>
	{
		private static class Id
		{
			internal const int Nissan = 1;
			internal const int Iyar = 2;
			internal const int Sivan = 3;
			internal const int Tammuz = 4;
			internal const int Av = 5;
			internal const int Elul = 6;
			internal const int Tishri = 7;
			internal const int Heshvan = 8;
			internal const int Kislev = 9;
			internal const int Tevet = 10;
			internal const int Shevat = 11;
			internal const int Adar = 12;
			internal const int Adar2 = 13;
		}

		public static readonly LunarMonthSmartEnum Nissan = new NissanMonth();
		public static readonly LunarMonthSmartEnum Iyar = new IyarMonth();
		public static readonly LunarMonthSmartEnum Sivan = new SivanMonth();
		public static readonly LunarMonthSmartEnum Tammuz = new TammuzMonth();
		public static readonly LunarMonthSmartEnum Av = new AvMonth();
		public static readonly LunarMonthSmartEnum Elul = new ElulMonth();
		public static readonly LunarMonthSmartEnum Tishri = new TishriMonth();
		public static readonly LunarMonthSmartEnum Heshvan = new HeshvanMonth();
		public static readonly LunarMonthSmartEnum Kislev = new KislevMonth();
		public static readonly LunarMonthSmartEnum Tevet = new TevetMonth();
		public static readonly LunarMonthSmartEnum Shevat = new ShevatMonth();
		public static readonly LunarMonthSmartEnum Adar = new AdarMonth();

		private LunarMonthSmartEnum(string name, int value) : base(name, value)
		{
		}

		//public abstract string Icon { get; }  //"far fa-moon" 

		public abstract string Hebrew { get; }
		public abstract string BiblicalName { get; }
		public abstract string BiblicalHebrew { get; }  // http://www.yashanet.com/library/hebrew-days-and-months.html


		private sealed class NissanMonth : LunarMonthSmartEnum
		{
			public NissanMonth() : base($"{nameof(Id.Nissan)}", Id.Nissan) { }
			public override string Hebrew => "ניסן"; public override string BiblicalName => "Abib"; public override string BiblicalHebrew => "הָאָבִיב";
		}

		private sealed class IyarMonth : LunarMonthSmartEnum
		{
			public IyarMonth() : base($"{nameof(Id.Iyar)}", Id.Iyar) { }
			public override string Hebrew => "אייר"; public override string BiblicalName => "Ziv"; public override string BiblicalHebrew => "זִו";
		}

		private sealed class SivanMonth : LunarMonthSmartEnum
		{
			public SivanMonth() : base($"{nameof(Id.Sivan)}", Id.Sivan) { }
			public override string Hebrew => "סיון"; public override string BiblicalName => "3rd"; public override string BiblicalHebrew => "";
		}

		private sealed class TammuzMonth : LunarMonthSmartEnum
		{
			public TammuzMonth() : base($"{nameof(Id.Tammuz)}", Id.Tammuz) { }
			public override string Hebrew => "תמוז"; public override string BiblicalName => "4th"; public override string BiblicalHebrew => "";
		}

		private sealed class AvMonth : LunarMonthSmartEnum
		{
			public AvMonth() : base($"{nameof(Id.Av)}", Id.Av) { }
			public override string Hebrew => "אב"; public override string BiblicalName => "5th"; public override string BiblicalHebrew => "";
		}

		private sealed class ElulMonth : LunarMonthSmartEnum
		{
			public ElulMonth() : base($"{nameof(Id.Elul)}", Id.Elul) { }
			public override string Hebrew => "אלול"; public override string BiblicalName => "6th"; public override string BiblicalHebrew => "";
		}

		private sealed class TishriMonth : LunarMonthSmartEnum
		{
			public TishriMonth() : base($"{nameof(Id.Tishri)}", Id.Tishri) { }
			public override string Hebrew => "תשרי"; public override string BiblicalName => "Ethanim"; public override string BiblicalHebrew => "הָאֵתָנִים";
		}

		private sealed class HeshvanMonth : LunarMonthSmartEnum
		{
			public HeshvanMonth() : base($"{nameof(Id.Heshvan)}", Id.Heshvan) { }
			public override string Hebrew => "חשון"; public override string BiblicalName => "Bul"; public override string BiblicalHebrew => "בּוּל";
		}

		private sealed class KislevMonth : LunarMonthSmartEnum
		{
			public KislevMonth() : base($"{nameof(Id.Kislev)}", Id.Kislev) { }
			public override string Hebrew => "כסלו"; public override string BiblicalName => "9th"; public override string BiblicalHebrew => "";
		}

		private sealed class TevetMonth : LunarMonthSmartEnum
		{
			public TevetMonth() : base($"{nameof(Id.Tevet)}", Id.Tevet) { }
			public override string Hebrew => "טבת"; public override string BiblicalName => "10th"; public override string BiblicalHebrew => "";
		}

		private sealed class ShevatMonth : LunarMonthSmartEnum
		{
			public ShevatMonth() : base($"{nameof(Id.Shevat)}", Id.Shevat) { }
			public override string Hebrew => "שבט"; public override string BiblicalName => "11th"; public override string BiblicalHebrew => "";
		}

		private sealed class AdarMonth : LunarMonthSmartEnum
		{
			public AdarMonth() : base($"{nameof(Id.Adar)}", Id.Adar) { }
			public override string Hebrew => "אדר א"; public override string BiblicalName => "12th"; public override string BiblicalHebrew => "";
		}

	} 

}


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

*/
