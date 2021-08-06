using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;
using SukkotApi.Data;
using SukkotApi.Domain;

namespace LivingMessiah.Web.Pages.SukkotAdmin.ErrorLog
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class ErrorLog
	{

		[Inject]
		public ILogger<ErrorLog> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		[Inject]
		NavigationManager NavManager { get; set; }

		public int AffectedRows { get; set; } = 0;
		public List<zvwErrorLog> ErrorLogs { get; set; }

		protected override async Task OnInitializedAsync()
		{
			try
			{
				ErrorLogs = await db.GetzvwErrorLog();
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, $"Inside {nameof(OnInitializedAsync)}, {nameof(db.GetzvwErrorLog)}");
				//ExceptionMessage = ex.Message ?? "";
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
		}

		public async Task LogErrorTest_ButtonClick()
		{
			Logger.LogDebug($"Event: {nameof(LogErrorTest_ButtonClick)} clicked");
			try
			{
				AffectedRows = await db.LogErrorTest();
				ErrorLogs = await db.GetzvwErrorLog();
				StateHasChanged();
			}
			catch (Exception)
			{
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
			
		}

		public async Task EmptyErrorLog_ButtonClick()
		{
			try
			{
				AffectedRows = await db.EmptyErrorLog();
				ErrorLogs = await db.GetzvwErrorLog();
				StateHasChanged();
			}
			catch (Exception)
			{
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
		}

	}
}
