using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Sukkot.Enums;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services;

public interface IRegistrationAdminService
{
	string ExceptionMessage { get; set; }

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

	public string ExceptionMessage { get; set; } = "";
	
	public async Task<RegistrationVM> GetById(int id)
	{
		string message = $"Inside {nameof(RegistrationAdminService)}!{nameof(GetById)}, id={id}";
		Logger.LogInformation(message);
		
		RegistrationVM VM = new();
		try
		{
			VM = await db.GetById(id);
			string email = await SvcClaims.GetEmail();
			VM.Status = Status.FromValue(VM.StatusId);

			var (week1, week2) = Helper.GetAttendanceDatesArray(VM.AttendanceBitwise);
			VM.AttendanceDateList = week1;
			VM.AttendanceDateList2ndMonth = week2;

			if (await SvcClaims.IsUserAuthoirized(email) == false)
			{
				ExceptionMessage = $"...logged in user:{email} lacks authority for to see content of id={id} / EMail:{VM.EMail}";
				Logger.LogWarning(ExceptionMessage);
				throw new UserNotAuthoirizedException(ExceptionMessage);
			}
			else
			{
				//Logger.LogDebug(string.Format("...VM.StatusId: {0}", VM.StatusId));
				return VM;
			}
			// Footnote 1: 
		}
		catch (Exception ex)
		{
			ExceptionMessage = $"Inside {nameof(GetById)}";
			Logger.LogError(ex, ExceptionMessage, id);
			ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(ExceptionMessage);
		}
	}

	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(RegistrationVM registrationVM)
	{
		Logger.LogInformation($"Inside {nameof(RegistrationAdminService)}!{nameof(Create)}; calling {nameof(db.Create)}");
		try
		{
			//string email = await SvcClaims.GetEmail();	if (await SvcClaims.IsUserAuthoirized(email))	{	}
			registrationVM.Status = Status.Payment;
			registrationVM.AttendanceBitwise = Helper.GetDaysBitwise(registrationVM.AttendanceDateList!, registrationVM.AttendanceDateList2ndMonth!, Sukkot.Enums.DateRangeType.Attendance);

			var sprocTuple = await db.Create(DTO_From_VM_To_DB(registrationVM));
			int newId = sprocTuple.Item1;
			int sprocReturnValue = sprocTuple.Item2;
			string returnMsg = sprocTuple.Item3;

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

	private Sukkot.Domain.RegistrationPOCO DTO_From_VM_To_DB(RegistrationVM registration)
	{
		Sukkot.Domain.RegistrationPOCO poco = new Sukkot.Domain.RegistrationPOCO
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


/*
Footnote: ToDo: How do I want to handle this
bool canOverride = await SvcClaims.AdminOrSukkotOverride();
if (registrationPOCO.StatusSmartEnum == BaseStatusSmartEnum.FullyPaid & !canOverride)
{
	throw new RegistratationException("Can not edit registration that has been fully paid.");
}
*/
