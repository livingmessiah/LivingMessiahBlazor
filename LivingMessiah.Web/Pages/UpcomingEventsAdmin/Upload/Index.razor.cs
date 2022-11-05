using Microsoft.AspNetCore.Components;
using LoginLink = LivingMessiah.Web.Links.Account;

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin.Upload;

public partial class Index
{
	[Inject] NavigationManager NavigationManager { get; set; }

	[Parameter] public int Id { get; set; } = 0;


	void RedirectToLoginClick(string returnUrl)
	{
		NavigationManager.NavigateTo($"{LoginLink.Login}?returnUrl={returnUrl}", true);
	}
}
