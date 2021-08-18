using LivingMessiah.Domain;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class CurrentParasha
	{
		[Parameter]
		public vwCurrentParasha vwCurrentParasha { get; set; }

		[Parameter]
		public bool IsXsOrSm { get; set; }

		[Parameter]
		public string CssUlStyle { get; set; }

		[Parameter]
		public string CssUlClass { get; set; }

		private const string _WarningCaretRight = "<span class='text-warning'><i class='fa-li fas fa-caret-right'></i></span>";
		public MarkupString GetWarningCaretRight() { return IsXsOrSm? (MarkupString)string.Empty : (MarkupString)_WarningCaretRight; }

	}
}
