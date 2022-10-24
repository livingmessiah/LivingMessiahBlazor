using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Store.Counter;
using System;

namespace LivingMessiah.Web.Shared;

public partial class CounterAnchorEventSubscription
{
	[Inject] private IState<CounterState> CounterState { get; set; }

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
