using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

using LivingMessiah.Web.Pages.Sukkot.Data;
using LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Donations;

// 1. Action
public record Add_Action(FormVM FormVM);
public record Form_Prep_Action(int RegistrationId, string? FullName);


// 2. State
public record DonationState
{
	public FormVM? FormVM { get; init; }
	public int RegistrationId { get; init; }
	public string? FullName { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<DonationState>
{
	public override string GetName() => "DonationStore";

	protected override DonationState GetInitialState()
	{
		return new DonationState
		{
			FormVM = new FormVM()
		};
	}
}


// 4. Reducers
public static class Reducers
{

	[ReducerMethod]
	public static DonationState On_Donation(DonationState state, Form_Prep_Action action)
	{
		return state with
		{
			RegistrationId = action.RegistrationId,
			FullName = action.FullName,
			FormVM = new FormVM()
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
	public async Task Add(Add_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(Add)}; RegistrationId: {action.FormVM.RegistrationId}";
		Logger.LogDebug(string.Format("Inside {0}", inside));

		try
		{
			var sprocTuple = await db.InsertRegistrationDonation(action.FormVM);
			Logger.LogDebug(string.Format("...sprocTuple.Item2=ReturnValue {0}", sprocTuple.Item2));

			if (sprocTuple.Item2 == 0) // Item2=ReturnValue
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

			//ToDo: General design question, in the exception branch of an EffectMethod,
			//      is it responsible to change which visible component to show, or showing the Toast is sufficient
			//dispatcher.Dispatch(new Set_VisibleComponent_Action(VisibleComponent.MasterList));
		}
	}

}