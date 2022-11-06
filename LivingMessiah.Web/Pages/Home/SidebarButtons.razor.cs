using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using LivingMessiah.Web.Domain;


namespace LivingMessiah.Web.Pages.Home;

public partial class SidebarButtons
{
	[Inject]
	public Services.ILinkService LinkService { get; set; }

	[Parameter, EditorRequired] public bool IsXsOrSm { get; set; }

	protected string TitleCSS;

	private IEnumerable<Link> HomeSidebarLinks;
	protected override void OnInitialized()
	{
		base.OnInitialized();
		TitleCSS = IsXsOrSm ? "h3 " : "h6";
		HomeSidebarLinks = LinkService.GetHomeSidebarLinks(IsXsOrSm);
	}
}
