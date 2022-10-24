namespace LivingMessiah.Web.Store.Toolbar;

public static class Reducers
{
	[ReducerMethod]
	public static ToolbarState ReduceSetMessageAction(ToolbarState state,
			SetMessageAction action) =>
					new ToolbarState(Message: action.Message, Enabled: state.Enabled); // Here Enabled is just passed through

	[ReducerMethod]
	public static ToolbarState ReduceSetEnabledAction(ToolbarState state,
			SetEnabledAction action) =>
					new ToolbarState(Message: state.Message, Enabled: action.Enabled); // And here Message is just passed through

}
