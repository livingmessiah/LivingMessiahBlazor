using Fluxor;
using LivingMessiah.Web.Enums;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser.Enums;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.AddOrEdit;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.Detail;
using LivingMessiah.Web.Pages.Sukkot.Enums;
/*

Show HRA tables with not registration just like  NoRegistrationTable (SukkotAdmin.HouseRulesAgreement)
- Include a Add Button on top
- Include a Delete Button Column

Show Combo GetEmailForm and HRA Modal Agreement Button

Show Registered Table
- Populate Columns with Registrations List<TableDetails>?
- Include a Add Button on top
- Include a Edit/View/Delete Button Action Columns

*/
namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

#region 1. Action
// 1.1 GetList() actions
public record Get_List_Action();
public record Get_List_Success_Action(List<Data.vwRegistration> vwRegistrations);
public record Get_List_Warning_Action(string WarningMessage);
public record Get_List_Failure_Action(string ErrorMessage);

// 1.2 GetItem() actions
public record Get_Item_Action(int Id, Enums.FormMode? FormMode);
public record Get_Item_Success_Action(FormVM? FormVM);
public record Get_Item_Warning_Action(string WarningMessage);
public record Get_Item_Failure_Action(string ErrorMessage);
public record Edit_Action(int Id);

// 1.2.1 GetDisplayItem() actions
public record Get_DisplayItem_Action(int Id);
public record Get_DisplayItem_Success_Action(DisplayVM? DisplayVM); 
public record Get_DisplayItem_Warning_Action(string WarningMessage);
public record Get_DisplayItem_Failure_Action(string ErrorMessage);
public record Display_Action(int Id);

// 1.3 Actions related to Form Submission
public record Submitting_Request_Action(FormVM FormVM, Enums.FormMode? FormMode);
public record Submitted_Response_Success_Action(string SuccessMessage);
public record Submitted_Response_Failure_Action(string ErrorMessage);

// 1.4 Actions related to MasterList
public record Add_Action();

// 1.5 Delete() actions
public record Delete_Action(int Id);
public record DeleteSuccess_Action(string SuccessMessage);
public record DeleteFailure_Action(string ErrorMessage);

// 1.6 Display actions
public record Set_PageHeader_For_Index_Action(PageHeaderVM PageHeaderVM);
public record Set_PageHeader_For_Detail_Action(string Title, string Icon, string Color, int Id);
#endregion


// 2. State
public record State
{
	public Enums.VisibleComponent? VisibleComponent { get; init; }
	public Enums.FormMode? FormMode { get; init; }
	public string? SuccessMessage { get; init; }
	public string? WarningMessage { get; init; }
	public string? ErrorMessage { get; init; }
	public FormVM? FormVM { get; init; }
	public DisplayVM? DisplayVM { get; init; }
	public List<Data.vwRegistration>? vwRegistrationList { get; init; }
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
			SuccessMessage = string.Empty,
			WarningMessage = string.Empty,
			ErrorMessage = string.Empty,
			PageHeaderVM = Constants.GetPageHeaderForIndexVM(),
			FormVM = new FormVM(),
			DisplayVM = new DisplayVM()
		};
	}
}


// 4. Reducers
public static class Reducers
{

