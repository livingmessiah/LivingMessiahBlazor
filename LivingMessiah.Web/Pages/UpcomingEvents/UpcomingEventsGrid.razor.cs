using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

//using LivingMessiah.Web.Pages.UpcomingEvents.Enums;

using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using LivingMessiah.Web.Pages.UpcomingEvents.Queries;

using Syncfusion.Blazor.Grids;
//using Syncfusion.Blazor.DropDowns;

namespace LivingMessiah.Web.Pages.UpcomingEvents;

public partial class UpcomingEventsGrid
{
		[Inject] public IUpcomingEventsRepository db { get; set; }

		[Inject]
		public ILogger<UpcomingEventsGrid> Logger { get; set; }

		protected List<UpcomingEvent> UpcomingEventList;

		protected override async Task OnInitializedAsync()
		{
				try
				{
						Logger.LogDebug(string.Format("Inside {0}", nameof(UpcomingEventsGrid) + "!" + nameof(OnInitializedAsync)));
						UpcomingEventList = await db.GetEvents(daysAhead: 100, daysPast: -3);
						if (UpcomingEventList is not null)
						{
								Logger.LogDebug(string.Format("...UpcomingEventList.Count:{0}", UpcomingEventList.Count));
						}
						else
						{
								DatabaseWarning = true;
								DatabaseWarningMsg = $"{nameof(UpcomingEventList)} NOT FOUND";
								//Logger.LogDebug($"{nameof(UpcomingEventList)} is null, Sql:{db.BaseSqlDump}");
						}
				}
				catch (Exception ex)
				{
						DatabaseError = true;
						DatabaseErrorMsg = $"Error reading database";
						Logger.LogError(ex, $"...{DatabaseErrorMsg}");
				}
		}

		#region ErrorHandling

		private void InitializeErrorHandling()
		{
				DatabaseInformationMsg = "";
				DatabaseInformation = false;
				DatabaseWarningMsg = "";
				DatabaseWarning = false;
				DatabaseErrorMsg = "";
				DatabaseError = false;
		}

		protected bool DatabaseInformation = false;
		protected string DatabaseInformationMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
		protected string DatabaseErrorMsg { get; set; }
		#endregion

}


