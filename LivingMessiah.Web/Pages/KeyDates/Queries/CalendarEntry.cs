using System;

namespace LivingMessiah.Web.Pages.KeyDates.Queries;

public class CalendarEntry
{
	public DateTime Date { get; set; }
	public int Detail { get; set; } //Id
	public int DateTypeId { get; set; }
	public int EnumId { get; set; }
	public string? Description { get; set; }   

	/*
	public override string ToString()
	{
		return $" Date: {Date.ToString("yyyy-MM-dd")}, Id: {Id}, Detail: {Detail}, Descr: {Descr}";
	}
	*/
}
