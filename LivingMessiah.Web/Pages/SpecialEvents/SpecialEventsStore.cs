using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

using Microsoft.Extensions.Logging;
using static LivingMessiah.Web.Pages.SqlServer;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using Blazored.Toast.Services;
using System;

namespace LivingMessiah.Web.Pages.SpecialEvents;

// 1. Action
public record SpecialEventsSubmitAction(FormVM FormVM);
public record SpecialEventsSubmitSuccessAction();
public record SpecialEventsSubmitFailureAction(string ErrorMessage);

// 2. State
public record SpecialEventsState
{
	public bool Submitting { get; init; }
	public bool Submitted { get; init; }
	public string? ErrorMessage { get; init; }
	public FormVM? Model { get; init; }
}

// 3. Feature
public class SpecialEventsStateFeature : Feature<SpecialEventsState>
{
	public override string GetName() => "SpecialEvents";

	protected override SpecialEventsState GetInitialState()
	{
		return new SpecialEventsState
		{
			Submitting = false,
			Submitted = false,
			ErrorMessage = string.Empty,
			Model = new FormVM()
		};
	}
}

// 4. Reducers
public static class SpecialEventsReducers
{
	[ReducerMethod(typeof(SpecialEventsSubmitAction))]
	public static SpecialEventsState OnSubmit(SpecialEventsState state)
	{
		return state with { Submitting = true };
	}

	[ReducerMethod(typeof(SpecialEventsSubmitSuccessAction))]
	public static SpecialEventsState OnSubmitSuccess(SpecialEventsState state)
	{
		return state with { Submitting = false, Submitted = true };
	}

	[ReducerMethod]
	public static SpecialEventsState OnSubmitFailure(
		SpecialEventsState state, SpecialEventsSubmitFailureAction action)
	{
		return state with { Submitting = false, ErrorMessage = action.ErrorMessage };
	}
}

// 5. Effects WASM version

/*
public class SpecialEventsEffects
{
	private readonly HttpClient _httpClient;
	public SpecialEventsEffects(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	[EffectMethod]
	public async Task SubmitSpecialEvents(SpecialEventsSubmitAction action, IDispatcher dispatcher)
	{
		await Task.Delay(500); // just so we can see the "submitting" message
		var response = await _httpClient.PostAsJsonAsync("Feedback", action.SpecialEventsModel);

		if (response.IsSuccessStatusCode)
		{
			dispatcher.Dispatch(new SpecialEventsSubmitSuccessAction());
		}
		else
		{
			dispatcher.Dispatch(new SpecialEventsSubmitFailureAction(response.ReasonPhrase));
		}
	}
}
*/


// 5. Effects Server version
public class SpecialEventsEffects
{
	//[Inject] public ILogger<SpecialEventsEffects> Logger { get; set; }
	[Inject] public IUpcomingEventsRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[EffectMethod]
	public async Task SubmitSpecialEvents(SpecialEventsSubmitAction action, IDispatcher dispatcher)
	{
		//Logger.LogDebug(string.Format("Inside {0}, Action: {1}", nameof(SpecialEventsEffects) + "!" + nameof(SubmitSpecialEvents), action ));

		await Task.Delay(500); // just so we can see the "submitting" message
		try
		{
			var sprocTuple = await db!.CreateSpecialEvent(action.FormVM);
			if (sprocTuple.NewId != 0)
			{
				Toast!.ShowInfo($"{sprocTuple.ReturnMsg}");
				//VM = new FormVM();
			}
			else
			{
				if (sprocTuple.SprocReturnValue == ReturnValueViolationInUniqueIndex)
				{
					Toast!.ShowWarning($"{sprocTuple.ReturnMsg}");
				}
				else
				{
					Toast!.ShowError($"{sprocTuple.ReturnMsg}");
				}
			}
		}
		catch (Exception)   // ex
		{
			//Logger!.LogError(ex, string.Format("...Inside catch of {0}", nameof(SpecialEventsEffects) + "!" + nameof(SubmitSpecialEvents)));
			dispatcher.Dispatch(new SpecialEventsSubmitFailureAction("An invalid operation occurred, contact your administrator"));
			//Toast!.ShowError("An invalid operation occurred, contact your administrator");
		}
	}
}