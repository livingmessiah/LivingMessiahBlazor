using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Store.Counter;

namespace LivingMessiah.Web.Shared;

public partial class CounterAnchorSimple
{
	[Inject] private IState<CounterState> CounterState { get; set; }

}
