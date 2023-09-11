using Microsoft.AspNetCore.Components;
using Markdig;

namespace LivingMessiah.Web.Components.MD;

public class Editor_MD_Base : ComponentBase
{
	public string Body { get; set; } = string.Empty;
	//public string Preview => Markdown.ToHtml(Body);

	protected string GetMdPipeline(string? text)
	{
		MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
		if (text is null)
		{
			return "null";
		}
		else
		{
			return Markdig.Markdown.ToHtml(text, pipeline);
		}
	}
}
