using Microsoft.Extensions.Logging;

using System.Threading.Tasks;
using System;
using System.Collections.Generic;

using LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Data;
using LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Enums;
using ParentState = LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Index;

namespace LivingMessiah.Web.Pages.Sukkot.ManageRegistration.MasterDetail;

// 1. Action
public record Set_Data_MasterList_Action(List<ManageRegistrationQuery> RegistrationList);
public record GetAll_Action();

// 2. State
public record MasterDetailState
{
	public List<ManageRegistrationQuery>? RegistrationList { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<MasterDetailState>
{
	public override string GetName() => "MasterListStore";

	protected override MasterDetailState GetInitialState()
	{
		return new MasterDetailState
		{
			//RegistrationList = new List<Data.ManageRegistrationQuery>()  // This prevents MasterList!OnInitialized from Dispatcher!.Dispatch(new GetAll_Action());
			//,	Dispatcher!.Dispatch(new GetAll_Action())
		};
	}
}


// 4. Reducers
public static class Reducers
{

	[ReducerMethod]
	public static MasterDetailState On_Set_Data_MasterList(
		MasterDetailState state, Set_Data_MasterList_Action action)
	{
		return state with
		{
			RegistrationList = action.RegistrationList
		};
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
	public async Task GetAll(GetAll_Action action, IDispatcher dispatcher) // action is never used
	{
		string inside = nameof(Effects) + "!" + nameof(GetAll) + "!" + nameof(GetAll_Action);

		Logger.LogDebug(string.Format("Inside {0}", inside));
		dispatcher.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.MasterList));

		try
		{
			List<ManageRegistrationQuery> vw = new();
			vw = await db!.GetAll();

			if (vw is not null)
			{
				dispatcher.Dispatch(new Set_Data_MasterList_Action(vw));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, "Some Registrations Found"));
			}
			else
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(vw)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, "No Registrations Found")); 
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}

		/*
		Question: 
			regardless if vw ends up having data, no data, or an exception occurred, 
			should I always do a dispatch Set_Data_MasterList_Action(vw) ??

		finally 
		{
			dispatcher.Dispatch(new Set_Data_MasterList_Action(vw));
		}
		*/
	}

}