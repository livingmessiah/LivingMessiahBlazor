using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.KeyDates.Queries;

namespace LivingMessiah.Web.Pages.KeyDates
{
	public partial class Year
	{
		[Parameter]
		public bool IsXsOrSm { get; set; }

		[Parameter]
		public bool WithMonths { get; set; }

		[Parameter]
		public List<CalendarEntryDateRange> CalendarEntryDateRanges { get; set; }

		[Parameter]
		public List<FeastDay> FeastDays { get; set; }

		[Parameter]
		public List<LunarMonth> LunarMonths { get; set; }

		[Parameter]
		public List<Season> Seasons { get; set; }

	}
}
