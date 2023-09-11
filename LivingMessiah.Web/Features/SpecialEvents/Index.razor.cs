using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class Index
{
	[Inject] private IState<State>? State { get; set; }
}


/*
ToDo: add the Login logic

using LoginLink = LivingMessiah.Web.Links.Account;

	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] NavigationManager? NavigationManager { get; set; }

	void RedirectToLoginClick(string returnUrl)
	{
		NavigationManager!.NavigateTo($"{LoginLink.Login}?returnUrl={returnUrl}", true);
	}

*/