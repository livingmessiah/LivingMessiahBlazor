using LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Domain;
using System;
using System.Linq;

namespace LivingMessiah.Web.Pages.Sukkot.Data;

public static class DTOHelper
{
	public static string? Scrub(string? notes)
	{
		if (!string.IsNullOrEmpty(notes))
		{
			return notes.Replace("\"", string.Empty).Replace("'", string.Empty);
		}
		else
		{
			return string.Empty;
		}
	}

	public static string? DumpAttendanceDates(DateTime[]? dtArray)
	{
		return dtArray is not null ?
			string.Join(", ", dtArray.Select(date => date.ToString("yyyy-MM-dd")))
			: "";
	}
}

