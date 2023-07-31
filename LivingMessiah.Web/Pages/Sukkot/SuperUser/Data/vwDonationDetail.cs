using System;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Data;

public class vwDonationDetail
{
	public int Id { get; set; }
	public int Detail { get; set; }
	public decimal Amount { get; set; }
	public string? Notes { get; set; }
	public string? ReferenceId { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime CreateDate { get; set; }

	public string AmountNoCents
	{
		get
		{
			return String.Format("{0:C0}", Amount);
		}
	}

}
