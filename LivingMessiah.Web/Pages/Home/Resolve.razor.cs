using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.SmartEnums;

namespace LivingMessiah.Web.Pages.Home;

public partial class Resolve
{
	[Parameter, EditorRequired] public MediaQuery MediaQuery { get; set; }
}

