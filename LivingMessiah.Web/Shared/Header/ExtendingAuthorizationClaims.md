
# Accessing and Extending Authorization Claims in ASP.NET Core and Blazor

[Visual Magazine Blog By Peter Vogel 11/01/2019](https://visualstudiomagazine.com/articles/2019/11/01/authorization-claims.aspx)

```csharp
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
```