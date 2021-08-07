using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;
using SukkotApi.Domain;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Sukkot;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Lodging
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class Details
	{
		[Inject]
		public ILogger<Details> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		[Inject]
		NavigationManager NavManager { get; set; }

		public decimal gtCost { get; set; } = 0;

		public string ExceptionMessage { get; set; }

		public List<vwLodgingDetail> LodgingDetails { get; set; }

		public const string DetailsFootnoteCode = "WTFSSMTWT";  //"WTFSSMTWTFSS"

		protected override async Task OnInitializedAsync()
		{
			try
			{
				Logger.LogDebug($"Inside: {nameof(Details)}!{nameof(OnInitializedAsync)}, calling {nameof(db.GetvwLodgingDetail)}");
				LodgingDetails = await db.GetvwLodgingDetail();
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"{ex.Message}";
				Logger.LogError(ex, ExceptionMessage);
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
		}

		void Edit_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
		}

		void Details_ButtonClick(int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Details + "/" + id + "/False");
		}

		public DateRangeLocal DateRangeLodging { get; set; } = DateRangeLocal.FromEnum(DateRangeEnum.LodgingDays);
		/*
														Min="@DateRangeLodging.DateRange.MinDate"
												Max="@DateRangeLodging.DateRange.MaxDate">

		public static LodgeDate FirstDay()
		{
			return All.SingleOrDefault(r => r.LodgeDateEnum == LodgeDateEnum.Day01);
		}

		public static LodgeDate LastDay()
		{
			return All.SingleOrDefault(r => r.LodgeDateEnum == lastDay);
		}
		*/
	}
}
