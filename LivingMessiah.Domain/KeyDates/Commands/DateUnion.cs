using System;
using LivingMessiah.Domain.KeyDates.Enums;

namespace LivingMessiah.Domain.KeyDates.Commands
{
	public class DateUnion
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public DateTypeEnum DateTypeEnum { get; set; } 
		public string Descr { get; set; }
	}
}
