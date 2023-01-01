using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Shared;

public partial class BibleQuote
{
		[Parameter] public RenderFragment ChildContent { get; set; }
		[Parameter, EditorRequired] public string Cite { get; set; }
}