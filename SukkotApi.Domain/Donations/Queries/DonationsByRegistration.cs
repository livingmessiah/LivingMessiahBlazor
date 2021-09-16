using SukkotApi.Domain.Enums;

namespace SukkotApi.Domain.Donations.Queries
{
	public class DonationsByRegistration
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string FamilyName { get; set; }
		public int StatusId { get; set; }
		public decimal TotalDonation { get; set; }
		public decimal AmountDue { get; set; }
		public int? Detail { get; set; }
		public decimal Amount { get; set; }  // ISNULL(Amount, 0) AS Amount
		public string NOTES { get; set; }
		public string ReferenceId { get; set; }
		public LocationEnum LocationEnum { get; set; }
		public string CreatedBy { get; set; }
		public string CreateDateMDY { get; set; }
	}
}
