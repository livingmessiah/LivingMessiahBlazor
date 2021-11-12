using LivingMessiah.Web.Pages.KeyDate.Enums;
using System;

namespace LivingMessiah.Web.Pages.KeyDate.Domain
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

		public string NewYear
		{
			get
			{
				return RowCntByGregorianYear == 1 ? YearId.ToString() : "";
			}
		}

		public string DateTypeDescr
		{
			get
			{
				return BaseDateTypeSmartEnum.FromValue(DateTypeId).Name;
			}
		}

		public override string ToString()
		{
			return $@"Id Range: {DateIdEnd}-{DateIdEnd}, YearId: {YearId}, Date: {Date}, GregorianYear: {GregorianYear}, DateTypeId: {DateTypeId}";
		}
	}
}
