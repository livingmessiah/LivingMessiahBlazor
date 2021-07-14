using System;

namespace SukkotApi.Domain
{
	public class MealsRelatedRegistrationData
	{
		public int Id { get; set; }
		public String EMail { get; set; }
		public String FamilyName { get; set; }
		public int Adults { get; set; }
		public int ChildBig { get; set; }
		public int ChildSmall { get; set; }
		public int StatusId { get; set; }
		public int AttendanceBitwise { get; set; }
		public decimal MealTotalCostAdult { get; set; }
		public decimal MealTotalCostChildBig { get; set; }

		public decimal MealRateAdult { get; set; }
		public decimal MealRateChildBig { get; set; }

	}
}
