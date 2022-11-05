using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.UpcomingEvents.Enums;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;

using static LivingMessiah.Web.Pages.SqlServer;
using Syncfusion.Blazor.RichTextEditor;
using Markdig;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin.CRUD;

public partial class Form
{
	[Inject] public IUpcomingEventsRepository db { get; set; }
	[Inject] public ILogger<Form> Logger { get; set; }
	[Inject] public IToastService Toast { get; set; }

	public FormVM VM { get; set; } = new FormVM();

	private string Title = "Add Upcoming Event";

	private string UserInterfaceMessage = "";
	private string LogExceptionMessage = "";

	protected override void OnInitialized()
	{
		VM.SpecialEventTypeId = SpecialEventType.Other.Value;
		VM.EventDate = DateTime.Now.AddDays(35);
		VM.ShowBeginDate = DateTime.Now.AddMonths(1);
		VM.ShowEndDate = DateTime.Now.AddDays(40);
	}

	protected async Task HandleValidSubmit()
	{
		Logger.LogDebug(string.Format("Inside {0}, VM.ToString: {1}"
			, nameof(Form) + "!" + nameof(HandleValidSubmit), VM.ToString()));
		try
		{
			VM.Id = 0;
			var sprocTuple = await db.Create(VM);
			if (sprocTuple.NewId != 0)
			{
				Toast.ShowInfo($"{sprocTuple.ReturnMsg}");
				VM = new FormVM();
			}
			else
			{
				if (sprocTuple.SprocReturnValue == ReturnValueViolationInUniqueIndex)
				{
					Toast.ShowWarning($"{sprocTuple.ReturnMsg}");
				}
				else
				{
					Toast.ShowError($"{sprocTuple.ReturnMsg}");
				}
			}
		}
		catch (Exception)  // catch (Exception ex)
		{
			UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
			LogExceptionMessage = string.Format("  Inside catch of {0}"
				, nameof(Form) + "!" + nameof(HandleValidSubmit));
			Logger.LogError(LogExceptionMessage);  //ex, LogExceptionMessage
			Toast.ShowError(UserInterfaceMessage);
		}
	}

	private void OnInvalidSubmit()
	{
		//Toast.ShowWarning("Invalid Submit");
	}

	private void OnValueChange(Syncfusion.Blazor.RichTextEditor.ChangeEventArgs args)
	{
		if (args.Value == null)
		{
			this.HtmlValue = null;
		}
		else
		{
			this.HtmlValue = Markdig.Markdown.ToHtml(args.Value, Pipeline);
		}
	}

	private bool IsPreview { get; set; }
	private string HtmlValue { get; set; }
	private MarkdownPipeline Pipeline { get; set; }
	/*
			private string MarkdownValue { get; set; } = @"The sample is added to showcase **markdown editing**.

	Type or edit the content and apply formatting to view markdown formatted content.

	We can add our own custom formation syntax for the Markdown formation, [sample link](https://blazor.syncfusion.com/demos/rich-text-editor/markdown-custom-format).

	The third-party library **Marked** is used in this sample to convert markdown into HTML content.";
	*/
	private void PreviewClick()
	{
		this.IsPreview = true;
	}

	private void CodeClick()
	{
		this.IsPreview = false;
	}

	private List<ToolbarItemModel> Items = new List<ToolbarItemModel>() {
				new ToolbarItemModel() { Name = "code", TooltipText = "Code View" },
		};

	private List<ToolbarItemModel> Tools = new List<ToolbarItemModel>()
		{
			new ToolbarItemModel() { Command = ToolbarCommand.Bold },
			new ToolbarItemModel() { Command = ToolbarCommand.Italic },
			new ToolbarItemModel() { Command = ToolbarCommand.Underline },
			new ToolbarItemModel() { Command = ToolbarCommand.Separator },
			new ToolbarItemModel() { Command = ToolbarCommand.Formats },
			new ToolbarItemModel() { Command = ToolbarCommand.Alignments },
			new ToolbarItemModel() { Command = ToolbarCommand.Separator },
			new ToolbarItemModel() { Command = ToolbarCommand.CreateLink },
			new ToolbarItemModel() { Command = ToolbarCommand.Image },
			new ToolbarItemModel() { Command = ToolbarCommand.CreateTable },
			new ToolbarItemModel() { Command = ToolbarCommand.Separator },
			new ToolbarItemModel() { Command = ToolbarCommand.SourceCode },
			new ToolbarItemModel() { Command = ToolbarCommand.Separator },
			new ToolbarItemModel() { Command = ToolbarCommand.Undo },
			new ToolbarItemModel() { Command = ToolbarCommand.Redo }
		};

	private Dictionary<string, string> ListSyntax { get; set; } = new Dictionary<string, string>(){
				{ "OL", "1., 2., 3." }
		};




} // class 

