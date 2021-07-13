using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Domain.KeyDates.Queries
{
	public class CalendarYear
	{
		public int Year { get; set; }
		public string ShortDescr { get; set; }
		public string ShortDescrHebrew { get; set; }
		public bool IsPregnant { get; set; }
		public List<CalendarEntry> CalendarEntrys { get; set; } = new List<CalendarEntry>();
		public List<FeastDay> FeastDays { get; set; } = new List<FeastDay>();
		public List<LunarMonth> LunarMonths { get; set; } = new List<LunarMonth>();
		//public List<Season> Seasons { get; set; } = new List<Season>();
		public List<SeasonOrEquinox> SeasonOrEqinoxList { get; set; } = new List<SeasonOrEquinox>();

		public override string ToString()
		{
			return $@"
Year: {Year}
, CalendarEntrys.Count: {CalendarEntrys.Count()}
, FeastDays.Count: {FeastDays.Count()}
, LunarMonths.Count: {LunarMonths.Count()}
, SeasonOrEqinoxList.Count: {SeasonOrEqinoxList.Count()}";
		}
	}
}

/*
, Seasons.Count: {Seasons.Count()}
, FeastDayDetails.Count: {FeastDayDetails.Count()}
*/
