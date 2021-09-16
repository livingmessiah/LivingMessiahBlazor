using Microsoft.AspNetCore.Components;
using SukkotApi.Domain.Donations.Enums;

/*
	@model SukkotApi.Domain.DonationStatus
	Location SukkotApi.Domain\EnumReplacement.cs
*/

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	public partial class IndexFilter
	{
		[Parameter]
		public DonationStatus DonationStatus { get; set; }

		public string ActiveFilter(DonationStatusEnum id)
		{
		//	if (id == Model.DonationStatusEnum)
		//	{
		//		return "active";
		//	}
		//	else
		//	{
				return "";
		//	}
		}

	}
}
