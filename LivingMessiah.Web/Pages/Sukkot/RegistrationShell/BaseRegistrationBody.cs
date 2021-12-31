using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using SukkotApi.Domain;
using Microsoft.Extensions.Options;
using LivingMessiah.Web.Settings;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationShell
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

		[Inject]
		public IOptions<SukkotSettings> SukkotSettings { get; set; }

		protected bool IsMealsAvailable;

		protected override void OnInitialized()
		{
			IsMealsAvailable = SukkotSettings.Value.IsMealsAvailable;
		}

	}
}
