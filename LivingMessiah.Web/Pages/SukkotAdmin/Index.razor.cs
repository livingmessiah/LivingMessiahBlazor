using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

namespace LivingMessiah.Web.Pages.SukkotAdmin
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class Index
	{
	}
}
