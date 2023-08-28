using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Page = LivingMessiah.Web.Links.Database.Error;

namespace LivingMessiah.Web.Features.Admin.Database;

public partial class DatabaseSelectorButton
{
	[Inject] public ILogger<DatabaseSelectorButton>? Logger { get; set; }
	[Parameter, EditorRequired] public Data.Enums.Database? CurrentDatabase { get; set; }
	[Parameter] public EventCallback<Data.Enums.Database> OnDatabaseSelected { get; set; }

	readonly string inside = $"page {Page.Index}; class: {nameof(DatabaseSelectorButton)}";

	private void OnButtonClicked(Data.Enums.Database database)
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}, db: {2}", inside, nameof(OnButtonClicked), database.Name));
		OnDatabaseSelected.InvokeAsync(database); // why doesn't this have to be async?
	}

	public string ActiveFilter(Data.Enums.Database filter)
	{
		if (filter == CurrentDatabase)
		{
			//Logger.LogDebug($"Inside {nameof(ActiveFilter)}; {filter.Name} now active");
			return "active";
		}
		else
		{
			return "";
		}
	}
}
