using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using LivingMessiah.Web.Domain;

namespace LivingMessiah.Web.Pages.Sitemap;

public partial class Sitemap
{
	[Inject] public Services.ILinkService? LinkService { get; set; }

	private IEnumerable<Link>? Links;
	protected override void OnInitialized()
	{
		base.OnInitialized();
		Links = LinkService!.GetSitemapLinks();
	}
}
