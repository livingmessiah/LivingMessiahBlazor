using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain.KeyDates.Enums;
using LivingMessiah.Domain.KeyDates.Queries;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.KeyDate
{
	public partial class HebrewYear
	{
		[Inject]
		public IUpcomingEventService Svc { get; set; }

		[Inject]
		public ILogger<HebrewYear> Logger { get; set; }

		[Parameter]
		public RelativeYearEnum RelativeYear { get; set; }

		[Parameter]
		public bool IsXsOrSm { get; set; }

		[Parameter]
		public bool WithMonths { get; set; }

		protected CalendarYear CalendarYear;
		protected List<CalendarEntry> CalendarEntries;
		
		protected List<FeastDay> FeastDays;
		protected List<LunarMonth> LunarMonths;
		protected List<Season> Seasons;
	
		protected bool LoadFailed = false;
		protected string LoadFailedMessasge = "";

		protected override async Task OnInitializedAsync()
		{
			string msg0 = $"Inside { nameof(HebrewYear)}!{ nameof(OnInitializedAsync)}; RelativeYear: {RelativeYear} ";
			try
			{
				LoadFailed = false;
				CalendarYear = await Svc.GetHebrewYearAndChildren(RelativeYear);

				if (CalendarYear==null)
				{
					LoadFailed = true;
					LoadFailedMessasge = "Year NOT FOUND";
					Logger.LogWarning($@"{msg0} after calling {nameof(Svc.GetHebrewYearAndChildren)}; {LoadFailedMessasge}");
				}
				else
				{
					CalendarEntries = CalendarYear.CalendarEntrys.ToList();
					LunarMonths = CalendarYear.LunarMonths.Where(w => w.YearId == CalendarYear.Year).ToList();
					Seasons = CalendarYear.Seasons.Where(w => w.YearId == CalendarYear.Year).ToList();
					FeastDays = CalendarYear.FeastDays.Where(w => w.YearId == CalendarYear.Year).ToList();

					//string debug = $@"{msg0} Year.ToString(): {Year}";
					//Logger.LogDebug($"{debug}");
				}
			}

			catch (System.Exception ex)
			{
				LoadFailed = true;
				Logger.LogError(ex, $"<br /><br /> {nameof(OnInitializedAsync)}");
			}

		}

	}
}

