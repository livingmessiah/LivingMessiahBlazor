using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.KeyDates.Data;
using CacheSettings = LivingMessiah.Web.Settings.Constants.PlannerCache;
using Microsoft.Extensions.Caching.Memory;
using Blazored.Toast.Services;
using System.Linq;

namespace LivingMessiah.Web.Pages.Calendar;

public partial class Planner
{
	[Inject] public IKeyDateRepository? db { get; set; }
	[Inject] public IMemoryCache? Cache { get; set; }
	[Inject] public ILogger<Planner>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter, EditorRequired] public int YearId { get; set; }
	[Parameter] public bool IsXsOrSm { get; set; }

	protected string? DateCSS;
	protected string? DateFormat;
	protected List<PlannerQuery>? PlannerQueries;  

	public Enums.DateTypeFilter CurrentFilter { get; set; } = Enums.DateTypeFilter.All; 
	protected async Task OnClickFilter(Enums.DateTypeFilter newFilter)
	{
		CurrentFilter = newFilter;
		Logger!.LogDebug(string.Format("...Inside {0}, {1} is now the current filter"
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
		Logger!.LogDebug(string.Format("Inside {0}, year={1}", nameof(Planner) + "!" + nameof(OnInitializedAsync), YearId));
		DateFormat = "ddd, MMMM dd, yyyy"; //DateFormat = IsXsOrSm ? "yyyy/MM/dd" : "ddd, MMMM dd, yyyy";
		DateCSS = IsXsOrSm ? "h6" : "h4";
		await PopulatePlanner(YearId, CurrentFilter);
	}

	private async Task PopulatePlanner(int year, Enums.DateTypeFilter filter)
	{
		Logger!.LogDebug(string.Format("...{0}", nameof(PopulatePlanner)));
		try
		{
			PlannerQueries = Cache!.Get<List<PlannerQuery>>(GetChacheKey());

			if (PlannerQueries is null)
			{
				PlannerQueries = await db!.GetPlannerQueries(YearId, filter);

				if (PlannerQueries is not null)
				{
					if (PlannerQueries.Any())
					{
						Logger!.LogDebug(string.Format("... Data gotten from DATABASE; PlannerQueries.Count: {0}", PlannerQueries.Count));
					}
					else
					{
						Logger!.LogDebug(string.Format("... Data gotten from DATABASE BUT NO RECORDS RETURNED!"));
					}

					PopulateFeastDetailInList();
					Cache!.Set(GetChacheKey(), PlannerQueries, TimeSpan.FromMinutes(CacheSettings.FromMinutes));
				}
				else
				{
					Toast!.ShowWarning($"Calendar Entries NOT FOUND for YearId: {YearId}");
				}
			}
			else
			{
				Logger!.LogDebug(string.Format("... Data gotten from CACHE"));
			}

		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Error calling={0}", nameof(db.GetPlannerQueries)));
			Toast!.ShowError($"Error reading database");
		}
	}

	string GetChacheKey()
	{
		return CacheSettings.Key + CurrentFilter.Value;
	}

	
	void PopulateFeastDetailInList()
	{
		foreach (var item in PlannerQueries!)
		{
			if (item.DateTypeId ==  Enums.DateType.Feast.Value)
			{
				item.FeastDay = Enums.FeastDay.FromValue(item.EnumId);
			}
			else
			{
				item.FeastDay = null;
			}
		}
	}

}
