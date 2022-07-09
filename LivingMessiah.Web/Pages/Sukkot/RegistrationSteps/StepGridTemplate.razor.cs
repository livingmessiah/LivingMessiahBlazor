using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class StepGridTemplate
{
	[Parameter, EditorRequired]
	public Status Status { get; set; }

	[Parameter, EditorRequired]
	public Status ComparisonStatus { get; set; }

	[Parameter]
	public RenderFragment ChildContent { get; set; }

}

