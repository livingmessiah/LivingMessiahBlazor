using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
using Domain = LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Enums;

using LivingMessiah.Web.Services;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.DropDowns;
using System.Linq;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration
{
	public partial class RegistrationGrid
	{
		[Inject]
		public ILogger<RegistrationGrid> Logger { get; set; }

		[Inject]
		public IRegistrationRepository db { get; set; }

		//[Inject]
		//public ISecurityClaimsService SvcClaims { get; set; }

		public IEnumerable<Domain.Registration> Registrations { get; set; }
		private Boolean Check = false;
		private Boolean Disabled = true;
		private Boolean Enabled = false;

		public Domain.Registration SelectedData = new Domain.Registration();

		public void RowSelectHandler(RowSelectEventArgs<Domain.Registration> args)
		{
			/*
			AttendanceBitwise int NOT NULL,
			LodgingDaysBitwise int NOT NULL,
			
			AttendanceDateList = "",
			LodgingDateList = "",

					BaseCampTypeSmartEnum = args.Data.BaseCampTypeSmartEnum,
					BaseStatusSmartEnum = args.Data.BaseStatusSmartEnum  //,

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
					CampId = args.Data.CampId,
					StatusId = args.Data.StatusId,
					LocationInt = args.Data.LocationInt
				};

			if (args.Data.CampId != 0)
			{
				SelectedCampTypeName = BaseCampTypeSmartEnum.FromValue(args.Data.CampId).ToString();
				Logger.LogDebug($"Inside {nameof(RegistrationGrid)}!{nameof(RowSelectHandler)}; SelectedCampTypeName{nameof(SelectedCampTypeName)}");
			}

			this.Disabled = false;
			this.Enabled = true;
		}

		int i = 0;

		public void RowDeSelectHandler(RowDeselectEventArgs<Domain.Registration> args)
		{
			SelectedData = new Domain.Registration();
			this.Disabled = true;
			this.Enabled = false;
		}

		public async Task Save()
		{
			Logger.LogDebug($"Inside {nameof(RegistrationGrid)}!{nameof(Save)}");
			if (SelectedData.Id != null)
			{
				Logger.LogDebug($"...SelectedData.Id.Value: {nameof(SelectedData.Id.Value)}");
				await this.Grid.SetRowData(SelectedData.Id, SelectedData);
				await Cancel();
			}
		}

		public async Task Cancel()
		{
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


