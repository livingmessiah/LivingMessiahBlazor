using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Leadership
{
	public partial class Header
	{
		protected readonly MarkupString AnchorEmail =
			new MarkupString($"<a href='mailto:{Emails.Info.Email()}{Emails.Info.Subject}'>{Emails.Info.Email()}</a>");
	}
}
