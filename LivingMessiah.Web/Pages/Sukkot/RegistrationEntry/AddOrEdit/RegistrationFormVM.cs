using LivingMessiah.Web.Pages.UpcomingEvents.Enums;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.AddOrEdit;

public class RegistrationFormVM
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
	public RegistrationSteps.Enums.Status? Status { get; set; }

	public int AttendanceBitwise { get; set; } // does the VM need this?
	public DateTime[]? AttendanceDateList { get; set; }
	public DateTime[]? AttendanceDateList2ndMonth { get; set; }

	public string? Notes { get; set; }
	public Decimal LmmDonation { get; set; }


	/*
	DON'T KNOW IF I NEED THIS; GOTTEN FROM C:\Users\JohnM\source\repos\fluxor\BlazorServerFluxorSerilog\BlzSrvFlxSrl\BlzSrvFlxSrl\Features\SpecialEvents\FormVM.cs
	ToDo: Warning	CS8618	Non-nullable property 'Description' et. al. must contain a non-null value
	  when exiting constructor. Consider declaring the property as nullable.
	public FormVM()
	{
		SpecialEventTypeId = SpecialEventType.Other.Value;
		EventDate = DateTime.Now.AddDays(35);
	}
	*/

}
