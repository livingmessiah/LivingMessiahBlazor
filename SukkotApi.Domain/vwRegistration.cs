using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain;

public class vwRegistration
{
	public int Id { get; set; }
	[DisplayName("Family Name")]
	public string FamilyName { get; set; }

	[DisplayName("First Name")]
	public string FirstName { get; set; }

	[DisplayName("Spouse Name")]
	public string SpouseName { get; set; }

	[DisplayName("Other Names")]
	public string OtherNames { get; set; }

	[DataType(DataType.EmailAddress)]
	public string EMail { get; set; }

	[DataType(DataType.PhoneNumber)]
	public string Phone { get; set; }

	[DisplayName("Number of Adults")]
	public int Adults { get; set; }

	[DisplayName("Children 6 to 12")]
	public int ChildBig { get; set; }
	[DisplayName("Children under 6")]
	public int ChildSmall { get; set; }

	[DisplayName("Attendance Total")]
	public int AttendanceTotal { get; set; }
	public string AttendanceDatesCSV { get; set; }

	public int StatusId { get; set; }
	public string StatusName
	{
		get
		{
			return Enums.Status.FromValue(StatusId).Name;
		}
	}

	public int StatusValue
	{
		get
		{
			return Enums.Status.FromValue(StatusId);
		}
	}

	[DataType(DataType.Currency)]
	public decimal RegistrationFee { get; set; }

	[DisplayName("Paid")]
	[DataType(DataType.Currency)]
	public decimal LmmDonation { get; set; }

	public string Notes { get; set; }

	public int AttendanceBitwise { get; set; }

	public string PayWithCheckMessage { get; set; }

	public string FullName(bool includeOthers)
	{
		string s = FirstName;
		if (!string.IsNullOrEmpty(SpouseName)) { s += " and " + SpouseName; }
		s += " " + FamilyName;
		if (includeOthers) { s += " and " + OtherNames; }
		return s;
	}


	public DateTime[] AttendanceDateList
	{
		get
		{
			if (!String.IsNullOrEmpty(AttendanceDatesCSV))
			{
				int length = AttendanceDatesCSV.Split(",").Length;
				DateTime[] list = new DateTime[length];
				string[] array = AttendanceDatesCSV.Split(",");
				int i = 0;
				foreach (string value in array)
				{
					list[i] = (DateTime.Parse(value));
					i += 1;
				}
				return list;
			}
			else
			{
				return null;
			}
		}
	}

}
