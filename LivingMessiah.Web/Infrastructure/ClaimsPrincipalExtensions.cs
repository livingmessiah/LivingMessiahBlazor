using System.Linq;
using System.Security.Claims;
using static LivingMessiah.Web.Services.Auth0;

//ToDo: remove this and use SecurityClaimsService (LivingMessiah.Web.Services)
namespace LivingMessiah.Web.Infrastructure;

public static class ClaimsPrincipalExtensions
{
	public static string? GetRoleLMM(this ClaimsPrincipal? user)
	{
		return user!.Claims!.FirstOrDefault(c => c.Type == "https://schemas.livingmessiah.com/roles")?.Value;
	}

	public static string? GetUserId(this ClaimsPrincipal? user)
	{
		return user!.Claims!.FirstOrDefault(c => c.Type == "sub")?.Value;
	}

	public static string? GetUserName(this ClaimsPrincipal user)
	{
		return user.Claims?.FirstOrDefault(c => c.Type == "name")?.Value;
	}

	public static string? GetUserEmail(this ClaimsPrincipal user)
	{
		return user.Claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
	}

	public static string? GetRoles(this ClaimsPrincipal user)
	{
		string roles = "";
		foreach (var claim in user.Claims)
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

	public static bool RoleHasAdminOrSukkot(this ClaimsPrincipal user)
	{
		foreach (var claim in user.Claims)
		{
			//ToDo: I wish this would work
			//if (claim.Type == SchemaNameSpace && claim.Value == Roles.AdminOrSukkot) { return true; }

			if (claim.Type == SchemaNameSpace && claim.Value == Roles.Admin)
			{
				return true;
			}
			else
			{
				if (claim.Type == SchemaNameSpace && claim.Value == Roles.Sukkot)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool RoleHasAdmin(this ClaimsPrincipal user)
	{
		foreach (var claim in user.Claims)
		{
			if (claim.Type == SchemaNameSpace && claim.Value == Roles.Admin)
			{
				return true;
			}
		}
		return false;
	}

	public static bool Verified(this ClaimsPrincipal user)
	{
		//return user.Claims?.FirstOrDefault(c => c.Type == "email_verified")?.Value;
		if (user.Claims != null)
		{
			var x = user.Claims?.FirstOrDefault(c => c.Type == "email_verified")?.Value;
			if (!string.IsNullOrEmpty(x))
			{
				bool b;
				bool.TryParse(x, out b);
				return b;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}


}
