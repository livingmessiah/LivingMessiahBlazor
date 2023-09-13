using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class DisplayCard
{
	[Parameter, EditorRequired] public FormVM? FormVM { get; set; }
	[Parameter] public bool ShowPrintAnchor { get; set; } = false;
}



