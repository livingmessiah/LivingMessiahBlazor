using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.KeyDates.Queries;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;

namespace LivingMessiah.Web.Pages.SmartEnums
{
	public partial class Index
	{
		[Inject]
		public IUpcomingEventsRepository db { get; set; }

		[Inject]
		public ILogger<Index> Logger { get; set; }

		[Parameter]
		public RelativeYearEnum RelativeYear { get; set; } = RelativeYearEnum.Current;

		protected List<DateExplode> DateExplodeList;

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }


		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(Index)}!{nameof(OnInitializedAsync)}");
			try
			{
				DateExplodeList = await db.GetDateExplode(RelativeYear);
				if (DateExplodeList == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "DateExplodeList NOT FOUND";
				}
				else
				{
					//LoadAppointmentDataLista();
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}


		}
	}
}
