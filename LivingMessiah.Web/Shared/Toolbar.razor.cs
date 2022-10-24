using LivingMessiah.Web.Store;
using System;

namespace LivingMessiah.Web.Shared;

public partial class Toolbar
{
	void UpdateMessageButtonClicked()
	{
		var msg = $"Message updated by {nameof(Shared)}!{nameof(Toolbar)} at {DateTime.Now.ToLongTimeString()}";
		var action = new SetMessageAction(msg);
		Dispatcher.Dispatch(action);
	}
}
