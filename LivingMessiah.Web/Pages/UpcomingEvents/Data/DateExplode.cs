using LivingMessiah.Web.Pages.KeyDates.Enums;
using System;

namespace LivingMessiah.Web.Pages.UpcomingEvents.Queries
{
	public class DateExplode
	{
		
		public int Year { get; set; }
		public DateTime Date { get; set; }
		public int GregorianYear { get; set; }
		public DateTypeEnum DateTypeEnum { get; set; }
		public int DateTypeEnumId { get; set; }
		/*
		 public int Id { get; set; }
		DateYMD
		public int RowCntByGregorianYear { get; set; }
		IsDateTypeContiguous	
		DateTypeId	
		DateType	
		public string DateTypeValue { get; set; }
			



		*/
		public int DateTypeId { get; set; }
	}
}
