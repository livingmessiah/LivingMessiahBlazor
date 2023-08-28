using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

using LivingMessiah.Web.Pages.Sukkot.SuperUser.Enums;
using LivingMessiah.Web.Pages.Sukkot.Enums;
using ParentState = LivingMessiah.Web.Pages.Sukkot.SuperUser.Index;

using System.Linq;
using LivingMessiah.Web.Data;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Detail;

// 1. Action
public record Add_Action(ReportVM ReportVM);
public record Get_Action(int Id);
public record Set_ReportVM_Action(ReportVM? ReportVM);

// 2. State
public record DetailState
{
	public ReportVM? ReportVM { get; init; } 
	public int RegistrationId { get; init; }
	public string? FullName { get; init; }
}

// 3. Feature
public class FeatureImplementation : Feature<DetailState>
{
	public override string GetName() => "DetailStore";

	protected override DetailState GetInitialState()
	{
		return new DetailState
		{
			ReportVM = new ReportVM()
		};
	}
}


// 4. Reducers
public static class Reducers
{

	[ReducerMethod]
	public static DetailState On_Set_ReportVM(DetailState state, Set_ReportVM_Action action)
	{
		return state with
		{
			ReportVM = action.ReportVM
		};
	}

}


// 5. Effects
public class Effects
{
	#region Constructor and DI
	private readonly ILogger Logger;
	private readonly IRepositoryNoBase dbNoBase;

	public Effects(ILogger<Effects> logger, IRepositoryNoBase repositoryNoBase)
	{
		Logger = logger;
		dbNoBase = repositoryNoBase;
	}
	#endregion

	[EffectMethod]
	public async Task Get(Get_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(Get)}; Id: {action.Id}";

		Logger.LogDebug(string.Format("Inside {0}", inside));
		try
		{
			ReportVM? reportVM = new();
			reportVM = await dbNoBase!.GetDisplayAndDonationsById(action.Id);

			if (reportVM is null)
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(reportVM)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"Registration [Display] Not Found; Id: {action.Id}"));
			}
			else
			{
				var tuple = Sukkot.Enums.Helper.GetAttendanceDatesArray(reportVM!.AttendanceBitwise);
				reportVM!.AttendanceDateList = tuple.week1;
				reportVM!.AttendanceDateList2ndMonth = tuple.week2!;
				Logger.LogDebug(string.Format("...FullName: {0}", reportVM!.FullName(false)));

				if (reportVM!.Donations is null)
				{
					Logger.LogDebug("...Donations is null");
				}
				else
				{
					Logger.LogDebug(string.Format("...Donations is NOT null; Count:{0}", reportVM!.Donations.Count()));
				}

				dispatcher.Dispatch(new Set_ReportVM_Action(reportVM));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Got {reportVM!.FullName(false)}"));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}

}