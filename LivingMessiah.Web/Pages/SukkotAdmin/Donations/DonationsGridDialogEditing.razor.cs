using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

using LivingMessiah.Web.Services;
using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class DonationsGridDialogEditing
	{
		[Inject]
		public ILogger<DonationsGridDialogEditing> Logger { get; set; }

		[Inject]
		public IDonationRepository db { get; set; }

		[Inject]
		public ISukkotAdminService svc { get; set; }

		public IEnumerable<DonationDetail> DonationDetails { get; set; }

		//protected override async Task OnInitializedAsync()
		//{
		//	//await GetDataWithParms(CurrentFilter);

		//	//BaseRegistrationSortSmartEnum sortAndDirection = BaseRegistrationSortSmartEnum.ByFirstName;
		//	//string sort = sortAndDirection.SqlTableColumnName + sortAndDirection.Order;

		//	Logger.LogDebug($"Inside {nameof(DonationsGridDialogEditing)}!{nameof(OnInitializedAsync)}");
		//	try
		//	{
		//		DonationDetails = await db.GetDonationDetailsAll();
		//	}
		//	catch (Exception ex)
		//	{
		//		DatabaseError = true;
		//		DatabaseErrorMsg = $"Error reading database";
		//		Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		//	}
		//	//StateHasChanged();  // https://stackoverflow.com/questions/56436577/blazor-form-submit-needs-two-clicks-to-refresh-view
		//}

		//private async Task GetDataWithParms(BaseDonationStatusFilterSmartEnum filter)
		//{ 
		//}

		void Failure(FailureEventArgs e)
		{
			DatabaseErrorMsg = $"Error inside {nameof(DonationsGridDialogEditing)}; e.Error: {e.Error}";
			Logger.LogDebug(DatabaseErrorMsg); // ToDo; don't show if in production
			DatabaseError = true;
			
		}

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
	}
}
