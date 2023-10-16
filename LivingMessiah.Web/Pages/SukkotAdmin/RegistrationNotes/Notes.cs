using LivingMessiah.Web.Infrastructure;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes;

public class Notes
{
	public int Id { get; set; }
	public string? FirstName { get; set; }
	public string? FamilyName { get; set; }
	public string? Phone { get; set; }
	public string? EMail { get; set; }
	public string? AdminNotes { get; set; }
	public string? UserNotes { get; set; }

	public string PhoneNumber
	{
		get
		{
			return (Phone ?? "").PhoneNumber();
		}
	}


}
