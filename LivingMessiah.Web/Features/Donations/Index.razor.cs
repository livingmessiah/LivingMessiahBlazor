namespace LivingMessiah.Web.Features.Donations;

public partial class Index
{
	protected Enums.NavItem currentNavItem = Enums.NavItem.Stripe;

	private const string navTabType = " nav-tabs";  //  nav-underlines,  nav-pills

	private void OnButtonClicked(Enums.NavItem tabItem)
	{
		currentNavItem = tabItem;
		StateHasChanged();
	}

	string AriaActive(Enums.NavItem navItem)
	{
		return currentNavItem == navItem ? "true" : "false";
	}

	string NavLinkActive(Enums.NavItem navItem)
	{
		return currentNavItem == navItem ? " active" : "";
	}

	string TabPageActive(Enums.NavItem navItem)
	{
		return currentNavItem == navItem ? " show active" : "";
	}
}
