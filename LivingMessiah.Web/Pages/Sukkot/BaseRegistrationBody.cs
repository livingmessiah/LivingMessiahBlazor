using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using SukkotApi.Domain;

namespace LivingMessiah.Web.Pages.Sukkot
{
	//https://www.ripteq.com.au/blog/blazor-code-behinds-and-base-classes
	public abstract class BaseRegistrationBody : ComponentBase
	{
		[Parameter]
		public bool IsXs { get; set; }

		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }

		[Parameter]
		public vwRegistrationShell vwRegistrationShell { get; set; }

		//public int Id { get; set; }

	}
}
