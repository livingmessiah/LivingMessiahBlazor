using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;

public class RegistrationVM
{
	[Required]
	[Key]
	[DisplayName("Reg #")]
	public int Id { get; set; }

	[Required]
	[MaxLength(75)]
	[DisplayName("Last Name")]
	public string? FamilyName { get; set; }

	[Required]
	[MaxLength(75)]
	[DisplayName("First Name")]
	public string? FirstName { get; set; }

	[MaxLength(75)]
	[DisplayName("Spouse Name")]
	public string? SpouseName { get; set; }

	[MaxLength(255)]
	[DisplayName("Other Names")]
	public string? OtherNames { get; set; }

	[Required]
	[MaxLength(75)]
	[DataType(DataType.EmailAddress)]
	[DisplayName("eMail")]
	public string? EMail { get; set; }

	[MaxLength(15)]
	[DataType(DataType.PhoneNumber)]
	public string? Phone { get; set; }

	[Required(ErrorMessage = "At least 1 adult required")]
	[Range(1, 10, ErrorMessage = "{0} must be between {1} and {2}")]
	[DisplayName("# of Adults")]
	public int Adults { get; set; }

	[Required(ErrorMessage = "For none, use 0")]
	[Range(0, 10, ErrorMessage = "{0} must be between {1} and {2}")]
	[DisplayName("Child 6 to 9")]
	public int ChildBig { get; set; }

	[Required(ErrorMessage = "For none, use 0")]
	[DisplayName("Child under 6")]
	[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
	public int ChildSmall { get; set; }

	public int StatusId { get; set; }
	public Status? Status { get; set; }

	[DisplayName("Attendance Bitwise")]
	public int AttendanceBitwise { get; set; }
	
	public DateTime[]? AttendanceDateList { get; set; }
	public DateTime[]? AttendanceDateList2ndMonth { get; set; }

	[DisplayName("Comments or Special Requests")]
	[DataType(DataType.MultilineText)]
	[StringLength(800)]
	public string? Notes { get; set; }

	[DisplayName("Picture (optional)")]
	[StringLength(255)]
	public string? Avatar { get; set; }

	[DisplayName("LMM Donation")]
	[DataType(DataType.Currency)]
	public Decimal LmmDonation { get; set; }

	public string DumpAttendanceDates
	{
		get
		{
			return this.AttendanceDateList is not null ?
				String.Join(", ", this.AttendanceDateList.Select(date => date.ToString("yyyy-MM-dd")))
				: "";
		}
	}

	public string DumpAttendanceDates2ndMonth
	{
		get
		{
			return this.AttendanceDateList2ndMonth is not null ?
				String.Join(", ", this.AttendanceDateList2ndMonth.Select(date => date.ToString("yyyy-MM-dd")))
				: "";
		}
	}

}
