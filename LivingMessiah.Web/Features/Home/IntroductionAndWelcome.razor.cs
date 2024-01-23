using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.Home;

public partial class IntroductionAndWelcome
{
	[Parameter] public LivingMessiah.Web.Enums.MediaQuery? MediaQuery { get; set; }
}