	[ReducerMethod]
	public static State On_Get_List_Success(
		State state, Get_List_Success_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
			WarningMessage = string.Empty,
			ErrorMessage = string.Empty,
			vwRegistrationList = action.vwRegistrations
		};
	}

	[ReducerMethod]
	public static State On_Get_List_Warning(
		State state, Get_List_Warning_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
			WarningMessage = action.WarningMessage
		};
	}

	[ReducerMethod]
	public static State On_Get_List_Failure(
		State state, Get_List_Failure_Action action)
	{
		return state with { ErrorMessage = action.ErrorMessage };
	}

	[ReducerMethod]
	public static State On_Get_Item(State state, Get_Item_Action action)
	{
		return state with { FormMode = action.FormMode };
	}

	[ReducerMethod]
	public static State On_Get_Item_Success(
		State state, Get_Item_Success_Action action)
	{
		return state with
		{
			FormVM = action.FormVM
		};
	}

	[ReducerMethod]
	public static State On_Get_Item_Failure(
			State state, Get_Item_Failure_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
			ErrorMessage = action.ErrorMessage
		};
	}

	//

	[ReducerMethod]
	public static State On_Get_DisplayItem_Action(State state, Get_DisplayItem_Action action)
	{
		return state;
	}
	
	[ReducerMethod]
	public static State On_Get_DisplayItem_Success(
		State state, Get_DisplayItem_Success_Action action)
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

	public static State On_Submitted_Response_Success(State state)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
			SuccessMessage = ""
		};
	}


	[ReducerMethod]
	public static State On_Submitted_Response_Failure(
			State state, Submitted_Response_Failure_Action action)
	{
		return state with
		{
			ErrorMessage = action.ErrorMessage,
			VisibleComponent = Enums.VisibleComponent.MasterList
		};
	}


	[ReducerMethod]
	public static State OnAdd(
		State state, Add_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.AddEditForm,
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
	public static State OnDelete(
		State state, Delete_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
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
		try
		{
			List<Data.vwRegistration> vwRegistrations = new();
			vwRegistrations = await db!.GetAll();

			if (vwRegistrations is not null)
			{
				dispatcher.Dispatch(new Get_List_Success_Action(vwRegistrations));
			}
			else
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(vwRegistrations)));
				dispatcher.Dispatch(new Get_List_Warning_Action("No Registrations Found"));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Get_List_Failure_Action("An invalid operation occurred, contact your administrator."));
		}
	}

	[EffectMethod]
	public async Task GetItem(Get_Item_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(GetItem)};  Action: {nameof(action.FormMode.Name)}; Id: {action.Id}";

		Logger.LogDebug(string.Format("Inside {0}", inside));
		try
		{
			Data.vwRegistration? vwRegistration = new();
			vwRegistration = await db!.GetById(action.Id);

			if (vwRegistration is null)
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(vwRegistration)));
				dispatcher.Dispatch(new Get_Item_Warning_Action($"Registration Not Found; Id: {action.Id}"));
			}
			else
			{
				Logger.LogDebug(string.Format("...FamilyName: {0}", vwRegistration!.FamilyName));
				FormVM formVM = new FormVM
				{
					Id = vwRegistration.Id,
					FamilyName = vwRegistration.FamilyName,
					FirstName = vwRegistration.FirstName,
					SpouseName = vwRegistration.SpouseName,
					OtherNames = vwRegistration.OtherNames,
					EMail = vwRegistration.EMail,
					Phone = vwRegistration.Phone,
					Adults = vwRegistration.Adults,
					ChildBig = vwRegistration.ChildBig,
					ChildSmall = vwRegistration.ChildSmall,
					StatusId = vwRegistration.StatusId
				};

				dispatcher.Dispatch(new Get_Item_Success_Action(formVM));
				dispatcher.Dispatch(new Edit_Action(action.Id));

				/*
				if (action.FormMode == Enums.FormMode.Edit)
				{
					dispatcher.Dispatch(new Edit_Action(action.Id));
				}
				else
				{
					dispatcher.Dispatch(new Display_Action(action.Id));
				}
				*/
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Get_Item_Failure_Action("An invalid operation occurred, contact your administrator"));
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
				dispatcher.Dispatch(new Get_DisplayItem_Warning_Action($"Registration [Display] Not Found; Id: {action.Id}"));
			}
			else
			{
				var tuple = Helper.GetAttendanceDatesArray(displayVM!.AttendanceBitwise);
				displayVM!.AttendanceDateList = tuple.week1;
				displayVM!.AttendanceDateList2ndMonth = tuple.week2!;
				Logger.LogDebug(string.Format("...FullName: {0}", displayVM!.FullName(false)));
				dispatcher.Dispatch(new Get_DisplayItem_Success_Action(displayVM));  //displayVM!.FullName(false)
				dispatcher.Dispatch(new Display_Action(action.Id));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Get_DisplayItem_Failure_Action("An invalid operation occurred, contact your administrator"));
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
			dispatcher.Dispatch(new DeleteSuccess_Action($"Registration {action.Id} has been deleted"));
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new DeleteFailure_Action($"An invalid operation occurred, contact your administrator"));
		}
	}
}
