using LivingMessiah.Web.Features.SpecialEvents.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Features.SpecialEvents;

#region 1. Action

public record Set_VisibleComponent_Action(VisibleComponent VisibleComponent);

// 1.1 GetList() actions
public record Get_List_Action();
public record Set_Data_MasterList_Action(List<Data.vwSpecialEvent> vwSpecialEvent);

// 1.2 GetItem() actions
public record Get_Item_Action(int Id, Enums.FormMode? FormMode);
public record Set_FormVM_Action(FormVM? FormVM);

public record Edit_Action(int Id);
public record Display_Action(int Id);

// 1.3 Actions related to Form Submission
public record Submitting_Request_Action(FormVM FormVM, Enums.FormMode? FormMode);

// 1.4 Actions related to MasterList
public record Add_Action();

// 1.5 Delete() actions
public record Delete_Action(int Id);

// 1.6 Delete() actions
public record Set_PageHeader_For_Index_Action(PageHeaderVM PageHeaderVM);
public record Set_PageHeader_For_Detail_Action(string Title, string Icon, string Color, int Id);


// 1.7 Toaster stuff
public record Response_Message_Action(ResponseMessage MessageType, string Message);

#endregion

// 2. State
public record State
{
	public Enums.VisibleComponent? VisibleComponent { get; init; }
	public Enums.FormMode? FormMode { get; init; }
	public FormVM? FormVM { get; init; }
	public EditMarkdownVM? EditMarkdownVM { get; init; }
	public List<Data.vwSpecialEvent>? SpecialEventList { get; init; }
	public PageHeaderVM? PageHeaderVM { get; init; }
}

// 3. Feature  
public class FeatureImplementation : Feature<State>
{
	public override string GetName() => "SpecialEvents";

	protected override State GetInitialState()
	{
		return new State
		{
			FormMode = null,
			VisibleComponent = Enums.VisibleComponent.MasterList,
			PageHeaderVM = Constants.GetPageHeaderForIndexVM(),
			FormVM = new FormVM(),
			EditMarkdownVM = new EditMarkdownVM()
		};
	}
}

// 4. Reducers
public static class Reducers
{

	[ReducerMethod]
	public static State On_Set_VisibleComponent(State state, Set_VisibleComponent_Action action)
	{
		return state with {	VisibleComponent = action.VisibleComponent	};
	}


	[ReducerMethod]
	public static State On_Set_Data_MasterList(State state, Set_Data_MasterList_Action action)
	{
		return state with	{	SpecialEventList = action.vwSpecialEvent};
	}

	//ToDo: Get_Item_Action should be split into Get_EditItem_Action and Get_DisplayItem_Action 
	[ReducerMethod]
	public static State On_Get_Item(State state, Get_Item_Action action)
	{
		return state with { FormMode = action.FormMode };
	}

	/*
	[ReducerMethod]
	public static State On_Set_DisplayVM(State state, Set_DisplayVM_Action action)
	{
		return state with	{	DisplayVM = action.DisplayVM	};
	}
	*/

	[ReducerMethod]
	public static State On_Set_FormVM(State state, Set_FormVM_Action action)
	{
		return state with {	FormVM = action.FormVM };
	}


	// Called by Form.HandleValidSubmit; Step 1
	[ReducerMethod]
	public static State On_Submitting_Request(State state, Submitting_Request_Action action)
	{
		return state with { FormMode = action.FormMode };
	}

