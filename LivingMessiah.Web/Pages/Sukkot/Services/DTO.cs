using System;

namespace LivingMessiah.Web.Pages.Sukkot.Services;

public record DTO
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

		public int StatusId { get; set; }

		public int AttendanceBitwise { get; set; }

		public string? Notes { get; set; }
		public string? Avatar { get; set; }
		public decimal LmmDonation { get; set; }

}
