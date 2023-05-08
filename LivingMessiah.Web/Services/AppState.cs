using Microsoft.AspNetCore.Components;
using System;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Services;

public class AppState
{
	#region Constructor and DI
	private readonly ILogger? Logger;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public AppState(ILogger<AppState> logger)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
		Logger = logger;
	}
	#endregion

	#region All properties requiring state persistence

	public string Message { get; private set; } = "";
	public void UpdateMessage(ComponentBase Source, string Message)
	{
		Logger!.LogDebug(string.Format("Inside {0}, Message: {1}"
			, nameof(AppState) + "!" + nameof(UpdateMessage), Message));
		this.Message = Message;
		NotifyStateChanged(Source, "Message");
	}

	/*
	public bool RefreshHraNotRegistered { get; private set; } = false;
	public void UpdatRefreshHraNotRegistered(ComponentBase Source, bool refreshHraNotRegistered)
	{
		Logger.LogDebug(string.Format("Inside {0}, Message: {1}"
			, nameof(AppState) + "!" + nameof(UpdatRefreshHraNotRegistered), refreshHraNotRegistered));
		RefreshHraNotRegistered = refreshHraNotRegistered;
		NotifyStateChanged(Source, "RefreshHraNotRegistered");
	}
	*/

	#endregion

	public event Action<ComponentBase, string> StateChanged;

	private void NotifyStateChanged(ComponentBase Source, string Property)
			=> StateChanged?.Invoke(Source, Property);
}

