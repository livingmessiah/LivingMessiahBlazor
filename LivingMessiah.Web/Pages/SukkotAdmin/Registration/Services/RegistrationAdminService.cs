using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Sukkot.Enums;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
using LivingMessiah.Web.Services;

using LivingMessiah.Web.Pages.Sukkot;  // Needed for DateRangeEnum.cs

using Syncfusion.Blazor.DropDowns;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services;

public interface IRegistrationAdminService
{
	string ExceptionMessage { get; set; }

	// Used by namespace SukkotAdmin
	Task<RegistrationVM> GetById(int id);                                                                   //        Registration\EditRegistrationForm
	Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(RegistrationVM registration);          // HouseRulesAgreement\AddRegistrationForm 
	Task<(int RowsAffected, int SprocReturnValue, string ReturnMsg)> Update(RegistrationVM registration);   //        Registration\EditRegistrationForm
}

public class RegistrationAdminService : IRegistrationAdminService
{
	#region Constructor and DI
	private readonly IRegistrationAdminRepository db;
	private readonly ILogger Logger;
	private readonly ISecurityClaimsService SvcClaims;

	public RegistrationAdminService(
		IRegistrationAdminRepository registrationRepository,
		ILogger<RegistrationAdminService> logger,
		ISecurityClaimsService securityClaimsService)
	{
		db = registrationRepository;
		Logger = logger;
		SvcClaims = securityClaimsService;
	}
	#endregion

	//ToDo: make this private
	public string ExceptionMessage { get; set; } = "";

	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(RegistrationVM registrationVM)
	{
		Logger.LogInformation($"Inside {nameof(RegistrationAdminService)}!{nameof(Create)}; calling {nameof(db.Create)}");
		try
		{
			//string email = await SvcClaims.GetEmail();	if (await SvcClaims.IsUserAuthoirized(email))	{	}
			registrationVM.Status = Status.Payment;
			registrationVM.AttendanceBitwise = Helper.GetDaysBitwise(registrationVM.AttendanceDateList!, registrationVM.AttendanceDateList2ndMonth!, Sukkot.Enums.DateRangeType.Attendance);

			int newId = 0;
			int sprocReturnValue = 0;
			string returnMsg = "";

			var sprocTuple = await db.Create(DTO_From_VM_To_DB(registrationVM));
			newId = sprocTuple.Item1;
			sprocReturnValue = sprocTuple.Item2;
			returnMsg = sprocTuple.Item3;
			return (newId, sprocReturnValue, returnMsg);
		}
		catch (Exception ex)
		{
			ExceptionMessage = $"...Error calling {nameof(db.Create)} (presumably)";
			Logger.LogError(ex, ExceptionMessage);

			// Note, the UI should NOT display this detailed Exception Message, unless maybe if Env.IsDevelopment
			ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(ExceptionMessage);
		}
	}

	private RegistrationPOCO DTO_From_VM_To_DB(RegistrationVM registration)
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
			StatusId = registration.Status!.Value,
			AttendanceBitwise = Helper.GetDaysBitwise(registration.AttendanceDateList!, registration.AttendanceDateList2ndMonth!, Sukkot.Enums.DateRangeType.Attendance),
			LmmDonation = registration.LmmDonation,
			Avatar = registration.Avatar,
			Notes = registration.Notes
		};
		return poco;
	}

	public async Task<RegistrationVM> GetById(int id)
	{
		Logger.LogInformation($"Inside {nameof(RegistrationAdminService)}!{nameof(GetById)}, id={id}");
		RegistrationPOCO registrationPOCO = new RegistrationPOCO();
		try
		{
			registrationPOCO = await db.GetPocoById(id);
			string email = await SvcClaims.GetEmail();

			if (await SvcClaims.IsUserAuthoirized(email) == false)
			{
				ExceptionMessage = $"...logged in user:{email} lacks authority for to see content of id={id} / EMail:{registrationPOCO.EMail}";
				Logger.LogWarning(ExceptionMessage);
				throw new UserNotAuthoirizedException(ExceptionMessage);
			}
			else
			{
				//Logger.LogDebug(string.Format("...registrationPOCO.StatusId: {0}", registrationPOCO.StatusId));
				return DTO_From_DB_To_VM(registrationPOCO);
			}

			/*
			ToDo: How do I want to handle this
			bool canOverride = await SvcClaims.AdminOrSukkotOverride();
			if (registrationPOCO.StatusSmartEnum == BaseStatusSmartEnum.FullyPaid & !canOverride)
			{
				throw new RegistratationException("Can not edit registration that has been fully paid.");
			}
			*/

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

	}

	private RegistrationVM DTO_From_DB_To_VM(RegistrationPOCO poco)
	{
		Logger.LogDebug(string.Format("Inside {0}"
		, nameof(RegistrationAdminService) + "!" + nameof(DTO_From_DB_To_VM)));

		var tuple = Helper.GetAttendanceDatesArray(poco.AttendanceBitwise);

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
			Status = Status.FromValue(poco.StatusId),
			AttendanceBitwise = poco.AttendanceBitwise,
			AttendanceDateList = tuple.week1,
			AttendanceDateList2ndMonth = tuple.week2,
			LmmDonation = poco.LmmDonation,
			Avatar = poco.Avatar,
			Notes = poco.Notes
		};

		return registration;
	}

	public async Task<(int RowsAffected, int SprocReturnValue, string ReturnMsg)> Update(RegistrationVM registrationVM)
	{
		const string MessageUpdate = $"Inside {nameof(RegistrationAdminService)}!{nameof(Update)}; calling {nameof(db.Update)}";
		Logger.LogInformation(MessageUpdate);

		try
		{
			var sprocTuple = await db.Update(DTO_From_VM_To_DB(registrationVM));

			int rowsAffected = sprocTuple.Item1;
			int sprocReturnValue = sprocTuple.Item2;
			string returnMsg = sprocTuple.Item3;
			Logger.LogInformation($"Registration updated for {registrationVM.FamilyName}/{registrationVM.EMail}");
			return (rowsAffected, sprocReturnValue, returnMsg);
		}
		catch (Exception ex)
		{
			ExceptionMessage = $"...Error calling {nameof(db.Update)} (presumably)";
			Logger.LogError(ex, ExceptionMessage);
			ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(ExceptionMessage);
		}

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
