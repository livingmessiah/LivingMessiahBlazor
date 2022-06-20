using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public class CurrentStatus
{
	public int Id { get; set; } = 0;
	public string FamilyName { get; set; } = string.Empty;
	public Status Status { get; set; } = Status.EmailNotConfirmed;
	public StatusFlagEnum StatusFlagEnum { get; set; }
	public decimal TotalDonation { get; set; }
	public decimal RemainingCost
	{
		get
		{
			return 0 - TotalDonation; // previously this had other costs that are no longer tracked
		}
	}

	public string UserName { get; set; }     
	public string EmailAddress { get; set; } 

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


}
