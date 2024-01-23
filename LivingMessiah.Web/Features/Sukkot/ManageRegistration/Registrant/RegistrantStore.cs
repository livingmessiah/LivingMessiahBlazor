using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

using LivingMessiah.Web.Features.Sukkot.ManageRegistration.Data;
using LivingMessiah.Web.Features.Sukkot.ManageRegistration.Enums;
using SukkotEnumsHelper = LivingMessiah.Web.Features.Sukkot.Enums.Helper;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.Registrant;

// 1. Action
//public record Add_Action(FormVM FormVM);
public record Get_Action(int Id, Enums.FormMode? FormMode); // FormMode is always Edit, was Get_EditItem_Action

//public record Form_Prep_Action(int RegistrationId, string? FullName);
public record Set_Registrant_FormVM_Action(Registrant.FormVM? FormVM);  // rename Form_Prep_Action?

public record Edit_Action(int Id);
public record AddOrEdit_Action(Registrant.FormVM FormVM, Enums.FormMode? FormMode);

public record Add_Registration_Action(string? EMail);


// 2. State
public record RegistrantState
{
	public Enums.FormMode? FormMode { get; init; }
	public string? HRA_EMail { get; init; } // why can't I just use HRA_FormVM.EMail?
	public FormVM? FormVM { get; init; }
	public int RegistrationId { get; init; }
	public string? FullName { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<RegistrantState>
{
	public override string GetName() => "RegistrantStore";

	protected override RegistrantState GetInitialState()
	{
		return new RegistrantState
		{
			FormVM = new FormVM()
		};
	}
}


// 4. Reducers
public static class Reducers
{
	/*
	[ReducerMethod]
	public static RegistrantState On_Donation(RegistrantState state, Form_Prep_Action action)
	{
		return state with
		{
			RegistrationId = action.RegistrationId,
			FullName = action.FullName,
			FormVM = new FormVM()
		};
	}
	*/

	[ReducerMethod]
	public static RegistrantState On_Add_Registration(
		RegistrantState state, Add_Registration_Action action)
	{
		return state with
		{
			HRA_EMail = action.EMail,
			FormMode = Enums.FormMode.Add,
			FormVM = new Registrant.FormVM()
		};
	}

	[ReducerMethod]
	public static RegistrantState On_Set_Registrant_FormVM(
	RegistrantState state, Set_Registrant_FormVM_Action action)
	{
		return state with
		{
			FormVM = action.FormVM
		};
	}

	[ReducerMethod]
	public static RegistrantState OnEdit(
		RegistrantState state, Edit_Action action)
	{
		return state with
		{
			FormMode = Enums.FormMode.Edit,
		};
	}


	[ReducerMethod]
	public static RegistrantState On_Get(RegistrantState state, Get_Action action)
	{
		return state with { FormMode = action.FormMode };  // This is always edit? 
	}

	[ReducerMethod]
	public static RegistrantState On_AddOrEdit(RegistrantState state, AddOrEdit_Action action)
	{
		return state with { FormMode = action.FormMode };
	}

}


// 5. Effects
public class Effects
{
	#region Constructor and DI
	private readonly ILogger Logger;
	private readonly IRepository db;

	public Effects(ILogger<Effects> logger, IRepository repository)
	{
		Logger = logger;
		db = repository;
	}
	#endregion


	[EffectMethod]
	public async Task Get(Get_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(Get)};  Action: {nameof(action.FormMode.Name)}; Id: {action.Id}";

		Logger.LogDebug(string.Format("Inside {0}", inside));
		try
		{
			Registrant.FormVM? formVM = new();
			formVM = await db!.Get(action.Id);

			if (formVM is null)
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(formVM)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"Registration Not Found; Id: {action.Id}"));
			}
			else
			{
				var tuple = SukkotEnumsHelper.GetAttendanceDatesArray(formVM!.AttendanceBitwise);
				formVM!.AttendanceDateList = tuple.week1;
				formVM!.AttendanceDateList2ndMonth = tuple.week2!;
				formVM!.Status = RegistrationSteps.Enums.Status.FromValue(formVM!.StatusId);

				dispatcher.Dispatch(new Set_Registrant_FormVM_Action(formVM));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Got {formVM!.FamilyName!}"));
				dispatcher.Dispatch(new Edit_Action(action.Id));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
			//dispatcher.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.MasterList));  // ToDo: does this make sense?
		}
	}


	[EffectMethod]
	public async Task Submit(AddOrEdit_Action action, IDispatcher dispatcher)
	{
		if (action.FormMode is null) throw new ArgumentException("Parameter cannot be null", nameof(action.FormMode));

		string inside = $"{nameof(Effects)}!{nameof(Submit)}; Action: {action.FormMode.Name}";

		if (action.FormMode == Enums.FormMode.Add)
		{
			Logger.LogDebug(string.Format("Inside {0}", inside));
			try
			{
				var sprocTuple = await db.CreateRegistration(action.FormVM);

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
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
				// dispatcher.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.MasterList)); // ToDo: does this make sense?
			}
		}
		else
		{
			Logger.LogDebug(string.Format("Inside {0}; Id: {1}", inside, action.FormVM.Id));
			try
			{
				var sprocTuple = await db.UpdateRegistration(action.FormVM);
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success
					, $"Registration Updated for id: [{action.FormVM.Id}], Affected Rows: {sprocTuple.Item1}")); //sprocTuple.RowsAffected
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"{Constants.Effects.RepopulateMessage}"));
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure
					, $"{Constants.Effects.ResponseMessageFailure}. Action: {action.FormMode.Name}"));
				// dispatcher.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.MasterList)); // ToDo: does this make sense?
			}
		}

	}


}