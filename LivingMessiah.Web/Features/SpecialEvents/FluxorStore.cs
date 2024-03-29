﻿using LivingMessiah.Web.Features.SpecialEvents.Enums;
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
public record Get_EditItem_Action(int Id);
public record Set_FormVM_Action(FormVM? FormVM);

public record Edit_Action(int Id);
public record Display_Action(int Id);

// 1.3 Actions related to Form Submission
public record Submitting_Request_Action(FormVM FormVM, Enums.FormMode? FormMode);

public record Add_Action();
public record Delete_Action(int Id);

public record Set_PageHeader_For_Index_Action(PageHeaderVM PageHeaderVM);
public record Set_PageHeader_For_Detail_Action(string Title, string Icon, string Color, int Id);
public record Set_Display_Item_Action(int Id);

// 1.7 Toaster stuff
public record Response_Message_Action(ResponseMessage MessageType, string Message);

#endregion

// 2. State
public record State
{
	public Enums.VisibleComponent? VisibleComponent { get; init; }

	public Enums.FormMode? FormMode { get; init; }
	public int EditItemId { get; init; }
	public FormVM? FormVM { get; init; }
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
			FormVM = new FormVM()
		};
	}
}

// 4. Reducers
public static class Reducers
{

	[ReducerMethod]
	public static State On_Set_VisibleComponent(State state, Set_VisibleComponent_Action action)
	{
		return state with { VisibleComponent = action.VisibleComponent };
	}


	[ReducerMethod]
	public static State On_Set_Data_MasterList(State state, Set_Data_MasterList_Action action)
	{
		return state with { SpecialEventList = action.vwSpecialEvent };
	}

	[ReducerMethod]
	public static State On_Get_EditItem(State state, Get_EditItem_Action action)
	{
		return state with { EditItemId = action.Id };
	}

	[ReducerMethod]
	public static State On_Set_Display_Item(State state, Set_Display_Item_Action action)
	{
		Data.vwSpecialEvent? se = new();
		se = state.SpecialEventList!.Where(w => w.Id == action.Id).SingleOrDefault();

		if (se is null) return state with { FormVM = null };

		FormVM formVM = new FormVM
		{
			Id = se!.Id,
			ShowBeginDate = se.ShowBeginDate,
			ShowEndDate = se.ShowEndDate,
			EventDate = se.EventDate,
			SpecialEventTypeId = se.SpecialEventTypeId,
			Title = se.Title,
			SubTitle = se.SubTitle,
			ImageUrl = se.ImageUrl,
			YouTubeId = se.YouTubeId,
			WebsiteUrl = se.WebsiteUrl,
			WebsiteDescr = se.WebsiteDescr,
			Description = se.Description
		};
		return state with { FormVM = formVM };
	}

