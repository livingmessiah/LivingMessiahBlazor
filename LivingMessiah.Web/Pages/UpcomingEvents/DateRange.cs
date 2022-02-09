using System;

namespace LivingMessiah.Web.Pages.UpcomingEvents;

// ToDo: not being used
// 
public class DateRange
{
		public DateTime? BeginDate { get; set; }
		public DateTime? EndDate { get; set; }
		public DateRange(DateTime x, DateTime y) => (BeginDate, EndDate) = (x, y);
}
