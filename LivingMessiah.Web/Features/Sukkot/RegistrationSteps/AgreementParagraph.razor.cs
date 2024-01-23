using Microsoft.AspNetCore.Components;
using System;

namespace LivingMessiah.Web.Features.Sukkot.RegistrationSteps;

public partial class AgreementParagraph
{
	[Parameter, EditorRequired]
	public bool IsXs { get; set; } = false;

	protected string? FormatSize;

	protected override void OnInitialized()
	{
		FormatSize = IsXs ? "" : "lead"; 
	}
}
