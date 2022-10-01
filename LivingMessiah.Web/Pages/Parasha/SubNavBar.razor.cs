using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using LivingMessiah.Web.LinkSmartEnums;

namespace LivingMessiah.Web.Pages.Parasha;

public partial class SubNavBar
{

	bool collapseNavMenu = true;
	string SubNavBarCssClass => collapseNavMenu ? "collapse" : null;
	void ToggleSubNavBar()
	{
		collapseNavMenu = !collapseNavMenu;
	}

	// Can't force EditorRequired because PrintTable is set to `Display=> false`
	[Parameter]
	public ParashaLink ActiveParashaEnum { get; set; }

	[Parameter]
	public bool UseDarkMode { get; set; } = false;

	string NavBarColor;

	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(0);
		NavBarColor = UseDarkMode ? " navbar-dark bg-dark " : " navbar-light bg-light "; // bg-white 
	}


	string GetActive(ParashaLink currentParashaEnum)
	{
		return ActiveParashaEnum == currentParashaEnum ? " active" : "";
	}
}
