using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

//using LivingMessiah.Web.Services;
using LivingMessiah.Domain.KeyDates.Enums;
using LivingMessiah.Web.Pages.KeyDate.Enums;
//using LivingMessiah.Domain.KeyDates.Queries;
using Domain = LivingMessiah.Web.Pages.KeyDate.Domain;

using LivingMessiah.Web.Pages.KeyDate.Data;
using LivingMessiah.Web.Pages.KeyDate.Domain;

using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.KeyDate
{
	public partial class Grid
	{
		[Inject]
		public IKeyDateRepository db { get; set; }

		[Inject]
		public ILogger<Grid> Logger { get; set; }

		//[Parameter]
		public RelativeYearEnum RelativeYear { get; set; } = RelativeYearEnum.Current;

		protected CalendarYear CalendarYear;
		protected List<CalendarEntry> CalendarEntries;

		private SfGrid<CalendarEntry> sfGrid;

		//protected List<FeastDay> FeastDays;
		//protected List<LunarMonth> LunarMonths;
		//protected List<Season> Seasons;

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(Grid)}!{nameof(OnInitializedAsync)}");
			try
			{
				CalendarYear = await db.GetHebrewYearAndChildren(RelativeYear);

				if (CalendarYear == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "Year NOT FOUND";
				}
				else
				{
					CalendarEntries = CalendarYear.CalendarEntrys.ToList();
					/*
					LunarMonths = CalendarYear.LunarMonths.Where(w => w.YearId == CalendarYear.Year).ToList();
					Seasons = CalendarYear.Seasons.Where(w => w.YearId == CalendarYear.Year).ToList();
					FeastDays = CalendarYear.FeastDays.Where(w => w.YearId == CalendarYear.Year).ToList();
					*/
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
			//StateHasChanged();
		}

		/*
		Seasons.razor 
		  <td class="h3"><span class="badge @context.BadgeColor"><i class="@context.Icon"></i> @context.Name</span></td>
		*/

		public void CustomizeCell(QueryCellInfoEventArgs<CalendarEntry> args)
		{
			if (args.Column.Field == nameof(CalendarEntry.DateTypeId))
			{
				//BaseDateTypeSmartEnum e = BaseDateTypeSmartEnum.FromName(args.Data.LocationName, false);
				BaseDateTypeSmartEnum e = BaseDateTypeSmartEnum.FromValue(args.Data.DateTypeId);
				args.Cell.AddClass(new string[] { e.TextColor });
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
}
