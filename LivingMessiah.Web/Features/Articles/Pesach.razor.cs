using System;
using System.Threading.Tasks;
using LivingMessiah.Web.Features.Calendar.ManageKeyDates.Constants;

namespace LivingMessiah.Web.Features.Articles;

public partial class Pesach
{
	protected string? PesachTitle;
	protected string? PesachDate;

	private DateTime pesachDate { get; set; }

	protected override Task OnInitializedAsync()
	{
		pesachDate = DateTime.Parse(Dates._12_Passover);
		PesachDate = pesachDate.ToString(DateFormat.dddd_MMMM_dd);
		PesachTitle = "Passover " + pesachDate.ToString("y");
		return base.OnInitializedAsync();
	}
}
