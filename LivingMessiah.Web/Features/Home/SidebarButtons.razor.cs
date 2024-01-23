using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using LivingMessiah.Web.Domain;
using LivingMessiah.Web.Settings;
using Microsoft.Extensions.Options;

namespace LivingMessiah.Web.Features.Home;

public partial class SidebarButtons
{
	[Inject] public Services.ILinkService? LinkService { get; set; }
	[Inject] public IOptions<AppSettings>? AppSettings { get; set; }

	[Parameter, EditorRequired] public bool IsXsOrSm { get; set; }

	protected string? TitleCSS;
	public int YearId { get; set; }

	private IEnumerable<Link>? HomeSidebarLinks;
	protected override void OnInitialized()
	{
		base.OnInitialized();
		YearId = AppSettings!.Value.YearId;
		TitleCSS = IsXsOrSm ? "h3 " : "h6";
		HomeSidebarLinks = LinkService!.GetHomeSidebarLinks(IsXsOrSm);
	}
}
