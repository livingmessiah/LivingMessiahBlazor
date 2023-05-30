using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser.Data;

public class vwRegistration
{
	public int Id { get; set; }
	public string? FamilyName { get; set; }
	public string? FirstName { get; set; }
	public string? SpouseName { get; set; }
	public string? OtherNames { get; set; }
	public string? EMail { get; set; }
	public string? Phone { get; set; }
	public int Adults { get; set; }
	public int ChildBig { get; set; }
	public int ChildSmall { get; set; }
	public int StatusId { get; set; } // The SuperUser!EntryForm needs this

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