using Microsoft.AspNetCore.Components;
using LoginLink = LivingMessiah.Web.Links.Account;
using LivingMessiah.Web.Pages.SpecialEvents.Stores;
using System.Threading.Tasks;
using System;

namespace LivingMessiah.Web.Pages.SpecialEvents;

public partial class Index
{
	[Inject] NavigationManager NavigationManager { get; set; }
	[Inject] private IState<MainState> MainState { get; set; }
	[Inject] public IDispatcher Dispatcher { get; set; }

	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(0);
		DateTime dt = DateTime.Now;
		DateRange dateRange = new DateRange();
		dateRange.DateBegin = dt.AddMonths(-1);
		dateRange.DateEnd = dt.AddMonths(6);

		var action = new SetDateRangeAction(dateRange);
		Dispatcher.Dispatch(action);

		var action2 = new SetCommandStateAction(Enums.CommandState.Hidden);
		Dispatcher.Dispatch(action2);

		var action3 = new SetCurrentIdAction(0);
		Dispatcher.Dispatch(action3);
	}

	void RedirectToLoginClick(string returnUrl)
	{
		NavigationManager.NavigateTo($"{LoginLink.Login}?returnUrl={returnUrl}", true);
	}
}
