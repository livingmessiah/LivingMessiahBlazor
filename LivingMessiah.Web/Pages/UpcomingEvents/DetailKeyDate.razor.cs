using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.KeyDates.Queries;

namespace LivingMessiah.Web.Pages.UpcomingEvents
{
	public partial class DetailKeyDate
	{
		[Parameter]
		public UpcomingEvent UpcomingEvent { get; set; }

		[Parameter]
		public bool IsXsOrSm { get; set; }
	}
}
