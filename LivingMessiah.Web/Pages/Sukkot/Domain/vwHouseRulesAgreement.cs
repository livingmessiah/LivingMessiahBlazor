using System;

namespace LivingMessiah.Web.Pages.Sukkot.Domain;

public class vwHouseRulesAgreement
{
	public int Id { get; set; }
	public string EMail { get; set; }
	public string TimeZone { get; set; }
	public DateTimeOffset AcceptedDate { get; set; }
}
