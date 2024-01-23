using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.ShabbatService;

public abstract class BaseSection : ComponentBase
{
	[Parameter] public RenderFragment? SubTitle { get; set; }
	[Parameter]	public RenderFragment? ChildContent { get; set; }
	[Parameter]	public bool LoadQuickly { get; set; } = false;
	[Parameter]	public string? CardCss { get; set; } = "";
	[Parameter]	public string? HeaderBadgeColor { get; set; } = "bg-warning";
	[Parameter]	public Section? Section { get; set; }
	[Parameter]	public bool IsPrinterFriendly { get; set; } = false;

	public bool IsCollapsed { get; set; } = true;

	protected void Collapsed_ButtonClick()
	{
		IsCollapsed = !IsCollapsed;
	}

}
