using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.BlazorSyncFusion.Shared //  BlazorDemos.Shared
;

/// <summary>
/// A base component to perform common functionalities.
/// </summary>
public class SampleBaseComponent : ComponentBase
{
		[Inject]
		protected SampleService SampleService { get; set; }

		protected override void OnAfterRender(bool firstRender)
		{
				base.OnAfterRender(firstRender);
				SampleService.Spinner?.Hide();
				SampleService.Spinner?.ShowModalSpinner(false);
		}
}
