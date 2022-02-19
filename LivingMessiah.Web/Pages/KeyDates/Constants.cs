namespace LivingMessiah.Web.Pages.KeyDates.Constants;

/*
ToDo: Delete
Used by: LivingMessiah.Web\Components\Pages\Shavuot\... 
- Header.razor
- OmerCount.razor
*/
public static class Years
{
	public const int Previous = 2021; // 2020
	public const int Current = 2022; // 2021
}

/*
ToDo: Delete
Used by: LivingMessiah.Web\Components\Pages\Shavuot\... 
- OmerCount.razor
- CountingInstructions.razor
- Shavuot.razor.cs
- TablePrint.razor
*/
public static class Omer
{
		// Days added to Dates._12_Passover
		public const int DaysAdded = 2; // If Sadducee interpretation, it's always 2
		public static System.DateTime Date { get; set; } =
			System.DateTime.Parse(Dates._12_Passover).AddDays(DaysAdded);
		public static string FirstDayAfterTheSabbath = Date.ToString("d");

		public static int CountInDays()
		{
				System.DateTime start = Omer.Date;
				start = start.AddDays(-1);
				System.DateTime cur = System.DateTime.Now;

				System.TimeSpan difference = cur - start;
				int days = (int)difference.TotalDays;
				return days;
		}
}

public static class Dates
{
		public const string _12_Passover = "4/17/2022";  // 4/25/2021
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
