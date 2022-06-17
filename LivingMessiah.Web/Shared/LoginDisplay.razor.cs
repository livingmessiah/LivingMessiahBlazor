using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Threading.Tasks;
using LivingMessiah.Web.Infrastructure;

namespace LivingMessiah.Web.Shared;

public partial class LoginDisplay
{
	[Inject]
	public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

	public string Name { get; set; }
	public string EmailAddress { get; set; }
	public bool Verified { get; set; }
	public string Role { get; set; }

	private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();

	protected override async Task OnInitializedAsync()
	{
		base.OnInitialized();
		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		var user = authState.User;

		if (user.Identity.IsAuthenticated)
		{
			Verified = true;
			_claims = user.Claims;
		}
		else
		{
			Verified = false;
		}

		Name = user.GetUserNameSoapVersion();
		EmailAddress = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
		Role = user.Claims.FirstOrDefault(c => c.Type == "https://schemas.livingmessiah.com/roles")?.Value;

	}

	public bool IsAdmin
	{
		get
		{
			if (Verified && Role.Contains("admin", System.StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public string BlueCheck
	{
		get
		{
			if (Verified)
			{
				return "<span class='text-primary'><i class='fas fa-check'></i></span>";
			}
			else
			{
				return "<span class='text-danger'>Unverified<i class='fas fa-question'></i></span>";
			}
		}
	}

}

/*
https://visualstudiomagazine.com/articles/2019/11/01/authorization-claims.aspx

using static LivingMessiah.Web.Services.Auth0;
using LivingMessiah.Web.Infrastructure;

[CascadingParameter]
private Task<AuthenticationState> authState { get; set; }

private System.Security.Claims.ClaimsPrincipal principal;
protected async override void OnParametersSet()
{
	if (authState != null)
	{
		principal = (await authState).User;
	}
}

public bool RoleHasAdmin
{
	get
	{
		if (principal.IsInRole("admin"))
		{
			return true;
		}
		else
		{
			return false;
		}

	}
}

 */

