using Microsoft.AspNetCore.Components;
using Page = LivingMessiah.Web.Features.Parasha.Enums.ParashaLinks;

namespace LivingMessiah.Web.Features.Parasha.ListByBook;

public partial class PrintButton
{
	[Inject] NavigationManager? NavManager { get; set; }

	[Parameter, EditorRequired] public int BookId { get; set; } = 0;

	private void PrintButtonClick()
	{
		NavManager!.NavigateTo(Page.IndexPrint + "/" + BookId);
	}
}
