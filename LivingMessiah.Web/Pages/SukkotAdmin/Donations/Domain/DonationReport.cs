﻿using SukkotApi.Domain.Enums;
using SukkotApi.Domain.Registrations.Enums;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain
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

		public int LocationInt { get; set; }

		public string LocationName
		{
			get
			{
				return BaseLocationSmartEnum.FromValue(LocationInt).Name;
			} 
		}

		public string FullyPaidIcon
		{
			get
			{
				return SukkotApi.Domain.Enums.BaseStatusSmartEnum.FromValue(StatusId) == BaseStatusSmartEnum.FullyPaid ? "X" : "";
			}
		}

	}
}
