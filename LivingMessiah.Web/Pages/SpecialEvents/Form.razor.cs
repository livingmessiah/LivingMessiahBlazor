using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor.RichTextEditor;
using Markdig;

namespace LivingMessiah.Web.Pages.SpecialEvents;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<SpecialEventsState>? SpecialEventsState { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private string Title = "Add Upcoming Event";

	private FormVM VM => SpecialEventsState!.Value.Model!; // model

	private const string Message = $"Inside Class!Method:{nameof(Form)}!{nameof(HandleValidSubmit)}; calling Dispatch {nameof(SpecialEventsSubmitAction)}";

	protected async Task HandleValidSubmit()
	{
		Logger!.LogDebug(Message);
		await Task.Delay(0);
		Dispatcher!.Dispatch(new SpecialEventsSubmitAction(SpecialEventsState!.Value.Model!));
	}

	private void OnInvalidSubmit()
	{
		
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
	private string? HtmlValue { get; set; }
	private MarkdownPipeline? Pipeline { get; set; }

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

