using LivingMessiah.Web.Features.Calendar.ManageKeyDates.Constants;

namespace LivingMessiah.Web.Features.Shavuot;

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