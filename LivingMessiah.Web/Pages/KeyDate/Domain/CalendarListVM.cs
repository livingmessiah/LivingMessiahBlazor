using System;

namespace LivingMessiah.Web.Pages.KeyDate.Domain
{
	public class CalendarListVM
	{
		public int YearId { get; set; }
		public int CalendarTemplateId { get; set; }
		public DateTime Date { get; set; }
		//[Helper]             NVARCHAR (255) NULL

		public override string ToString()
		{
			return $@"
Year: {YearId}
, CalendarTemplateId: {CalendarTemplateId}
, Date: {Date.ToString("yyyy-MM-dd")}";
		}
	}

}
