using System;

namespace LivingMessiah.Web.Pages.Calendar;

public class PlannerQuery
{
	public DateTime Date { get; set; }
	public int Detail { get; set; }
	public int DateTypeId { get; set; }
	public string? Description { get; set; }
	public int EnumId { get; set; }
	public Enums.FeastDay? FeastDay { get; set; }

	public override string ToString()
	{
		return $" Date: {Date.ToString("yyyy-MM-dd")}, Detail: {Detail}, DateTypeId: {DateTypeId}, EnumId: {EnumId}, Description: {Description ?? "null"}";
	}
}
