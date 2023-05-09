using System;

namespace LivingMessiah.Web.Pages.KeyDates.Queries;

public class CalendarEntry
{
	public int CalendarTemplateId { get; set; } //Id
	public DateTime Date { get; set; }
	public string? Descr { get; set; }   //CalendarTemplateId.Descr
	public Enums.DateTypeEnum DateTypeEnum { get; set; }  // 1:Month; 2:Feast; 3:Season; CalendarTemplate.DateTypeId AS DateTypeEnum

	/*
	public override string ToString()
	{
		return $" Date: {Date.ToString("yyyy-MM-dd")}, Id: {Id}, Detail: {Detail}, Descr: {Descr}";
	}
	*/
}
