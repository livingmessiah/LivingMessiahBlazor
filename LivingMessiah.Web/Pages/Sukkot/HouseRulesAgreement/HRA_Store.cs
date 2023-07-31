using Fluxor;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using LivingMessiah.Web.Pages.Sukkot.Data;
using LivingMessiah.Web.Pages.Sukkot.SuperUser;
using LivingMessiah.Web.Pages.Sukkot.SuperUser.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.HouseRulesAgreement;

#region 1. Action
public record Form_Prep_Action(HRA_FormState HRA_FormState);
public record Add_Action(FormVM FormVM, string TimeZone);
public record ReSet_Action(FormVM FormVM, HRA_FormState HRA_FormState);
public record Delete_Registration_Action(int Id);
public record Delete_HRA_Action(int Id);

//public record Response_Message_Action(ResponseMessage MessageType, string Message);
#endregion

// 2. State
public record HRA_State
{
	public string? FullName { get; init; }
	public FormVM? FormVM { get; init; }
	public HRA_FormState? HRA_FormState { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<HRA_State>
{
	public override string GetName() => "HRA_Store";

	protected override HRA_State GetInitialState()
	{
		return new HRA_State
		{
			FormVM = new FormVM(),
			HRA_FormState = HRA_FormState.Start
		};
	}
}

// 4. Reducers
public static class Reducers
{
	[ReducerMethod]
	public static HRA_State On_Set_HRA_FormState(
	HRA_State state, Form_Prep_Action action)
	{
		return state with
		{
			HRA_FormState = action.HRA_FormState
		};
	}

	[ReducerMethod]
	public static HRA_State On_ReSet_HRA(
	HRA_State state, ReSet_Action action)
	{
		return state with
		{
			HRA_FormState = action.HRA_FormState,
			FormVM = action.FormVM
		};
	}

	// Add_HRA_Action is used by Reg. FormVM!HandleValidSubmit and [EffectMethod]  AddHra(
	[ReducerMethod]
	public static HRA_State On_Add_HRA(HRA_State state, Add_Action action)
	{
		//return state with {  HRA_EMail = action.EMail };  // why can't I just use HRA_FormVM.EMail?
		return state with { FormVM = action.FormVM };
	}

}

// 5. Effects
public class Effects
{
	#region Constructor and DI
	private readonly ILogger Logger;
	private readonly IRepository db;

	public Effects(ILogger<Effects> logger, IRepository repository, IRepositoryNoBase repositoryNoBase)
	{
		Logger = logger;
		db = repository;
	}
	#endregion

	[EffectMethod]
	public async Task AddHra(Add_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(AddHra)}; Email: {action.FormVM.EMail}";  // action.EMail
		Logger.LogDebug(string.Format("Inside {0}", inside));

		try
		{
			var sprocTuple = await db.InsertHouseRulesAgreement(action.FormVM.EMail!, action.TimeZone);

			if (sprocTuple.Item2 != 2601) // Unique Index Violation
			{
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"{sprocTuple.Item3}"));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"{LivingMessiah.Web.Pages.Sukkot.SuperUser.Constants.Effects.RepopulateMessage}"));
			}
			else
			{
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"{sprocTuple.Item3}"));
			}

		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, $"{LivingMessiah.Web.Pages.Sukkot.SuperUser.Constants.Effects.ResponseMessageFailure}."));
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
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"{LivingMessiah.Web.Pages.Sukkot.SuperUser.Constants.Effects.RepopulateMessage}"));
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, LivingMessiah.Web.Pages.Sukkot.SuperUser.Constants.Effects.ResponseMessageFailure));
		}
	}

}

// Ignore Spelling: HRA