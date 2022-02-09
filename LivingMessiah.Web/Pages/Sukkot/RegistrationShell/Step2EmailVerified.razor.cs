using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using Link = LivingMessiah.Web.Links.Account;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationShell;

public partial class Step2EmailVerified
{
		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		void RedirectToLoginClick(string returnUrl)
		{
				NavigationManager.NavigateTo($"{Link.Login}?returnUrl={returnUrl}", true);
		}

}
