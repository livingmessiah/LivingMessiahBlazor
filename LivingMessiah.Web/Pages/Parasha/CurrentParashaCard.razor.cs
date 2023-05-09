using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Parasha;

public partial class CurrentParashaCard
{
	[Parameter] public CurrentParasha? CurrentParasha { get; set; }
	[Parameter]	public bool IsXsOrSm { get; set; }
	[Parameter]	public string? CssUlStyle { get; set; }
	[Parameter]	public string? CssUlClass { get; set; }

	private const string _WarningCaretRight = "<span class='text-warning'><i class='fa-li fas fa-caret-right'></i></span>";
	public MarkupString GetWarningCaretRight() { return IsXsOrSm ? (MarkupString)string.Empty : (MarkupString)_WarningCaretRight; }

	public static MarkupString CardClass(bool isXsOrSm)
	{
		return isXsOrSm ?
			(MarkupString)$"card bg-light text-center" :
			(MarkupString)$"card bg-light";
	}

	public static MarkupString CardHeaderClass(bool isXsOrSm)
	{
		return isXsOrSm ?
			(MarkupString)$"card-header" :
			(MarkupString)$"card-header text-center";
	}

	public static MarkupString CardBodyParagraphClass(bool isXsOrSm)
	{
		return isXsOrSm ?
			(MarkupString)$"mb-4" :
			(MarkupString)$"float-end";
	}

}
