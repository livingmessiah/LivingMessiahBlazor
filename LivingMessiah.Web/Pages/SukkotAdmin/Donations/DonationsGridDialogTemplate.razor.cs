using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;

using Syncfusion.Blazor.Grids;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class DonationsGridDialogTemplate
	{
		[Inject]
		public ILogger<DonationsGridDialogTemplate> Logger { get; set; }

		public IEnumerable<DonationDetail> DonationDetails { get; set; }

		private bool Check = false;
		private DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "450px" };

		public void ActionCompleteHandler(ActionEventArgs<DonationDetail> args)
		{
			Logger.LogDebug($"Inside {nameof(ActionCompleteHandler)}; args.RowData.RegistrationId: {args.RowData.RegistrationId}");
			Check = args.RequestType.ToString() == "Add" ? true : false;
		}

		public void ActionBeginHandler(ActionEventArgs<DonationDetail> args)
		{
			Logger.LogDebug($"Inside {nameof(ActionBeginHandler)}");  // ; args.RowData.RegistrationId: {args.RowData.RegistrationId}

			//https://www.syncfusion.com/forums/156713/i-want-to-set-the-default-value-of-the-row-when-performing-inline-edit-add-in-grid
			if (args.RequestType.ToString() == "Add")
			{
				args.Data.RegistrationId = 9999;
				args.Data.CreateDate = DateTime.Now; // .UtcNow
			}
		}

		public void RecordDoubleClickHandler(RecordDoubleClickEventArgs<DonationDetail> args)
		{
			Logger.LogDebug($"args.RowData: {args.RowData.ToString()}");
		}

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
