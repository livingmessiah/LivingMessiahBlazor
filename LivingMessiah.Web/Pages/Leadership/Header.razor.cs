using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Leadership
{
	public partial class Header
	{
		const string EmailSubject = "?Subject=To%20LMM%20Leadership";
		protected readonly MarkupString AnchorEmail = 
			new MarkupString($"<a href='{Address.EmailHref()}{EmailSubject}'>{Address.Email()}</a>");
	}
}
