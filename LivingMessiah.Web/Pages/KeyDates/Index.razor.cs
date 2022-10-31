using Microsoft.AspNetCore.Components;
using LinkLogin = LivingMessiah.Web.Links.Account;

namespace LivingMessiah.Web.Pages.KeyDates;

public partial class Index
{
	[Inject] NavigationManager NavigationManager { get; set; }
	void RedirectToLoginClick(string returnUrl)
	{
		NavigationManager.NavigateTo($"{LinkLogin.Login}?returnUrl={returnUrl}", true);
	}
}
