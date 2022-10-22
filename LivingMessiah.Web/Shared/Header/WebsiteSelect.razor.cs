using LivingMessiah.Web.Shared.Header.Enums;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Shared.Header;

public partial class WebsiteSelect
{
	protected BibleWebsite bw = BibleWebsite.MyHebrewBible;
	private string? selectedBibleWebsite;


	private bool IsSelectedBibleWebsite(string bibleWebsite)
	{
		return bibleWebsite == selectedBibleWebsite;
	}

	private void ChangingBibleWebsite(ChangeEventArgs e)
	{
		selectedBibleWebsite = e.Value.ToString();
		bw = BibleWebsite.FromName((string)selectedBibleWebsite);
	}

}
