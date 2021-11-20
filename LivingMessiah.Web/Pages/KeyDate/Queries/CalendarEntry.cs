using System;

namespace LivingMessiah.Web.Pages.KeyDates.Queries
{
	public class CalendarEntry
	{
		public DateTime Date { get; set; }
		public int GregorianYear { get; set; }
		public int YearId { get; set; }
		public int DateTypeId { get; set; }  // Enums/DateType.cs
		public int DateIdBeg { get; set; }
		public int DateIdEnd { get; set; }
		public int RowCntByGregorianYear { get; set; }
		
		public override string ToString()
		{
			return $@"Id Range: {DateIdEnd}-{DateIdEnd}, YearId: {YearId}, Date: {Date}, GregorianYear: {GregorianYear}, DateTypeId: {DateTypeId}";
		}

		//public DateType DateType { get { return (DateType)DateTypeId; } }

	}
}

/*
Used by 
 LivingMessiah.Web\Pages\KeyDate\HebrewYear.razor.cs(30):protected List<CalendarEntry> CalendarEntries;
 LivingMessiah.Web\Pages\KeyDate\Queries\CalendarYear.cs(12):public List<CalendarEntry> CalendarEntrys { get; set; } = new List<CalendarEntry>(); {twice}
 LivingMessiah.Web\Pages\KeyDate\Year.razor.cs(16):public List<CalendarEntry> CalendarEntries { get; set; }
 LivingMessiah.Web\Pages\UpcomingEvents\Data\UpcomingEventsRepository.cs(122):calendarYear.CalendarEntrys = (await multi.ReadAsync<CalendarEntry>()).ToList();   // #2
*/
