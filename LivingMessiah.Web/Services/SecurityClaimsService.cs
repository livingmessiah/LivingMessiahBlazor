using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using static LivingMessiah.Web.Services.Auth0;
using System.Linq;
using System.Collections.Generic;

namespace LivingMessiah.Web.Services;

public interface ISecurityClaimsService
{
	Task<string> GetEmail();
	Task<string> GetRoles();
	Task<bool> IsUserAuthoirized(string registrationEmail);
	Task<bool> RoleHasAdminOrSukkot();
	Task<bool> AdminOrSukkotOverride();
}

//ToDo:
//  Make this into a fluent API, because they all need to do GetUser
//  Consider making 
public class SecurityClaimsService : ISecurityClaimsService
{
	#region Constructor and DI

	private AuthenticationStateProvider ASP;
	public SecurityClaimsService(AuthenticationStateProvider aSP)
	{
		ASP = aSP;
	}
	#endregion

	ClaimsPrincipal _user;

	private async Task<ClaimsPrincipal> GetUser()
	{
		var authState = await ASP.GetAuthenticationStateAsync();
		return authState.User;
	}

	public async Task<string> GetEmail()
	{
		/*
		var authState = await ASP.GetAuthenticationStateAsync();
		ClaimsPrincipal User;
		User = authState.User;
		return User.GetUserEmail();
		ClaimsPrincipal User = await GetUser();
		return User.GetUserEmail();
		//return _user.GetUserEmail();
		*/
		_user = await GetUser();
		return _user.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

	}

	public async Task<string> GetRoles()
	{
		_user = await GetUser();

		string roles = "";
		foreach (var claim in _user.Claims)
		{
			if (claim.Type == SchemaNameSpace)
			{
				roles += claim.Value;
			}
		}

		if (roles.Length > 0 && roles.IndexOf(',') > 0)
		{
			roles.Remove(roles.IndexOf(','));
		}

		return roles;
	}

	public async Task<bool> IsUserAuthoirized(string registrationEmail)
	{
		_user = await GetUser();
		string sEmail = _user.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

		if (sEmail == registrationEmail) { return true; }
		return SearchRoles(_user.Claims, Roles.Admin, Roles.Sukkot);
	}

	public async Task<bool> RoleHasAdminOrSukkot()
	{
		_user = await GetUser();
		return SearchRoles(_user.Claims, Roles.Admin, Roles.Sukkot);
	}

	public async Task<bool> AdminOrSukkotOverride()
	{
		string roles = await this.GetRoles();

		if (roles == Auth0.Roles.Admin | roles == Auth0.Roles.Sukkot)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	//ToDo: Refactor this so that it can recieve an array of strings, and/or convert the foreach into a Linq statement
	private static bool SearchRoles(IEnumerable<Claim> claims, string role1, string role2)
	{
		foreach (var claim in claims)
		{
			if (claim.Type == SchemaNameSpace && claim.Value == role1)
			{
				return true;
			}
			else
			{
				if (claim.Type == SchemaNameSpace && claim.Value == role2)
				{
					return true;
				}
			}
		}
		return false;
	}


}


