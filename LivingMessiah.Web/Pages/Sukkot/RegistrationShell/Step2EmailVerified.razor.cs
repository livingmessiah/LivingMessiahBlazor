using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationShell
{
	public partial class Step2EmailVerified
	{
		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }
	}
}
