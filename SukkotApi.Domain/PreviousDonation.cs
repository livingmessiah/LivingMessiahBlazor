using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain
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
