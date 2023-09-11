using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class DisplayCard
{
	[Inject] private IState<State>? State { get; set; }
	FormVM? FormVM => State!.Value.FormVM;
}



