using Syncfusion.Blazor.Calendars;
using System;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.SpecialEvents.Stores;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.SpecialEvents;

public partial class DateRangePicker
{
	[Inject] private IState<MainState>? MainState { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }
	[Inject] public ILogger<DateRangePicker>? Logger { get; set; }

	private DateRange SelectedDates { get; set; } = new DateRange();

	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(0);
		if (MainState!.Value.DateRange is not null)
		{
			SelectedDates.DateBegin = MainState.Value.DateRange.DateBegin;
			SelectedDates.DateEnd = MainState.Value.DateRange.DateEnd;
		}
	}

	public async void ValueChangeHandler(RangePickerEventArgs<DateTime?> args)
	{
		Logger!.LogDebug(string.Format("Inside {0}"
			, nameof(DateRangePicker) + "!" + nameof(ValueChangeHandler)));
		await Task.Delay(0);

		if (args is not null)
		{
			SelectedDates.DateBegin = args.StartDate!.Value;
			SelectedDates.DateEnd = args.EndDate!.Value;
			Logger!.LogDebug(string.Format("...calling Dispatch, selected Dates: {0}", SelectedDates.ToString()));
			var action = new SetDateRangeAction(SelectedDates);
			Dispatcher!.Dispatch(action);
		}
		else
		{
			Logger!.LogDebug(string.Format("...MainState.Value.DateRange is NOT null but args IS null, NOTHING TO DO"));
		}

	}
}
