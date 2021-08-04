using SukkotApi.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain
{
	public class RegistrationSummary
	{
		public int Id { get; set; }
		public string FamilyName { get; set; }
		public string EMail { get; set; }
		public int Adults { get; set; }
		public int ChildBig { get; set; }
		public int ChildSmall { get; set; }

		public int AdultRate { get; set; }
		[DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal ChildRate { get; set; }


		public int StatusId { get; set; }
		public int AttendanceBitwise { get; set; }

		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal RegistrationFee { get; set; }

		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal MealTotalCostAdult { get; set; }

		[DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal MealTotalCostChildBig { get; set; }

		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal CampCost { get; set; }
		public string CampDescr { get; set; }
		public string CampDays { get; set; }

		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal TotalDonation { get; set; }

		public Enums.LocationEnum LocationEnum { get; set; }
		public bool IncludeCampCost => LocationEnum == LocationEnum.WildernessRanch;

		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal TotalCost
		{
			get
			{
				return RegistrationFee + MealTotalCostAdult + MealTotalCostChildBig + CampCost;
			}
		}


		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal RemainingCost
		{
			get
			{
				return RegistrationFee + MealTotalCostAdult + MealTotalCostChildBig + CampCost - TotalDonation;
			}
		}

	}
}
