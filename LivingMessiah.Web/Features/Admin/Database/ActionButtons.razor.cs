using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Page = LivingMessiah.Web.Links.Database.Error;

namespace LivingMessiah.Web.Features.Admin.Database;

public partial class ActionButtons
{
	[Inject] public ILogger<ActionButtons>? Logger { get; set; }

	[Parameter, EditorRequired] public Enums.Database? CurrentDatabase { get; set; }
	[Parameter] public EventCallback<Enums.Action> OnActionSelected { get; set; }

	readonly string inside = $"page {Page.Index}; class: {nameof(ActionButtons)}";

	private void OnButtonClicked(Enums.Action action)
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}, action: {2}", inside, nameof(OnButtonClicked), action.Name));
		OnActionSelected.InvokeAsync(action);
	}

}
