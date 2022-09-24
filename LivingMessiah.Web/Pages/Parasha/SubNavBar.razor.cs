using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Parasha;

public partial class SubNavBar
{

	bool collapseNavMenu = true;
	string NavMenuCssClass => collapseNavMenu ? "collapse" : null;
	void ToggleNavMenu()
	{
		collapseNavMenu = !collapseNavMenu;
	}

	[Parameter]
	public bool UseDarkMode { get; set; } = false;

	string NavBarColor;
	string TextColor;

	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(0);
		NavBarColor = UseDarkMode ? " navbar-dark bg-dark " : " navbar-light bg-white ";
		TextColor = UseDarkMode ? " text-white " : " text-dark ";
	}
}
