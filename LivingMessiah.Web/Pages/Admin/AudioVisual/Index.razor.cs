using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LinkLogin = LivingMessiah.Web.Links.Account;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] NavigationManager? NavigationManager { get; set; }

	void RedirectToLoginClick(string returnUrl)
	{
		NavigationManager!.NavigateTo($"{LinkLogin.Login}?returnUrl={returnUrl}", true);
	}

}
