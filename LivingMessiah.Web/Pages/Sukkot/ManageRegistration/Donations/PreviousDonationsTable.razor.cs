using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Data;
using LivingMessiah.Web.Pages.Sukkot.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using Syncfusion.Blazor.Data;
using System.Linq;
using LivingMessiah.Web.Pages.Sukkot.ManageRegistration;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Donations;

public partial class PreviousDonationsTable
{
	[Inject] public IRepository? db { get; set; }
	[Inject] public ILogger<PreviousDonationsTable>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter, EditorRequired] public int RegistrationId { get; set; }

	public List<DonationDetailQuery>? DonationDetails { get; set; } = new List<DonationDetailQuery>(); //init;

	private decimal Total { get; set; } = 0;
	private string TotalNoCents { get; set; } = string.Empty;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}; RegistrationId: {1}"
			, nameof(PreviousDonationsTable) + "!" + nameof(OnInitialized), RegistrationId));
		await PopulateTable();
	}

	private async Task DeleteHandler(int id)
	{
		try
		{
			int affectedRows = 0;
			affectedRows = await db!.DeleteDonationDetail(id);
			Toast!.ShowSuccess($"Deleted id: {id}; affectedFows: {affectedRows}");

			//StateHasChanged(); await InvokeAsync(() => StateHasChanged()) 
			await InvokeAsync(StateHasChanged);
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error reading database");
			Toast!.ShowError("Error reading database");
		}
	}

	//void EditHandler(int id)
	//{
	//	Toast!.ShowInfo($"{nameof(EditHandler)}; id: {id}; ToDo: handle Edit");
	//}


	private async Task PopulateTable()
	{
		try
		{
			DonationDetails = await db!.GetByRegistrationId(RegistrationId);

			if (DonationDetails is null)
			{
				Toast!.ShowWarning($"{nameof(DonationDetailQuery)} NOT FOUND");
			}
			else
			{
				Total = DonationDetails.Select(s => s.Amount).Sum();  // (s => s.Amount ?? 0)
				TotalNoCents = String.Format("{0:C0}", Total);
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error reading database");
			Toast!.ShowError("Error reading database");
		}
	}
}
