using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Leadership.Enums;

namespace LivingMessiah.Web.Pages.Leadership;

public partial class OfficeContent
{
	[Parameter, EditorRequired]
	public Office Office { get; set; }

	[Parameter, EditorRequired]
	public bool IsXs { get; set; }

	protected string ImgClass => IsXs ? "w-100 p-3" : Office.ImgClassSmMdLg;

}

