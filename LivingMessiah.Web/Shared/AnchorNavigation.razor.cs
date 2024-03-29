using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace LivingMessiah.Web.Shared;

// Code gotten from https://www.meziantou.net/anchor-navigation-in-a-blazor-application.htm

public partial class AnchorNavigation
{
	protected override void OnInitialized()
	{
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
		NavigationManager.LocationChanged += OnLocationChanged;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await ScrollToFragment();
	}

	public void Dispose()
	{
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
		NavigationManager.LocationChanged -= OnLocationChanged;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

	}

	private async void OnLocationChanged(object sender, LocationChangedEventArgs e)
	{
		await ScrollToFragment();
	}

	private async Task ScrollToFragment()
	{
		var uri = new Uri(NavigationManager.Uri, UriKind.Absolute);
		var fragment = uri.Fragment;
		if (fragment.StartsWith('#'))
		{
			// Handle text fragment (https://example.org/#test:~:text=foo)
			// https://github.com/WICG/scroll-to-text-fragment/
			var elementId = fragment.Substring(1);
			var index = elementId.IndexOf(":~:", StringComparison.Ordinal);
			if (index > 0)
			{
				elementId = elementId.Substring(0, index);
			}

			if (!string.IsNullOrEmpty(elementId))
			{
				await JSRuntime.InvokeVoidAsync("BlazorScrollToId", elementId);
			}
		}
	}
}
