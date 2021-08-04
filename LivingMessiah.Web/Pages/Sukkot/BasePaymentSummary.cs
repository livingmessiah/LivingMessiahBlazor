using Microsoft.AspNetCore.Components;
using SukkotApi.Domain;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public abstract class BasePaymentSummary : ComponentBase
	{
		[Parameter]
		public RegistrationSummary RegistrationSummary { get; set; }



	}
}
