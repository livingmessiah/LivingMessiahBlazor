using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data; // using SukkotApi.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;

using LivingMessiah.Web.Services;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.DropDowns;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	public partial class AddGrid
	{
		[Inject]
		public ILogger<AddGrid> Logger { get; set; }

		[Inject]
		public IDonationRepository db { get; set; }

		[Inject] 
		public ISecurityClaimsService SvcClaims { get; set; }

		public IEnumerable<DonationDetail> DonationDetails { get; set; }

		public List<RegistrationLookup> RegistrationLookupList { get; set; }

		private SfGrid<DonationDetail> GridAdd;

		protected override async Task OnInitializedAsync()
		{
			RegistrationLookupList = await db.PopulateRegistrationLookup();
		}

	
		private int SelectedRegistrantId = 0;
		private string SelectedRegistrantName = "";

		private void OnValueChanged(ChangeEventArgs<string, RegistrationLookup> args) 
		{
			if (String.IsNullOrEmpty(args.Value))
			{
				SelectedRegistrantId = 0;
			}
		}

		public async Task OnSelect(SelectEventArgs<RegistrationLookup> args)
		{
			//https://www.syncfusion.com/forums/165055/custom-text-and-value-with-odata-endpoint
			//https://www.syncfusion.com/forums/155340/autocomplete-autoformat-display-text-vs-search-text-binding
			//Logger.LogDebug($"Inside {nameof(Donations.AddGrid)}!{nameof(OnSelect)}");
			//SelectedRegistrantName = args.ItemData.Text;

			SelectedRegistrantId = int.TryParse(args.ItemData.ID, out SelectedRegistrantId) ? SelectedRegistrantId : 0;
			await PopulateDonationDetails(SelectedRegistrantId);
		}

		private async Task PopulateDonationDetails(int registrationId)
		{
			Logger.LogDebug($"Inside {nameof(AddGrid)}!{nameof(PopulateDonationDetails)}; registrationId:{registrationId}");
			try
			{
				DatabaseWarning = false;
				DatabaseWarningMsg = "";
				DonationDetails = await db.GetDonationDetails(registrationId);
				if (DonationDetails == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = $"DonationDetails NOT FOUND; registrationId:{registrationId}";
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
			StateHasChanged();  // https://stackoverflow.com/questions/56436577/blazor-form-submit-needs-two-clicks-to-refresh-view
		}


		public async Task OnSaveClicked(CommandClickEventArgs<DonationDetail> args)
		{
			Logger.LogDebug($"Inside {nameof(Donations.AddGrid)}!{nameof(OnSaveClicked)}");

			string email = await SvcClaims.GetEmail();
			if (String.IsNullOrEmpty(email)) email = "test@test.com";

			Donation addDetail = new Donation()
			{
				RegistrationId = SelectedRegistrantId,
				Amount = args.RowData.Amount,
				Notes = args.RowData.Notes,
				ReferenceId = args.RowData.ReferenceId,
				CreateDate = DateTime.Now,
				CreatedBy = email
			};

			int newId = 0;
			try
			{
				newId = await db.InsertRegistrationDonation(addDetail);

				DatabaseWarning = false;
				DatabaseWarningMsg = "";
				if (newId==0)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = $"DonationDetails NOT INSERTED; SelectedRegistrantId:{SelectedRegistrantId}";
					Logger.LogWarning($"...{DatabaseWarningMsg}");
				}
				else
				{
					Logger.LogInformation($"...Donation created for RegistrationId: {addDetail.RegistrationId}; newId={newId}; Calling {nameof(PopulateDonationDetails)}");
					await PopulateDonationDetails(SelectedRegistrantId);
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error inserting record in database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
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

	public class RegistrationLookup
	{
		public string ID { get; set; }
		public string Text { get; set; }
	}
}

