using Microsoft.AspNetCore.Components;
using Page = LivingMessiah.Web.Links.Parasha;
//using LivingMessiah.Web.Enums;  // using LivingMessiah.Web.SmartEnums;

namespace LivingMessiah.Web.Pages.Parasha;

public partial class ParashotTableToggle
{
	[Parameter]
	public string CardCss { get; set; } = "border-primary my-3";

	[Parameter]
	public string HeaderBadgeColor { get; set; } = "bg-warning";

	[Parameter]
	public string Title { get; set; } = "Parasha Table";

	[Parameter]
	public bool IsXsOrSm { get; set; } = false;

	[Parameter]
	public int BookId { get; set; } = 0;

	[Inject]
	NavigationManager NavManager { get; set; }

	public string ButtonText { get; set; } = "Details";
	public string ButtonChevron { get; set; } = " fas fa-chevron-down"; 

	public bool IsCollapsed { get; set; } = true;

	protected void ToggleButtonClick(bool isCollapsed)
	{
		IsCollapsed = !isCollapsed;
		ButtonText = IsCollapsed ? "Details" : "Hide";
		ButtonChevron = IsCollapsed ? "fas fa-chevron-down" : "fas fa-chevron-up"; 
	}

	private void PrintButtonClick(int bookId)
	{
		//string name = BibleBook.FromValue(BookId).Name;
		//NavManager.NavigateTo(Page.IndexPrint + "/" + bibleBookName);
		NavManager.NavigateTo(Page.IndexPrint + "/" + bookId);  //  + "/" + name
	}
}
