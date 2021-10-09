using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Enums;
using LivingMessiah.Web.Services;

// ToDo: should this get converted to BaseSmartEnumDateRange
using LivingMessiah.Web.Pages.Sukkot;  // Needed for DateRangeEnum.cs


/*
using LivingMessiah.Web.Pages.Sukkot.CreateEdit;
using SukkotApi.Domain.Enums;
using LivingMessiah.Web.Pages.Sukkot.Constants;
using LivingMessiah.Web.Infrastructure;

using SukkotApi.Data;

		//[Inject]
		//public ISecurityClaimsService SvcClaims { get; set; }
*/

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services
{
	public interface IRegistrationService
	{
		string ExceptionMessage { get; set; }
		Task<RegistrationVM> GetById(int id);
		Task<int> Create(RegistrationVM registration);

		/*
				Task<vwRegistration> Details(int id, ClaimsPrincipal user, bool showPrintInstructionMessage = false);
				Task<vwRegistration> DeleteConfirmation(int id, ClaimsPrincipal user);
				Task<int> Edit(RegistrationVM registration, ClaimsPrincipal user);
				Task<int> DeleteConfirmed(int id);
				Task<RegistrationSummary> Summary(int id, ClaimsPrincipal user);
		*/
	}

	public class RegistrationService : IRegistrationService
	{
		#region Constructor and DI
		private readonly IRegistrationRepository db;
		private readonly ILogger Logger;
		private readonly ISecurityClaimsService SvcClaims;

		public RegistrationService(
			IRegistrationRepository registrationRepository, ILogger<RegistrationService> logger, ISecurityClaimsService securityClaimsService)
		{
			db = registrationRepository;
			Logger = logger;
			SvcClaims = securityClaimsService;
		}
		#endregion

		public string ExceptionMessage { get; set; } = "";

		public async Task<int> Create(RegistrationVM registration)
		{
			int newId = 0;

			try
			{
				Logger.LogInformation($"Calling {nameof(db.Create)}");
				//string email = await SvcClaims.GetEmail();	if (await SvcClaims.IsUserAuthoirized(email))	{	}

				registration.StatusEnum = BaseStatusSmartEnum.RegistrationFormCompleted;
				registration.AttendanceBitwise = GetDaysBitwise(registration.AttendanceDateList, DateRangeEnum.AttendanceDays);
				registration.LodgingDaysBitwise = GetDaysBitwise(registration.LodgingDateList, DateRangeEnum.LodgingDays);

				newId = await db.Create(DTO(registration));
				Logger.LogInformation($"Registration created for {registration.FamilyName}/{registration.EMail}; newId={newId}, registration.StatusId={registration.StatusEnum}");
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(Create)}, {nameof(db.Create)}";
				Logger.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return newId;
		}

		//LivingMessiah.Web.Pages.Sukkot DateRangeEnum
		private int GetDaysBitwise(DateTime[] dateList, DateRangeEnum dateRangeEnum)
		{
			if (dateList == null) { return 0; }

			//Logger.LogDebug($"Inside: {nameof(RegistrationService)}!{nameof(GetDaysBitwise)}, dateRangeEnum: {dateRangeEnum}");
			DateRangeLocal DateRangeLocal = DateRangeLocal.FromEnum(dateRangeEnum);

			int bitwise = 0;

			if (dateRangeEnum == DateRangeEnum.AttendanceDays)
			{
				int a = 0;
				foreach (DateTime day in dateList)
				{
					a = DateFactory.GetAttendanceBitwise(day);
					//Logger.LogDebug($"......a:{a} for day:{day}");
					bitwise = bitwise + a;
				}
			}
			else
			{
				foreach (DateTime day in dateList)
				{
					int l = 0;
					l = DateFactory.GetLodgingBitwise(day);
					//Logger.LogDebug($"......l:{l} for day:{day}");
					bitwise = bitwise + l;
				}
			}
			//Logger.LogDebug($"...bitwise: {bitwise}");
			return bitwise;
		}

		private RegistrationPOCO DTO(RegistrationVM registration)
		{
			RegistrationPOCO poco = new RegistrationPOCO
			{
				Id = registration.Id,
				FamilyName = registration.FamilyName,
				FirstName = registration.FirstName,
				SpouseName = registration.SpouseName,
				OtherNames = registration.OtherNames,
				EMail = registration.EMail,
				Phone = registration.Phone,
				Adults = registration.Adults,
				ChildBig = registration.ChildBig,
				ChildSmall = registration.ChildSmall,
				LocationEnum = registration.LocationEnum,
				CampTypeEnum = registration.CampTypeEnum,  //CampId = registration.CampTypeEnum,
				StatusEnum = registration.StatusEnum,  //StatusId = registration.StatusEnum,
				AttendanceBitwise = registration.AttendanceBitwise,
				LodgingDaysBitwise = registration.LodgingDaysBitwise,
				AssignedLodging = registration.AssignedLodging,
				LmmDonation = registration.LmmDonation,
				WillHelpWithMeals = registration.WillHelpWithMeals,
				Avitar = registration.Avitar,
				Notes = registration.Notes
			};
			return poco;
		}

		public async Task<RegistrationVM> GetById(int id)  
		{
			Logger.LogInformation($"Inside {nameof(RegistrationService)}!{nameof(GetById)}, id={id}");
			RegistrationPOCO registrationPOCO = new RegistrationPOCO();
			try
			{
				registrationPOCO = await db.GetPocoById(id);
				string email = await SvcClaims.GetEmail();

				if (await SvcClaims.IsUserAuthoirized(email))
				{
					ExceptionMessage = $"Inside {nameof(GetById)}, logged in user:{registrationPOCO.EMail} lacks authority for id={id}";
					Logger.LogWarning(ExceptionMessage);
					throw new UserNotAuthoirizedException(ExceptionMessage);
				}

				bool canOverride = await SvcClaims.AdminOrSukkotOverride();
				if (registrationPOCO.StatusEnum == BaseStatusSmartEnum.FullyPaid & !canOverride)
				{
					throw new RegistratationException("Can not edit registration that has been fully paid.");
				}

			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetById)}";
				Logger.LogError(ex, ExceptionMessage, id);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}

			//Logger.LogDebug($"...Calling {nameof(GetByIdDTO)}");
			//Logger.LogDebug($".....AttendanceDateList: {DumpDateRange(registrationPOCO.AttendanceDateList)}");
			//Logger.LogDebug($".....LodgingDateList: {DumpDateRange(registrationPOCO.LodgingDateList)}");

			return GetByIdDTO(registrationPOCO);
		}

		private RegistrationVM GetByIdDTO(RegistrationPOCO poco)
		{
			RegistrationVM registration = new RegistrationVM
			{
				Id = poco.Id,
				FamilyName = poco.FamilyName,
				FirstName = poco.FirstName,
				SpouseName = poco.SpouseName,
				OtherNames = poco.OtherNames,
				EMail = poco.EMail,
				Phone = poco.Phone,
				Adults = poco.Adults,
				ChildBig = poco.ChildBig,
				ChildSmall = poco.ChildSmall,
				CampTypeEnum = poco.CampTypeEnum,
				LocationEnum = poco.LocationEnum,
				StatusEnum = poco.StatusEnum, // poco.StatusId,
				AttendanceBitwise = poco.AttendanceBitwise,
				AttendanceDateList = poco.AttendanceDateList,
				LodgingDaysBitwise = poco.LodgingDaysBitwise,
				LodgingDateList = poco.LodgingDateList,
				AssignedLodging = poco.AssignedLodging,
				LmmDonation = poco.LmmDonation,
				WillHelpWithMeals = poco.WillHelpWithMeals,
				Avitar = poco.Avitar,
				Notes = poco.Notes
			};

			Logger.LogDebug($"Inside {nameof(RegistrationService)}!{nameof(GetByIdDTO)}");
			//Logger.LogDebug($"...registration.StatusEnum: {registration.StatusEnum}, registration.CampTypeEnum: {registration.CampTypeEnum}");
			//Logger.LogDebug($"...AttendanceDateList: {registration.AttendanceDateList}; LodgingDateList: {registration.LodgingDateList}");
			//Logger.LogDebug($"...AttendanceBitwise: {registration.AttendanceBitwise}; LodgingDaysBitwise: {registration.LodgingDaysBitwise}");
			//Logger.LogDebug($"...LocationEnum: {registration.LocationEnum}");
			return registration;
		}

		private static string DumpDateRange(DateTime[] dateList)
		{
			if (dateList == null) { return ""; }
			string s = "";
			foreach (DateTime day in dateList)
			{
				s += day.ToString("MM/dd") + ", ";
			}
			return s;
		}

		#region CustomExceptions Classes

		public class UserNotAuthoirizedException : Exception
		{
			public UserNotAuthoirizedException()
			{
			}
			public UserNotAuthoirizedException(string message)
					: base(message)
			{
			}

			public UserNotAuthoirizedException(string message, Exception inner)
					: base(message, inner)
			{
			}
		}

		public class RegistratationException : Exception
		{
			public RegistratationException()
			{
			}
			public RegistratationException(string message)
					: base(message)
			{
			}

			public RegistratationException(string message, Exception inner)
					: base(message, inner)
			{
			}
		}

		/*
		 # Notes on Exceptions
		 http://blog.abodit.com/2010/03/using-exception-data-to-add-additional-information-to-an-exception/
		 catch (RegistratationException e) when (e.Data != null)
		foreach (DictionaryEntry de in e.Data)
			Console.WriteLine("    Key: {0,-20}      Value: {1}", 
													 "'" + de.Key.ToString() + "'", de.Value);
		*/

		#endregion

	}
}