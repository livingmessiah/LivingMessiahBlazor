using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

using LivingMessiah.Web.Features.Sukkot.RegistrationSteps;
using LivingMessiah.Web.Features.Sukkot.Domain;
using LivingMessiah.Web.Features.Sukkot.Data;
using LivingMessiah.Web.Features.Sukkot.RegistrationSteps.Enums;
using LivingMessiah.Web.Features.Sukkot.Constants;
using LivingMessiah.Web.Infrastructure;
using LivingMessiah.Web.Services;
using LivingMessiah.Web.Features.Sukkot.Enums;
using System.Linq;

namespace LivingMessiah.Web.Features.Sukkot.Services;

public interface ISukkotService
{
	string UserInterfaceMessage { get; set; }
	Task<vwRegistration> Details(int id, ClaimsPrincipal user, bool showPrintInstructionMessage = false);
	Task<vwRegistration> DeleteConfirmation(int id, ClaimsPrincipal user);

	Task<int> DeleteConfirmed(int id);
	Task<RegistrationSummary> Summary(int id, ClaimsPrincipal user);
	Task<IndexVM> GetRegistrationStep();
}

public class SukkotService : ISukkotService
{
	#region Constructor and DI
	private readonly ISukkotRepositoryUsedBySukkotService db;
	private readonly ILogger Logger;
	private readonly AuthenticationStateProvider AuthenticationStateProvider;

	public SukkotService(
		ISukkotRepositoryUsedBySukkotService sukkotRepository, ILogger<SukkotService> logger, AuthenticationStateProvider authenticationStateProvider)
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
						//vm.HouseRulesAgreement = new HouseRulesAgreement();
						vm.HouseRulesAgreement = new LivingMessiah.Web.Features.Sukkot.RegistrationSteps.HouseRulesAgreement();
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
