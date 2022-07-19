using Microsoft.AspNetCore.Components;
using System;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Services;

public class AppState
{
	#region Constructor and DI
	private readonly ILogger Logger;
	public AppState(ILogger<AppState> logger)
	{
		Logger = logger;
	}
	#endregion

	#region All properties requiring state persistence

	public string Message { get; private set; } = "";
	public void UpdateMessage(ComponentBase Source, string Message)
	{
		Logger.LogDebug(string.Format("Inside {0}, Message: {1}"
			, nameof(AppState) + "!" + nameof(UpdateMessage), Message));
		this.Message = Message;
		NotifyStateChanged(Source, "Message");
	}
	#endregion

	public event Action<ComponentBase, string> StateChanged;

	private void NotifyStateChanged(ComponentBase Source, string Property)
			=> StateChanged?.Invoke(Source, Property);
}

