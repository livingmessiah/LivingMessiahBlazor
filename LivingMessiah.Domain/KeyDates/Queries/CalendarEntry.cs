using System;

namespace LivingMessiah.Domain.KeyDates.Queries
{
	public class CalendarEntry
	{
		public DateTime Date { get; set; }
		public int GregorianYear { get; set; }
		public int YearId { get; set; }
		public int DateTypeId { get; set; }  // Enums/DateType.cs
		public int DateIdBeg { get; set; }
		public int DateIdEnd { get; set; }
		public int RowCntByGregorianYear { get; set; }
		
		public override string ToString()
		{
			return $@"Id Range: {DateIdEnd}-{DateIdEnd}, YearId: {YearId}, Date: {Date}, GregorianYear: {GregorianYear}, DateTypeId: {DateTypeId}";
		}

		//public DateType DateType { get { return (DateType)DateTypeId; } }

	}
}


