using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;
using Syncfusion.Blazor.Inputs;

namespace LivingMessiah.Web.Pages.UpcomingEvents.Upload
{
	//[Authorize(Roles = Roles.AdminOrAnnouncements)]
	public partial class UploadComponent
	{
		//[Inject]
		//public ILogger<UploadComponent> Logger { get; set; }

		[Parameter]
		public int Id { get; set; }

		public void OnFileRemove(RemovingEventArgs args)
		{
			args.PostRawFile = false;
		}

	}
}
