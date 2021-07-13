using System.Collections.Generic;
using System;

namespace LivingMessiah.Domain.KeyDates.Queries
{
	public class FeastDay
	{
		public int Id { get; set; }
		public int YearId { get; set; }
		public int DateId { get; set; }
		public string Name { get; set; }
		public string Transliteration { get; set; }
		public string Hebrew { get; set; }
		public string Details { get; set; }

		public DateTime Date { get; set; }
		public string AddDaysDescr { get; set; }
		public int AddDays { get; set; }

		//SELECT COUNT(*) FROM KeyDate.FeastDayDetail fdd	WHERE fd.Id = fdd.FeastDayId
		public int DetailCount { get; set; }

		public List<FeastDayDetail> FeastDayDetails { get; set; } = new List<FeastDayDetail>();

		// public const string ddd_mm_dd = "ddd, MM/dd";  //ddd, MM/dd/yyyy
		//<span class='float-right'>@((MarkupString)item.DateHtml)</span>
		public string DateHtml
		{
			get
			{
				if (AddDaysDescr == "" || AddDays == 0)
				{
					return "<span class='float-right'>" + Date.ToString("ddd, MM/dd") + "</span>";
				}
				else
				{
					if (AddDays < 0)
					{
						// return AddDaysDescr + " " + Date.AddDays(AddDays).ToString("ddd, MM/dd") + "<br />" + Date.ToString("ddd, MM/dd");
						return $@"
 <span class='float-right'> 
{AddDaysDescr} {Date.AddDays(AddDays):ddd, MM/dd} 
</span>
<br /> 
<span class='float-right'> 
{Date:ddd, MM/dd}
</span>
";
					}
					else
					{
						//return Date.ToString("ddd, MM/dd") + "<br />" + AddDaysDescr + Date.AddDays(AddDays).ToString("ddd, MM/dd");
						return $@"
 <span class='float-right'>
{Date:ddd, MM/dd}
</span>
<br /> 
<span class='float-right'> 
{AddDaysDescr} {Date.AddDays(AddDays):ddd, MM/dd} 
</span>
";

					}

				}
			}
		}

		public override string ToString()
		{
			return $@"Id: {Id}, YearId: {YearId}, DateId: {DateId}, Name: {Name}";
		}

	}
}
