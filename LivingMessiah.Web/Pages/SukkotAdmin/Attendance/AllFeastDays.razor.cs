using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;
using SukkotApi.Domain;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Attendance;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class AllFeastDays
{
		[Inject]
		public ILogger<AllFeastDays> Logger { get; set; }

		[Inject]
		public ISukkotAdminRepository db { get; set; }

		[Inject]
		NavigationManager NavManager { get; set; }

		public string ExceptionMessage { get; set; }

		public List<vwAttendanceAllFeastDays> AttendanceAllFeastDaysList { get; set; }
		public vwAttendancePeopleSummary AttendancePeopleSummary { get; set; }

		public int gtPeeps { get; set; } = 0;

		protected override async Task OnInitializedAsync()
		{
				try
				{
						//ToDo figure out how to do multiple calls using Dapper
						Logger.LogInformation($"Calling {nameof(AllFeastDays)}!{nameof(db.GetAttendanceAllFeastDays)}");
						AttendanceAllFeastDaysList = await db.GetAttendanceAllFeastDays();

						Logger.LogInformation($"Calling {nameof(AllFeastDays)}!{nameof(db.GetAttendancePeopleSummary)}");
						AttendancePeopleSummary = await db.GetAttendancePeopleSummary();
				}
				catch (Exception ex)
				{
						ExceptionMessage = $"Inside {nameof(AllFeastDays)}!{nameof(OnInitializedAsync)}, <br><br> {ex.Message}";
						Logger.LogError(ex, ExceptionMessage);
						NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
				}
		}

}
