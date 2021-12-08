using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.KeyDates.Enums;
using System;

namespace LivingMessiah.Web.Pages.UpcomingEvents
{
	public partial class NonKeyDate
	{
		[Inject]
		NavigationManager NavManager { get; set; }

		[Parameter]
		public int Id { get; set; }

		[Parameter]
		public DateTime EventDate { get; set; }

		[Parameter]
		public EventType EventType { get; set; }

		[Parameter]
		public String Title { get; set; }

		[Parameter]
		public String SubTitle { get; set; }

		[Parameter]
		public String ImageUrl { get; set; }

		[Parameter]
		public String WebsiteUrl { get; set; }

		[Parameter]
		public String WebsiteDescr { get; set; }

		[Parameter]
		public String YouTubeId { get; set; }

		[Parameter]
		public String Description { get; set; }

		protected override void OnInitialized()
		{
			SetShowVideoButton();
		}

		public bool ShowVideo { get; set; } = false;
		protected string OppositeIcon;
		protected string OppositeToggleMsg;

		protected void ShowVideo_Button_Click()
		{
			ShowVideo = !ShowVideo;
			SetShowVideoButton();
			StateHasChanged();
		}

		protected void SetShowVideoButton()
		{
			if (ShowVideo)
			{
				OppositeIcon = "<i class='far fa-arrow-alt-circle-up'></i>";
				OppositeToggleMsg = "Hide Video";
			}
			else
			{
				OppositeIcon = "<i class='far fa-arrow-alt-circle-down'></i>";
				OppositeToggleMsg = "Show Video";
			}
		}

		private void Edit_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.UpcomingEventsEditMarkdown.Edit + "/" + id);
		}
	}
}
