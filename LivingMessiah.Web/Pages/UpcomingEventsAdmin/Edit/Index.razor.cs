using Microsoft.AspNetCore.Components;
using LoginLink = LivingMessiah.Web.Links.Account;

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin.Edit;

public partial class Index
{
	[Inject] NavigationManager NavigationManager { get; set; }

	void RedirectToLoginClick(string returnUrl)
	{
		NavigationManager.NavigateTo($"{LoginLink.Login}?returnUrl={returnUrl}", true);
	}
}
