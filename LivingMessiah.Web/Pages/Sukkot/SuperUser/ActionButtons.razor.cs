using LivingMessiah.Web.Pages.Sukkot.SuperUser.Enums;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser;

public partial class ActionButtons
{
	[Parameter, EditorRequired] public Crud? ParmCrud { get; set; }
	[Parameter] public string? EMail { get; set; } // Required for Add
	[Parameter, EditorRequired] public int Id { get; set; } // Use 0 for Add and Repopulate
	[Parameter, EditorRequired] public bool IsXsOrSm { get; set; }
	[Parameter] public EventCallback<CrudAndIdArgs> OnCrudActionSelected { get; set; }

	private async Task OnButtonClicked()
	{
		CrudAndIdArgs args = new CrudAndIdArgs
		{
			Crud = ParmCrud!,
			EMail = EMail ?? "???",
			Id = Id
		};
		await OnCrudActionSelected.InvokeAsync(args);
	}

	private MarkupString GetBr()
	{
		return IsXsOrSm ? (MarkupString)"<br>" : (MarkupString)"";
	}

	private string GetTitle()
	{
		if (Id != 0)
		{
			return $"Id: {Id}";
		}
		else
		{
			return ""; 
		}
	}

}

public struct CrudAndIdArgs
{
	public Crud Crud { get; set; }
	public string EMail { get; set; } 
	public int Id { get; set; }
}

