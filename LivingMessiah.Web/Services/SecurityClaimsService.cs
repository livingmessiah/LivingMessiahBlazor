using System.Threading.Tasks;
using LivingMessiah.Web.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace LivingMessiah.Web.Services
{
	public interface ISecurityClaimsService
	{
		Task<string> GetEmail();
		Task<string> GetRole();
	}

	public class SecurityClaimsService : ISecurityClaimsService
	{
		#region Constructor and DI

		private AuthenticationStateProvider ASP;
		public SecurityClaimsService(AuthenticationStateProvider aSP)
		{
			ASP = aSP;
		}
		#endregion

		public async Task<string> GetEmail() 
		{
			var authState = await ASP.GetAuthenticationStateAsync();
			ClaimsPrincipal User;
			User = authState.User;
			return User.GetUserEmail();
		}

		public async Task<string> GetRoles()
		{
			var authState = await ASP.GetAuthenticationStateAsync();
			ClaimsPrincipal User;
			User = authState.User;
			
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

		public static bool RoleHasAdminOrSukkot()
		{
			var authState = await ASP.GetAuthenticationStateAsync();
			ClaimsPrincipal User;
			User = authState.User;

			foreach (var claim in user.Claims)
			{

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


	}
}


