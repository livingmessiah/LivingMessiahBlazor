using Microsoft.AspNetCore.Components.Web;

namespace LivingMessiah.Web.Pages.WindmillRanch;

public enum PartialViewEnum
{
		Intro = 1,
		Audit = 2,
		Permaculture = 3,
		Projects = 4,
		Priorities = 5,
		Album = 6,
		Support = 7,
		Archive = 8
}

public partial class PartialViewButtons
{
		protected PartialViewEnum CurrentPartialView;

		//https://docs.microsoft.com/en-us/aspnet/core/blazor/components/event-handling?view=aspnetcore-3.1#lambda-expressions
		private void ChangeContent(MouseEventArgs e, PartialViewEnum currentPartialView)
		{
				CurrentPartialView = currentPartialView;
				StateHasChanged();
		}

		protected override void OnInitialized()
		{
				CurrentPartialView = PartialViewEnum.Intro;
				base.OnInitialized();
		}

		/*
		See example WeeklyVerses.razor (LivingMessiah.Web\Components\Pages\WeeklyVerses\)
		if I decide to do nav nav-pills

		protected string ActiveType(PartialViewEnum id)
		{
			if (id == CurrentPartialView)
			{
				return "active";
			}
			else
			{
				return "";
			}
		}
		*/
}
