using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes;

public class TypeaheadEditFormVM
{
	[Required]
	public Notes? SelectedNote { get; set; }
}