	[ReducerMethod]
	public static State OnAdd(State state, Add_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.AddEditForm,
			FormMode = Enums.FormMode.Add,
			FormVM = new FormVM(),
			EditMarkdownVM = new EditMarkdownVM()
		};
	}

	[ReducerMethod]
	public static State OnEdit(State state, Edit_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.AddEditForm,
			FormMode = Enums.FormMode.Edit,
		};
	}

	[ReducerMethod]
	public static State OnDisplay(State state, Display_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.DisplayCard
		};
	}

	[ReducerMethod]
	public static State OnDelete(State state, Delete_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
		};
	}

	[ReducerMethod]
	public static State On_Set_PageHeader_For_Index(State state, Set_PageHeader_For_Index_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
			PageHeaderVM = Constants.GetPageHeaderForIndexVM()
		};
	}


	[ReducerMethod]
	public static State On_Set_PageHeader_For_Detail(State state, Set_PageHeader_For_Detail_Action action)
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
	private readonly Data.IRepository db;

	public Effects(ILogger<Effects> logger, Data.IRepository repository)
	{
		Logger = logger;
		db = repository;
	}
	#endregion

	[EffectMethod]
	public async Task GetList(Get_List_Action action, IDispatcher dispatcher)
	{
		string inside = nameof(Effects) + "!" + nameof(GetList) + "!" + nameof(Get_List_Action);
		Logger.LogDebug(string.Format("Inside {0}; Date Range:{1} to {2}", inside, Constants.DateRange.Start, Constants.DateRange.End));
		try
		{
			List<Data.vwSpecialEvent> specialEvents = new();
			specialEvents = await db!.GetEventsByDateRange(DateTime.Parse(Constants.DateRange.Start), DateTime.Parse(Constants.DateRange.End));

			if (specialEvents is not null)
			{
				Logger.LogDebug(string.Format("...calling {0}, Count: {1}", nameof(Set_Data_MasterList_Action), specialEvents.Count()));
				dispatcher.Dispatch(new Set_Data_MasterList_Action(specialEvents));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, "Special Events Found")); // To Verbose
			}
			else
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(specialEvents)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, "No Special Events Found"));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
		/*
		Question: 
			regardless if vwSpecialEvent ends up having data, no data, or an exception occurred, 
			should I always do a dispatch Set_Data_MasterList_Action(specialEvents) ??

		finally 
		{
			dispatcher.Dispatch(new Set_Data_MasterList_Action(specialEvents));
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
				var sprocTuple = await db.CreateSpecialEvent(action.FormVM);
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, sprocTuple.Item3));
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure
									, $"{Constants.Effects.ResponseMessageFailure}. Action: {action.FormMode.Name}"));
			}
		}
		else
		{
			Logger.LogDebug(string.Format("Inside {0}; Id: {1}", inside, action.FormVM.Id));
			try
			{
				var sprocTuple = await db.UpdateSpecialEvent(action.FormVM);
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success
					, $"Special Event Updated for id: [{action.FormVM.Id}], Affected Rows: {sprocTuple.Item1}")); //sprocTuple.RowsAffected
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure
					, $"{Constants.Effects.ResponseMessageFailure}. Action: {action.FormMode.Name}"));
			}
		}

	}


	//ToDo: Get_Item_Action should be split into Get_EditItem_Action and Get_DisplayItem_Action

	[EffectMethod]
	public async Task GetItem(Get_Item_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(GetItem)};  Action: {nameof(action.FormMode.Name)}; Id: {action.Id}";

		Logger.LogDebug(string.Format("Inside {0}", inside));
		try
		{
			Data.vwSpecialEvent? specialEvent = new();
			specialEvent = await db!.GetEventById(action.Id);

			if (specialEvent is null)
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(specialEvent)));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"Special Event Not Found; Id: {action.Id}"));
				// dispatcher.Dispatch(new Set_FormVM_Action(null)); ToDo: should I make FormVM null?
			}
			else
			{
				Logger.LogDebug(string.Format("...Title: {0}", specialEvent!.Title));
				FormVM formVM = new FormVM
				{
					Id = specialEvent.Id,
					ShowBeginDate = specialEvent.ShowBeginDate,
					ShowEndDate = specialEvent.ShowEndDate,
					EventDate = specialEvent.EventDate,
					SpecialEventTypeId = specialEvent.SpecialEventTypeId,
					Title = specialEvent.Title,
					SubTitle = specialEvent.SubTitle,
					ImageUrl = specialEvent.ImageUrl,
					YouTubeId = specialEvent.YouTubeId,
					WebsiteUrl = specialEvent.WebsiteUrl,
					WebsiteDescr = specialEvent.WebsiteDescr,
					Description = specialEvent.Description
				};

				dispatcher.Dispatch(new Set_FormVM_Action(formVM));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Got {formVM!.Title!}"));

				if (action.FormMode == Enums.FormMode.Edit)
				{
					dispatcher.Dispatch(new Edit_Action(action.Id));
				}
				else
				{
					dispatcher.Dispatch(new Display_Action(action.Id));
				}
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Set_VisibleComponent_Action(VisibleComponent.MasterList));
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
			var affectedRows = await db.RemoveSpecialEvent(action.Id);
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"Special Event {action.Id} has been deleted"));
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}

}
