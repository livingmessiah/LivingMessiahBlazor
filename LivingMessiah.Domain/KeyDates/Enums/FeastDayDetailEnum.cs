using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Domain.KeyDates.Enums
{
	public enum FeastDayDetailEnum
	{
		//                          Id-FeastDayId-Detail
		SederMeal = 1,						// 1-3-1
		UnleavenedBreadDay1 = 2,  // 1-3-2
		OmerStart = 3,            // 1-3-3
		UnleavenedBreadDay7 = 4,  // 1-3-4
		SukkotDay0 = 5,						// 5-7-2
		SukkotDay1 = 6,						// 6-7-2
		SukkotLastGreatDay = 7,		// 7-7-3
		SukkotEndsAtSundown = 8,	// 8-7-4
		HanukkahLastDay = 9,			// not using
		YomKippurBegins = 10			// not using
	}

	public class FDD
	{
		public static List<FDD> All { get; } = new List<FDD>();

		public static FDD SederMeal { get; } = new FDD(FeastDayDetailEnum.SederMeal, 1, 0, "Seder Meal", false);
		public static FDD UnleavenedBreadDay1 { get; } = new FDD(FeastDayDetailEnum.UnleavenedBreadDay1, 2, 1, "First day of Unleavened Bread", true);
		public static FDD OmerStart { get; } = new FDD(FeastDayDetailEnum.OmerStart, 3, 2, "Omer Start", false);  // This is more dynamic
		public static FDD UnleavenedBreadDay7 { get; } = new FDD(FeastDayDetailEnum.UnleavenedBreadDay7, 4, 7, "Last day of Unleavened Bread", true);

		public static FDD SukkotDay0 { get; } = 
			new FDD(FeastDayDetailEnum.SukkotDay0, 5, -1, "Sukkot Day: Preparation Day, High Sabbath begins at sunset", true);
		public static FDD SukkotDay1 { get; } = 
			new FDD(FeastDayDetailEnum.SukkotDay1, 6, 0, "Sukkot Day: First Day", false);
		public static FDD SukkotLastGreatDay { get; } = 
			new FDD(FeastDayDetailEnum.SukkotLastGreatDay, 7, 7, "Sukkot Last Day (Great Day)", true);
		public static FDD SukkotEndsAtSundown { get; } = 
			new FDD(FeastDayDetailEnum.SukkotEndsAtSundown, 8, 8, "Sukkot ended previoius night; tear down camp", false); 

		public static FDD HanukkahLastDay { get; } = new FDD(FeastDayDetailEnum.HanukkahLastDay, 9, 8, "Hanukkah Last Day", false);
		public static FDD YomKippurBegins { get; } = new FDD(FeastDayDetailEnum.YomKippurBegins, 10, -1, "Yom Kippur Begins", true);

		public FeastDayDetailEnum FeastDayDetailEnum { get; private set; }
		public int Id { get; private set; }
		public int AddDays { get; set; }
		public string Description { get; private set; }
		public bool IsHighSabbath { get; set; }

		private FDD(FeastDayDetailEnum fddEnum, int id, int addDays, string description, bool isHighSabbath)
		{
			FeastDayDetailEnum = fddEnum;
			Id = id;
			AddDays = addDays;
			Description = description;
			IsHighSabbath = isHighSabbath;
			All.Add(this);
		}

		public static FDD FromEnum(FeastDayDetailEnum enumValue)
		{
			return All.SingleOrDefault(r => r.FeastDayDetailEnum == enumValue);
		}

		public static FDD FromInt(int intValue)
		{
			return All.SingleOrDefault(r => r.Id == intValue);
		}

		/*
		public static FDD FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}
		*/

	}  // class FDD
}    // namespace


/*

SELECT Id, FeastDayId, Detail, Name FROM KeyDate.FeastDayDetail

Id	FeastDayId	Detail	Name

		Passover
1				3					1				Passover Seder Meal
2				3					2				Feast of unleavened bread (1st day)
3				3					3				First Day of Omer
4				3					4				Feast of unleavened bread (last day)

		Tabernacles									
5				7					1				Preperation day, High Sabbath begins at sunset
6				7					2				1st day
7				7					3				Last Great Day
8				7					4				Ending of Sukkot at sundown

		Hanukkah
9				4/8				1				HanukkahLastDay

		Yom Kippur
10			6					1				Yom Kippur Begins


SELECT fd.Id, Name, DateId, CONVERT(nvarchar(30), d.Date, 111) AS DateYMD
FROM KeyDate.FeastDay fd
INNER JOIN KeyDate.Date d ON fd.DateId = d.Id

Id	Name					DateId	DateYMD
1		Hanukkah				4			2020/12/10
2		Purim						9			2021/02/26
3		Passover				13		2021/04/25
4		Weeks						16		2021/06/15
5		Trumpets				22		2021/10/06
6		Yom Kippur			24		2021/10/16
7		Tabernacles			25		2021/10/20
8		Hanukkah EOY		27		2021/11/29 

 */
