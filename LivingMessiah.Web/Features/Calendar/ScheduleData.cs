using System;
using System.Collections.Generic;

namespace LivingMessiah.Web.Features.Calendar;

public class ScheduleData
{

	public static Dictionary<string, object> ApplyCategoryColor(string CategoryColor, Dictionary<string, object> Attributes, Syncfusion.Blazor.Schedule.View CurrentView)
	{
		Dictionary<string, object> attributes = new Dictionary<string, object>();
		if (CurrentView == Syncfusion.Blazor.Schedule.View.Agenda)
		{
			attributes.Add("style", "border-start-color: " + CategoryColor);
		}
		else
		{
			attributes.Add("style", "background: " + CategoryColor);
		}
		return attributes;
	}

	public class AppointmentData
	{
		public int Id { get; set; }
		public string? Subject { get; set; }
		public string? Location { get; set; }
		public string? Description { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public Nullable<bool> IsAllDay { get; set; }
		public string? CategoryColor { get; set; }  //  #1aaa55=Forest; #357cd2=Blue; #7fa900=Olive; #ea7a57=Clay; #00bdae=Turqoise; #f57f17=pumpkin
		public string? RecurrenceRule { get; set; }
		public Nullable<int> RecurrenceID { get; set; }
		public Nullable<int> FollowingID { get; set; }
		public string? RecurrenceException { get; set; }
		public string? StartTimezone { get; set; }
		public string? EndTimezone { get; set; }
	}

	public class ReadonlyEventsData : AppointmentData
	{
		public bool IsReadonly { get; set; }
	}


}
