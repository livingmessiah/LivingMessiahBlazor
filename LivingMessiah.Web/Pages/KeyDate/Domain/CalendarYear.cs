using System.Linq;
using System.Collections.Generic;


namespace LivingMessiah.Web.Pages.KeyDate.Domain
{
	public class CalendarYear
	{
		public int Year { get; set; }
		public string ShortDescr { get; set; }
		public string ShortDescrHebrew { get; set; }
		public bool IsPregnant { get; set; }
		public List<CalendarEntryDateRange> CalendarEntryDateRanges { get; set; } = new List<CalendarEntryDateRange>();
		/*
		public List<FeastDay> FeastDays { get; set; } = new List<FeastDay>();
		public List<LunarMonth> LunarMonths { get; set; } = new List<LunarMonth>();
		public List<Season> Seasons { get; set; } = new List<Season>();
		*/
		/*
		public override string ToString()
		{
			return $@"
Year: {Year}
, CalendarEntrys.Count: {CalendarEntrys.Count}
, FeastDays.Count: {FeastDays.Count}
, LunarMonths.Count: {LunarMonths.Count}
, SeasonList.Count: {Seasons.Count}";
		}
		*/
	}
}

/*

, Seasons.Count: {Seasons.Count()}
, FeastDayDetails.Count: {FeastDayDetails.Count()}
*/