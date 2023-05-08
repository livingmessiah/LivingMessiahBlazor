using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Grid
{
	[Parameter, EditorRequired]
	public string? EmailParm { get; set; }

	[Parameter, EditorRequired]
	public bool IsXs { get; set; }

	[Parameter, EditorRequired]
	public Status? UsersCurrentStatus { get; set; }

	[Parameter, EditorRequired]
	public Status? ComparisonStatus { get; set; }

	[Parameter, EditorRequired]
	public RegistrationStep? RegistrationStep { get; set; }
}
