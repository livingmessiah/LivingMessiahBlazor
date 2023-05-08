using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using PageEditMarkdown = LivingMessiah.Web.Links.UpcomingEventsAdmin.EditMarkdown;
using PageUploadImage = LivingMessiah.Web.Links.UpcomingEventsAdmin.UploadImage;
using LivingMessiah.Web.Pages.UpcomingEvents.Enums;

using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin.Edit;

public partial class GridComponent
{
	[Inject] public ILogger<GridComponent>? Logger { get; set; }
	[Inject] NavigationManager? NavManager { get; set; }

	protected void OnActionBegin(ActionEventArgs<EditVM> args)
	{
		if (args.RequestType.ToString() == "Add")
		{
			args.Data.SpecialEventType = SpecialEventType.GuestSpeaker;
			DateTime current = DateTime.Now;
			args.Data.EventDate = current.AddDays(35); ;
			args.Data.ShowBeginDate = current.AddMonths(1);
			args.Data.ShowEndDate = current.AddDays(40);
		}
	}

	private void Image_ButtonClick(int id)
	{
		NavManager!.NavigateTo(PageUploadImage.Page + "/" + id);
	}

	private void Edit_ButtonClick(int id)
	{
		NavManager!.NavigateTo(PageEditMarkdown.Page + "/" + id);
	}

	void Failure(FailureEventArgs e)
	{
		string message = $"Error inside {nameof(GridComponent)}; e.Error: {e.Error}";
		Logger!.LogDebug(message);
	}

}
