using System;
using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain;

public class vwRegistrationShell
{
		public int Id { get; set; }
		public String FamilyName { get; set; }
		public int StatusId { get; set; }

		[DataType(DataType.Currency)]
		public Decimal TotalDonation { get; set; }
		public int MealCount { get; set; }

		[DataType(DataType.Currency)]
		public Decimal MealCost { get; set; }
		[DataType(DataType.Currency)]

		public Decimal CampCost { get; set; }

		[DataType(DataType.Currency)]
		public decimal TotalCost
		{
				get
				{
						return MealCost + CampCost;
				}
		}

		[DataType(DataType.Currency)]
		public decimal RemainingCost
		{
				get
				{
						return MealCost + CampCost - TotalDonation;
				}
		}

}
