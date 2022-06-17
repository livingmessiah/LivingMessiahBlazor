using System;
using System.ComponentModel.DataAnnotations;
using SukkotApi.Domain.Enums;

namespace SukkotApi.Domain;

public class vwRegistrationShell
{
	public int Id { get; set; }
	public String FamilyName { get; set; }

	public Status Status { get; set; }

	[DataType(DataType.Currency)]
	public Decimal TotalDonation { get; set; }

	[DataType(DataType.Currency)]
	public decimal RemainingCost
	{
		get
		{
			return 0 - TotalDonation; // previously this had other costs that are no longer tracked
		}
	}
}
