using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using LivingMessiah.Web.Domain;

namespace LivingMessiah.Web.Pages.Home;

public partial class SidebarButtonTestPage
{
	[Inject] public Services.ILinkService LinkService { get; set; }

	private IEnumerable<Link> HomeSidebarLinks;
	protected override void OnInitialized()
	{
		HomeSidebarLinks = LinkService.GetHomeSidebarLinks();
	}
}
