using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Domain.KeyDates.Enums
{

	public enum LunarMonthEnum
	{
		Nissan = 1,
		Iyar = 2,
		Sivan =3,
		Tammuz = 4,
		Av = 5,
		Elul = 6,
		Tishri = 7,
		Heshvan = 8,
		Kislev = 9,
		Tevet = 10,
		Shevat = 11,
		Adar = 12,
		Adar2 = 13
	}

	public class LunarMonthLocal
	{
		public static List<LunarMonthLocal> All { get; } = new List<LunarMonthLocal>();

		public static LunarMonthLocal Nissan { get; } = new LunarMonthLocal(LunarMonthEnum.Nissan, 1, "Nissan", "ניסן", "Abib", "הָאָבִיב");
		public static LunarMonthLocal Iyar { get; } = new LunarMonthLocal(LunarMonthEnum.Iyar, 2, "Iyar", "אייר", "Ziv", "זִו");
		public static LunarMonthLocal Sivan { get; } = new LunarMonthLocal(LunarMonthEnum.Sivan, 3, "Sivan", "סיון", "3rd", "");
		public static LunarMonthLocal Tammuz { get; } = new LunarMonthLocal(LunarMonthEnum.Tammuz, 4, "Tammuz", "תמוז", "4th", "");
		public static LunarMonthLocal Av { get; } = new LunarMonthLocal(LunarMonthEnum.Av, 5, "Av", "אב", "5th", "");
		public static LunarMonthLocal Elul { get; } = new LunarMonthLocal(LunarMonthEnum.Elul, 6, "Elul", "אלול", "6th", "");
		public static LunarMonthLocal Tishri { get; } = new LunarMonthLocal(LunarMonthEnum.Tishri, 7, "Tishri", "תשרי", "Ethanim", "הָאֵתָנִים");
		public static LunarMonthLocal Heshvan { get; } = new LunarMonthLocal(LunarMonthEnum.Heshvan, 8, "Heshvan", "חשון", "Bul", "בּוּל");
		public static LunarMonthLocal Kislev { get; } = new LunarMonthLocal(LunarMonthEnum.Kislev, 9, "Kislev", "כסלו", "9th", "");
		public static LunarMonthLocal Tevet { get; } = new LunarMonthLocal(LunarMonthEnum.Tevet, 10, "Tevet", "טבת", "10th", "");
		public static LunarMonthLocal Shevat { get; } = new LunarMonthLocal(LunarMonthEnum.Shevat, 11, "Shevat", "שבט", "11th", "");
		public static LunarMonthLocal Adar { get; } = new LunarMonthLocal(LunarMonthEnum.Adar, 12, "Adar", "אדר א", "12th", "");

		public LunarMonthEnum LunarMonthEnum {  get; private set; }
		public int Id { get; private set; }  // Aka Number
		public string Name {  get; private set; }  // Aka Month
		public string Hebrew {  get; private set; }
		public string BiblicalName {  get; private set; }
		public string BiblicalHebrew {  get; private set; } // http://www.yashanet.com/library/hebrew-days-and-months.html
		public string Icon { get; private set; } = "far fa-moon";

		private LunarMonthLocal(LunarMonthEnum lunarMonthEnum, int id, string month, string hebrew,
			string biblicalName,  string biblicalHebrew)
		{
			LunarMonthEnum = lunarMonthEnum;
			Id = id;
			Name = month;
			Hebrew = hebrew;
			BiblicalName = biblicalName;
			BiblicalHebrew = biblicalHebrew;
			All.Add(this);
		}

		public static LunarMonthLocal FromEnum(LunarMonthEnum enumValue)
		{
			return All.SingleOrDefault(r => r.LunarMonthEnum == enumValue);
		}

		public static LunarMonthLocal FromInt(int intValue)
		{
			return All.SingleOrDefault(r => r.Id == intValue);
		}

		public override string ToString()
		{
			return $@"Id: {Id}, Name: {Name}, Icon: {Icon}";
		}


	} // class LunarMonthLocal
} // namespace

/*
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


