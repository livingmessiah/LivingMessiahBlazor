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

	[Parameter]
	public bool UseDarkMode { get; set; } = false;

	string NavBarColor;
	string TextColor;

	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(0);
		NavBarColor = UseDarkMode ? " navbar-dark bg-dark " : " navbar-light bg-light "; // bg-white 
		TextColor = UseDarkMode ? " text-white " : " text-dark ";
	}

	[Parameter, EditorRequired]
	public ParashaLink ParashaEnum { get; set; }

	string GetActive(ParashaLink currentParashaEnum)
	{
		return ParashaEnum == currentParashaEnum ? " active" : "";
	}
}
