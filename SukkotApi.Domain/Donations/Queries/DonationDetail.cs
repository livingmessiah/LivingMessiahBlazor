using SukkotApi.Domain.Enums;
using System;

namespace SukkotApi.Domain.Donations.Queries
{
	public class DonationDetail
	{
		public Int32 RegistrationId { get; set; }
		public Int32 Detail { get; set; }
		public Decimal Amount { get; set; }
		public String Notes { get; set; }
		public String ReferenceId { get; set; }
		public DateTime CreateDate { get; set; }
		public String CreatedBy { get; set; }
		public String FamilyName { get; set; }
		public LocationEnum LocationEnum { get; set; }
	}
}
