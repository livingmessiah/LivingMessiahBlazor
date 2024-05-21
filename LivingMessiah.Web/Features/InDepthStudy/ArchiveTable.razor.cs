using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Features.InDepthStudy;

public partial class ArchiveTable
{
	[Inject] public ILogger<ArchiveTable>? Logger { get; set; }
	[Inject] public Data.IRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	protected bool TurnSpinnerOff = false;
	public IReadOnlyList<Data.ArchiveQuery>? ArchiveList;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug("{Class}!{Method}", nameof(ArchiveTable), nameof(OnInitializedAsync));
		await PopulateTable();
	}

	protected async Task PopulateTable()
	{
		try
		{
			Logger!.LogDebug("{Class}!{Method}", nameof(ArchiveTable), nameof(PopulateTable));
			ArchiveList = await db!.GetArchive()!;  //100
		}
		catch (System.Exception ex)
		{
			Logger!.LogError(ex, "{Class}!{Method}; {Command}", nameof(ArchiveTable), nameof(PopulateTable), nameof(db.GetIndepth));
			Toast!.ShowError("An invalid operation occurred reading database, contact your administrator");
		}
		finally
		{
			TurnSpinnerOff = true;
		}
	}

}