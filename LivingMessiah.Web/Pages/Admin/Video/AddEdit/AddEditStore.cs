using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

using LivingMessiah.Web.Pages.Admin.Video.Data;
using LivingMessiah.Web.Pages.Admin.Video.Enums;
using LivingMessiah.Web.Pages.Admin.Video.Models;

namespace LivingMessiah.Web.Pages.Admin.Video.AddEdit;

// 1. Action
//public record Add_Action(FormVM FormVM);

public record Set_FormVM_Action(FormVM? FormVM);
public record Set_FormMode_To_Edit_Action(int Id);
public record Load_YouTubeFeed_Action(YouTubeFeed YouTubeFeed);  
public record Set_ShabbatWeeks_Action(List<Models.ShabbatWeek> ShabbatWeekList);
public record DB_InsertOrUpdate_Action(FormVM FormVM, Enums.FormMode? FormMode);
public record DB_Delete_Action(int Id);
public record DB_Get_Action(int Id, Enums.FormMode? FormMode);
public record DB_Populate_ShabbatWeekList();

// 2. State
public record AddEditState
{
	public Enums.FormMode? FormMode { get; init; }
	public FormVM? FormVM { get; init; }
	public YouTubeFeed? YouTubeFeed { get; init; }
	public List<Models.ShabbatWeek>? ShabbatWeekList { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<AddEditState>
{
	public override string GetName() => Constants.FluxorStores.AddEdit;

	protected override AddEditState GetInitialState()
	{
		return new AddEditState
		{
			FormVM = new FormVM()
		};
	}
}


// 4. Reducers
public static class Reducers
{
	/**/
		[ReducerMethod]
		public static AddEditState On_Set_Data_MasterList_ShabbatWeeks(
			AddEditState state, Set_ShabbatWeeks_Action action)
		{
			return state with
			{
				ShabbatWeekList = action.ShabbatWeekList
			};
		}
	
	[ReducerMethod]
	public static AddEditState On_Load_YouTubeFeed(AddEditState state, Load_YouTubeFeed_Action action)
	{
		return state with
		{
			FormMode = Enums.FormMode.Add,
			//ToDo: I think I need to change FormMV so it doesn't have a constructor.
			//FormVM = new AddEdit.FormVM(action.YouTubeFeed.Id_Zero_If_Null, action.YouTubeFeed.YouTubeId ?? "???", action.YouTubeFeed.Title ?? "???")
			YouTubeFeed = action.YouTubeFeed,
			FormVM = new AddEdit.FormVM()
		};
	}

	[ReducerMethod]
	public static AddEditState On_Set_AddEdit_FormVM(
	AddEditState state, Set_FormVM_Action action)
	{
		return state with
		{
			FormVM = action.FormVM
		};
	}

	[ReducerMethod]
	public static AddEditState OnEdit(
		AddEditState state, Set_FormMode_To_Edit_Action action)
	{
		return state with
		{
			FormMode = Enums.FormMode.Edit,
		};
	}

	[ReducerMethod]
	public static AddEditState On_Get(AddEditState state, DB_Get_Action action)
	{
		return state with { FormMode = action.FormMode };  
	}

	[ReducerMethod]
	public static AddEditState On_AddOrEdit(AddEditState state, DB_InsertOrUpdate_Action action)
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
	public async Task Get(DB_Get_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(Get)};  Action: {nameof(action.FormMode.Name)};  Id: {action.Id}";

		Logger.LogDebug(string.Format("Inside {0}", inside));
		try
		{
			AddEdit.FormVM? formVM = await db!.Get(action.Id);

			if (formVM is null)
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(formVM)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"Video Not Found; Id: {action.Id}"));
			}
			else
			{
				dispatcher.Dispatch(new Set_FormVM_Action(formVM));
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Got {formVM!.FamilyName!}"));
				//dispatcher.Dispatch(new Edit_Action(action.Id));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}


	[EffectMethod]
	public async Task InsertOrUpdate(DB_InsertOrUpdate_Action action, IDispatcher dispatcher)
	{
		if (action.FormMode is null) throw new ArgumentException("Parameter cannot be null", nameof(action.FormMode));

		string inside = $"{nameof(Effects)}!{nameof(InsertOrUpdate)}; Action: {action.FormMode.Name}";

		if (action.FormMode == Enums.FormMode.Add)
		{
			Logger.LogDebug(string.Format("Inside {0}", inside));
			try
			{
				var sprocTuple = await db.WeeklyVideoInsert(action.FormVM);
				Logger.LogDebug(string.Format("...sprocTuple.Item2=ReturnValue {0}", sprocTuple.Item2));

				if (sprocTuple.Item2 == 0)
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
			}
		}
		else
		{

			Logger.LogDebug(string.Format("Inside {0}; Id: {1}", inside, action.FormVM.Id));
			try
			{
				var sprocTuple = await db.WeeklyVideoUpdate(action.FormVM);
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success
					, $"Video Updated for id: [{action.FormVM.Id}], Affected Rows: {sprocTuple.Item1}")); 
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"{Constants.Effects.RepopulateMessage}"));
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure
					, $"{Constants.Effects.ResponseMessageFailure}. Action: {action.FormMode.Name}"));
			}
			
		}

	}

	[EffectMethod]
	public async Task PopulateShabbatWeek(DB_Populate_ShabbatWeekList action, IDispatcher dispatcher)
	{
		string inside = nameof(Effects) + "!" + nameof(PopulateShabbatWeek) + "!" + nameof(DB_Populate_ShabbatWeekList);
		Logger.LogDebug(string.Format("Inside {0}", inside));

		try
		{
			List<Models.ShabbatWeek> shabbatWeekList = await db!.GetShabbatWeekList(Constants.Effects.WeekCount);
			if (shabbatWeekList is not null)
			{
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"shabbatWeekList RowCnt: {shabbatWeekList.Count}"));
				dispatcher.Dispatch(new Set_ShabbatWeeks_Action(shabbatWeekList));

			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}


	[EffectMethod]
	public async Task Delete(DB_Delete_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(Delete)}; Id: {action.Id}";
		Logger.LogDebug(string.Format("Inside {0}; Id: {1}", inside, action.Id));
		try
		{
			var sprocTuple = await db.WeeklyVideoDelete(action.Id);
			Logger.LogDebug(string.Format("...sprocTuple.Item2=ReturnValue {0}", sprocTuple.Item2));

			if (sprocTuple.Item2 == 0)
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
		}
	}


}