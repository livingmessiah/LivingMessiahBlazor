using System;

namespace LivingMessiah.Web.Pages.Admin.WirecastFolder;

public class Download
{
	public bool Selected { get; set; }
	public int ZeroBasedRowCnt { get; set; }
	public int Id { get; set; }
	public string? FamilyName { get; set; }
	public string? FirstName { get; set; }
	public string? SpouseName { get; set; }
	public string? EMail { get; set; }
	//public string? Phone { get; set; }
	public int StatusId { get; set; }
	public Decimal TotalDonation { get; set; }
	public Decimal RegistrationFee { get; set; }
}
