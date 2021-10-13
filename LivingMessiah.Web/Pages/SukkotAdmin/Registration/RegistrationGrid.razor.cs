using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
using Domain = LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Enums;

using Syncfusion.Blazor.Grids;
using System.Linq;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using LivingMessiah.Web.Pages.Sukkot;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class RegistrationGrid
	{
		[Inject]
		public ILogger<RegistrationGrid> Logger { get; set; }

		[Inject]
		public IRegistrationRepository db { get; set; }

		[Inject]
		public IRegistrationService svc { get; set; }

		//[Inject]
		//public ISecurityClaimsService SvcClaims { get; set; }

		public IEnumerable<Domain.Registration> Registrations { get; set; }
		private bool Check = false;
		private bool Disabled = true;
		private bool Enabled = false;

		//ToDo this shoud come from the Sukkot.Constants and saved in cache		
		public DateRangeLocal DateRangeAttendance { get; set; } = DateRangeLocal.FromEnum(DateRangeEnum.AttendanceDays);
		public DateRangeLocal DateRangeLodging { get; set; } = DateRangeLocal.FromEnum(DateRangeEnum.LodgingDays);

		public Domain.Registration SelectedData = new Domain.Registration();

		public void RowSelectHandler(RowSelectEventArgs<Domain.Registration> args)
		{
			/*
			AttendanceBitwise int NOT NULL,
			LodgingDaysBitwise int NOT NULL,
			*/
			SelectedData =
				new Domain.Registration()
				{
					Id = args.Data.Id,
					FirstName = args.Data.FirstName,
					FamilyName = args.Data.FamilyName,
					SpouseName = args.Data.SpouseName,
					OtherNames = args.Data.OtherNames,
					EMail = args.Data.EMail,
					Phone = args.Data.Phone,
					Adults = args.Data.Adults,
					ChildBig = args.Data.ChildBig,
					ChildSmall = args.Data.ChildSmall,
					WillHelpWithMeals = false,
					AttendanceDateList = args.Data.AttendanceDateList,
					LodgingDateList = args.Data.LodgingDateList,
					CampId = args.Data.CampId,
					StatusId = args.Data.StatusId,
					LocationInt = args.Data.LocationInt
				};

			if (args.Data.CampId != 0)
			{
				SelectedCampTypeName = BaseCampTypeSmartEnum.FromValue(args.Data.CampId).ToString();
				Logger.LogDebug($"Inside {nameof(RegistrationGrid)}!{nameof(RowSelectHandler)}; SelectedCampTypeName{nameof(SelectedCampTypeName)}");
			}
			else
			{
			}
			msg = $"args.Data.CampId: {args.Data.CampId}; {nameof(SelectedCampTypeName)}: ==>{SelectedCampTypeName}<==";
			this.Disabled = false;
			this.Enabled = true;
		}

		public void RowDeSelectHandler(RowDeselectEventArgs<Domain.Registration> args)
		{
			SelectedData = new Domain.Registration();
			this.Disabled = true;
			this.Enabled = false;
			msg = "";
		}

		public async Task Add()
		{
			Domain.Registration addData = new Domain.Registration
			{
				//Id = args.Data.Id,
				FirstName = "First",
				FamilyName = "Last",
				SpouseName = "",
				OtherNames = "",
				EMail = "test@test.fake",
				Phone = "555=1212",
				Adults = 1,
				ChildBig = 0,
				ChildSmall = 0,
				WillHelpWithMeals = false,
				CampId = BaseCampTypeSmartEnum.RvDryCampOnly,
				StatusId = BaseStatusSmartEnum.AcceptedHouseRules,
				LocationInt = BaseLocationSmartEnum.WindmillRanch
			};

			await this.Grid.AddRecord(addData);
		}

		public async Task Save()
		{
			Logger.LogDebug($"Inside {nameof(RegistrationGrid)}!{nameof(Save)}");
			if (SelectedData.Id != null)
			{
				Logger.LogDebug($"...SelectedData.Id.Value: {SelectedData.Id.Value}");


				int newId = 0;
				try
				{
					Domain.RegistrationVM vm = new Domain.RegistrationVM();
					// ToDo: the code above an below needs to be fixed once I figure out how to deal with the 3 classes in the Domain sub folder

					await Task.Delay(0);
					//newId = await svc.Create(vm);

				}
				catch (Exception)
				{
					/*
									{
										ExceptionMessage = svc.ExceptionMessage; // Log is handled in the service
										NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
									}
									AlertMsg = $"Registration created! Id={newId}";
									Logger.LogInformation(AlertMsg);
									NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.RegistrationShell);
					 */

					throw;
				}
				await this.Grid.SetRowData(SelectedData.Id, SelectedData);
				await Cancel();
			}
			else
			{
				Logger.LogDebug($"...{nameof(SelectedData.Id)} IS NULL");
			}
		}

		private string msg = "";

		public async Task Delete()
		{
			Logger.LogDebug($"Inside {nameof(RegistrationGrid)}!{nameof(Delete)}");
			if (SelectedData.Id != null)
			{
				Logger.LogDebug("ToDo: Add delete confirmation logic and actual deletion");
			}
			else
			{
				Logger.LogDebug($"...{nameof(SelectedData.Id)} IS NULL");
			}

			msg = $"{nameof(SelectedData.Id.Value)}: {SelectedData.Id.Value}; ";

			await this.Grid.DeleteRecord();
		}

		public async Task Cancel()
		{
			msg = "";
			SelectedData = new Domain.Registration() { };
			await this.Grid.ClearSelection();
		}

		private SfGrid<Domain.Registration> Grid;

		protected override async Task OnInitializedAsync()
		{
			//RegistrationLookupList = await db.PopulateRegistrationLookup();
			await GetDataWithParms();
			PopulateCampDDL();
		}

		//BaseDonationStatusFilterSmartEnum filter
		private async Task GetDataWithParms()
		{
			//BaseRegistrationSortSmartEnum sortAndDirection = BaseRegistrationSortSmartEnum.ByFirstName;
			//string sort = sortAndDirection.SqlTableColumnName + sortAndDirection.Order;
			//; smartEnumFilter.Name:{filter.Name}; sort:{sort}
			Logger.LogDebug($"Inside {nameof(RegistrationGrid)}!{nameof(GetDataWithParms)}");
			try
			{
				Registrations = await db.GetAll();  // filter, sort
				if (Registrations == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "Registrations NOT FOUND";
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
			StateHasChanged();
		}

		public void CustomizeCell(QueryCellInfoEventArgs<Domain.Registration> args)
		{
			if (args.Column.Field == nameof(Domain.Registration.LocationName))
			{
				BaseLocationSmartEnum e = BaseLocationSmartEnum.FromName(args.Data.LocationName, false);
				args.Cell.AddClass(new string[] { e.TextColor });
			}
		}

		#region PopulateDDL
		/*
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
			//Logger.LogDebug($"Inside {nameof(AddGrid)}!{nameof(OnSelect)}");
			//SelectedRegistrantName = args.ItemData.Text;

			SelectedRegistrantId = int.TryParse(args.ItemData.ID, out SelectedRegistrantId) ? SelectedRegistrantId : 0;
			await PopulateDonationDetails(SelectedRegistrantId);
		}
		*/


		private string SelectedCampTypeName = "";

		public List<Domain.CampType> CampTypeLookupList { get; set; }

		private void PopulateCampDDL()
		{
			CampTypeLookupList = new List<Domain.CampType>();
			foreach (var item in BaseCampTypeSmartEnum.List.OrderBy(o => o.Value).ToList())
			{
				CampTypeLookupList.Add(new Domain.CampType() { ID = item.Value.ToString(), Text = item.Name });
			}
		}

		#endregion

		#region ErrorHandling
		void Failure(FailureEventArgs e)
		{
			DatabaseErrorMsg = $"Error inside {nameof(RegistrationGrid)}; e.Error: {e.Error}";
			Logger.LogDebug(DatabaseErrorMsg);
			DatabaseError = true;
		}

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		#endregion

	}

}


/*
		public List<RegistrationLookup> RegistrationLookupList { get; set; }
*/


