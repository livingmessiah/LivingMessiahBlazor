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
using Syncfusion.Blazor.Buttons;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	public partial class AddGrid
	{
		[Inject]
		public ILogger<AddGrid> Logger { get; set; }

		[Inject]
		public IDonationRepository db { get; set; }

		public IEnumerable<DonationDetail> DonationDetails { get; set; }

		private SfGrid<DonationDetail> Grid;

		/*
		ErrorLogID	ErrorTime	UserName	ErrorNumber	ErrorSeverity	ErrorState	ErrorProcedure	ErrorLine	ErrorMessage	BatchLogJobId
22	2021-09-27 23:18:24.700	dbo	547	16	0	Sukkot.stpDonationInsert 	39	The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Donation_Registration". 
		The conflict occurred in database "Sukkot", table "Sukkot.Registration", column 'Id'.	0

		*/
		public async Task OnCommandClicked(CommandClickEventArgs<DonationDetail> args)
		{
			Logger.LogDebug($"Inside {nameof(AddGrid)}!{nameof(OnCommandClicked)}");
			Donation addDetail = new Donation()
			{
				RegistrationId = args.RowData.RegistrationId,
				Amount = args.RowData.Amount,
				Notes = args.RowData.Notes,
				ReferenceId = args.RowData.ReferenceId,
				CreateDate = DateTime.Now,
				CreatedBy = "test@test.com"
			};

			int rows = 0;
			try
			{
				rows = await db.InsertRegistrationDonation(addDetail, "test@test.com");
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error inserting record in database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
			Logger.LogDebug($"...rows={nameof(rows)}");
		}


		private readonly decimal RegistrationIdDefault = 3;
		private readonly decimal AmountDefault = 1;
		private readonly string NotesDefault = "Test Note";
		private readonly string ReferenceIdDefault = "Test ReferenceId";

		public async Task InsertAsync(BeforeBatchSaveArgs<DonationDetail> args)  //Task<int>(Donation donation)
		{
			Logger.LogDebug($"Inside {nameof(AddGrid)}!{nameof(InsertAsync)}; calling {nameof(db.InsertRegistrationDonation)}");
			int rows = 0;

			var BatchChanges = args.BatchChanges;

			if (BatchChanges.ChangedRecords.Count > 0)
			{
				Logger.LogDebug($"...Changed Records: {BatchChanges.ChangedRecords.Count}");
				try
				{
					foreach (var item in BatchChanges.ChangedRecords)
					{
						//DonationDetail addDetail = new DonationDetail()
						Donation addDetail = new Donation()
						{
							RegistrationId = item.RegistrationId,
							Amount = item.Amount,
							Notes = item.Notes,
							ReferenceId = item.ReferenceId,
							CreateDate = DateTime.Now,
							CreatedBy = "test@test.com"
						};

						rows += await db.InsertRegistrationDonation(addDetail, "test@test.com");
					}

					//Logger.LogDebug($"...calling {nameof(db.InsertRegistrationDonation)}");
					
				}
				catch (Exception ex)
				{
					DatabaseError = true;
					DatabaseErrorMsg = $"Error inserting record in database";
					Logger.LogError(ex, $"...{DatabaseErrorMsg}");
				}
			}
			Logger.LogDebug($"...rows={nameof(rows)}");
			//return i;
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
		[Inject] public ISecurityClaimsService SvcClaims { get; set; }
 
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