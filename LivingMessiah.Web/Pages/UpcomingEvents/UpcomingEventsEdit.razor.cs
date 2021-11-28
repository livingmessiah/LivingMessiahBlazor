using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.UpcomingEvents
{
	[Authorize(Roles = Roles.AdminOrAnnouncements)]
	public partial class UpcomingEventsEdit
	{
		[Inject]
		public ILogger<UpcomingEventsEdit> Logger { get; set; }

		protected void OnActionBegin(ActionEventArgs<UpcomingEventsEditVM> args)
		{
			if (args.RequestType.ToString() == "Add")
			{
				args.Data.EventTypeEnum = KeyDates.Enums.EventTypeEnum.GuestSpeaker;
				DateTime current = DateTime.Now;
				args.Data.YearId = current.Year;
				args.Data.EventDate = current;
				args.Data.ShowBeginDate = current;
				args.Data.ShowEndDate = current;
			}
		}


		#region ErrorHandling

		void Failure(FailureEventArgs e)
		{
			Logger.LogDebug($"Error inside {nameof(UpcomingEventsEdit)}; e.Error: {e.Error}");
			DatabaseErrorMsg = e.Error.ToString();
			DatabaseError = true;
		}

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
		protected bool DatabaseError { get; set; } 
		protected string DatabaseErrorMsg { get; set; }
		#endregion
	}
}
