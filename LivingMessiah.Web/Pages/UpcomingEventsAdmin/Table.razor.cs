using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using LivingMessiah.Web.Pages.UpcomingEvents.Queries;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin;

public partial class Table
{
	[Inject] public IUpcomingEventsRepository db { get; set; }

	protected List<SpecialEvent> SpecialEvents;
	protected override async Task OnInitializedAsync()
	{
		SpecialEvents = await db.GetEvents(daysAhead: 100, daysPast: -100);
	}
}
