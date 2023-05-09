namespace LivingMessiah.Web.Pages.SpecialEvents.Stores;

public static class Reducers
{
	[ReducerMethod]
	public static MainState ReduceSetDateRangeAction(MainState state,
			SetDateRangeAction action) =>
					new MainState(
						DateRange: action.DateRange!,
						CommandState: state.CommandState,
						CurrentId: state.CurrentId);

	[ReducerMethod]
	public static MainState ReduceSetCommandStateAction(MainState state,
			SetCommandStateAction action) =>
					new MainState(
						DateRange: state.DateRange,
						CommandState: action.CommandState,
						CurrentId: state.CurrentId);

	[ReducerMethod]
	public static MainState ReduceSetCurrentIdAction(MainState state,
			SetCurrentIdAction action) =>
					new MainState(
						DateRange: state.DateRange,
						CommandState: state.CommandState,
						CurrentId: action.CurrentId);

}
