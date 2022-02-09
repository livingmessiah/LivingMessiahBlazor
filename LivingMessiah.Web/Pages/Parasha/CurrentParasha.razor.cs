using LivingMessiah.Domain.Parasha.Queries;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Parasha;

public partial class CurrentParasha
{
		[Parameter]
		public LivingMessiah.Domain.Parasha.Queries.Parasha Parasha { get; set; }

		[Parameter]
		public bool IsXsOrSm { get; set; }

		[Parameter]
		public string CssUlStyle { get; set; }

		[Parameter]
		public string CssUlClass { get; set; }

		private const string _WarningCaretRight = "<span class='text-warning'><i class='fa-li fas fa-caret-right'></i></span>";
		public MarkupString GetWarningCaretRight() { return IsXsOrSm ? (MarkupString)string.Empty : (MarkupString)_WarningCaretRight; }

}
