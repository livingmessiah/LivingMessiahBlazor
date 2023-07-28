using Fluxor;
using LivingMessiah.Web.Pages.Sukkot.SuperUser.Enums;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using LivingMessiah.Web.Pages.Sukkot.SuperUser.Detail;
using LivingMessiah.Web.Pages.Sukkot.Enums;
using LivingMessiah.Web.Pages.Sukkot.Data;
using System.Linq;

/*
using LivingMessiah.Web.Pages.Sukkot.SuperUser.Registrant;
*/

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser;

#region 1. Action

// 1.0 Common actions
public record Set_VisibleComponent_Action(VisibleComponent VisibleComponent);

// 1.1 GetList() actions
public record Get_List_Action();  // used by EffectMethod db!.GetAll, There is no ReducerMethod.
public record Set_Data_MasterList_Action(List<Data.vwSuperUser> vwSuperUserList);

// 1.2 GetItem() actions
public record Get_EditItem_Action(int Id, Enums.FormMode? FormMode); // FormMode is always Edit
public record Set_Registrant_FormVM_Action(Registrant.FormVM? FormVM);
public record Edit_Action(int Id);

// 1.2.1 GetDisplayItem() actions
public record Get_DisplayItem_Action(int Id);
public record Set_ReportVM_Action(ReportVM? ReportVM);

public record Display_Action(int Id);

// ToDo: "Submitting_Request" is to generic because I have two forms one for HRA and one for Registration
// 1.3 Actions related to Form Submission
public record Submitting_Request_Action(Registrant.FormVM FormVM, Enums.FormMode? FormMode);



public record Set_BypassAgreement_Action(bool BypassAgreement); // HouseRulesAgreement.FormVM FormVM 
public record Set_HRA_FormState_Action(HouseRulesAgreement.HRA_FormState HRA_FormState);

//public record Add_HRA_Action(string? EMail, string TimeZone);  // db.InsertHouseRulesAgreement, do I need this
public record Add_HRA_Action(HouseRulesAgreement.FormVM FormVM, string TimeZone);
public record ReSet_HRA_Action(HouseRulesAgreement.FormVM HRA_FormVM, HouseRulesAgreement.HRA_FormState HRA_FormState);

// 1.4 Actions related to MasterList
public record Add_Registration_Action(string? EMail);


// 1.5 Delete() actions
public record Delete_Registration_Action(int Id);
public record Delete_HRA_Action(int Id);


// 1.6 Display actions
public record Set_PageHeader_For_Index_Action(PageHeaderVM PageHeaderVM);
public record Set_PageHeader_For_Detail_Action(string Title, string Icon, string Color, int Id);
public record Set_DetailPageHeader_Action(string Label, string Value);

public record Response_Message_Action(ResponseMessage MessageType, string Message);

#endregion


// 2. State
public record State
{
	public Enums.VisibleComponent? VisibleComponent { get; init; }
	public PageHeaderVM? PageHeaderVM { get; init; }
	public DetailPageHeaderVM? DetailPageHeaderVM { get; init; }
	public Enums.FormMode? FormMode { get; init; }
	public Registrant.FormVM? RegistrantFormVM { get; init; }


	
	public int RegistrationId { get; init; }
	public string? FullName { get; init; }

	public string? HRA_EMail { get; init; } // why can't I just use HRA_FormVM.EMail?
	public HouseRulesAgreement.FormVM? HRA_FormVM { get; init; }
	public bool BypassAgreement { get; init; }
	public HouseRulesAgreement.HRA_FormState? HRA_FormState { get; init; }  
	

