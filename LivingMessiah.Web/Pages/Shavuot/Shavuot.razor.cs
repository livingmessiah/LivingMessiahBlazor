using LivingMessiah.Web.Pages.KeyDates.Constants;
using System;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Shavuot
{
	public partial class Shavuot
	{
		protected int CurrentDay { get; set; }

		protected override Task OnInitializedAsync()
		{
			// CurrentDay = GetCurrentDay("5/1/2020"); // Save for testing
			CurrentDay = GetCurrentDay();
			return base.OnInitializedAsync();
		}

		private static int GetCurrentDay(string overrideDate="")
		{
			DateTime cur;
			DateTime curOverRide;
			if (DateTime.TryParse(overrideDate, out curOverRide))
			{
				cur = curOverRide;
			}
			else
			{
				cur = DateTime.Now;
			}

			DateTime start = Omer.Date;
			start = start.AddDays(-1);
			TimeSpan difference = cur - start;
			int days = (int)difference.TotalDays;
			return days;
		}

	}
}
