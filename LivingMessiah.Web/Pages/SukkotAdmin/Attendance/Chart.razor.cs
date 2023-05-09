using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.SukkotAdmin.Attendance.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Data;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Attendance;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class Chart
{
	[Inject] public ILogger<Chart>? Logger { get; set; }
	[Inject] public ISukkotAdminRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public List<vwAttendanceChart>? AttendanceChartList { get; set; }
	public List<string> XLabelAges { get; private set; } = new List<string> { "Adults", "ChildBig", "ChildSmall" };
	public List<string> YLabelDays { get; private set; } = new List<string> { "Tue 19", "Wed 20", "Thu 21", "Fri 22", "Sat 23", "Sun 24", "Mon 25", "Tue 26", "Wed 27", "Thu 28" };
	public List<Column>? Columns { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Chart) + "!" + nameof(OnInitializedAsync)));
		try
		{
			AttendanceChartList = await db!.GetAttendanceChart();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error reading database");
			Toast!.ShowError("Error reading database");

		}
		Load();

	}

	public void Load()
	{
		Columns = new List<Column>();
		List<ColumnPart>? columnParts = new List<ColumnPart>();

		string prevFeastDay2 = "";
		foreach (var item in AttendanceChartList!)
		{
			if (prevFeastDay2 == item.FeastDay2 | prevFeastDay2 == "")
			{
				columnParts.Add(new ColumnPart() { DimensionOne = item.AgeDesc, Days = item.Days });
			}
			else
			{
				Columns.Add(new Column { StackedDimensionOne = prevFeastDay2, ColumnParts = columnParts });
				columnParts = null; //columnParts.Clear();
				columnParts = new List<ColumnPart>();
				columnParts.Add(new ColumnPart() { DimensionOne = item.AgeDesc, Days = item.Days });
			}
			prevFeastDay2 = item.FeastDay2!;
		}
		Columns.Add(new Column { StackedDimensionOne = prevFeastDay2, ColumnParts = columnParts });
	}

}

