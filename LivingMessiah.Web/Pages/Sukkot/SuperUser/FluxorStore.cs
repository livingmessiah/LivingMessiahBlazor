using Fluxor;
using LivingMessiah.Web.Pages.Sukkot.SuperUser.Enums;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using LivingMessiah.Web.Pages.Sukkot.Enums;
using LivingMessiah.Web.Pages.Sukkot.Data;
using System.Linq;
using ParentState = LivingMessiah.Web.Pages.Sukkot.SuperUser.Index;

//using LivingMessiah.Web.Pages.Sukkot.SuperUser.Index;
//using LivingMessiah.Web.Pages.Sukkot.SuperUser.Detail;

/*
using LivingMessiah.Web.Pages.Sukkot.SuperUser.Registrant;
*/

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser;

#region 1. Action

// 1.0 Common actions


// 1.1 GetList() actions
public record Get_List_Action();  // used by EffectMethod db!.GetAll, There is no ReducerMethod.
public record Set_Data_MasterList_Action(List<Data.vwSuperUser> vwSuperUserList);

// ToDo: "Submitting_Request" is to generic because I have two forms one for HRA and one for Registration
// 1.3 Actions related to Form Submission


public record Set_BypassAgreement_Action(bool BypassAgreement); // HouseRulesAgreement.FormVM FormVM 
public record Set_HRA_FormState_Action(HouseRulesAgreement.HRA_FormState HRA_FormState);

//public record Add_HRA_Action(string? EMail, string TimeZone);  // db.InsertHouseRulesAgreement, do I need this
public record Add_HRA_Action(HouseRulesAgreement.FormVM FormVM, string TimeZone);
public record ReSet_HRA_Action(HouseRulesAgreement.FormVM HRA_FormVM, HouseRulesAgreement.HRA_FormState HRA_FormState);

// 1.4 Actions related to MasterList


// 1.5 Delete() actions
public record Delete_Registration_Action(int Id);
public record Delete_HRA_Action(int Id);


public record Response_Message_Action(ResponseMessage MessageType, string Message);

#endregion


// 2. State
public record State
{
	
	
	public string? FullName { get; init; }

	
	public HouseRulesAgreement.FormVM? HRA_FormVM { get; init; }
	public bool BypassAgreement { get; init; }
	public HouseRulesAgreement.HRA_FormState? HRA_FormState { get; init; }  
	

	
	public List<Data.vwSuperUser>? vwSuperUserList { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<State>
{
	public override string GetName() => "SuperUser";

	protected override State GetInitialState()
	{
		return new State
		{
			HRA_FormVM = new HouseRulesAgreement.FormVM(),
			BypassAgreement = true,
			HRA_FormState = HouseRulesAgreement.HRA_FormState.Start
			
		};
	}
}


// 4. Reducers
public static class Reducers
{
	[ReducerMethod]
	public static State On_Set_BypassAgreement(
	State state, Set_BypassAgreement_Action action)
	{
		return state with
		{
			BypassAgreement = action.BypassAgreement
		};
	}


	[ReducerMethod]
	public static State On_Set_HRA_FormState(
	State state, Set_HRA_FormState_Action action)
	{
		return state with
		{
			HRA_FormState = action.HRA_FormState
		};
	}

	[ReducerMethod]
	public static State On_ReSet_HRA(
	State state, ReSet_HRA_Action action)
	{
		return state with
		{
			HRA_FormState = action.HRA_FormState,
			HRA_FormVM = action.HRA_FormVM
			//HRA_EMail = string.Empty
		};
	}


	[ReducerMethod]
	public static State On_Set_Data_MasterList(
		State state, Set_Data_MasterList_Action action)
	{
		return state with
		{
			vwSuperUserList = action.vwSuperUserList
		};
	}



	// Add_HRA_Action is used by Reg. FormVM!HandleValidSubmit and [EffectMethod]  AddHra(
	[ReducerMethod]
	public static State On_Add_HRA(
		State state, Add_HRA_Action action)
	{
		//return state with {  HRA_EMail = action.EMail };  // why can't I just use HRA_FormVM.EMail?
		return state with { HRA_FormVM = action.FormVM };
	}









}

// 5. Effects
public class Effects
{
	#region Constructor and DI
	private readonly ILogger Logger;
	private readonly IRepository db;
	private readonly IRepositoryNoBase dbNoBase;

	public Effects(ILogger<Effects> logger, IRepository repository, IRepositoryNoBase repositoryNoBase)
	{
		Logger = logger;
		db = repository;
		dbNoBase = repositoryNoBase;
	}
	#endregion

	[EffectMethod]
	public async Task GetList(Get_List_Action action, IDispatcher dispatcher) // action is never used
	{
		string inside = nameof(Effects) + "!" + nameof(GetList) + "!" + nameof(Get_List_Action);

		Logger.LogDebug(string.Format("Inside {0}", inside));
		dispatcher.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.MasterList));

		try
		{
			List<Data.vwSuperUser> vwSuperUserList = new();
			vwSuperUserList = await db!.GetAll();

			if (vwSuperUserList is not null)
			{
				dispatcher.Dispatch(new Set_Data_MasterList_Action(vwSuperUserList));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, "Some Registrations Found"));
			}
			else
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(vwSuperUserList)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, "No Registrations Found"));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			//dispatcher.Dispatch(new Get_List_Failure_Action("An invalid operation occurred, contact your administrator."));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}

		/*
		Question: 
			regardless if vwSuperUserList ends up having data, no data, or an exception occurred, 
			should I always do a dispatch Set_Data_MasterList_Action(vwSuperUserList) ??

		finally 
		{
			dispatcher.Dispatch(new Set_Data_MasterList_Action(vwSuperUserList));
		}
		*/
	}



	[EffectMethod]
	public async Task AddHra(Add_HRA_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(AddHra)}; Email: {action.FormVM.EMail}";  // action.EMail
		Logger.LogDebug(string.Format("Inside {0}", inside));

		try
		{
			var sprocTuple = await db.InsertHouseRulesAgreement(action.FormVM.EMail!, action.TimeZone);

			if (sprocTuple.Item2 != 2601) // Unique Index Violation
			{
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"{sprocTuple.Item3}"));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"{Constants.Effects.RepopulateMessage}"));
			}
			else
			{
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"{sprocTuple.Item3}"));
			}

		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, $"{Constants.Effects.ResponseMessageFailure}."));
		}
	}



	[EffectMethod]
	public async Task DeleteHRA(Delete_HRA_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(DeleteHRA)}; Id: {action.Id}";
		Logger.LogDebug(string.Format("Inside {0}; Id: {1}", inside, action.Id));
		try
		{
			var affectedRows = await db.DeleteHRA(action.Id);
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"House Rules Agreement {action.Id} has been deleted"));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"{Constants.Effects.RepopulateMessage}"));
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}




}


// Ignore Spelling: HRA