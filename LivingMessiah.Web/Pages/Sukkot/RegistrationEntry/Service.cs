﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.Sukkot.Enums;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry;

public interface IService
{
	string ExceptionMessage { get; set; }

	Task<ViewModel> GetById(int id);                                                                   
	Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(ViewModel registration);          
	Task<(int RowsAffected, int SprocReturnValue, string ReturnMsg)> Update(ViewModel registration);   
}

public class Service : IService
{
	#region Constructor and DI
	private readonly IRepository db;
	private readonly ILogger Logger;
	private readonly ISecurityClaimsService SvcClaims;

	public Service(
		IRepository registrationRepository,
		ILogger<Service> logger,
		ISecurityClaimsService securityClaimsService)
	{
		db = registrationRepository;
		Logger = logger;
		SvcClaims = securityClaimsService;
	}
	#endregion

	public string ExceptionMessage { get; set; } = "";
	
	public async Task<ViewModel> GetById(int id)
	{
		string message = $"Inside {nameof(Service)}!{nameof(GetById)}, id={id}";
		Logger.LogInformation(message);
		
		ViewModel VM = new();
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

	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(ViewModel vm)
	{
		Logger.LogInformation($"Inside {nameof(Service)}!{nameof(Create)}; calling {nameof(db.Create)}");
		try
		{
			//string email = await SvcClaims.GetEmail();	if (await SvcClaims.IsUserAuthoirized(email))	{	}
			vm.Status = Status.Payment;
			vm.AttendanceBitwise = Helper.GetDaysBitwise(vm.AttendanceDateList!, vm.AttendanceDateList2ndMonth!, Sukkot.Enums.DateRangeType.Attendance);

			var sprocTuple = await db.Create(DTO_From_VM_To_DB(vm));
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

	public async Task<(int RowsAffected, int SprocReturnValue, string ReturnMsg)> Update(ViewModel vm)
	{
		const string MessageUpdate = $"Inside {nameof(Service)}!{nameof(Update)}; calling {nameof(db.Update)}";
		Logger.LogInformation(MessageUpdate);
		Logger.LogDebug(string.Format("... vm.StatusId: {0}", vm.StatusId));
		try
		{
			var sprocTuple = await db.Update(DTO_From_VM_To_DB(vm));

			int rowsAffected = sprocTuple.Item1;
			int sprocReturnValue = sprocTuple.Item2;
			string returnMsg = sprocTuple.Item3;
			Logger.LogInformation($"Registration updated for {vm.FamilyName}/{vm.EMail}");
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

	private DTO DTO_From_VM_To_DB(ViewModel vm)
	{
		DTO poco = new DTO
		{
			Id = vm.Id,
			FamilyName = vm.FamilyName,
			FirstName = vm.FirstName,
			SpouseName = vm.SpouseName!.Trim(),
			OtherNames = vm.OtherNames,
			EMail = vm.EMail,
			Phone = vm.Phone,
			Adults = vm.Adults,
			ChildBig = vm.ChildBig,
			ChildSmall = vm.ChildSmall,
			StatusId = vm.Status!.Value,
			AttendanceBitwise = Helper.GetDaysBitwise(vm.AttendanceDateList!, vm.AttendanceDateList2ndMonth!, Sukkot.Enums.DateRangeType.Attendance),
			LmmDonation = vm.LmmDonation,
			Avatar = vm.Avatar,
			Notes = vm.Notes
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
