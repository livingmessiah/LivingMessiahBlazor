﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;
using LivingMessiah.Web.Pages.Sukkot.Domain;
using LivingMessiah.Web.Pages.Sukkot.Data;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;
using LivingMessiah.Web.Pages.Sukkot.Constants;
using LivingMessiah.Web.Infrastructure;
using LivingMessiah.Web.Services;
using LivingMessiah.Web.Pages.Sukkot.Enums;
using System.Linq;

namespace LivingMessiah.Web.Pages.Sukkot.Services;

public interface ISukkotService
{
	string UserInterfaceMessage { get; set; }
	Task<vwRegistration> Details(int id, ClaimsPrincipal user, bool showPrintInstructionMessage = false);
	Task<vwRegistration> DeleteConfirmation(int id, ClaimsPrincipal user);

	Task<int> DeleteConfirmed(int id);
	Task<RegistrationSummary> Summary(int id, ClaimsPrincipal user);
	Task<IndexVM> GetRegistrationStep();
	Task<int> AddHouseRulesAgreementRecord(string email, string timeZone);
	Task<int> DeleteHouseRulesAgreementRecord(string email);
}

public class SukkotService : ISukkotService
{
	#region Constructor and DI
	private readonly ISukkotRepository db;
	private readonly ILogger Logger;
	private readonly AuthenticationStateProvider AuthenticationStateProvider;

	public SukkotService(
		ISukkotRepository sukkotRepository, ILogger<SukkotService> logger, AuthenticationStateProvider authenticationStateProvider)
	{
		db = sukkotRepository;
		Logger = logger;
		AuthenticationStateProvider = authenticationStateProvider;
	}
	#endregion

	private string LogExceptionMessage { get; set; } = "";
	public string UserInterfaceMessage { get; set; } = "";

	public async Task<RegistrationSummary> Summary(int id, ClaimsPrincipal user)
	{
		UserInterfaceMessage = "";
		Logger.LogDebug(string.Format("Inside {0} id:{1}"
			, nameof(SukkotService) + "!" + nameof(Summary), id));
		var vm = new RegistrationSummary();
		try
		{
			vm = await db.GetRegistrationSummary(id);
			if (vm is null)
			{
				UserInterfaceMessage = "Payment Summary Record NOT Found";
				Logger.LogWarning(string.Format("Inside {0} id:{1}"
					, nameof(SukkotService) + "!" + nameof(Summary), UserInterfaceMessage));
				throw new PaymentSummaryException(UserInterfaceMessage);
			}
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(Summary)}, db.{nameof(db.GetRegistrationSummary)}";
			Logger.LogError(ex, LogExceptionMessage);

			/*
			ToDo: figure this out
			I would think that if a PaymentSummaryRecordNotFoundException is thrown, that would be the end of it.
			But this catchall exception IS ALSO CALLED??? 
			I don't want this, but this will do for now so I concatenated UserInterfaceMessage with a +=
			*/
			UserInterfaceMessage += "An invalid operation occurred, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}

		if (!IsUserAuthoirized(vm.EMail!, id, user))
		{
			LogExceptionMessage = $"Inside {nameof(Summary)}, logged in user:{vm.EMail} lacks authority for id={id}";
			Logger.LogWarning(LogExceptionMessage);
			UserInterfaceMessage += "User not authorized";
			throw new UserNotAuthoirizedException(UserInterfaceMessage);
		}

		return vm;
	}


