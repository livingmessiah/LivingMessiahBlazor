using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Sukkot.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Data;
using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Services;

public interface ISukkotAdminService
{
	string UserInterfaceMessage { get; set; }
	Task<List<vwRegistration>> GetAll(EnumsOld.RegistrationSortEnum sort, bool isAscending);
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

}
