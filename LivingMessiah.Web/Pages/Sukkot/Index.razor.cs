using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LivingMessiah.Web.Pages.Sukkot
{
	[AllowAnonymous]
	public partial class Index
	{
		public const string EarlyRegistrationLastDay = "August 31";
		public const string EarlyRegistrationFee = "$25";
		public const string FinalRegistrationDay = "September 20";
		public const string RegistrationFee = "$40";
		public const bool IsRegistrationClosed = false;
		public const string ContactName = "Ralphie";
		public const string ContactEmail = "ralphie@livingmessiah.com";
		public const string Title1 = "Late Sukkot Registration Question";
		//public const string Dates = "Thursday, October 1, through sundown Saturday, October 10"; <span class="text-warning">
		public const string Dates = "Thursday, October 1, through sundown <strike>Saturday, October 10</strike> <b class='text-danger'><u>Friday, October 9th</u></b>";
		public const string ArrivalDate = "Wednesday September 30th";
		//public const string CleanupDate = "Sunday October 11th";
		public const string CleanupDate = "<strike>Sunday October 11th</strike><b class='text-danger'><u> Saturday, October 10th</u></b>";
		public const string ShabbatServiceDate = "October 3rd";
	}
}
