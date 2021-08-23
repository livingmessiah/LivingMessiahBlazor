using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LivingMessiah.Web.Pages.BlazorExamples
{
	public partial class EventCallbackParent
	{
    private string message;
    private void ShowMessage(MouseEventArgs e)
    {
      message = $"Blaze a new trail with Blazor! ({e.ScreenX}:{e.ScreenY})";
    }
  }
}
