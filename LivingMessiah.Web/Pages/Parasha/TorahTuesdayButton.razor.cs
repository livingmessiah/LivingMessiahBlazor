using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Domain;

namespace LivingMessiah.Web.Pages.Parasha;

public partial class TorahTuesdayButton
{
	[Inject]
	public Services.ILinkService LinkService { get; set; }

	protected Link TorahTuesdayLink { get; set; } 

	protected override async Task OnInitializedAsync()
	{
		TorahTuesdayLink =  LinkService.GetHomeSidebarLinks().Where(w => w.Index == Links.TorahTuesday.Index).SingleOrDefault();
	}
}
/*

protected Link TorahTuesdayLink { get; set; } = LinkService.GetHomeSidebarLinks().Where(w => w.Index == Links.TorahTuesday.Index).SingleOrDefault();

Error	CS0236: A field initializer cannot reference the non-static field, method, or property 'TorahTuesdayButton.LinkService'

ToDo: Can `Services.ILinkService` be static?  Thereby you can for go the OnInitializedAsync

*/