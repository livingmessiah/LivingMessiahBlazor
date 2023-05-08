using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Enums;
using LivingMessiah.Web.Shared.Header.Enums;
using LivingMessiah.Web.Stores;
using LivingMessiah.Web.Shared.Header.Store;

namespace LivingMessiah.Web.Shared.Header;

public partial class BibleSearch
{
	[Inject] private IState<ToolbarState>? ToolbarState { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private async Task<IEnumerable<BibleBook>> SearchBibleBooks(string searchText)
	{
		return await Task.FromResult(BibleBook.List
			.Where(x => x.Title.ToLower().Contains(searchText.ToLower()))
			.OrderBy(o => o.Value));
	}
	private BibleBook? SelectedBook { get; set; }

	private async Task SelectedResultChanged(BibleBook result)
	{
		if (ToolbarState!.Value.BibleWebsite is null)
		{
			var action1 = new SetBibleWebsiteAction(BibleWebsite.MyHebrewBible);
			Dispatcher!.Dispatch(action1);
		}

		await Task.Delay(0);  // ToDo: can I do this without async/await ?
		SelectedBook = result;
		var action2 = new SetBibleBookAction(result);
		Dispatcher!.Dispatch(action2);
	}
}
