using Microsoft.AspNetCore.Components;
using Link = LivingMessiah.Web.Links.Account;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;
public partial class LoginRedirectButton
{
	[Inject] NavigationManager? NavigationManager { get; set; }
	[Parameter] public string? ReturnUrl { get; set; }

	void RedirectToLoginClick()
	{
		NavigationManager!.NavigateTo($"{Link.Login}?returnUrl={ReturnUrl}", true);
	}
}
