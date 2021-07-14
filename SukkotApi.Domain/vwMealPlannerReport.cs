using System;

namespace SukkotApi.Domain
{
	public class vwMealPlannerReport
	{
		public string MealDay { get; set; }
		public string BruOrDin { get; set; }
		public string MealTypeDescr { get; set; }  //MealTypeEnum
		public string Menu { get; set; }
		public int Adult { get; set; }
		public int ChildBig { get; set; }
		public int ChildSmall { get; set; }
		public int TotalMeals { get; set; }
	}
}
