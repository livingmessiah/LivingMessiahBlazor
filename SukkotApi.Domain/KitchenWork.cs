using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SukkotApi.Domain
{
	public class KitchenWork
	{
		[Required]
		public int Id { get; set; }

		[DisplayName("RegistrationId")]
		[Required]
		public int RegistrationId { get; set; }

//		[Required]
//		public int MealPlanId { get; set; }

//		[Required]
//		public int MealTimeDateId { get; set; }

		//[Required]
		public string MealDay { get; set; }

		//[Required]
		public string MealTime { get; set; }

		[Required]
		public int KitchenWorkTypeId { get; set; }

		[Required]
		[MaxLength(50)]
		public String Volunteer { get; set; }


	}

}

