using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using SukkotApi.Domain;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public partial class Step5Payment
	{
		[Parameter]
		public vwRegistrationShell vwRegistrationShell { get; set; }

		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }
	}
}
