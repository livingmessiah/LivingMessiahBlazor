using LivingMessiah.Web.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.BibleSearch;

public partial class Index
{
	private FormValidation VM = new FormValidation();


	private async Task<IEnumerable<BibleBook>> SearchBibleBooks(string searchText)
	{
		return await Task.FromResult(BibleBook.List
			.Where(x => x.Title.ToLower().Contains(searchText.ToLower()))
			.OrderBy(o => o.Value));
	}

	private void HandleFormSubmit()  // Used only by Form
	{
		// ToDo: maybe log books search?
	}

}