	public ReportVM? DetailReportVM { get; init; }
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
			VisibleComponent = Enums.VisibleComponent.MasterList,
			PageHeaderVM = Constants.GetPageHeaderForIndexVM(),
			FormMode = null,
			RegistrantFormVM = new Registrant.FormVM(),
			HRA_EMail = string.Empty, // why can't I just use HRA_FormVM.EMail?
			HRA_FormVM = new HouseRulesAgreement.FormVM(),
			BypassAgreement = true,
			HRA_FormState = HouseRulesAgreement.HRA_FormState.Start,
			DetailReportVM = new ReportVM()
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
			HRA_FormVM = action.HRA_FormVM,
			HRA_EMail = string.Empty
		};
	}

	

	[ReducerMethod]
	public static State On_Set_VisibleComponent(
		State state, Set_VisibleComponent_Action action)
	{
		return state with
		{
			VisibleComponent = action.VisibleComponent
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

	[ReducerMethod]
	public static State On_Set_Registrant_FormVM(
	State state, Set_Registrant_FormVM_Action action)
	{
		return state with
		{
			RegistrantFormVM = action.FormVM
		};
	}


	[ReducerMethod]
	public static State On_Get_EditItem(State state, Get_EditItem_Action action)
	{
		return state with { FormMode = action.FormMode };
	}

	// Why do I need this ReducerMethod if it's not changing the State?
	[ReducerMethod]
	public static State On_Get_DisplayItem_Action(State state, Get_DisplayItem_Action action)
	{
		return state;
	}

	[ReducerMethod]
	public static State On_Set_ReportVM(
	State state, Set_ReportVM_Action action)
	{
		return state with
		{
			DetailReportVM = action.ReportVM
		};
	}

	[ReducerMethod]
	public static State On_Submitting_Request(
		State state, Submitting_Request_Action action)
	{
		return state with { FormMode = action.FormMode };
	}

	// Add_HRA_Action is used by Reg. FormVM!HandleValidSubmit and [EffectMethod]  AddHra(
	[ReducerMethod]
	public static State On_Add_HRA(
		State state, Add_HRA_Action action)
	{
		//return state with {  HRA_EMail = action.EMail };  // why can't I just use HRA_FormVM.EMail?
		return state with { HRA_FormVM = action.FormVM };
	}

	[ReducerMethod]
	public static State On_Add_Registration(
		State state, Add_Registration_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.AddEditForm,
			HRA_EMail = action.EMail,
			FormMode = Enums.FormMode.Add,
			RegistrantFormVM = new Registrant.FormVM()
		};
	}


	[ReducerMethod]
	public static State OnEdit(
		State state, Edit_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.AddEditForm,
			FormMode = Enums.FormMode.Edit,
		};
	}

	[ReducerMethod]
	public static State OnDisplay(
		State state, Display_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.DisplayCard
		};
	}

	[ReducerMethod]
	public static State On_Set_PageHeader_For_Index(
	State state, Set_PageHeader_For_Index_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
			PageHeaderVM = Constants.GetPageHeaderForIndexVM()
		};
	}

	[ReducerMethod]
	public static State On_Set_PageHeader_For_Detail(
		State state, Set_PageHeader_For_Detail_Action action)
	{
		return state with
		{
			PageHeaderVM = new PageHeaderVM { Title = action.Title, Icon = action.Icon, Color = action.Color, Id = action.Id }
		};
	}

	[ReducerMethod]
	public static State On_Set_DetailPageHeader(
		State state, Set_DetailPageHeader_Action action)
	{
		return state with
		{
			DetailPageHeaderVM = new DetailPageHeaderVM { Label = action.Label, Value = action.Value }
		};
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
		dispatcher.Dispatch(new Set_VisibleComponent_Action(VisibleComponent.MasterList));

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
	public async Task Submit(Submitting_Request_Action action, IDispatcher dispatcher)
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
				dispatcher.Dispatch(new Set_VisibleComponent_Action(VisibleComponent.MasterList));
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
				dispatcher.Dispatch(new Set_VisibleComponent_Action(VisibleComponent.MasterList));
			}
		}

	}


	[EffectMethod]
	public async Task GetRegistration(Get_EditItem_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(GetRegistration)};  Action: {nameof(action.FormMode.Name)}; Id: {action.Id}";

		Logger.LogDebug(string.Format("Inside {0}", inside));
		try
		{
			Registrant.FormVM? formVM = new();
			formVM = await db!.GetAddOrEditId(action.Id);

			if (formVM is null)
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(formVM)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"Registration Not Found; Id: {action.Id}"));
			}
			else
			{
				var tuple = Helper.GetAttendanceDatesArray(formVM!.AttendanceBitwise);
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
			dispatcher.Dispatch(new Set_VisibleComponent_Action(VisibleComponent.MasterList));  // ToDo: does this make sense?
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
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
			dispatcher.Dispatch(new Set_VisibleComponent_Action(VisibleComponent.MasterList));
		}
	}

	[EffectMethod]
	public async Task GetDisplayItem(Get_DisplayItem_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(GetDisplayItem)}; Id: {action.Id}";

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
				var tuple = Helper.GetAttendanceDatesArray(reportVM!.AttendanceBitwise);
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
				dispatcher.Dispatch(new Display_Action(action.Id));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
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