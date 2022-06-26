using System;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public class IndexVM
{
	public string EmailAddress { get; set; }  // user.GetUserEmail();
	public string UserName { get; set; }      // user.GetUserNameSoapVersion();
	public Status Status { get; set; } = Status.EmailNotConfirmed;
	
	public Enums.StatusFlag StatusFlag { get; set; } = Enums.StatusFlag.EmailNotConfirmed;
	
	public bool Has(Enums.StatusFlag statusFlag)
	{
		return StatusFlag.HasFlag(statusFlag);
	}

	public HouseRulesAgreement HouseRulesAgreement { get; set; }
	public RegistrationStep RegistrationStep { get; set; }

	public string Title
	{
		get
		{
			if (string.IsNullOrEmpty(UserName) | string.IsNullOrEmpty(EmailAddress))
			{
				return "Registration Steps";
			}
			else
			{
				return $"Registration for {EmailAddress}";
			}
		}
	}

	public void AddToFlag(Enums.StatusFlag statusFlag) =>	this.StatusFlag |= statusFlag;

}

public class HouseRulesAgreement
{
	public int Id { get; set; }
	//public string EmailAddress { get; set; }
	public DateTimeOffset AcceptedDate { get; set; }
	public string TimeZone { get; set; }
}

public class RegistrationStep
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string FamilyName { get; set; } = string.Empty;
	public decimal TotalDonation { get; set; }
	public decimal RegistrationFeeAdjusted { get; set; }
	public decimal RemainingCost
	{
		get
		{
			return RegistrationFeeAdjusted - TotalDonation; // previously this had other costs that are no longer tracked
		}
	}


}
