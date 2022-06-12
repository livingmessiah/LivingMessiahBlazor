using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SukkotApi.Data;
using Microsoft.Extensions.Logging;
using SukkotApi.Domain;
using SukkotApi.Domain.Registrations.Enums;

namespace LivingMessiah.Web.Services;

public interface ISukkotAdminService
{
	string UserInterfaceMessage { get; set; }

	Task<List<vwRegistration>> GetAll(RegistrationSortEnum sort);
	Task<List<Notes>> GetNotes(RegistrationSortEnum sort);

	Task<int> LogErrorTest();
	Task<List<zvwErrorLog>> GetzvwErrorLog();
	Task<int> EmptyErrorLog();

	Task<int> MealTicketPunchInsert(MealTicketPunchLog mealTicketPunchLog);
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

	public async Task<List<vwRegistration>> GetAll(RegistrationSortEnum sort)
	{
		var vm = new List<vwRegistration>();
		//var profiler = MiniProfiler.Current;
		try
		{
			//using (profiler.Step($"profiling {nameof(db.GetAll)}"))
			//{
			vm = await db.GetAll(sort);
			//}
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(SukkotAdminService)}!{nameof(GetAll)}, db.{nameof(db.GetAll)}";
			Logger.LogError(ex, LogExceptionMessage, sort);
			LogExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(LogExceptionMessage);
		}
		return vm;
	}

	public async Task<List<Notes>> GetNotes(RegistrationSortEnum registrationSortEnum)
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


	public async Task<int> MealTicketPunchInsert(MealTicketPunchLog mealTicketPunchLog)
	{
		int count = 0;
		try
		{
			count = await db.MealTicketPunchInsert(mealTicketPunchLog);
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(SukkotAdminService)}!{nameof(MealTicketPunchInsert)}, db.{nameof(db.MealTicketPunchInsert)}";
			Logger.LogError(ex, LogExceptionMessage); // , donation.ToString()
			LogExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(LogExceptionMessage);
		}
		return count;
	}

}
