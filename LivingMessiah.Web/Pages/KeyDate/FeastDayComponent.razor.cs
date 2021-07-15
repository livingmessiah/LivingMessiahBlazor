using Microsoft.AspNetCore.Components;
using LivingMessiah.Domain.KeyDates.Queries;
using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.KeyDate
{
	public partial class FeastDayComponent
	{
		[Parameter]
		public List<FeastDay> FeastDays { get; set; }

		[Parameter]
		public bool IsXsOrSm { get; set; }

	}
}
