using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LivingMessiah.Web.Pages.BlazorExamples
{
	public partial class EventCallbackChild
	{
    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
  }
}
