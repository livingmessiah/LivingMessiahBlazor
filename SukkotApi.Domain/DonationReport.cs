﻿using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain
{
	public class DonationReport
    {
		public int Id { get; set; }
		public string EMail { get; set; }
		public string FamilyName { get; set; }
		public string FirstName { get; set; }
		public int StatusId { get; set; }
		public string StatusDescr { get; set; }

		[DataType(DataType.Currency)]
		public decimal MealTotalCost { get; set; }

		[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal RegistrationFee { get; set; }

		[DataType(DataType.Currency)]
		public decimal CampCost { get; set; }

		[DataType(DataType.Currency)]
		public decimal TotalDonation { get; set; }

		[DataType(DataType.Currency)]
		public decimal AmountDue { get; set; }
	}
}