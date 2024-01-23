using LivingMessiah.Web.Features.Sukkot.Domain;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.Sukkot;

public abstract class BasePaymentSummary : ComponentBase
{
	[Parameter] public RegistrationSummary? RegistrationSummary { get; set; }
}
