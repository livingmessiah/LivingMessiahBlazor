using System;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.UpcomingEvents
{

	//https://www.ripteq.com.au/blog/blazor-code-behinds-and-base-classes
	public abstract class BaseKeyDates : ComponentBase
	{
		[Parameter]
		public DateTime EventDate { get; set; }

		[Parameter]
		public string DaysAhead { get; set; }

		[Parameter]
		public bool IsXsOrSm { get; set; }
	}

}
