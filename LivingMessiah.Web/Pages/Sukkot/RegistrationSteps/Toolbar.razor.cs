using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static LivingMessiah.Web.Links.Sukkot;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Toolbar
{
	[Inject]
	NavigationManager NavManager { get; set; }

	[Parameter, EditorRequired]
	public Enums.StatusFlag StatusFlag { get; set; }

	[Parameter, EditorRequired]
	public Status Status { get; set; }

	[Parameter, EditorRequired]
	public int Id { get; set; }
	

	void Payment_ButtonClick(MouseEventArgs e, int id)
	{
		NavManager.NavigateTo(Links2.Payment + "/" + id);
	}

	void Details_ButtonClick(MouseEventArgs e, int id, bool showPrintMsg)
	{
		NavManager.NavigateTo(Links.Sukkot.Details + "/" + id + "/" + showPrintMsg);
	}

	void Edit_ButtonClick(MouseEventArgs e, int id)
	{
		
		NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
	}

	void DeleteConfirmation_ButtonClick(MouseEventArgs e, int id)
	{
		NavManager.NavigateTo(Links.Sukkot.DeleteConfirmation + "/" + id);
	}

}


