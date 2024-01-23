using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Features.ShabbatService;

public class IntroductionBase : ComponentBase
{
	[Parameter]	public string? SpanishUrlId { get; set; }
	[Parameter]	public bool IsSpanish { get; set; }
	[Parameter]	public EventCallback<bool> OnLanguageSelection { get; set; }

	protected async Task CheckBoxChanged(ChangeEventArgs? e) 
		=> await OnLanguageSelection.InvokeAsync((bool)e!.Value!);
}
