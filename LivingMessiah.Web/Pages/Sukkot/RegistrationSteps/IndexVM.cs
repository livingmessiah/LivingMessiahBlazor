using System;
using System.Collections.Generic;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public class IndexVM
{
	public string EmailAddress { get; set; }  // user.GetUserEmail();
	public string UserName { get; set; }      // user.GetUserNameSoapVersion();
	
	public Status Status { get; set; } = Status.EmailNotConfirmed;

	public HouseRulesAgreement HouseRulesAgreement { get; set; }
	public RegistrationStep RegistrationStep { get; set; }

	public bool IsRegistrationFormCompleted()
	{
		if (this.HouseRulesAgreement is not null && this.Status >= Status.RegistrationFormCompleted)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool IsPartiallyPaid()
	{
		if (this.HouseRulesAgreement is not null && this.Status == Status.PartiallyPaid)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool IsFullyPaid()
	{
		if (this.HouseRulesAgreement is not null && this.Status == Status.FullyPaid)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

}

public class HouseRulesAgreement
{
	public int Id { get; set; }
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
