using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using static LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Step4Registration
{
	[Inject]
	NavigationManager NavManager { get; set; }

	[Inject]
	public ILogger<Step4Registration> Logger { get; set; }

	[Parameter, EditorRequired]
	public bool IsXs { get; set; }

	[Parameter, EditorRequired]
	public Enums.StatusFlag StatusFlag { get; set; }

	[Parameter, EditorRequired]
	public int RegistrationId { get; set; }

	void Details_ButtonClick(MouseEventArgs e, int id, bool showPrintMsg)
	{
		NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.Details + "/" + id + "/" + showPrintMsg);
	}

	void Edit_ButtonClick(MouseEventArgs e, int id)
	{
		//Logger.LogDebug($"Inside {nameof(Step4Registration)}!{nameof(Edit_ButtonClick)}, id:{id}");
		NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
	}

	void DeleteConfirmation_ButtonClick(MouseEventArgs e, int id)
	{
		NavManager.NavigateTo(Links.Sukkot.DeleteConfirmation + "/" + id);
	}

}
