using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser;

public partial class Index
{
	[Inject] private IState<State>? State { get; set; }
}
