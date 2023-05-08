using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Infrastructure;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Sukkot;

public partial class Index
{
	[Inject] public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

	public string? Salutation { get; set; }
	public ClaimsPrincipal? User { get; set; }

	/*
	Because of this unanswered question https://community.auth0.com/t/my-blazor-server-app-wont-display-the-user-name/85054
	I cant just do this ==> @context.User.Identity.Name
	*/
	protected override async Task OnInitializedAsync()
	{
		base.OnInitialized();
		var authState = await AuthenticationStateProvider!.GetAuthenticationStateAsync();
		User = authState.User;
		Salutation = User.GetUserNameSoapVersion();
	}
}
