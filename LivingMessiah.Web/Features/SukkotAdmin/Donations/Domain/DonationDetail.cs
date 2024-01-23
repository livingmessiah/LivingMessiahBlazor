using System;

namespace LivingMessiah.Web.Features.SukkotAdmin.Donations.Domain;

public class DonationDetail
{
		public int Id { get; set; }
		public int RegistrationId { get; set; } // Note 1
		public int Detail { get; set; }
		public decimal Amount { get; set; }
		public string? Notes { get; set; }      // Null
		public string? ReferenceId { get; set; } // Note 2
		public DateTime CreateDate { get; set; }
		public string? CreatedBy { get; set; }
		public string? Name { get; set; }  // ToDo: this should be deleted
}

/*
Notes:
	1: IX_Sukkot.Donation_Unqiue (RegistrationId ASC, Detail ASC) 
	2: This is a number that can be traced back to if from Stripe.  If it's a manual entry, put in the User.Email. 
			ToDo: Determine how Stripe does this
*/