using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services; // using service, don't need .Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Enums;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration
{
	//[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class AddForm
	{
		[Inject]
		public IRegistrationService svc { get; set; }

		[Inject]
		public ILogger<AddForm> Logger { get; set; }

		public RegistrationVM RegistrationVM { get; set; } = new RegistrationVM();

		//ToDo this should come from the Sukkot.Constants and saved in cache		
		public Sukkot.DateRangeLocal DateRangeAttendance { get; set; } = Sukkot.DateRangeLocal.FromEnum(Sukkot.DateRangeEnum.AttendanceDays);
		public Sukkot.DateRangeLocal DateRangeLodging { get; set; } = Sukkot.DateRangeLocal.FromEnum(Sukkot.DateRangeEnum.LodgingDays);

		protected bool LoadFailed;
		protected bool CanEditCampType => RegistrationVM.LocationEnum == SukkotApi.Domain.Enums.LocationEnum.WildernessRanch;
		/*
		protected override void OnInitialized()
		{
			Logger.LogDebug($"Inside {nameof(AddForm)}!{nameof(OnInitialized)}");
			InitializeErrorHandling();
			Registration = new RegistrationVM
			{
				Id = 0,
				StatusEnum = BaseStatusSmartEnum.EmailConfirmation,
				//HouseRulesAgreement = DateTime.UtcNow, // Task 687: Persist the moment House Rules were agreed to database
				//EMail = User.GetUserEmail()  // This is an admin form, so this can be entered by the UI
			};
		}
		*/

		protected async Task HandleValidSubmit()
		{
			InitializeErrorHandling();
			Logger.LogDebug($"Inside {nameof(HandleValidSubmit)}, calling {nameof(svc.Create)})");

			const int ViolationInUniqueIndex = 2601;

			int newId = 0;
			try
			{
				RegistrationVM.Id = 0;
				RegistrationVM.StatusSmartEnum = BaseStatusSmartEnum.EmailConfirmation;
				
				var sprocTuple = await svc.Create(RegistrationVM);
				if (sprocTuple.Item1 != 0)
				{
					DatabaseInformation = true;
					DatabaseInformationMsg = $"{sprocTuple.Item3}";
					RegistrationVM = new RegistrationVM();
	}
				else
				{
					if (sprocTuple.Item2 == ViolationInUniqueIndex)
					{
						DatabaseWarning = true;
						DatabaseWarningMsg = sprocTuple.Item3;
					}
					else
					{
						DatabaseError = true;
						DatabaseErrorMsg = sprocTuple.Item3;
					}
				}

			}
			catch (Exception)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error saving to database";
				Logger.LogError($"...Logging svc returned {nameof(svc.ExceptionMessage)} message {svc.ExceptionMessage}");
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


	} // class 
} // namespace
