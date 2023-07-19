using System;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Data;

// Copied from Pages\SukkotAdmin\Donations\Domain\Donation.cs
public class Donation
{
	public int Id { get; set; }
	public int RegistrationId { get; set; }
	public int Detail { get; set; }
	public decimal Amount { get; set; }
	public string? Notes { get; set; }
	public string? ReferenceId { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime CreateDate { get; set; }
}
