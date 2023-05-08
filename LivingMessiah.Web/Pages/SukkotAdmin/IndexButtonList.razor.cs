using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.SukkotAdmin;

public partial class IndexButtonList
{
	[Parameter] public bool IsXs { get; set; }
	[Parameter] public string? BtnGroupCss { get; set; }

	/*
	public string btnGroupCss { get; set; } = IsXs  ? "btn-group " : "btn-group btn-group-lg ";
	Error: A field initializer cannot reference the non-static field, method, or property

	public string BtnGroupCss2 { get; set; } => IsXs ? "btn-group " : "btn-group btn-group-lg ";		 
	Error	CS8057	Block bodies and expression bodies cannot both be provided

	 */

}
