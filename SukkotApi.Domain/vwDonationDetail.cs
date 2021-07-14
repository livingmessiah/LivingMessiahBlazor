using System;

namespace SukkotApi.Domain
{
	public class vwDonationDetail
	{
		public Int32 RegistrationId { get; set; }
		public Int32 Detail { get; set; }
		public Decimal Amount { get; set; }
		public String Notes { get; set; }
		public String ReferenceId { get; set; }
		public DateTime CreateDate { get; set; }
		public String CreatedBy { get; set; }
		public String FamilyName { get; set; }
	}

}
