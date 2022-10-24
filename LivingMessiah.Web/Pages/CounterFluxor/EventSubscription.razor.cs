using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Store.Counter;
using LivingMessiah.Web.Store;
using System;

namespace LivingMessiah.Web.Pages.CounterFluxor;

public partial class EventSubscription
{
	[Inject] private IState<CounterState> CounterState { get; set; }
	[Inject] public IDispatcher Dispatcher { get; set; }

	private void IncrementCount()
	{
		var action = new IncrementCounterAction();
		Dispatcher.Dispatch(action);
	}

	// Handle StateChanged event rather than inherit FluxorComponent
	protected override void OnAfterRender(bool firstRender)
	{
		if (firstRender)
		{
			CounterState.StateChanged += StateChanged;
		}
	}

	public void StateChanged(object sender, EventArgs args)
	{
		InvokeAsync(StateHasChanged);
	}

	void IDisposable.Dispose()
	{
		CounterState.StateChanged -= StateChanged;
	}
}
