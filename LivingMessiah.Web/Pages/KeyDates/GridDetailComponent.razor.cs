using System;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.KeyDates.Queries;
using System.Collections.Generic;
using LivingMessiah.Web.Pages.KeyDates.Enums;

using System.Linq;


namespace LivingMessiah.Web.Pages.KeyDates
{
	public partial class GridDetailComponent
	{
		[Parameter]
		public DateTypeEnum DateTypeEnum { get; set; }

		[Parameter]
		public int Detail { get; set; }

		[Parameter]
		public DateTime Date { get; set; }

		// ToDo: is FDD is converted, to this BaseLunarMonthSmartEnum.List.OrderBy(o => o.Value).ToList()">
		protected List<FDD> PassoverDetails => FDD.All.Where(w => w.Id <= 4).OrderBy(o => o.Id).ToList();
		protected List<FDD> SukkotDetails => FDD.All.Where(w => w.Id > 4).OrderBy(o => o.Id).ToList();


		protected string Notes(string addedDaysDescr, int? addedDays) 
		{
			if (addedDaysDescr == "" || addedDays is null)
			{
				return "";
			}
			else
			{
				double d = (double)addedDays;
				return addedDaysDescr + " " + Date.AddDays(d).ToString(DateFormat.ddd_mm_dd);
			}
		}

		protected string NotesFDD(int addedDays)
		{
			double d = (double)addedDays;
			return Date.AddDays(d).ToString(DateFormat.ddd_mm_dd);
		}

		/*
		 * ddd, MMMM dd/yyyy
		 * 
		return addedDaysDescr + " " + Date.AddDays(d).ToShortDateString();		
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
		*/

	}
}

