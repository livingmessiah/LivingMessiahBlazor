using System.Threading.Tasks;
using LivingMessiah.Web.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace LivingMessiah.Web.Services
{
	public interface ISecurityClaimsService
	{
		Task<string> GetEmail();
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
	}
}


