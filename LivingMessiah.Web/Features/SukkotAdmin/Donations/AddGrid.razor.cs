using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Features.SukkotAdmin.Donations.Data;
using LivingMessiah.Web.Features.SukkotAdmin.Donations.Domain;

using LivingMessiah.Web.Services;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.DropDowns;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Features.SukkotAdmin.Donations;

public partial class AddGrid
{
	[Inject] public ILogger<AddGrid>? Logger { get; set; }
	[Inject] public IDonationRepository? db { get; set; }
	[Inject] public ISecurityClaimsService? SvcClaims { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public IEnumerable<DonationDetail>? DonationDetails { get; set; }
	public List<RegistrationLookup>? RegistrationLookupList { get; set; }
	private SfGrid<DonationDetail>? GridAdd;

	protected override async Task OnInitializedAsync()
	{
		RegistrationLookupList = await db!.PopulateRegistrationLookup();
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
		//Logger.LogDebug($"Inside {nameof(AddGrid)}!{nameof(OnSelect)}");
		//SelectedRegistrantName = args.ItemData.Text;

		SelectedRegistrantId = int.TryParse(args.ItemData.ID, out SelectedRegistrantId) ? SelectedRegistrantId : 0;
		await PopulateDonationDetails(SelectedRegistrantId);
	}

	private async Task PopulateDonationDetails(int registrationId)
	{
		string message = $"Inside {nameof(AddGrid)}!{nameof(PopulateDonationDetails)}; registrationId:{registrationId}";
		Logger!.LogDebug(message);
		try
		{
			DonationDetails = await db!.GetDonationDetails(registrationId);
			if (DonationDetails == null)
			{
				Toast!.ShowWarning($"DonationDetails NOT FOUND; registrationId:{registrationId}");
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error reading database");
			Toast!.ShowError("Error reading database");
		}
		StateHasChanged();  // https://stackoverflow.com/questions/56436577/blazor-form-submit-needs-two-clicks-to-refresh-view
	}

	public async Task OnSaveClicked(CommandClickEventArgs<DonationDetail> args)
	{
		Logger!.LogDebug($"Inside {nameof(AddGrid)}!{nameof(OnSaveClicked)}");

		string email = await SvcClaims!.GetEmail();
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
		string message = string.Empty;
		try
		{
			newId = await db!.InsertRegistrationDonation(addDetail);

			if (newId == 0)
			{
				message = $"DonationDetails NOT INSERTED; SelectedRegistrantId:{SelectedRegistrantId}";
				Logger!.LogWarning(message);
				Toast!.ShowWarning(message);
			}
			else
			{
				message = $"...Donation created for RegistrationId: {addDetail.RegistrationId}; newId={newId}; Calling {nameof(PopulateDonationDetails)}";
				Logger!.LogInformation(message);
				await PopulateDonationDetails(SelectedRegistrantId);
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, $"...Error inserting record in database");
			Toast!.ShowError("Error reading database");
		}
	}

	void Failure(FailureEventArgs e)
	{
		string message = $"Error inside {nameof(AddGrid)}; e.Error: {e.Error}";
		Logger!.LogDebug(message);
		Toast!.ShowError("Error reading database");
	}

}

public class RegistrationLookup
{
	public string? ID { get; set; }
	public string? Text { get; set; }
}

