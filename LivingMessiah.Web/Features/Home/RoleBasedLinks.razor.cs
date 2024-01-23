using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Features.Home;

public partial class RoleBasedLinks
{
	[Inject] public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

	public string? Role { get; set; }

	protected override async Task OnInitializedAsync()
	{
		base.OnInitialized();
		var authState = await AuthenticationStateProvider!.GetAuthenticationStateAsync();
		var user = authState.User;
		Role = user.Claims.FirstOrDefault(c => c.Type == "https://schemas.livingmessiah.com/roles")?.Value ?? "";
	}
}
