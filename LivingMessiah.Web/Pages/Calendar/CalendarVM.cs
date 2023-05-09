using System;
namespace LivingMessiah.Web.Pages.Calendar;

public class CalendarVM
{
	public int Id { get; set; }
	public int CalendarTemplateId { get; set; } //Id
	public DateTime Date { get; set; }
	public int Detail { get; set; }
	public string? Descr { get; set; }   //CalendarTemplateId.Descr
	public int DateTypeId { get; set; }
	public Enums.LunarMonth? LunarMonth { get; set; }
	public Enums.FeastDay? FeastDay { get; set; }

	public override string ToString()
	{
		return $" Date: {Date.ToString("yyyy-MM-dd")}, Id: {Id}, Detail: {Detail}, DateTypeId: {DateTypeId}, Descr: {Descr}";
	}

}
