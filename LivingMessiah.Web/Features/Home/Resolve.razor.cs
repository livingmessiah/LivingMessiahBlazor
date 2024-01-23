using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.Home;

public partial class Resolve
{
	[Parameter, EditorRequired] public LivingMessiah.Web.Enums.MediaQuery? MediaQuery { get; set; }
}

