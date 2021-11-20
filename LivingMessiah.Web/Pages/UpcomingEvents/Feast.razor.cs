using System;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.UpcomingEvents
{
	partial class Feast : BaseKeyDates
	{
		[Parameter]
		public FeastDayLocal FeastDayLocal { get; set; }

		public string GetDateHtml()
		{
			return "NOT DONE";
			/*
			if (AddDaysDescr == "" || AddDays == 0)
			{
				return "<span class='float-right'>" + Date.ToString("ddd, MM/dd") + "</span>";
			}
			else
			{
				if (AddDays < 0)
				{
					// return AddDaysDescr + " " + Date.AddDays(AddDays).ToString("ddd, MM/dd") + "<br />" + Date.ToString("ddd, MM/dd");
					return $@"
 <span class='float-right'> 
{AddDaysDescr} {Date.AddDays(AddDays):ddd, MM/dd} 
</span>
<br /> 
<span class='float-right'> 
{Date:ddd, MM/dd}
</span>
";
				}
				else
				{
					//return Date.ToString("ddd, MM/dd") + "<br />" + AddDaysDescr + Date.AddDays(AddDays).ToString("ddd, MM/dd");
					return $@"
 <span class='float-right'>
{Date:ddd, MM/dd}
</span>
<br /> 
<span class='float-right'> 
{AddDaysDescr} {Date.AddDays(AddDays):ddd, MM/dd} 
</span>
";

				}

			}
			*/
		}


	}
}
