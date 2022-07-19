using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Sukkot.Web.Service;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class GridContent
{
	[Inject]
	public ILogger<GridContent> Logger { get; set; }

	[Inject]
	public ISukkotService svc { get; set; }

	[Parameter, EditorRequired]
	public bool IsXs { get; set; }

	[Parameter, EditorRequired]
	public Status UsersCurrentStatus { get; set; }

	[Parameter, EditorRequired]
	public Status ComparisonStatus { get; set; }

	[Parameter, EditorRequired]
	public string EmailParm { get; set; }

	[Parameter, EditorRequired]
	public RegistrationStep RegistrationStep { get; set; }
}
