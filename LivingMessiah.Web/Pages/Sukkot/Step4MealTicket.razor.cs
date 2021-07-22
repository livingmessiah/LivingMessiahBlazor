using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using SukkotApi.Domain;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public partial class Step4MealTicket
	{
		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }

		[Parameter]
		public vwRegistrationShell vwRegistrationShell { get; set; }
	}
}
