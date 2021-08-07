using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;
using SukkotApi.Domain;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Lodging
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class PivotOnCampCode
	{
		[Inject]
		public ILogger<PivotOnCampCode> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		[Inject]
		NavigationManager NavManager { get; set; }

		public string ExceptionMessage { get; set; }

		public List<vwLodgingDaysPivotOnCampCode> LodgingDaysPivotOnCampCodeList { get; set; }
		public int OffsiteCount { get; set; } = 0;

		int tent = 0;
		int cabin = 0;
		int cabPeep = 0;
		int rvH = 0;
		int rvD = 0;
		int gtLodgers = 0;
		decimal gtCost = 0;

		protected override async Task OnInitializedAsync()
		{
			try
			{
				Logger.LogDebug($"Inside: {nameof(PivotOnCampCode)}!{nameof(OnInitializedAsync)}, calling {nameof(db.GetvwLodgingDaysPivotOnCampCode)}");
				LodgingDaysPivotOnCampCodeList = await db.GetvwLodgingDaysPivotOnCampCode();
				OffsiteCount = await db.GetOffsiteCount();
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"{ex.Message}";
				Logger.LogError(ex, ExceptionMessage);
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
		}
	}

}
