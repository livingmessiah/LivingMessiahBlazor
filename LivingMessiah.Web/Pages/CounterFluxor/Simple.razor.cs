using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Store.Counter;
using LivingMessiah.Web.Store;

namespace LivingMessiah.Web.Pages.CounterFluxor;

public partial class Simple
{
	[Inject] private IState<CounterState> CounterState { get; set; }
	[Inject] public IDispatcher Dispatcher { get; set; }

	private void IncrementCount()
	{
		var action = new IncrementCounterAction();
		Dispatcher.Dispatch(action);
	}

}
