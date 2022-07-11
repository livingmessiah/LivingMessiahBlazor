using Microsoft.AspNetCore.Components;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class StepGridTemplateXs
{
	[Parameter, EditorRequired]
	public Status Status { get; set; }

	[Parameter, EditorRequired]
	public Status ComparisonStatus { get; set; }

	[Parameter]
	public RenderFragment ChildContent { get; set; }
}
