using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using System;
using Page = LivingMessiah.Web.Links.SpecialEvents;

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FormVM? VM => State!.Value.FormVM;
	private FluentValidationValidator? _fluentValidationValidator;

	private EditMarkdownVM MDVM => State!.Value.EditMarkdownVM;

	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(HandleValidSubmit)));
		Dispatcher!.Dispatch(new Submitting_Request_Action(State!.Value.FormVM!, State!.Value.FormMode!));  
		Dispatcher!.Dispatch(new Get_List_Action());
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(Constants.GetPageHeaderForIndexVM()));
	}

	void CancelActionHandler()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(CancelActionHandler)));
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(Constants.GetPageHeaderForIndexVM()));
	}

	
	readonly string inside = $"page {Page.Index}; class: {nameof(Form)}";
	
	protected async Task ValidSubmit(EditContext context)
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}, action: {2}", inside, nameof(ValidSubmit)));
		int rows = 0;
		EditMarkdownVM vm = (EditMarkdownVM)context.Model;
		//try
		//{
		//	rows = await db!.UpdateDescription(vm.Id, vm.Description!);
		//	HasRowBeenUpdated = true;
		//	Toast!.ShowInfo("Description Updated");
		//}
		//catch (Exception ex)
		//{
		//	UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
		//	LogExceptionMessage = string.Format("  Inside catch of {0}"
		//		, nameof(SpecialEventDescriptionMdEdit) + "!" + nameof(ValidSubmit));
		//	Logger!.LogError(ex, LogExceptionMessage);
		//	Toast!.ShowError(UserInterfaceMessage);
		//}
		Logger!.LogDebug(string.Format("...rows {0}", rows));
	}
} 

