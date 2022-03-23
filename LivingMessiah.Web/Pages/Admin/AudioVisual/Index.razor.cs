using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

//using LivingMessiah.Web.Services;

using LinkLogin = LivingMessiah.Web.Links.Account;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public partial class Index
{
	[Inject]
	public ILogger<Index> Logger { get; set; }

	[Inject]
	public IWeeklyVideosRepository db { get; set; }

	[Inject]
		NavigationManager NavigationManager { get; set; }

		void RedirectToLoginClick(string returnUrl)
		{
				NavigationManager.NavigateTo($"{LinkLogin.Login}?returnUrl={returnUrl}", true);
		}

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0} WeekCount:{1}", nameof(Index) + "!" + nameof(OnInitialized), WeekCount));
		await PopulateShabbatWeek();
	}

	#region Shabbat Week Lookup
	private int WeekCount = 2;

	protected int MaxShabbatWeekId;
	protected DateTime MaxShabbatDate;

	protected int MinShabbatWeekId;
	protected DateTime MinShabbatDate;

	private async Task PopulateShabbatWeek()
	{
		Logger.LogDebug(string.Format("Inside {0}; WeekCount:{1}", nameof(Index) + "!" + nameof(PopulateShabbatWeek), WeekCount));
		Tuple<int, DateTime, int, DateTime> ShabbatWeeksTuple;

		try
		{
			ShabbatWeeksTuple = await db.GetShabbatWeekLookup(WeekCount);

			if (ShabbatWeeksTuple is not null)
			{
				MaxShabbatWeekId = ShabbatWeeksTuple.Item1;
				MaxShabbatDate = ShabbatWeeksTuple.Item2;
				MinShabbatWeekId = ShabbatWeeksTuple.Item3;
				MinShabbatDate = ShabbatWeeksTuple.Item4;
				Logger.LogDebug(string.Format("...MaxShabbatWeekId:{0}/MinShabbatWeekId:{1}", MaxShabbatWeekId, MinShabbatWeekId));
			}
			else
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = $"{nameof(ShabbatWeeksTuple)} NOT FOUND";
			}

		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
	}
	#endregion

	#region ErrorHandling
	private void InitializeErrorHandling()
	{
		DatabaseInformationMsg = "";
		DatabaseInformation = false;
		DatabaseWarningMsg = "";
		DatabaseWarning = false;
		DatabaseErrorMsg = "";
		DatabaseError = false;
	}

	protected bool DatabaseInformation = false;
	protected string DatabaseInformationMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }
	protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
	protected string DatabaseErrorMsg { get; set; }
	#endregion


}
