using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain
{
	public class vwDonationsByRegistration
	{
		public int Id { get; set; }
		public string FamilyName { get; set; }
		public int StatusId { get; set; }

		[DataType(DataType.Currency)]
		public decimal TotalDonation { get; set; }

		[DataType(DataType.Currency)]
		public decimal AmountDue { get; set; }
		public int? Detail { get; set; }

		//[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal? Amount { get; set; }
		public string NOTES { get; set; }
		public string ReferenceId { get; set; }
		public string CreatedBy { get; set; }
		public string CreateDateMDY { get; set; }
	}
}
