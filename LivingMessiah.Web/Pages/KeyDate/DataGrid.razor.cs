using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivingMessiah.Data;
using LivingMessiah.Domain.KeyDates.Enums;
using LivingMessiah.Domain.KeyDates.Commands;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.KeyDate
{
	public partial class DataGrid
	{
		[Inject]
		public IUpcomingEventsRepository db { get; set; }

		[Inject]
		public ILogger<DataGrid> Logger { get; set; }

		[Parameter]
		public RelativeYearEnum RelativeYear { get; set; } = RelativeYearEnum.Next;

		protected List<DateUnion> DateUnionList;

		protected string LoadFailedMessasge = "";

		protected override async Task OnInitializedAsync()
		{
			DateUnionList = await db.GetDateUnionList(RelativeYear);
			if (DateUnionList == null)
			{
				LoadFailedMessasge = "DateUnionList NOT FOUND";
			}

		}

	}
}
