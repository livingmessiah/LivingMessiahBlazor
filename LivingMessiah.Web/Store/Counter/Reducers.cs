namespace LivingMessiah.Web.Store.Counter;

public static class Reducers
{
	[ReducerMethod]
	public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action) =>
		new CounterState(clickCount: state.ClickCount + action.Increment);  // + 1

	// new(clickCount: state.ClickCount + 1);
}

/*
Naming convention for the method name is Reduce + the action parameter type e.g. IncrementCounterAction
*/