using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SukkotApi.Domain;

public class MealTicketPunch
{
		[Required]
		public int RegistrationId { get; set; }

		[Required]
		public MealTypeEnum MealTypeEnum { get; set; }

		[Required]
		public AgeEnum AgeEnum { get; set; }

}
/*
		[Required]
		[Key]
		public int Id { get; set; }

	[Required]
		[MaxLength(1)]
		public string MealType { get; set; } //  (R)egular or (V)egetarian

 */
