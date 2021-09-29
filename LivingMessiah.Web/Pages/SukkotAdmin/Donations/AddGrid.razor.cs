using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data; // using SukkotApi.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Services;

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


		//https://www.syncfusion.com/forums/165055/custom-text-and-value-with-odata-endpoint
		//https://www.syncfusion.com/forums/155340/autocomplete-autoformat-display-text-vs-search-text-binding
		
		private int SelectedRegistrantId = 0;
		private string SelectedRegistrantName = "";

		public async Task OnSelect(SelectEventArgs<RegistrationLookup> args)
		{
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
}


/*
		private string Message = string.Empty;
		async void OnValidSubmit()
		{
			//Logger.LogDebug($"Inside {nameof(Donations.AddGrid)}!{nameof(OnValidSubmit)}; (lookup.id)={(lookup.ID == null ? "":"") }");
			Logger.LogDebug($"Inside {nameof(Donations.AddGrid)}!{nameof(OnValidSubmit)}; (lookup.id)={lookup.ID?? "is null"}");
			Message = "Form Submitted Successfully!";
			await Task.Delay(2000);
			Message = string.Empty;
			lookup.Text = null;
			StateHasChanged();
		}
		private void OnInvalidSubmit()
		{
			Message = string.Empty;
			SelectedRegistrantId = string.Empty;
			SelectedRegistrantName = string.Empty;
		}


<AutoCompleteEvents TItem="RegistrationLookup" TValue="string" ValueChange="OnChange"></AutoCompleteEvents>		
//https://www.syncfusion.com/forums/164196/how-to-get-the-selected-object-in-blazor-autocomplete
		private void OnChange(ChangeEventArgs<string, RegistrationLookup> args)
		{
			//var DropVal = args.Value;
			//SelectedRegistrantId = args.Value;
			SelectedRegistrantId = args.Item.ID;
			SelectedRegistrantName = args.Value;
			//var value = AutoVal;
			//SelectedRegistrantName = lookup.Text;
		}

		////https://www.syncfusion.com/forums/164196/how-to-get-the-selected-object-in-blazor-autocomplete
		//private void OnChange(ChangeEventArgs<string, RegistrationLookup> args)
		//{
		//	//var DropVal = args.Value;
		//	//SelectedRegistrantId = args.Value;
		//	SelectedRegistrantId = args.Item.ID;
		//	SelectedRegistrantName = args.Value;
		//	//var value = AutoVal;
		//	//SelectedRegistrantName = lookup.Text;
		//}

 		<GridEvents OnBatchSave="InsertAsync" TValue="DonationDetail" />
		<GridEvents CommandClicked="OnCommandClicked" OnActionBegin="Begin" TValue="DonationDetail"></GridEvents>


		private void AddBtnHandler(Microsoft.AspNetCore.Components.Web.MouseEventArgs args, DonationDetail dtl)
		{
			Donation addDetail = new Donation()
			{
				RegistrationId = dtl.RegistrationId,
				Amount = dtl.Amount,
				Notes = dtl.Notes,
				ReferenceId = dtl.ReferenceId,
				CreateDate = DateTime.Now,
				CreatedBy = "test@test.com"
			};
		}
		
		private SfButton AddBtn;

 						<Template>
							@{
								var dtl = (context as DonationDetail);
								<SfButton >Add</SfButton>
							}
						</Template>

	<SfButton CssClass="e-success" @onclick="Add"> Add Detail Record</SfButton>
		<SfButton @onclick="Update"> Update - 1001 </SfButton>
		<SfButton @onclick="Delete"> Delete the selected row </SfButton>

		public async Task Add()
		{
			DonationDetail addDetail = new DonationDetail()
			{
				RegistrationId = 3,
				Amount = 1,
				Notes = "Test Insert",
				ReferenceId = "Test Reference Id",
				CreateDate = DateTime.Now,
				CreatedBy = "test@test.com"
			};
			await this.Grid.AddRecord(addDetail);
		}

		[Inject] 		public IDonationService Svc { get; set; }
		
 
		Logger.LogDebug($"Inside {nameof(ZZZ)}!{nameof(OnInitializedAsync)}");
		try
		{
			MyList = await db.MyDatabaseCallOrServiceCall();
			if (MyList == null)
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = "MyList NOT FOUND";
				DatabaseWarningMsg = $"{nameof(MyList)} NOT FOUND";
				//Logger.LogDebug($"{nameof(MyList)} is null, Sql:{db.BaseSqlDump}");					
			}
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}

		protected override async Task OnInitializedAsync()
		{
			try
			{
				await Task.Delay(0);
			}
			catch (System.Exception ex)
			{
				Logger.LogError(ex, $"Inside {nameof(OnInitializedAsync)}");
			}
		}

		How to add record with dynamic NewRowPosition using context menu?
		https://www.syncfusion.com/kb/12296/how-to-add-record-with-dynamic-newrowposition-using-context-menu


		public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
		{
			if (args.Item.Id == "Grid_excelexport") //Id is combination of Grid's ID and itemname
			{
				await this.Grid.ExcelExport();
				//this.Grid.AddRecordAsync(data, this.Grid.Cu)
				//    this.TreeGrid.AddRecord(data, this.TreeGrid.GetCurrentViewRecords().Count - 1, RowPosition.Bottom);
			}
		}


public async Task<DonationDetail> InsertAsync()
{
	Logger.LogDebug($"Inside {nameof(AddGrid)}!{nameof(InsertAsync)}; calling {nameof(db.InsertRegistrationDonation)}; key:{key}");
	try
	{
		int i = Svc.InsertRegistrationDonation(DonationInsertModel d);
	}
	catch (Exception ex)
	{
		string strErrMsg = $"...Error calling {nameof(db.InsertRegistrationDonation)}";
		Logger.LogError(ex, strErrMsg);
		throw new InvalidOperationException(strErrMsg, ex);
		//throw ex;
	}

	return data;
}
*/