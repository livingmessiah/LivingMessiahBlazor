using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Infrastructure 
{
	//ToDo: Where does this get used?
	public class UserInfoClaims : IClaimsTransformation
	{
		public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		{
			return Task.FromResult(principal);
		}
	}
}
