using LivingMessiah.Web.Stores;

namespace LivingMessiah.Web.Shared.Header.Store;

public static class Reducers
{
	[ReducerMethod]
	public static ToolbarState ReduceSetBibleBookAction(ToolbarState state,
			SetBibleBookAction action) =>
					new ToolbarState(
						BibleBook: action.BibleBook,
						BibleWebsite: state.BibleWebsite);

	[ReducerMethod]
	public static ToolbarState ReduceSetBibleWebsiteAction(ToolbarState state,
			SetBibleWebsiteAction action) =>
					new ToolbarState(
						BibleBook: state.BibleBook,
						BibleWebsite: action.BibleWebsite); 
}
