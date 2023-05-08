using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using Microsoft.AspNetCore.Components.Forms;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin.EditMarkdown;

public partial class SpecialEventDescriptionMdEdit
{
	[Inject] public ILogger<SpecialEventDescriptionMdEdit>? Logger { get; set; }
	[Inject] public IUpcomingEventsRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter] public int Id { get; set; }

	private EditMarkdownVM VM = new EditMarkdownVM();

	private string UserInterfaceMessage = "";
	private string LogExceptionMessage = "";

	protected override async Task OnInitializedAsync()
	{
		try
		{
			Logger!.LogDebug(string.Format("Inside {0} Id:{1}"
				, nameof(SpecialEventDescriptionMdEdit) + "!" + nameof(OnInitializedAsync), Id));

			VM = await db!.GetDescription(Id);
			if (VM is null)
			{
				UserInterfaceMessage = $"{nameof(VM)} NOT FOUND";
				Toast!.ShowWarning(UserInterfaceMessage);
			}
		}
		catch (Exception ex)
		{
			UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
			LogExceptionMessage = string.Format("  Inside catch of {0}"
				, nameof(SpecialEventDescriptionMdEdit) + "!" + nameof(OnInitializedAsync));
			Logger!.LogError(ex, LogExceptionMessage);
			Toast!.ShowError(UserInterfaceMessage);
		}
	}

	private bool HasRowBeenUpdated { get; set; } = false;
	private void CloseDialog()
	{
		this.HasRowBeenUpdated = false;
	}

	protected async Task ValidSubmit(EditContext context)
	{
		Logger!.LogDebug(string.Format("Inside {0}"
			, nameof(SpecialEventDescriptionMdEdit) + "!" + nameof(ValidSubmit)));
		int rows = 0;
		EditMarkdownVM vm = (EditMarkdownVM)context.Model;
		try
		{
			rows = await db!.UpdateDescription(vm.Id, vm.Description);
			HasRowBeenUpdated = true;
			Toast!.ShowInfo("Description Updated");
		}
		catch (Exception ex)
		{
			UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
			LogExceptionMessage = string.Format("  Inside catch of {0}"
				, nameof(SpecialEventDescriptionMdEdit) + "!" + nameof(ValidSubmit));
			Logger!.LogError(ex, LogExceptionMessage);
			Toast!.ShowError(UserInterfaceMessage);
		}
		Logger!.LogDebug(string.Format("...rows {0}", rows));
	}


}
