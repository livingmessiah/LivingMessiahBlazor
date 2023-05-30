using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.Detail;


public class DisplayVM
{
	public int Id { get; set; }
	public string? FamilyName { get; set; }
	public string? FirstName { get; set; }
	public string? SpouseName { get; set; }
	public string? OtherNames { get; set; }
	public string? EMail { get; set; }
	public string? Phone { get; set; }

	public string Phone2 
	{
		get
		{
			return !String.IsNullOrEmpty(this.Phone) ? $"Phone: {this.Phone}, " : "";
		}
	}

	public int Adults { get; set; }
	public int ChildBig { get; set; }
	public int ChildSmall { get; set; }
	public int AttendanceTotal { get; set; }

	public int StatusId { get; set; }
	public string StatusName
	{
		get
		{
			return RegistrationSteps.Enums.Status.FromValue(StatusId).Name;
		}
	}

	public int StatusValue
	{
		get
		{

			return RegistrationSteps.Enums.Status.FromValue(StatusId);
		}
	}

	public decimal RegistrationFeeAdjusted { get; set; }
	public decimal LmmDonation { get; set; } // Paid, DataType.Currency

	public string? Notes { get; set; }
	public int AttendanceBitwise { get; set; }  // ToDo Delete?
	public DateTime[]? AttendanceDateList { get; set; }
	public DateTime[]? AttendanceDateList2ndMonth { get; set; }

	public string? PayWithCheckMessage { get; set; }

	public string? HouseRulesAgreementDate { get; set; }

	public string FullName(bool includeOthers)
	{
		string? s = FirstName;
		if (!string.IsNullOrEmpty(SpouseName)) { s += " and " + SpouseName; }
		s += " " + FamilyName;
		if (includeOthers) { s += " and " + OtherNames; }
		return s;
	}


}
