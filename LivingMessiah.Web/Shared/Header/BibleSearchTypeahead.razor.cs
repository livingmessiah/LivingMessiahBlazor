using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Shared.Header;

public partial class BibleSearchTypeahead
{

	private ViewModel VM = new ViewModel();

	private async Task<IEnumerable<BibleBook>> SearchBibleBooks(string searchText)
	{
		return await Task.FromResult(BibleBook.List
			.Where(x => x.Title.ToLower().Contains(searchText.ToLower()))
			.OrderBy(o => o.Value));
	}
}

public class ViewModel
{
	public BibleBook SelectedBook { get; set; }

	public string MyHebrewBibleBookChapterUrl(int chapter)
	{
		return "https://myhebrewbible.com/BookChapter/" + SelectedBook.Title + "/" + chapter + "/slug";
	}

	public string MyHebrewBibleBookChapterTitle(int chapter)
	{
		return "MyHebrewBible.com/BookChapter/" + SelectedBook.Title + "/" + chapter;
	}

}