using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes;

public class TypeaheadVM
{
	[Required]
	public Notes? SelectedNote { get; set; }
}


