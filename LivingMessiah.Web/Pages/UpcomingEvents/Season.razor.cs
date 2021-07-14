using Microsoft.AspNetCore.Components;
using LivingMessiah.Domain.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.UpcomingEvents
{
	partial class Season : BaseKeyDates
	{
		[Parameter]
		public SeasonLocal SeasonLocal { get; set; }

	}
}

