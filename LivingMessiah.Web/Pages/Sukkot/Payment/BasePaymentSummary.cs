using LivingMessiah.Web.Pages.Sukkot.Domain;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot;

public abstract class BasePaymentSummary : ComponentBase
{
	[Parameter] public RegistrationSummary? RegistrationSummary { get; set; }
}
