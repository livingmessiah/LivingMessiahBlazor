using System;

namespace LivingMessiah.Domain.KeyDates.Queries
{
	public class LunarMonth
	{
		public int Id { get; set; }
		public int YearId { get; set; }
		public DateTime Date { get; set; }
		public int DateId { get; set; }
		public int EnumId { get; set; }
		public string Month { get; set; }
		public string Hebrew { get; set; }
		public string Length { get; set; }
		public string Gregorian { get; set; }
		public string BiblicalName { get; set; }
		public string BiblicalHebrew { get; set; } // http://www.yashanet.com/library/hebrew-days-and-months.html
		public string Notes { get; set; }

		public override string ToString()
		{
			return $@"Id: {Id}, YearId: {YearId}, Date: {Date}, Month: {Month}";
		}
	}
}
