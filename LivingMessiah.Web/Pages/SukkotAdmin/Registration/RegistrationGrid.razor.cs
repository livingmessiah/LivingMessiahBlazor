using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class RegistrationGrid
	{

	}

}

