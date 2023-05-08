using System;

namespace LivingMessiah.Web.Pages.SukkotAdmin.ErrorLog.Domain;

public class zvwErrorLog
{
	public string? ErrorProcedure { get; set; }
	public Int32 ErrorLine { get; set; }
	public string? ErrorMessage { get; set; }
	public string? HowLongAgoHMS { get; set; }
	public Int32 ErrorLogID { get; set; }
	public string? ErrorTime2 { get; set; }
}

