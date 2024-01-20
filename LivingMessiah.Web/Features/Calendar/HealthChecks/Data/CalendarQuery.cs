using System;

namespace LivingMessiah.Web.Features.Calendar.HealthChecks.Data;

public class CalendarQuery
{
	public int YearId { get; set; }
	public DateTime Date { get; set; }
	public string? DateYMD { get; set; }
	public int DateTypeId { get; set; }			// 1:1 SmartEnum<DateType>	(Features.Calendar.Enums)
	public int EnumId { get; set; }         // 1:1 SmartEnum<FeastDay>	(Features.Calendar.Enums)
	public string? TypeDescr { get; set; }	//dt.Descr AS TypeDescr; e.g. Feast, Month, Season
	public string? EventDescr { get; set; } //c.Description AS EventDescr; e.g. Passover, Sivan | 3rd, Summer

	// FROM KeyDate.Calendar c INNER JOIN KeyDate.DateType dt ON c.DateTypeId = dt.Id
	// public string DateMDY { get	{	return ShabbatDate.ToString("MMMM dd");	}	}

	/*
	public DateOnly Date { get; set; } 
	
	- https://stackoverflow.com/questions/72118892/how-to-convert-system-datetime-to-system-dateonly


	public static DateOnly ToDateOnly(this DateTime datetime) 
    => DateOnly.FromDateTime(datetime);
	*/
}


// Ignore Spelling: DateYMD, Descr, Sivan

