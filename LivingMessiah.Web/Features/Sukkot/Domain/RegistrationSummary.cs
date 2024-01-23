using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Features.Sukkot.Domain;

public class RegistrationSummary
{
	public int Id { get; set; }
	public string? FamilyName { get; set; }
	public string? EMail { get; set; }
	public int Adults { get; set; }
	public int ChildBig { get; set; }
	public int ChildSmall { get; set; }
	public int AttendanceBitwise { get; set; }

	[DisplayFormat(DataFormatString = "{0:C0}")]
	public decimal RegistrationFeeAdjusted { get; set; }

	[DisplayFormat(DataFormatString = "{0:C0}")]
	public decimal TotalDonation { get; set; }

	[DisplayFormat(DataFormatString = "{0:C0}")]
	public decimal TotalCost
	{
		get
		{
			return RegistrationFeeAdjusted;
		}
	}

	[DisplayFormat(DataFormatString = "{0:C0}")]
	public decimal RemainingCost
	{
		get
		{
			return RegistrationFeeAdjusted - TotalDonation;
		}
	}
}
