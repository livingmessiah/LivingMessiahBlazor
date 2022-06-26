using SukkotApi.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;

public class DonationReport
{
	public int Id { get; set; }
	public string EMail { get; set; }
	public string FamilyName { get; set; }
	public string FirstName { get; set; }
	public int StatusId { get; set; }
	public string StatusDescr { get; set; }

	[DataType(DataType.Currency)]
	[DisplayFormat(DataFormatString = "{0:C0}")]
	public decimal RegistrationFeeAdjusted { get; set; }

	[DataType(DataType.Currency)]
	public decimal TotalDonation { get; set; }

	[DataType(DataType.Currency)]
	public decimal AmountDue { get; set; }

	public string FullyPaidIcon
	{
		get
		{
			return Status.FromValue(StatusId) == Status.FullyPaid ? "X" : "";
		}
	}

}
