using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	public class DonationInsertModel
	{
		public int RegistrationId { get; set; }  // Hidden field
		public string FamilyName { get; set; }  // Hidden field

		[Required(ErrorMessage = "An amount is required")]
		[Range(0, 2000, ErrorMessage = "{0} numbers must range between {1} and {2}")]
		[DataType(DataType.Currency)]
		public decimal Amount { get; set; }

		[MaxLength(255)]
		public String Notes { get; set; }

		[MaxLength(100)]
		[Required(ErrorMessage = "Reference is required")]
		[DisplayName("Reference Id")]
		public String ReferenceId { get; set; }

		[DisplayName("Create Date")]
		[DataType(DataType.Date)]
		[Required]
		public DateTime CreateDate { get; set; }
	}
}
/*
Notes about columns omitted
- public int Id { get; set; } // This Identity field is not included because the class is setup for inserting only
- public int Detail { get; set; } Not needed as this is handled on the back-end by stpDonationInsert
- public string CreatedBy { get; set; } Handled by the DTO before sent to 
*/
