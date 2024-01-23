using System;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.Registrant;

public class FormVM
{
	public int Id { get; set; }
	public string? FamilyName { get; set; }
	public string? FirstName { get; set; }
	public string? SpouseName { get; set; }
	public string? OtherNames { get; set; }
	public string? EMail { get; set; }
	public string? Phone { get; set; }
	public int Adults { get; set; }
	public int ChildBig { get; set; }
	public int ChildSmall { get; set; }

	public int StatusId { get; set; } // The ManageRegistration!EntryForm needs this
	public RegistrationSteps.Enums.Status? Status { get; set; }

	public int AttendanceBitwise { get; set; } // does the VM need this?
	public DateTime[]? AttendanceDateList { get; set; }
	public DateTime[]? AttendanceDateList2ndMonth { get; set; }

	public string? Notes { get; set; }
	public string? AdminNotes { get; set; }
	public bool DidNotAttend { get; set; }
	public Decimal LmmDonation { get; set; }
}
