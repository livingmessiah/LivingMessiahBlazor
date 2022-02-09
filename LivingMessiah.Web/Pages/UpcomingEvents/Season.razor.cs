using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.UpcomingEvents;

partial class Season : BaseKeyDates
{
		[Parameter]
		public SeasonLocal SeasonLocal { get; set; }

}

