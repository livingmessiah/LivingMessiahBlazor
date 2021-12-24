using System;
using LivingMessiah.Web.Pages.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.KeyDates.Queries
{
	public class CalendarEntry
	{
		//public int YearId { get; set; }
		public int CalendarTemplateId { get; set; } //Id
		public DateTime Date { get; set; }
		public int Detail { get; set; }
		public string Descr { get; set; }   //CalendarTemplateId.Descr
		public DateTypeEnum DateTypeEnum { get; set; }  // 1:Month; 2:Feast; 3:Season; CalendarTemplate.DateTypeId AS DateTypeEnum

		//public string TypeDescr { get; set; }  // KeyDate.DateType dt dt.Descr AS TypeDescr

		public override string ToString()
		{
			return $" Date: {Date.ToString("yyyy-MM-dd")}, Detail: {Detail}, Descr: {Descr}, DateTypeEnum: {DateTypeEnum}";
			//return $@" @CalendarTemplateId:{@CalendarTemplateId}; YearId:{YearId}, Date: {Date.ToString("yyyy-MM-dd")}";
		}

	}
}
