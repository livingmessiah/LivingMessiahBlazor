using System;
using LivingMessiah.Web.Pages.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.KeyDates.Queries
{
	public class DateUnion
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public DateTypeEnum DateTypeEnum { get; set; } 
		public string Descr { get; set; }
	}
}
