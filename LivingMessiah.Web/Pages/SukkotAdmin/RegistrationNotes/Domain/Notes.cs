using LivingMessiah.Web.Infrastructure;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Domain;

public class Notes
{
	public int Id { get; set; }
	public string? FirstName { get; set; }
	public string? FamilyName { get; set; }
	public string? Phone { get; set; }
	public string? EMail { get; set; }
	public string? AdminOrUserNotes { get; set; }
	public string PhoneNumber
	{
		get
		{
			return StringExtensions.PhoneNumber(Phone ?? "");
		}
	}


}
