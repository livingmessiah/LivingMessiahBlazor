using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class StepGridTemplate<TItem>
{
	[Parameter]
	public RenderFragment GridHeader { get; set; }

	[Parameter]
	public RenderFragment<TItem> RowTemplate { get; set; }

	[Parameter]
	public IReadOnlyList<TItem> Items { get; set; }

}
