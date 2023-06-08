using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;

//ToDo Delete, use LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.HouseRulesAgreement.FormVM
public class HouseRulesAgreementVM
{
	[Required]
	[MaxLength(75)]
	[DataType(DataType.EmailAddress)]
	[DisplayName("eMail")]
	public string? EMail { get; set; }

}

