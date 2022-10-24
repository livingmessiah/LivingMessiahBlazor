using System;

namespace LivingMessiah.Web.Pages.Home;

public partial class TopRowFluxor
{
	void ToggleEnabledButtonClicked()
	{
		var enabled = !ToolbarState.Value.Enabled;
		var action = new LivingMessiah.Web.Store.SetEnabledAction(enabled);
		Dispatcher.Dispatch(action);
	}

	void UpdateMessageButtonClicked()
	{
		var msg = $"Message updated by {nameof(Home)}!{nameof(TopRowFluxor)} at {DateTime.Now.ToLongTimeString()}";
		var action = new LivingMessiah.Web.Store.SetMessageAction(msg);
		Dispatcher.Dispatch(action);
	}
}
