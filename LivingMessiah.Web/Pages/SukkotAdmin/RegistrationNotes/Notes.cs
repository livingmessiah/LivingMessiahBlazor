using LivingMessiah.Web.Infrastructure;
using System;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes;

// Rename NotesQuery
public class Notes
{
	public int Id { get; set; }
	public string? FirstName { get; set; }
	public string? FamilyName { get; set; }
	public string? Phone { get; set; }
	public string? EMail { get; set; }
	public string? AdminNotes { get; set; }
	public string? UserNotes { get; set; }

	public bool HasAdminNotes => !String.IsNullOrEmpty(AdminNotes);
	public bool HasUserNotes => !String.IsNullOrEmpty(UserNotes);
	public string PhoneNumber => (Phone ?? "").PhoneNumber();
}
