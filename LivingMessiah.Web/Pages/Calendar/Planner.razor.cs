using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.KeyDates.Data;
using LivingMessiah.Web.Pages.KeyDates.Queries;
using CacheSettings = LivingMessiah.Web.Settings.Constants.CalendarCache;
using Microsoft.Extensions.Caching.Memory;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.Calendar;

public partial class Planner
{
	[Inject] public IKeyDateRepository db { get; set; }
	[Inject] public IMemoryCache Cache { get; set; }
	[Inject] public ILogger<Planner> Logger { get; set; }
	[Inject] public IToastService Toast { get; set; }

	[Parameter, EditorRequired] public int YearId { get; set; }
	[Parameter] public bool IsXsOrSm { get; set; }

	protected string DateCSS;
	protected string DateFormat;
	protected List<CalendarVM> CalendarVMs;

	public Enums.DateTypeFilter CurrentFilter { get; set; } = Enums.DateTypeFilter.Feast;
	protected async Task OnClickFilter(Enums.DateTypeFilter newFilter)
	{
		CurrentFilter = newFilter;
		Logger.LogDebug(string.Format("...Inside {0}, {1} is now the current filter"
			, nameof(Planner) + "!" + nameof(OnClickFilter), newFilter.Name));
		await PopulatePlanner(YearId, CurrentFilter);
	}

	public string ActiveFilter(Enums.DateTypeFilter filter)
	{
		if (filter == CurrentFilter)
		{
			return "active";
		}
		else
		{
			return "";
		}
	}


	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0}, year={1}", nameof(Planner) + "!" + nameof(OnInitializedAsync), YearId));
		DateFormat = "ddd, MMMM dd, yyyy"; //DateFormat = IsXsOrSm ? "yyyy/MM/dd" : "ddd, MMMM dd, yyyy";
		DateCSS = IsXsOrSm ? "h6" : "h4";
		await PopulatePlanner(YearId, CurrentFilter);
	}

	private async Task PopulatePlanner(int year, Enums.DateTypeFilter filter)
	{
		try
		{
			CalendarVMs = Cache.Get<List<CalendarVM>>(GetChacheKey());

			if (CalendarVMs is null)
			{
				CalendarVMs = await db.GetPlannerVM(YearId, filter);

				if (CalendarVMs is not null)
				{
					PopulateDateTypesInList();
					Logger.LogDebug(string.Format("... Data gotten from DATABASE"));
					Cache.Set(GetChacheKey(), CalendarVMs, TimeSpan.FromMinutes(CacheSettings.FromMinutes));
				}
				else
				{
					Toast.ShowWarning($"Calendar Entries NOT FOUND for YearId: {YearId}");
				}
			}
			else
			{
				Logger.LogDebug(string.Format("... Data gotten from CACHE"));
			}

		}
		catch (Exception ex)
		{
			Toast.ShowError($"Error reading database");
			Logger.LogError(ex, string.Format("...Error calling={0}", nameof(db.GetCalendarEntries)));
		}
	}

	string GetChacheKey()
	{
		return CacheSettings.Key + CurrentFilter.Value;
	}

	void PopulateDateTypesInList()
	{
		foreach (var item in CalendarVMs)
		{
			if (item.DateTypeId == Enums.DateType.Month.Value)
			{
				item.LunarMonth = Enums.LunarMonth.FromValue(item.Detail);
			}
			else
			{
				item.LunarMonth = null;
			}

			if (item.DateTypeId ==  Enums.DateType.Feast.Value)
			{
				item.FeastDay = Enums.FeastDay.FromValue(item.Detail);
			}
			else
			{
				item.FeastDay = null;
			}
		}
	}



}
