
using System;

namespace LivingMessiah.Web.Features.Calendar.ManageKeyDates.Queries;

public class CalendarAnalysisQuery
{
	public int YearId { get; set; }
	public DateTime Date { get; set; }
	public DateTime PrevDate { get; set; }
	public string? EventDescr { get; set; }
	public int DateTypeId { get; set; } // 1:Month; 2:Feast; 3:Season; CalendarTemplate.DateTypeId AS DateTypeEnum
	public int Detail { get; set; }
	public int EnumId { get; set; }
	public int DiffFromPrevDate { get; set; }
	public string? PrevDateYMD { get; set; }
	public string? DateYMD { get; set; }
}
/*
Ignore Spelling: Descr
Ignore Spelling: Diff
*/