using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain
{
	public class MealPlan
	{
		[Required]
		[Key]
		public int Id { get; set; }

		/*
		public int MealDateId { get; set; }
		public int MealTimeId { get; set; }
		public int MealTypeId { get; set; }
		*/

		[Required]
		[MaxLength(255)]
		public string Menu { get; set; }
	}
}

