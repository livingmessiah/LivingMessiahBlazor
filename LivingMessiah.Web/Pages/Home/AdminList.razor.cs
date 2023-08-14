using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using LivingMessiah.Web.Domain;

namespace LivingMessiah.Web.Pages.Home;

public partial class AdminList
{
	[Inject] public Services.ILinkService? LinkService { get; set; }

	private IEnumerable<LinkBasic>? AdminLinks;

	protected override void OnInitialized()
	{
		base.OnInitialized();
		AdminLinks = LinkService!.GetAdminLinks();
	}
}