	[ReducerMethod]
	public static State On_Set_FormVM(State state, Set_FormVM_Action action)
	{
		return state with { FormVM = action.FormVM };
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
			FormVM = new FormVM()
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
	private readonly ILogger? Logger;
	private readonly Data.IRepository query;
	private readonly Data.ICommands commands;

	public Effects(ILogger<Effects> logger, Data.IRepository repository, Data.ICommands commands)
	{
		Logger = logger;
		query = repository;
		this.commands = commands;
	}
	#endregion

	[EffectMethod]
	public async Task GetList(Get_List_Action action, IDispatcher dispatcher)
	{
		DateTimeOffset start = DateTimeOffset.UtcNow.AddDays(-200);
		DateTimeOffset end = DateTimeOffset.UtcNow.AddDays(+200);
		string inside = nameof(Effects) + "!" + nameof(GetList) + "!" + nameof(Get_List_Action);
		Logger!.LogDebug(string.Format("Inside {0}; Date Range:{1} to {2}", inside, start.ToString("d"), end.ToString("d")));
		try
		{
			List<Data.vwSpecialEvent> specialEvents = new();
			specialEvents = await query!.GetEventsByDateRange(start, end);


			if (specialEvents is not null)
			{
				Logger!.LogDebug(string.Format("...calling {0}, Count: {1}", nameof(Set_Data_MasterList_Action), specialEvents.Count()));
				dispatcher.Dispatch(new Set_Data_MasterList_Action(specialEvents));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, "Special Events Found")); // To Verbose
			}
			else
			{
				Logger!.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(specialEvents)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, "No Special Events Found"));
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside));
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

		if (action.FormMode == Enums.FormMode.Add)
		{
			Logger!.LogDebug("{Class}!{Method}!{Action}; DataInterface: {DataInterface}"
			, nameof(Effects), nameof(Submit), action.FormMode.Name, nameof(commands));
			try
			{
				var sprocTuple = await commands.CreateSpecialEvent(action.FormVM);

				if (sprocTuple.Item2 == 0)  // Item2:SprocReturnValue
				{
					Logger!.LogInformation("{Class}!{Method}; NewId: {NewId}; DataInterface: {DataInterface}"
					, nameof(Effects), nameof(Submit), sprocTuple.Item1, nameof(commands));
					dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"{sprocTuple.Item3}")); // Item3:ReturnMsg
				}
				else
				{
					Logger!.LogWarning("{Class}!{Method}; ReturnValue: {ReturnValue}; DataInterface: {DataInterface}"
					, nameof(Effects), nameof(Submit), sprocTuple.Item2, nameof(commands));
					dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"{sprocTuple.Item3}")); // Item3:ReturnMsg
				}

			}
			catch (Exception ex)
			{
				Logger!.LogError(ex, "catch of {Class}!{Method}; DataInterface: {DataInterface}"
				, nameof(Effects), nameof(Submit), nameof(commands));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure
									, $"{Constants.Effects.ResponseMessageFailure}. Action: {action.FormMode.Name}"));
			}
		}
		else
		{
			Logger!.LogDebug("{Class}!{Method}!{Action}; Id: {Id}; DataInterface: {DataInterface}"
			, nameof(Effects), nameof(Submit), action.FormMode.Name, action.FormVM.Id, nameof(commands));
			try
			{
				var sprocTuple = await commands.UpdateSpecialEvent(action.FormVM);

				if (sprocTuple.Item1 == 0)  // Item1:SprocReturnValue
				{
					dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success
						, $"Special Event Updated for id: [{action.FormVM.Id}], SPROC Returned Code: {sprocTuple.Item1}")); //sprocTuple.SprocReturnValue
				}
				else
				{
					Logger!.LogWarning("{Class}!{Method}; ReturnValue: {ReturnValue}; DataInterface: {DataInterface}"
					, nameof(Effects), nameof(Submit), sprocTuple.Item1, nameof(commands));
					dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"{sprocTuple.Item2}")); // Item2:ReturnMsg

				}
			}
			catch (Exception ex)
			{
				Logger!.LogError(ex, "catch of {Class}!{Method}; DataInterface: {DataInterface}"
				, nameof(Effects), nameof(Submit), nameof(commands));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure
					, $"{Constants.Effects.ResponseMessageFailure}. Action: {action.FormMode.Name}"));
			}
		}
	}


	[EffectMethod]
	public async Task GetItem(Get_EditItem_Action action, IDispatcher dispatcher)
	{
		Logger!.LogDebug("{Class}!{Method}; Id: {Id}; DataInterface: {DataInterface}"
		, nameof(Effects), nameof(GetItem), action.Id, nameof(query));
		try
		{
			FormVM? FormVM = new();
			FormVM = await query!.GetEventById(action.Id);

			if (FormVM is not null)
			{
				Logger!.LogDebug("{Class}!{Method}; Title: {Title}; DataInterface!Method: {DataInterface}!{DbMethod}"
				, nameof(Effects), nameof(GetItem), FormVM!.Title, nameof(query), nameof(query.GetEventById));
				dispatcher.Dispatch(new Set_FormVM_Action(FormVM));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Got {FormVM!.Title!}"));
			}
			else
			{
				Logger!.LogWarning("{Class}!{Method}; FormVM: {FormVM} is null; DataInterface!Method: {DataInterface}!{DbMethod}"
				, nameof(Effects), nameof(GetItem), nameof(FormVM), nameof(query), nameof(query.GetEventById));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"Special Event Not Found; Id: {action.Id}"));
				// dispatcher.Dispatch(new Set_FormVM_Action(null)); ToDo: should I make FormVM null?
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "catch of {Class}!{Method}; DataInterface: {DataInterface}"
			, nameof(Effects), nameof(GetItem), nameof(query));
			dispatcher.Dispatch(new Set_VisibleComponent_Action(VisibleComponent.MasterList));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}

	[EffectMethod]
	public async Task Delete(Delete_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(Delete)}; Id: {action.Id}";
		Logger!.LogDebug(string.Format("Inside {0}; Id: {1}", inside, action.Id));
		try
		{
			var affectedRows = await commands.RemoveSpecialEvent(action.Id);
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"Special Event {action.Id} has been deleted"));
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}

}
