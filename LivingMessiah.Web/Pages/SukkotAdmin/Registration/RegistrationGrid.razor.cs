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
		
		public Domain.Registration SelectedData = new Domain.Registration();
		public void RowSelectHandler(RowSelectEventArgs<Domain.Registration> args)
		{
			/*
			CampId int NOT NULL,
			StatusId int NOT NULL,
			AttendanceBitwise int NOT NULL,
			LodgingDaysBitwise int NOT NULL,
			*/
			SelectedData = 
				new Domain.Registration() { 
					Id = args.Data.Id, 
					FirstName = args.Data.FirstName, 
					SpouseName = args.Data.SpouseName,
					OtherNames = args.Data.OtherNames, 
					EMail = args.Data.Email, 
					Phone = args.Data.Phone,
					Adults = args.Data.Adults,
					ChildBig - args.Data.ChildBig,
					ChildSmall - args.Data.ChildSmall,
					BaseCampTypeSmartEnum = args.Data.BaseCampTypeSmartEnum,
					BaseStatusSmartEnum = args.Data.BaseStatusSmartEnum,
					LodgingDateList


				};
			this.Disabled = false;
			this.Enabled = true;
		}
		public void RowDeSelectHandler(RowDeselectEventArgs<OrderDetails> args)
		{
			SelectedData = new OrderDetails();
			this.Disabled = true;
			this.Enabled = false;
		}

		public async Task Save()
		{
			if (SelectedData.Id != null)
			{
				await this.Grid.SetRowData(SelectedData.Id, SelectedData);
				await Cancel();
			}
		}

		public async Task Cancel()
		{
			SelectedData = new OrderDetails() { };
			await this.Grid.ClearSelection();
		}

		private SfGrid<Domain.Registration> Grid;

		protected override async Task OnInitializedAsync()
		{
			//RegistrationLookupList = await db.PopulateRegistrationLookup();
			GetDataWithParms();
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


