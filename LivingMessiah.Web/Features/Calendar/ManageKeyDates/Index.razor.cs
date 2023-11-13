using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

using LivingMessiah.Web.Settings;
using FeastDatEnum = LivingMessiah.Web.Features.Calendar.Enums.FeastDay;

using CalendarEnums = LivingMessiah.Web.Features.Calendar.Enums;
using Page = LivingMessiah.Web.Links.KeyDatesEdit;
using System.Linq;
using System;
using LivingMessiah.Web.Features.Calendar.ManageKeyDates.Queries;
using Blazored.Toast.Services;
using Syncfusion.Blazor.Data;

namespace LivingMessiah.Web.Features.Calendar.ManageKeyDates;

public partial class Index
{
	[Inject] IOptions<AppSettings>? AppSettings { get; set; }
	protected int _currentYear;
	protected int _previousYear;

	[Inject] public IToastService? Toast { get; set; }
	[Inject] Data.IRepository? db { get; set; }
	[Inject] public ILogger<Index>? Logger { get; set; }

	protected string inside = $"page {Page.Index}; class: {nameof(Index)}";


	protected override async Task OnInitializedAsync()
	{
		_currentYear = AppSettings!.Value.YearId;
		_previousYear = _currentYear - 1;

		Logger!.LogDebug(string.Format("{0}; {1}; {2}", inside, nameof(OnInitializedAsync), _currentYear));

		try
		{
			await PopulateCalendarEntries();
			if (CalendarEntries is not null)
			{
				PopulateCalendarEntryFiltered();
				Logger!.LogDebug(string.Format("...CalendarEntries.Count:{0}, CalendarEntriesFiltered.Count: {1}"
					, CalendarEntries.Count, CalendarEntriesFiltered!.Count));
			}

			await PopulateCalendarAnalysisQuery();
			if (CalendarAnalysis is not null)
			{
				Logger!.LogDebug(string.Format("...CalendarAnalysis.Count: {0}", CalendarAnalysis.Count));
				PopulateCalendarEntryFiltered();
				PopulateFeastAnalysisList();
				PopulateMonthAnalysisList();
				PopulateSeasonAnalysisList();
			}

			await PopulateKeyDateConstantsList();

		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(OnInitializedAsync)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(OnInitializedAsync)}");
		}

		base.OnInitialized();
	}

	#region EventCallbacks
	protected CalendarEnums.DateType? CurrentFilter { get; set; } = Constants.Defaults.Filter;

	private void ReturnedFilter(CalendarEnums.DateType filter)
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}, filter: {2}", inside, nameof(ReturnedFilter), filter.Name));
		CurrentFilter = filter;
		PopulateCalendarEntryFiltered();
	}

	private async Task ReturnedRefresh(CalendarEnums.DateType filter)
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}, filter: {2}"
			, inside, nameof(ReturnedRefresh), filter.Name));

		await PopulateCalendarAnalysisQuery();

		var f = filter;
		f
			.When(CalendarEnums.DateType.Month!).Then(() => PopulateMonthAnalysisList())
			.When(CalendarEnums.DateType.Feast).Then(() => PopulateFeastAnalysisList())
			.When(CalendarEnums.DateType.Season).Then(() => PopulateSeasonAnalysisList())
			.Default(() => Logger!.LogDebug(string.Format("...{0} ignored", nameof(filter))));
	}

	#endregion

	protected List<CalendarEntry>? CalendarEntries;
	private async Task PopulateCalendarEntries()
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}", inside, nameof(PopulateCalendarEntries)));
		try
		{
			//ToDo: need to change this
			CalendarEntries = await db!.GetCalendarEntries(_currentYear, CalendarEnums.DateType.Feast);
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(PopulateCalendarEntries)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(PopulateCalendarEntries)}");
		}
	}

	protected List<Queries.CalendarAnalysisQuery>? CalendarAnalysis;
	private async Task PopulateCalendarAnalysisQuery()
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}", inside, nameof(PopulateCalendarAnalysisQuery)));
		try
		{
			CalendarAnalysis = await db!.GetCalendarAnalysisQuery(_currentYear, CalendarEnums.DateType.All);
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(PopulateCalendarAnalysisQuery)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(PopulateCalendarAnalysisQuery)}");
		}
	}


	protected List<Queries.CalendarEntry>? CalendarEntriesFiltered;
	private void PopulateCalendarEntryFiltered()
	{
		CalendarEntriesFiltered = CalendarEntries!.Where(w => w.DateTypeId == CurrentFilter!.Value).ToList();
	}

	protected List<FeastAnalysisVM>? FeastAnalysisList;
	private void PopulateFeastAnalysisList()
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}", inside, nameof(PopulateFeastAnalysisList)));
		FeastAnalysisList = new List<FeastAnalysisVM>();

		foreach (var item in CalendarAnalysis!
			.Where(w => CalendarEnums.DateType.FromValue(w.DateTypeId) == CalendarEnums.DateType.Feast.Value).ToList())
		{
			FeastDatEnum feastDay = FeastDatEnum.FromValue(item.EnumId);

			if (feastDay.DaysFromPrevFeast is not null)
			{
				FeastAnalysisList!.Add
				(
					new FeastAnalysisVM
					{
						Event = item.EventDescr,
						PreviousDate = item.PrevDateYMD,
						Date = item.DateYMD,
						ActualDifference = item.DiffFromPrevDate,
						RequiredDifference = (int)feastDay.DaysFromPrevFeast
					}
				);
			}

		}

	}

	protected List<MonthAnalysisVM>? MonthAnalysisList;
	private void PopulateMonthAnalysisList()
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}", inside, nameof(PopulateMonthAnalysisList)));
		MonthAnalysisList = new List<MonthAnalysisVM>();

		try
		{
			foreach (var item in CalendarAnalysis!
				.Where(w => CalendarEnums.DateType.FromValue(w.DateTypeId) == CalendarEnums.DateType.Month.Value).ToList())
			{
				MonthAnalysisList!.Add
				(
					new MonthAnalysisVM
					{
						Month = item.EventDescr,
						PreviousDate = item.PrevDateYMD,
						Date = item.DateYMD,
						ActualDifference = item.DiffFromPrevDate
					}
				);
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(PopulateMonthAnalysisList)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(PopulateMonthAnalysisList)}");
		}
	}

	protected List<SeasonAnalysisVM>? SeasonAnalysisList;
	private void PopulateSeasonAnalysisList()
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}", inside, nameof(PopulateSeasonAnalysisList)));
		SeasonAnalysisList = new List<SeasonAnalysisVM>();

		try
		{
			foreach (var item in CalendarAnalysis!
				.Where(w => CalendarEnums.DateType.FromValue(w.DateTypeId) == CalendarEnums.DateType.Season.Value).ToList())
			{
				SeasonAnalysisList!.Add
				(
					new SeasonAnalysisVM
					{
						Season = item.EventDescr,
						PreviousDate = item.PrevDateYMD,
						Date = item.DateYMD,
						ActualDifference = item.DiffFromPrevDate
					}
				);
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(PopulateSeasonAnalysisList)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(PopulateSeasonAnalysisList)}");
		}
	}

	protected KeyDateConstantsQuery? KeyDateConstants;
	private async Task PopulateKeyDateConstantsList()
	{
		Logger!.LogDebug(string.Format("Inside {0}; {1}", inside, nameof(PopulateKeyDateConstantsList)));
		try
		{
			KeyDateConstants = await db!.GetKeyDateConstants();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(PopulateKeyDateConstantsList)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(PopulateKeyDateConstantsList)}");
		}
	}

}