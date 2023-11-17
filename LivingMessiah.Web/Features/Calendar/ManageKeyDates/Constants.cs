
using System;
using CalendarEnumDateType = LivingMessiah.Web.Features.Calendar.Enums.DateType;
using CalendarEnumFeastDay = LivingMessiah.Web.Features.Calendar.Enums.FeastDay;

namespace LivingMessiah.Web.Features.Calendar.ManageKeyDates.Constants;

public static class Defaults
{
	public static CalendarEnumDateType Filter = CalendarEnumDateType.Feast;
	public const int MonthLengthAverage = 29;
	public const int SeasonLengthAverage = 91;
}

public static class Omer
{
	//public static System.DateTime Date { get; set; } = System.DateTime.Parse(Dates._12_Passover).AddDays(CalendarEnumFeastDayDetail.OmerStart.AddDays);
	//public static System.DateTime Date { get; set; } =
	//	System.DateTime.Parse(Dates._12_Passover).AddDays(CalendarEnumFeastDay.Passover.AddDays);

	public static System.DateTime Date { get; set; } =	 Enums.FeastDay.Passover.Date.AddDays(2);
}

public static class Dates
{
	public const string _12_Passover = "4/22/2024";
	/*
	Called by...
	- Pages\Articles\Pesach.razor.cs
	- Pages\Shavuot\Constants.cs 

	Logically connected to 
	- Features\Calendar\Enums\FeastDayDetail.cs
	
	ToDo: this is a duplication of information and needs to be centralized.

	*/

	/*
	" And you shall count from the <b>morrow of the Sabbath</b> from the day you bring the Omer [sheaf] of waving; ...
	Leviticus 23:15-16,21
	https://www.nehemiaswall.com/truth-shavuot

	There's a debate about what the <b>morrow of the Sabbath</b> means, is it...
	- "morrow of the 1st day of <b>Unleavened Bread.<b>"
		- Interpretation of the <b>Pharisees</b> and "followed by most Jews until this very day."
		- Therefore a High Sabbath and which is always on the second day of Unleavened Bread (Aviv 16)
	or
	- "morrow of the weekly Sabbathy" 
		- Interpretation of the <b>Sadducees</b>
		- on the weekly Sabbath that falls out during the seven-days of the Feast of Unleavened Bread. 
		- Therefore the date in Aviv and day of the week varies between Aviv 15th to the 21st 

	orElse FWIW
	- in the link above, Nehemiah Gordon says there's a third option of the <b>Essenes</b> 
*/
}
