using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;
using Page = LivingMessiah.Web.Links.SpecialEvents;
using Syncfusion.Blazor.RichTextEditor;
using System.Collections.Generic;

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FormVM? VM => State!.Value.FormVM;
	private FluentValidationValidator? _fluentValidationValidator;

	readonly string inside = $"page {Page.Index}; class: {nameof(Form)}";

	protected override void OnInitialized()
	{
		base.OnInitialized();
		if (VM is not null)
		{
			if (VM.EventDate == System.DateTime.MinValue)VM.EventDate = System.DateTime.Now.AddDays(35);
			if (VM.ShowBeginDate is null) VM.ShowBeginDate = System.DateTime.Now.AddDays(25);
			if (VM.ShowEndDate is null)	VM.ShowEndDate = System.DateTime.Now.AddDays(36);
		}
	}

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


	//https://blazor.syncfusion.com/documentation/rich-text-editor/styling
	private List<ToolbarItemModel> Tools = new List<ToolbarItemModel>()
	{
		new ToolbarItemModel() { Command = ToolbarCommand.Bold },
		new ToolbarItemModel() { Command = ToolbarCommand.Italic },
		new ToolbarItemModel() { Command = ToolbarCommand.Underline },
		new ToolbarItemModel() { Command = ToolbarCommand.FontName },
		new ToolbarItemModel() { Command = ToolbarCommand.FontSize },
		new ToolbarItemModel() { Command = ToolbarCommand.FontColor },
		new ToolbarItemModel() { Command = ToolbarCommand.BackgroundColor },
		new ToolbarItemModel() { Command = ToolbarCommand.Separator },
		new ToolbarItemModel() { Command = ToolbarCommand.Formats },
		new ToolbarItemModel() { Command = ToolbarCommand.Alignments },
		new ToolbarItemModel() { Command = ToolbarCommand.Separator },
		new ToolbarItemModel() { Command = ToolbarCommand.CreateLink },
		new ToolbarItemModel() { Command = ToolbarCommand.Image },
		new ToolbarItemModel() { Command = ToolbarCommand.Separator },
		new ToolbarItemModel() { Command = ToolbarCommand.SourceCode },
		new ToolbarItemModel() { Command = ToolbarCommand.Separator },
		new ToolbarItemModel() { Command = ToolbarCommand.Undo },
		new ToolbarItemModel() { Command = ToolbarCommand.Redo }
	};

	private List<DropDownItemModel> FontFamilyItems = new List<DropDownItemModel>()
	{
		new DropDownItemModel() { Text = "Segoe UI", Value = "Arial,Helvetica,sans-serif" },
		new DropDownItemModel() { Text = "Arial", Value = "Roboto" },
		new DropDownItemModel() { Text = "Georgia", Value = "Georgia,serif" },
		new DropDownItemModel() { Text = "Impact", Value = "Impact,Charcoal,sans-serif" },
		new DropDownItemModel() { Text = "Tahoma", Value = "Tahoma,Geneva,sans-serif" }
	};

	private List<DropDownItemModel> FontSizeItems = new List<DropDownItemModel>()
	{
		new DropDownItemModel() { Text = "8 pt", Value = "8pt" },
		new DropDownItemModel() { Text = "10 pt", Value = "10pt" },
		new DropDownItemModel() { Text = "12 pt", Value = "12pt" },
		new DropDownItemModel() { Text = "14 pt", Value = "14pt" },
		new DropDownItemModel() { Text = "42 pt", Value = "42pt" }
	};

}

