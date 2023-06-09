using Fluxor;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser.Enums;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.AddOrEdit;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.Detail;
using LivingMessiah.Web.Pages.Sukkot.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

#region 1. Action

// 1.0 Common actions
public record Set_VisibleComponent_Action(VisibleComponent VisibleComponent);

// 1.1 GetList() actions
public record Get_List_Action();
public record Set_Data_MasterList_Action(List<Data.vwSuperUser> vwSuperUserList);

// 1.2 GetItem() actions
public record Get_EditItem_Action(int Id, Enums.FormMode? FormMode); // FormMode is always Edit
public record Set_Registration_FormVM_Action(FormVM? FormVM);
public record Edit_Action(int Id);

// 1.2.1 GetDisplayItem() actions
public record Get_DisplayItem_Action(int Id);
public record Set_DisplayVM_Action(DisplayVM? DisplayVM);

public record Display_Action(int Id);

// ToDo: "Submitting_Request" is to generic because I have two forms one for HRA and one for Registration
// 1.3 Actions related to Form Submission
public record Submitting_Request_Action(FormVM FormVM, Enums.FormMode? FormMode);

public record Add_HRA_Action(string? EMail, string TimeZone);


// 1.4 Actions related to MasterList
public record Add_Action(string? EMail); // , int StatusId 

// 1.5 Delete() actions
public record Delete_Action(int Id);
public record Delete_HRA_Action(int Id);


// 1.6 Display actions
public record Set_PageHeader_For_Index_Action(PageHeaderVM PageHeaderVM);
public record Set_PageHeader_For_Detail_Action(string Title, string Icon, string Color, int Id);

public record Response_Message_Action(ResponseMessage MessageType, string Message);  

#endregion


// 2. State
public record State
{
	public Enums.VisibleComponent? VisibleComponent { get; init; }
	public Enums.FormMode? FormMode { get; init; }
	public FormVM? FormVM { get; init; } // Consider renaming to RegistrationFormVM
	public string? HRA_EMail { get; init; } // This doesn't have a FormVM
	public DisplayVM? DisplayVM { get; init; }
	public List<Data.vwSuperUser>? vwSuperUserList { get; init; }
	public PageHeaderVM? PageHeaderVM { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<State>
{
	public override string GetName() => "RegistrationEntry";

	protected override State GetInitialState()
	{
		return new State
		{
			FormMode = null,
			VisibleComponent = Enums.VisibleComponent.MasterList,
			PageHeaderVM = Constants.GetPageHeaderForIndexVM(),
			FormVM = new FormVM(),
			HRA_EMail = string.Empty,
			DisplayVM = new DisplayVM()
		};
	}
}


// 4. Reducers
public static class Reducers
{

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
	public static State On_Set_Registration_FormVM(
	State state, Set_Registration_FormVM_Action action)
	{
		return state with
		{
			FormVM = action.FormVM
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
	public static State On_Set_DisplayVM(
	State state, Set_DisplayVM_Action action)
	{
		return state with
		{
			DisplayVM = action.DisplayVM
		};
	}

	[ReducerMethod]
	public static State On_Submitting_Request(
		State state, Submitting_Request_Action action)
	{
		return state with { FormMode = action.FormMode };
	}

	[ReducerMethod]
	public static State On_Add_HRA(
		State state, Add_HRA_Action action)
	{
		return state with { HRA_EMail = action.EMail };
	}


	[ReducerMethod]
	public static State OnAdd(
		State state, Add_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.AddEditForm,
			HRA_EMail = action.EMail,
			FormMode = Enums.FormMode.Add,
			FormVM = new FormVM()
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
	public async Task GetList(Get_List_Action action, IDispatcher dispatcher)
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
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, "Some Registrations Found"));
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
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, sprocTuple.Item3));
				//SEE NOTES ON SpecialEventsRepository 
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
	public async Task GetItem(Get_EditItem_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(GetItem)};  Action: {nameof(action.FormMode.Name)}; Id: {action.Id}";

		Logger.LogDebug(string.Format("Inside {0}", inside));
		try
		{
			AddOrEdit.FormVM? formVM = new();
			formVM = await db!.GetAddOrEditId(action.Id);

			if (formVM is null)
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(formVM)));
				//dispatcher.Dispatch(new Get_Item_Warning_Action($"Registration Not Found; Id: {action.Id}"));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"Registration Not Found; Id: {action.Id}"));
			}
			else
			{
				var tuple = Helper.GetAttendanceDatesArray(formVM!.AttendanceBitwise);
				formVM!.AttendanceDateList = tuple.week1;
				formVM!.AttendanceDateList2ndMonth = tuple.week2!;
				formVM!.Status = RegistrationSteps.Enums.Status.FromValue(formVM!.StatusId);

				dispatcher.Dispatch(new Set_Registration_FormVM_Action(formVM));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Got {formVM!.FamilyName!}"));
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
		string inside = $"{nameof(Effects)}!{nameof(AddHra)}; Email: {action.EMail}";
		Logger.LogDebug(string.Format("Inside {0}", inside));

		int id = 0;
		try
		{
			id = await db.InsertHouseRulesAgreement(action.EMail!, action.TimeZone);
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"House Rules Agreement added; id: {id}"));
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure
			, $"{Constants.Effects.ResponseMessageFailure}. Email: {action.EMail}"));
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
			DisplayVM? displayVM = new();
			displayVM = await db!.GetDisplayById(action.Id);

			if (displayVM is null)
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(displayVM)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"Registration [Display] Not Found; Id: {action.Id}"));
			}
			else
			{
				var tuple = Helper.GetAttendanceDatesArray(displayVM!.AttendanceBitwise);
				displayVM!.AttendanceDateList = tuple.week1;
				displayVM!.AttendanceDateList2ndMonth = tuple.week2!;
				Logger.LogDebug(string.Format("...FullName: {0}", displayVM!.FullName(false)));
				dispatcher.Dispatch(new Set_DisplayVM_Action(displayVM));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Got {displayVM!.FullName(false)}"));
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
	public async Task Delete(Delete_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(Delete)}; Id: {action.Id}";
		Logger.LogDebug(string.Format("Inside {0}; Id: {1}", inside, action.Id));
		try
		{
			var affectedRows = await db.Delete(action.Id);
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"Registration {action.Id} has been deleted"));
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
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}

}
