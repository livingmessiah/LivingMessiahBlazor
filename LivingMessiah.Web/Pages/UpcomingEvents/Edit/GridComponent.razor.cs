using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.UpcomingEvents.Edit
{
	[Authorize(Roles = Roles.AdminOrAnnouncements)]
	public partial class GridComponent
	{
		[Inject]
		public ILogger<GridComponent> Logger { get; set; }
		
		[Inject]
		NavigationManager NavManager { get; set; }

		protected void OnActionBegin(ActionEventArgs<EditVM> args)
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

		private void Image_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.UpcomingEventsUploadImage.Upload + "/" + id);
		}

		private void Edit_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.UpcomingEventsEditMarkdown.Edit + "/" + id);
		}

		#region ErrorHandling

		void Failure(FailureEventArgs e)
		{
			Logger.LogDebug($"Error inside {nameof(GridComponent)}; e.Error: {e.Error}");
		}

		#endregion
	}
}
