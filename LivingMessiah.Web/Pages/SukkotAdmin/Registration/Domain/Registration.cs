using SukkotApi.Domain.Enums;
using System;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;

public class Registration
{
	public int? Id { get; set; }  // If null, then adding a record
	public string FirstName { get; set; }
	public string FamilyName { get; set; }
	public string SpouseName { get; set; }
	public string OtherNames { get; set; }
	public string EMail { get; set; }
	public string Phone { get; set; }  // NULL
	public int Adults { get; set; }
	public int ChildBig { get; set; }
	public int ChildSmall { get; set; }

	public int StatusId { get; set; }
	public string StatusName
	{
		get
		{
			return Status.FromValue(StatusId).Name;
		}
	}

	public int AttendanceBitwise { get; set; }
	public DateTime[] AttendanceDateList { get; set; }  // NOT NULL
	public string AttendanceDatesCSV { get; set; }

	public string Notes { get; set; }


	//public string Avatar { get; set; }
	//public decimal LmmDonation { get; set; }		// NOT NULL
}
