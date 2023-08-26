using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Page = LivingMessiah.Web.Links.Database.Error;

namespace LivingMessiah.Web.Features.Admin.Database;

public partial class Index
{
	[Inject] public IToastService? Toast { get; set; }
	[Inject] public LM.IRepository? dbLivingMessiah { get; set; }
	[Inject] public Sukkot.IRepository? dbSukkot { get; set; }
	[Inject] public ILogger<Index>? Logger { get; set; }

	protected Enums.Database? CurrentDatabase { get; set; }
	private string inside = $"page {Page.Index}; class: {nameof(Index)}";
	public List<zvwErrorLog>? ErrorLogs { get; set; }

	protected override async Task OnInitializedAsync()
	{
		if (CurrentDatabase is null)
		{
			CurrentDatabase = Enums.Database.LivingMessiah;
			Logger!.LogDebug(string.Format("Inside {0}; {1}, CurrentDatabase is null so calling {2}; CurrentDatabase: {3}"
				, inside, nameof(OnInitializedAsync), nameof(PopulateTable), CurrentDatabase));
			await PopulateTable();
		}
	}

	private async Task ReturnedDatabase(Enums.Database database)
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}, action: {2}", inside, nameof(ReturnedDatabase), database.Name));
		CurrentDatabase = database;
		await PopulateTable();
		// FN 1:
	}

	private async Task ReturnedAction(Enums.Action action)
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}, action: {2}", inside, nameof(ReturnedAction), action.Name ));
		switch (action.Name)
		{
			case nameof(Enums.Action.EmptyLog):
				await EmptyErrorLog();
				Toast!.ShowInfo($"...just called {nameof(EmptyErrorLog)}, now calling {nameof(PopulateTable)}");
				await PopulateTable();
				break;

			case nameof(Enums.Action.TestInsert):
				await LogErrorTest();
				Toast!.ShowInfo($"...just called {nameof(LogErrorTest)}, now calling {nameof(PopulateTable)}");
				await PopulateTable();
				break;

			default:
				break;
		}

	}

	#region DatabaseAction

	private int AffectedRows { get; set; } = 0;

	private async Task LogErrorTest()
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}", inside, nameof(LogErrorTest)));
		try
		{
			if (CurrentDatabase == Enums.Database.LivingMessiah)
			{
				AffectedRows = await dbLivingMessiah!.LogErrorTest();
			}
			else
			{
				AffectedRows = await dbSukkot!.LogErrorTest();
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(LogErrorTest)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(OnInitializedAsync)}!{nameof(LogErrorTest)}");
		}

	}

	private async Task EmptyErrorLog()
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}", inside, nameof(EmptyErrorLog)));
		try
		{
			if (CurrentDatabase == Enums.Database.LivingMessiah)
			{
				AffectedRows = await dbLivingMessiah!.EmptyErrorLog();
			}
			else
			{
				AffectedRows = await dbSukkot!.EmptyErrorLog();
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error calling EmptyErrorLog or  GetzvwErrorLog");
			Toast!.ShowError("Error calling EmptyErrorLog or  GetzvwErrorLog");
		}

	}

	private async Task PopulateTable()
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}", inside, nameof(PopulateTable)));
		await Task.Delay(500);
		try
		{
			if (CurrentDatabase == Enums.Database.LivingMessiah)
			{
				ErrorLogs = await dbLivingMessiah!.GetzvwErrorLog();
			}
			else
			{
				ErrorLogs = await dbSukkot!.GetzvwErrorLog();
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(PopulateTable)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(PopulateTable)}");
		}

	}



	#endregion

}

/*
# Note
- FN 1: A call to StateHasChanged isn't required because StateHasChanged is called automatically to rerender the Parent component,
*/