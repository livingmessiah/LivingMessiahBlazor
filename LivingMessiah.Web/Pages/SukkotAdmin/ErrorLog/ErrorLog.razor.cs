using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;
using LivingMessiah.Web.Pages.SukkotAdmin.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.ErrorLog.Domain;

namespace LivingMessiah.Web.Pages.SukkotAdmin.ErrorLog;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class ErrorLog
{

	[Inject]
	public ILogger<ErrorLog> Logger { get; set; }

	[Inject]
	public ISukkotAdminRepository db { get; set; }

	public int AffectedRows { get; set; } = 0;
	public List<zvwErrorLog> ErrorLogs { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(ErrorLog) + "!" + nameof(OnInitializedAsync)));
		try
		{
			ErrorLogs = await db.GetzvwErrorLog();
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
	}

	public async Task LogErrorTest_ButtonClick()
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(ErrorLog) + "!" + nameof(LogErrorTest_ButtonClick)));
		try
		{
			AffectedRows = await db.LogErrorTest();
			ErrorLogs = await db.GetzvwErrorLog();
			StateHasChanged();
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error calling LogErrorTest or  GetzvwErrorLog";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}

	}

	public async Task EmptyErrorLog_ButtonClick()
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(ErrorLog) + "!" + nameof(EmptyErrorLog_ButtonClick)));
		try
		{
			AffectedRows = await db.EmptyErrorLog();
			ErrorLogs = await db.GetzvwErrorLog();
			StateHasChanged();
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error calling EmptyErrorLog or  GetzvwErrorLog";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
	}

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
