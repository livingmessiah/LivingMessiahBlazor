using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Sukkot.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.ErrorLog.Domain;
using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Services;

public interface ISukkotAdminService
{
	string UserInterfaceMessage { get; set; }

	Task<List<vwRegistration>> GetAll(EnumsOld.RegistrationSortEnum sort, bool isAscending);
	Task<List<Notes>> GetNotes(EnumsOld.RegistrationSortEnum sort);
	Task<int> LogErrorTest();
	Task<List<zvwErrorLog>> GetzvwErrorLog();
	Task<int> EmptyErrorLog();
}

public class SukkotAdminService : ISukkotAdminService
{
	#region Constructor and DI
	private readonly ISukkotAdminRepository db;
	private readonly ILogger Logger;
	private readonly ISecurityClaimsService svcClaims;

	public SukkotAdminService(
		ISukkotAdminRepository dbRepository, ILogger<SukkotAdminService> logger, ISecurityClaimsService serviceClaims)
	{
		db = dbRepository;
		Logger = logger;
		svcClaims = serviceClaims;
	}
	#endregion

	public string UserInterfaceMessage { get; set; } = "";
	private string LogExceptionMessage { get; set; } = "";

	public async Task<List<vwRegistration>> GetAll(EnumsOld.RegistrationSortEnum registrationSortEnum, bool isAscending)
	{
		var vm = new List<vwRegistration>();
		//var profiler = MiniProfiler.Current;
		try
		{
			//using (profiler.Step($"profiling {nameof(db.GetAll)}"))
			//{
			vm = await db.GetAll(registrationSortEnum, isAscending);
			//}
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(SukkotAdminService)}!{nameof(GetAll)}, db.{nameof(db.GetAll)}";
			Logger.LogError(ex, LogExceptionMessage, registrationSortEnum);
			UserInterfaceMessage += "An invalid operation occurred getting list of registrations, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}
		return vm;
	}

	public async Task<List<Notes>> GetNotes(EnumsOld.RegistrationSortEnum registrationSortEnum)
	{
		Logger.LogDebug(string.Format("Inside {0} registrationSortEnum:{1}"
			, nameof(SukkotAdminService) + "!" + nameof(GetNotes), registrationSortEnum));
		
		var vm = new List<Notes>();
		try
		{
			vm = await db.GetNotes(registrationSortEnum);
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(SukkotAdminService)}, db.{nameof(db.GetNotes)}";
			Logger.LogError(ex, LogExceptionMessage);
			UserInterfaceMessage = "An invalid operation occurred getting registration notes, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}
		return vm;
	}

	public async Task<int> LogErrorTest()
	{
		int count = 0;
		try
		{
			count = await db.LogErrorTest();
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(SukkotAdminService)}!{nameof(LogErrorTest)}, db.{nameof(db.LogErrorTest)}";
			Logger.LogError(ex, LogExceptionMessage);
			LogExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(LogExceptionMessage);
		}
		return count;
	}

	public async Task<List<zvwErrorLog>> GetzvwErrorLog()
	{
		var vm = new List<zvwErrorLog>();
		try
		{
			vm = await db.GetzvwErrorLog();
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(SukkotAdminService)}!{nameof(GetzvwErrorLog)}, db.{nameof(db.GetzvwErrorLog)}";
			Logger.LogError(ex, LogExceptionMessage);
			LogExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(LogExceptionMessage);
		}
		return vm;
	}

	public async Task<int> EmptyErrorLog()
	{
		int count = 0;
		try
		{
			count = await db.EmptyErrorLog();
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(SukkotAdminService)}!{nameof(EmptyErrorLog)}, db.{nameof(db.EmptyErrorLog)}";
			Logger.LogError(ex, LogExceptionMessage);
			LogExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(LogExceptionMessage);
		}
		return count;
	}

}