	public async Task<IndexVM> GetRegistrationStep()
	{
		UserInterfaceMessage = "";
		Logger.LogDebug(string.Format("Inside {0}"
			, nameof(SukkotService) + "!" + nameof(GetRegistrationStep)));

		var vm = new IndexVM();
		try
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			ClaimsPrincipal? user = authState.User;

			if (user.Identity!.IsAuthenticated)
			{
				if (user.Verified())
				{
					vm.UserName = user.GetUserName() ?? "?";
					vm.EmailAddress = user.GetUserEmail();

					var vw = new vwRegistrationStep();
					vw = await db.GetByEmail(vm.EmailAddress!);

					if (vw is not null)
					{
						vm.HouseRulesAgreement = new HouseRulesAgreement();
						vm.HouseRulesAgreement.Id = vw.Id;
						vm.HouseRulesAgreement.AcceptedDate = vw.HouseRulesAgreementAcceptedDate;
						vm.HouseRulesAgreement.TimeZone = vw.HouseRulesAgreementTimeZone;

						if (vw.RegistrationId is not null)
						{
							vm.RegistrationStep = new RegistrationStep();
							vm.RegistrationStep.Id = (int)vw.RegistrationId;
							vm.RegistrationStep.FirstName = vw.FirstName;
							vm.RegistrationStep.FamilyName = vw.FamilyName;
							vm.RegistrationStep.TotalDonation = vw.TotalDonation;
							vm.RegistrationStep.RegistrationFeeAdjusted = vw.RegistrationFeeAdjusted;

							vm.Status = Status.FromValue((int)vw.StatusId!);
						}
						else
						{
							vm.Status = Status.StartRegistration;
						}
					}
					else
					{
						vm.Status = Status.AgreementNotSigned;
					}
				}
				else
				{
					vm.Status = Status.EmailNotConfirmed;
				}
			}
			else
			{
				vm.Status = Status.NotAuthenticated;
			}

		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(GetRegistrationStep)}, db.{nameof(db.GetByEmail)}";
			Logger.LogError(ex, LogExceptionMessage);
			UserInterfaceMessage += "An invalid operation occurred, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}
		return vm;
	}

	public async Task<int> AddHouseRulesAgreementRecord(string email, string timeZone)
	{
		Logger.LogInformation(string.Format("Inside {0}, email:{1}, timeZone:{2}"
			, nameof(SukkotService) + "!" + nameof(AddHouseRulesAgreementRecord), email, timeZone));
		int id = 0;
		try
		{
			id = await db.InsertHouseRulesAgreement(email, timeZone);
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(AddHouseRulesAgreementRecord)}, db.{nameof(db.InsertHouseRulesAgreement)}";
			Logger.LogError(ex, LogExceptionMessage, id);
			UserInterfaceMessage += "An invalid operation occurred adding House Rules Agreement record, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}
		return id;
	}


	public async Task<int> DeleteHouseRulesAgreementRecord(string email)
	{
		int count = 0;
		try
		{
			Logger.LogInformation("Delete House Rules Agreement Record");
			count = await db.DeleteHouseRulesAgreementRecord(email);
			Logger.LogInformation(string.Format("House Rules Agreement Record deleted for email {0}; affected rows={1}", email, count));
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(DeleteHouseRulesAgreementRecord)}, {nameof(db.DeleteHouseRulesAgreementRecord)}, email={email}";
			Logger.LogError(ex, LogExceptionMessage);
			UserInterfaceMessage += "An invalid operation occurred during House Rules Agreement deletion, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}
		return count;
	}
		
	public async Task<vwRegistration> Details(int id, ClaimsPrincipal user, bool showPrintInstructionMessage = false)
	{
		var vm = new vwRegistration();
		try
		{
			vm = await db.ById(id);
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(Details)}, {nameof(db.ById)}";
			Logger.LogError(ex, LogExceptionMessage, id, showPrintInstructionMessage);
			UserInterfaceMessage += "An invalid operation occurred getting registration details, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}

		if (!IsUserAuthoirized(vm.EMail!, id, user))
		{
			LogExceptionMessage = $"Inside {nameof(Details)}, logged in user:{vm.EMail} lacks authority for id={id}";
			Logger.LogWarning(LogExceptionMessage);
			UserInterfaceMessage += "User not authorized";
			throw new UserNotAuthoirizedException(UserInterfaceMessage);
		}

		if (showPrintInstructionMessage) { vm.PayWithCheckMessage = Other.PayWithCheckModalMessage; }

		var tuple = Helper.GetAttendanceDatesArray(vm.AttendanceBitwise);
		vm.AttendanceDateList = tuple.week1;
		vm.AttendanceDateList2ndMonth = tuple.week2!;
		return vm;
	}

	public async Task<vwRegistration> DeleteConfirmation(int id, ClaimsPrincipal user)
	{
		var vm = new vwRegistration();
		try
		{
			vm = await db.ById(id);
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(DeleteConfirmation)}, {nameof(db.ById)}";
			Logger.LogError(ex, LogExceptionMessage, id);
			UserInterfaceMessage += "An invalid operation occurred getting registration details, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}

		if (!IsUserAuthoirized(vm.EMail!, id, user))
		{
			LogExceptionMessage = $"Inside {nameof(DeleteConfirmation)}, logged in user:{vm.EMail!} lacks authority for id={id}";
			Logger.LogWarning(LogExceptionMessage);
			UserInterfaceMessage += "User not authorized";
			throw new UserNotAuthoirizedException(LogExceptionMessage);
		}

		return vm;
	}

	public static string DumpDateRange(DateTime[] dateList)
	{
		if (dateList == null) { return ""; }
		string s = "";
		foreach (DateTime day in dateList)
		{
			s += day.ToString("MM/dd") + ", ";
		}
		return s;
	}


	public async Task<int> DeleteConfirmed(int id)
	{
		int count = 0;
		try
		{
			Logger.LogInformation($"Delete registration in one call");
			count = await db.Delete(id);
			Logger.LogInformation($"Registration deleted for id={id}; affected rows={count}");
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(DeleteConfirmed)}, {nameof(db.Delete)}, id={id}";
			Logger.LogError(ex, LogExceptionMessage);
			UserInterfaceMessage += "An invalid operation occurred during registration deletion, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}
		return count;
	}

	private bool AdminOrSukkotOverride(ClaimsPrincipal user)
	{
		if (user.GetRoles() == Auth0.Roles.Admin | user.GetRoles() == Auth0.Roles.Sukkot)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private bool IsUserAuthoirized(string registrationEmail, int id, ClaimsPrincipal user)
	{
		string? userEmail = user.GetUserEmail();
		if (userEmail == registrationEmail) { return true; }

		if (user.RoleHasAdminOrSukkot())
		{
			return true;
		}
		else
		{
			return false;
		}
	}

}

#region CustomExceptions Classes


public class RegistrationNotFoundException : Exception
{
	public RegistrationNotFoundException()
	{
	}
	public RegistrationNotFoundException(string message)
			: base(message)
	{
	}

	public RegistrationNotFoundException(string message, Exception inner)
			: base(message, inner)
	{
	}
}


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

public class PaymentSummaryException : Exception
{
	public PaymentSummaryException()
	{
	}
	public PaymentSummaryException(string message)
			: base(message)
	{
	}
	public PaymentSummaryException(string message, Exception inner)
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


public class StatusException : Exception
{
	public StatusException()
	{
	}
	public StatusException(string message)
			: base(message)
	{
	}
	public StatusException(string message, Exception inner)
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

