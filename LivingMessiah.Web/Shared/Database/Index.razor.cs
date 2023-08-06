using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

using Blazored.Toast.Services;

namespace LivingMessiah.Web.Shared.Database;

[Authorize(Roles = Roles.Admin)]
public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public IRepositoryLivingMessiah? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public int AffectedRows { get; set; } = 0;
	public List<zvwErrorLog>? ErrorLogs { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside  {0}", "Database" + "!" + nameof(Index) + "!" + nameof(OnInitializedAsync)));
		try
		{
			ErrorLogs = await db!.GetzvwErrorLog();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error reading database");
			Toast!.ShowError("...Error reading database");
		}
	}

	public async Task LogErrorTest_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Index) + "!" + nameof(LogErrorTest_ButtonClick)));
		try
		{
			AffectedRows = await db!.LogErrorTest();
			ErrorLogs = await db.GetzvwErrorLog();
			StateHasChanged();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error calling LogErrorTest or  GetzvwErrorLog");
			Toast!.ShowError("Error calling LogErrorTest or  GetzvwErrorLog");
		}
	}

	public async Task EmptyErrorLog_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Index) + "!" + nameof(EmptyErrorLog_ButtonClick)));
		try
		{
			AffectedRows = await db!.EmptyErrorLog();
			ErrorLogs = await db.GetzvwErrorLog();
			Toast!.ShowInfo($"Errors emptied, AffectedRows: {AffectedRows}");
			StateHasChanged();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error calling EmptyErrorLog or  GetzvwErrorLog");
			Toast!.ShowError("Error calling EmptyErrorLog or  GetzvwErrorLog");
		}
	}


}
