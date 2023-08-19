using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SpecialEvents.Stores;

namespace LivingMessiah.Web.Pages.SpecialEvents;

public partial class Table
{
	[Inject] public Data.IRepository? db { get; set; }
	[Inject] public ILogger<Table>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Inject] private IState<MainState>? MainState { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	protected List<Models.SpecialEventVM>? VM;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Table) + "!" + nameof(OnInitializedAsync) ) );
		if (MainState!.Value.DateRange is not null) 
		{
			await PopulateTable(MainState.Value.DateRange.DateBegin, MainState.Value.DateRange.DateEnd);
		}
		else
		{
			Logger!.LogDebug(string.Format("...MainState.Value.DateRange is null, nothing to do"));
			//Toast.ShowInfo($"MainState.Value.DateRange is null");
		}

	}

	protected async Task PopulateTable(DateTime dateBegin, DateTime dateEnd)
	{
		Logger!.LogDebug(string.Format("Inside {0}, DateBegin:{1}, DateEnd:{2}"
		, nameof(Table) + "!" + nameof(PopulateTable), dateBegin.ToShortDateString(), dateEnd.ToShortDateString() ));
		VM = await db!.GetEventsByDateRange(dateBegin, dateEnd);
	}

	void AddActionHandler()
	{
		var action2 = new SetCommandStateAction(Enums.CommandState.Add);
		Dispatcher!.Dispatch(action2);
		var action3 = new SetCurrentIdAction(0);
		Dispatcher.Dispatch(action3);
	}

	void EditActionHandler(int id)
	{
		var action2 = new SetCommandStateAction(Enums.CommandState.Edit);
		Dispatcher!.Dispatch(action2);
		var action3 = new SetCurrentIdAction(id);
		Dispatcher.Dispatch(action3);
	}

	void DisplayActionHandler(int id)
	{
		var action2 = new SetCommandStateAction(Enums.CommandState.Read);
		Dispatcher!.Dispatch(action2);
		var action3 = new SetCurrentIdAction(id);
		Dispatcher.Dispatch(action3);
	}

	void DeleteActionHandler(int id)
	{
		var action2 = new SetCommandStateAction(Enums.CommandState.Delete);
		Dispatcher!.Dispatch(action2);
		var action3 = new SetCurrentIdAction(id);
		Dispatcher.Dispatch(action3);
	}

}
