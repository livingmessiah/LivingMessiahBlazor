using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.KeyDates.Enums
{
	public abstract class BaseLunarMonthSmartEnum : SmartEnum<BaseLunarMonthSmartEnum>
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

		public static readonly BaseLunarMonthSmartEnum Nissan = new NissanMonth();
		public static readonly BaseLunarMonthSmartEnum Iyar = new IyarMonth();
		public static readonly BaseLunarMonthSmartEnum Sivan = new SivanMonth();
		public static readonly BaseLunarMonthSmartEnum Tammuz = new TammuzMonth();
		public static readonly BaseLunarMonthSmartEnum Av = new AvMonth();
		public static readonly BaseLunarMonthSmartEnum Elul = new ElulMonth();
		public static readonly BaseLunarMonthSmartEnum Tishri = new TishriMonth();
		public static readonly BaseLunarMonthSmartEnum HeshvanPrevGregYr = new HeshvanPrevGregYrMonth();
		public static readonly BaseLunarMonthSmartEnum KislevPrevGregYr = new KislevPrevGregYrMonth();
		public static readonly BaseLunarMonthSmartEnum TevetPrevGregYr = new TevetPrevGregYrMonth();
		public static readonly BaseLunarMonthSmartEnum Shevat = new ShevatMonth();
		public static readonly BaseLunarMonthSmartEnum Adar = new AdarMonth();
		public static readonly BaseLunarMonthSmartEnum Heshvan = new HeshvanMonth();
		public static readonly BaseLunarMonthSmartEnum Kislev = new KislevMonth();
		public static readonly BaseLunarMonthSmartEnum Tevet = new TevetMonth();
		public static readonly BaseLunarMonthSmartEnum Adar2 = new Adar2Month();

		private BaseLunarMonthSmartEnum(string name, int value) : base(name, value)	{	}

		//public abstract string Icon { get; }  //"far fa-moon" 

		#region Extra Fields
		public abstract string FullName { get; }
		public abstract string Hebrew { get; }
		public abstract string BiblicalName { get; }
		public abstract string BiblicalHebrew { get; }  // http://www.yashanet.com/library/hebrew-days-and-months.html
		#endregion

		#region Private Instantiation

		private sealed class NissanMonth : BaseLunarMonthSmartEnum
		{
			public NissanMonth() : base($"{nameof(Id.Nissan)}", Id.Nissan) { }
			public override string Hebrew => "ניסן"; public override string BiblicalName => "Abib"; public override string BiblicalHebrew => "הָאָבִיב";
			public override string FullName => nameof(Id.Nissan);
		}

		private sealed class IyarMonth : BaseLunarMonthSmartEnum
		{
			public IyarMonth() : base($"{nameof(Id.Iyar)}", Id.Iyar) { }
			public override string Hebrew => "אייר"; public override string BiblicalName => "Ziv"; public override string BiblicalHebrew => "זִו";
			public override string FullName => nameof(Id.Iyar);
		}

		private sealed class SivanMonth : BaseLunarMonthSmartEnum
		{
			public SivanMonth() : base($"{nameof(Id.Sivan)}", Id.Sivan) { }
			public override string Hebrew => "סיון"; public override string BiblicalName => "3rd"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Sivan);
		}

		private sealed class TammuzMonth : BaseLunarMonthSmartEnum
		{
			public TammuzMonth() : base($"{nameof(Id.Tammuz)}", Id.Tammuz) { }
			public override string Hebrew => "תמוז"; public override string BiblicalName => "4th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Tammuz);
		}

		private sealed class AvMonth : BaseLunarMonthSmartEnum
		{
			public AvMonth() : base($"{nameof(Id.Av)}", Id.Av) { }
			public override string Hebrew => "אב"; public override string BiblicalName => "5th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Av);
		}

		private sealed class ElulMonth : BaseLunarMonthSmartEnum
		{
			public ElulMonth() : base($"{nameof(Id.Elul)}", Id.Elul) { }
			public override string Hebrew => "אלול"; public override string BiblicalName => "6th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Elul);
		}

		private sealed class TishriMonth : BaseLunarMonthSmartEnum
		{
			public TishriMonth() : base($"{nameof(Id.Tishri)}", Id.Tishri) { }
			public override string Hebrew => "תשרי"; public override string BiblicalName => "Ethanim"; public override string BiblicalHebrew => "הָאֵתָנִים";
			public override string FullName => nameof(Id.Tishri);
		}

		
		private sealed class HeshvanPrevGregYrMonth : BaseLunarMonthSmartEnum
		{
			public HeshvanPrevGregYrMonth() : base($"{nameof(Id.HeshvanPrevGregYr)}", Id.HeshvanPrevGregYr) { }
			public override string Hebrew => "חשון"; public override string BiblicalName => "Bul"; public override string BiblicalHebrew => "בּוּל";
			public override string FullName => nameof(Id.Heshvan) + PGY;
		}

		private sealed class KislevPrevGregYrMonth : BaseLunarMonthSmartEnum
		{
			public KislevPrevGregYrMonth() : base($"{nameof(Id.KislevPrevGregYr)}", Id.KislevPrevGregYr) { }
			public override string Hebrew => "כסלו"; public override string BiblicalName => "9th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Kislev) + PGY;
		}

		private sealed class TevetPrevGregYrMonth : BaseLunarMonthSmartEnum
		{
			public TevetPrevGregYrMonth() : base($"{nameof(Id.TevetPrevGregYr)}", Id.TevetPrevGregYr) { }
			public override string Hebrew => "טבת"; public override string BiblicalName => "10th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Tevet) + PGY;
		}


		private sealed class HeshvanMonth : BaseLunarMonthSmartEnum
		{
			public HeshvanMonth() : base($"{nameof(Id.Heshvan)}", Id.Heshvan) { }
			public override string Hebrew => "חשון"; public override string BiblicalName => "Bul"; public override string BiblicalHebrew => "בּוּל";
			public override string FullName => nameof(Id.Heshvan);
		}

		private sealed class KislevMonth : BaseLunarMonthSmartEnum
		{
			public KislevMonth() : base($"{nameof(Id.Kislev)}", Id.Kislev) { }
			public override string Hebrew => "כסלו"; public override string BiblicalName => "9th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Kislev);
		}

		private sealed class TevetMonth : BaseLunarMonthSmartEnum
		{
			public TevetMonth() : base($"{nameof(Id.Tevet)}", Id.Tevet) { }
			public override string Hebrew => "טבת"; public override string BiblicalName => "10th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Tevet);
		}


		private sealed class ShevatMonth : BaseLunarMonthSmartEnum
		{
			public ShevatMonth() : base($"{nameof(Id.Shevat)}", Id.Shevat) { }
			public override string Hebrew => "שבט"; public override string BiblicalName => "11th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Shevat);
		}

		private sealed class AdarMonth : BaseLunarMonthSmartEnum
		{
			public AdarMonth() : base($"{nameof(Id.Adar)}", Id.Adar) { }
			public override string Hebrew => "אדר א"; public override string BiblicalName => "12th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Adar);
		}

		private sealed class Adar2Month : BaseLunarMonthSmartEnum
		{
			public Adar2Month() : base($"{nameof(Id.Adar2)}", Id.Adar2) { }
			public override string Hebrew => "אדר ב"; public override string BiblicalName => "13th"; public override string BiblicalHebrew => "";
			public override string FullName => nameof(Id.Adar) + Adar2Suffix;
		}
		#endregion
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
