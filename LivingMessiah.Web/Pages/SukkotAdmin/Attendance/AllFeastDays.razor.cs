using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.SukkotAdmin.Data; 
using LivingMessiah.Web.Pages.SukkotAdmin.Attendance.Domain;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Attendance;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class AllFeastDays
{
	[Inject] public ILogger<AllFeastDays>? Logger { get; set; }
	[Inject] public ISukkotAdminRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public List<vwAttendanceAllFeastDays>? AttendanceAllFeastDaysList { get; set; }
	public vwAttendancePeopleSummary? AttendancePeopleSummary { get; set; }

	public int gtPeeps { get; set; } = 0;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			Logger!.LogDebug(string.Format("Inside {0}"
				, nameof(AllFeastDays) + "!" + nameof(OnInitializedAsync)));
			
			//ToDo figure out how to do multiple calls using Dapper

			Logger!.LogDebug(string.Format("...call db.{0}", nameof(db.GetAttendanceAllFeastDays)));
			AttendanceAllFeastDaysList = await db!.GetAttendanceAllFeastDays();

			Logger!.LogDebug(string.Format("...call db.{0}", nameof(db.GetAttendancePeopleSummary)));
			AttendancePeopleSummary = await db!.GetAttendancePeopleSummary();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error reading database");
			Toast!.ShowError("Error reading database");
		}
	}

}