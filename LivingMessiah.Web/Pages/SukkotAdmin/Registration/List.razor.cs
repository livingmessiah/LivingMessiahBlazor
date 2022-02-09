using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
//using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using Domain = LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using LivingMessiah.Web.Pages.Sukkot;
using Syncfusion.Blazor.Grids;
using LivingMessiah.Web.Pages.SukkotAdmin.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration;

//[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class List
{
		[Inject]
		public ILogger<RegistrationGrid> Logger { get; set; }

		[Inject]
		public IRegistrationRepository db { get; set; }

		//[Inject]
		//public IRegistrationService svc { get; set; }

		public IEnumerable<Domain.Registration> Registrations { get; set; }

		private SfGrid<Domain.Registration> Grid;

		/*
		private bool Check = false;
		private bool Disabled = true;
		private bool Enabled = false;
		*/

		protected override async Task OnInitializedAsync()
		{
				Logger.LogDebug($"Inside {nameof(List)}!{nameof(OnInitializedAsync)}");
				try
				{
						Registrations = await db.GetAll();
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

		private void InitializeErrorHandling()
		{
				DatabaseInformationMsg = "";
				DatabaseInformation = false;
				DatabaseWarningMsg = "";
				DatabaseWarning = false;
				DatabaseErrorMsg = "";
				DatabaseError = false;
		}

		protected bool DatabaseInformation = false;
		protected string DatabaseInformationMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
		protected string DatabaseErrorMsg { get; set; }
		#endregion


}
