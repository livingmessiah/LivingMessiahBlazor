using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Attendance.Domain;

public class Column
{
	public string StackedDimensionOne { get; set; }
	public List<ColumnPart> ColumnParts { get; set; }
}

