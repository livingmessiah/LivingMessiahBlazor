using Markdig;
using System.Reflection.Metadata;

namespace LivingMessiah.Web.Pages.SampleCode;

public partial class Index
{
	private string? HtmlValue { get; set; }
	protected MarkdownPipeline? pipeline { get; set; }

protected string MD = $@"
# Explanation

- *Bible Cascading Dropdown List* is a good example of multiple `InputSelect` controls where the first one (the parent) drives the values in the second one (child)

### Parent Select

```csharp
<InputSelect class=""form-control""
	ValueChanged=""@((int value) => BibleGroupIdHasChanged(value))""
	ValueExpression=""@(() => bibleGroupId)""
	Value=""@bibleGroupId"">
```
This doesn't have a `@bind-Value-` like the second one does, rather is uses **ValueChanged**, **ValueExpression** and **Value**


This sample could be improved by having the VM passed in as a `[Parameter]` so that is could show off how to pre-polulate fields


## PageBibleSearch
Calls `<BibleSearchForm/>` found in the Shared folder.  This is the same as what's in the top toolbar


## Bible Book Chapter | Syncfusion DDL
This is a uses the Syncfusion controll `SfDropDownList`



";

		protected string? Html { get; set; }

	protected override void OnInitialized()
	{
		Html = Markdig.Markdown.ToHtml(MD, pipeline)?? "";
	}
}
