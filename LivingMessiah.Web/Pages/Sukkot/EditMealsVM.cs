using SukkotApi.Domain;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public class EditMealsVM
	{
		public string Title { get; set; }
		public string FamilyName { get; set; }
		public int RegistrationId { get; set; }
		public int StatusId { get; set; }

		public int AdultCount { get; set; }
		public int ChildBigCount { get; set; }
		public int ChildSmallCount { get; set; }

		public Meal MealAdult { get; set; }
		public Meal MealChildBig { get; set; }
		public Meal MealChildSmall { get; set; }

		//Vegetarian
		public Meal MealAdultVeg { get; set; }
		public Meal MealChildBigVeg { get; set; }
		public Meal MealChildSmallVeg { get; set; }

		public decimal MealRateAdult { get; set; }
		public decimal MealRateChildBig { get; set; }

		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal MealTotalCostAdult { get; set; }

		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal MealTotalCostChildBig { get; set; }
	}
}

