using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain
{
	public class RegistrationLookup
	{
		public string ID { get; set; }
		[Required(ErrorMessage = "The name field is required.")]
		public string Text { get; set; }
	}
}
