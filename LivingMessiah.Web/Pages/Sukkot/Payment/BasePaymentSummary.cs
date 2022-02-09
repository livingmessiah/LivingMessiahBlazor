using Microsoft.AspNetCore.Components;
using SukkotApi.Domain;
using Microsoft.Extensions.Options;
using LivingMessiah.Web.Settings;

namespace LivingMessiah.Web.Pages.Sukkot;

public abstract class BasePaymentSummary : ComponentBase
{
		[Inject]
		public IOptions<SukkotSettings> SukkotSettings { get; set; }


		[Parameter]
		public RegistrationSummary RegistrationSummary { get; set; }

		protected bool IsMealsAvailable;
		protected override void OnInitialized()
		{
				IsMealsAvailable = SukkotSettings.Value.IsMealsAvailable;
		}

}
