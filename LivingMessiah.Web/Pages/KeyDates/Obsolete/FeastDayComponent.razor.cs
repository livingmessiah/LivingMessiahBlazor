using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.KeyDates.Queries;
using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.KeyDates.Obsolete
{
	public partial class FeastDayComponent
	{
		[Parameter]
		public List<FeastDay> FeastDays { get; set; }

		[Parameter]
		public bool IsXsOrSm { get; set; }

	}
}
