using System;

namespace LivingMessiah.Web.Pages.KeyDates.Queries
{
	public class AppointmentData
	{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Nullable<bool> IsAllDay { get; set; }
    public string CategoryColor { get; set; }
    public string RecurrenceRule { get; set; }
    public Nullable<int> RecurrenceID { get; set; }
    public string RecurrenceException { get; set; }
    public string StartTimezone { get; set; }
    public string EndTimezone { get; set; }
  }
}
