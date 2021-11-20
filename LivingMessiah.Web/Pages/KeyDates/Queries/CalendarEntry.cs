using System;
using LivingMessiah.Web.Pages.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.KeyDates.Queries
{
	public class CalendarEntry
	{
		public int YearId { get; set; }
		public int CalendarTemplateId { get; set; } //Id
		public DateTime Date { get; set; }
		public int Detail { get; set; }
		public string EventDescr { get; set; }
		//public int DateTypeId { get; set; }
		public LivingMessiah.Web.Pages.KeyDates.Enums.DateTypeEnum DateTypeEnum { get; set; }
		public BaseFeastDaySmartEnum FeastDaySmartEnum { get; set; }
		//public List<BaseFeastDaySmartEnum> FeastDaySmartEnums { get; set; }

		public string TypeDescr { get; set; }  // KeyDate.DateType dt dt.Descr AS TypeDescr
		//public string DateTypeDescr
		//{
		//	get
		//	{
		//		return BaseDateTypeSmartEnum.FromValue(DateTypeId).Name; // Month, Feast or Season
		//	}
		//}

		public override string ToString()
		{
			return $@" @CalendarTemplateId:{@CalendarTemplateId}; YearId:{YearId}, Date: {Date.ToString("yyyy-MM-dd")}";
		}

	}
}

//public string ShortDescr { get; set; }
//public string ShortDescrHebrew { get; set; }
//public bool IsPregnant { get; set; }
