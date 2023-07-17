using System;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Data;

public class vwRegistration
{
	public int Id { get; set; }
	public string? EMail { get; set; }
	public string? FullName { get; set; }
	public int StatusId { get; set; } // The SuperUser!EntryForm needs this
	public string StatusName
	{
		get
		{
			return RegistrationSteps.Enums.Status.FromValue(StatusId).Name;
		}
	}
	public string? Phone { get; set; }

	/*
	public string Disabled
	{
		get
		{
			return RegistrationSteps.Enums.Status.FromValue(this.StatusId) == RegistrationSteps.Enums.Status.NotAuthenticated ? " disabled" : "";
		}
	}
	*/

	//	public string? Notes { get; set; }
	//	public string? Avatar { get; set; }
	//	public Decimal LmmDonation { get; set; }
}


/*
SELECT Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone
, Adults, ChildBig, ChildSmall
, StatusId
FROM Sukkot.Registration
ORDER BY FirstName 
 */