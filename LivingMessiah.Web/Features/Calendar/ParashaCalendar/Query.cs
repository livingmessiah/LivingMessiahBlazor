using System;

namespace LivingMessiah.Web.Features.Calendar.ParashaCalendar;

public class Query
{
	public DateTime ShabbatDate { get; set; }
	public string? Torah { get; set; }
	public string? Haftorah { get; set; }
	public string? Brit { get; set; }

	public string ShabbatDateMD
	{
		get
		{
			return ShabbatDate.ToString("MMMM dd");
		}
	}
}

/*
	  
If you use a nullable DateTime you have to reference the dates Value
	public DateTime? ShabbatDate { get; set; }
	public string ShabbatDateMD 	{	get	{ return ShabbatDate!.Value.ToString("MMM dd");	} }
	
	*/

// Ignore Spelling: Shabbat Haftorah
