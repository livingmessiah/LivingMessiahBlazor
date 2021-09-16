using System;

namespace SukkotApi.Domain.Donations.Queries
{
	public class PreviousDonation
	{
		public Int32 Detail { get; set; }
		public Decimal Amount { get; set; }
		public String Notes { get; set; }
		public String ReferenceId { get; set; }
		public String CreatedBy { get; set; }
		public DateTime CreateDate { get; set; }

	}


}
