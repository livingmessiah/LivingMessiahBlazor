using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.Sukkot.NormalUser;

public partial class Toggle
{
	[Parameter, EditorRequired] public int? Id { get; set; }
	[Parameter, EditorRequired] public string? Email { get; set; }

	public bool IsCollapsed { get; set; } = true;
	protected void ToggleButtonClick(bool isCollapsed)
	{
		IsCollapsed = !isCollapsed;
	}

}
