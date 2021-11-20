using System;

namespace LivingMessiah.Web.Pages.KeyDates.Queries
{
	public class CalendarEntryDateRange
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

		public string NewYear
		{
			get
			{
				return RowCntByGregorianYear == 1 ? Date.ToString("yyyy") : "";
			}
		}

		//public DateType DateType { get { return (DateType)DateTypeId; } }

	}
}

