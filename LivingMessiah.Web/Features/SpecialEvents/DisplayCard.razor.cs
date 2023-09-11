using Blazored.Toast.Services;
using Markdig;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class DisplayCard
{
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	FormVM? FormVM => State!.Value.FormVM;

	/* 
	 if Description has no special md (e.g. Tables) then you don't need the pipeline
	 - https://stackoverflow.com/questions/67577034/blazor-webassembly-app-markdig-can-not-render-pipe-tables-or-grid-tables-in
	 */
	protected string GetDescriptionMdPipeline(string? description)
	{
		MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
		if (description is null)
		{
			return "null";
		}
		else
		{
			return Markdig.Markdown.ToHtml(description, pipeline);
		}
	}


	private void Edit_ButtonClick(int id)
	{
		Toast!.ShowInfo($"{nameof(Edit_ButtonClick)} clicked");
		//NavManager.NavigateTo(Links.UpcomingEventsAdmin.EditMarkdown.Page + "/" + id);
	}

	void CancelActionHandler()
	{
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(Constants.GetPageHeaderForIndexVM()));
	}
}



