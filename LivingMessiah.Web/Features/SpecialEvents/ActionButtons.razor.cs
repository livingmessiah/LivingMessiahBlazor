using LivingMessiah.Web.Features.SpecialEvents.Enums;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;  // moved this from ActionButtons.razor

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class ActionButtons
{
	[Parameter, EditorRequired] public Crud? ParmCrud { get; set; } 
	[Parameter, EditorRequired] public int Id { get; set; } // Use 0 for Add and Repopulate
	[Parameter, EditorRequired] public bool IsXsOrSm { get; set; }
	[Parameter]	public EventCallback<CrudAndIdArgs> OnCrudActionSelected{ get; set; }

	private async Task OnButtonClicked()
	{
		CrudAndIdArgs args = new CrudAndIdArgs
		{
			Crud=ParmCrud!,
			Id=Id
		};
		await OnCrudActionSelected.InvokeAsync(args);
	}

	private MarkupString GetBr() 
	{
		return IsXsOrSm ? (MarkupString)"<br>" : (MarkupString)"";
	}
}

public struct CrudAndIdArgs
{
	public Crud Crud { get; set; }
	public int Id { get; set; }
}

