using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

using LivingMessiah.Web.Pages.Admin.VideoMasterDetail.Data;
using LivingMessiah.Web.Pages.Admin.VideoMasterDetail.Enums;
using ParentState = LivingMessiah.Web.Pages.Admin.VideoMasterDetail.Index;
using WVT = LivingMessiah.Web.SmartEnums;
using AV = LivingMessiah.Web.Pages.Admin.AudioVisual;
using AVSvc = LivingMessiah.Web.Pages.Admin.AudioVisual.Services;
using LivingMessiah.Web.Pages.Sukkot.Services;


namespace LivingMessiah.Web.Pages.Admin.VideoMasterDetail.AddEdit;

// 1. Action
//public record Add_Action(FormVM FormVM);
public record DB_Populate_ShabbatWeekList();

public record Set_FormVM_Action(FormVM? FormVM);
public record Edit_Action(int Id);
public record DB_AddOrEdit_Action(FormVM FormVM, Enums.FormMode? FormMode);
public record Load_FormVM_Action(string? YouTubeId, string? Title);
public record Set_ShabbatWeeks_Action(List<AV.ShabbatWeek> ShabbatWeekList);

// 2. State
public record AddEditState
{
	public Enums.FormMode? FormMode { get; init; }
	public FormVM? FormVM { get; init; }
	public List<AV.ShabbatWeek>? ShabbatWeekList { get; init; }
	//public int VideoId { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<AddEditState>
{
	public override string GetName() => Constants.FluxorStores.AddEdit;

	protected override AddEditState GetInitialState()
	{
		return new AddEditState
		{
			//FormVM = new FormVM()
			FormVM = new FormVM("", "")
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
	public static AddEditState On_Load_FormVM(
		AddEditState state, Load_FormVM_Action action)
	{
		return state with
		{
			FormMode = Enums.FormMode.Add,
			FormVM = new AddEdit.FormVM(action.YouTubeId ?? "???", action.Title ?? "???")
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
		AddEditState state, Edit_Action action)
	{
		return state with
		{
			FormMode = Enums.FormMode.Edit,
		};
	}

	[ReducerMethod]
	public static AddEditState On_AddOrEdit(AddEditState state, DB_AddOrEdit_Action action)
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
	private readonly AV.IWeeklyVideosRepository? db_DELETE;

	public Effects(ILogger<Effects> logger, IRepository repository, AV.IWeeklyVideosRepository oldRepository)  // , IRepository repository
	{
		Logger = logger;
		db = repository;
		db_DELETE = oldRepository;
	}
	#endregion

	/*
	[EffectMethod]
	public async Task Get(Get_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(Get)};  Action: {nameof(action.FormMode.Name)}; Id: {action.Id}";

		Logger.LogDebug(string.Format("Inside {0}", inside));
		try
		{
			AddEdit.FormVM? formVM = new();
			formVM = await db!.Get(action.Id);

			if (formVM is null)
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(formVM)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"Video Not Found; Id: {action.Id}"));
			}
			else
			{
				dispatcher.Dispatch(new Set_AddEdit_FormVM_Action(formVM));
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
	*/

	[EffectMethod]
	public async Task AddOrEdit(DB_AddOrEdit_Action action, IDispatcher dispatcher)
	{
		if (action.FormMode is null) throw new ArgumentException("Parameter cannot be null", nameof(action.FormMode));

		string inside = $"{nameof(Effects)}!{nameof(AddOrEdit)}; Action: {action.FormMode.Name}";

		if (action.FormMode == Enums.FormMode.Add)
		{
			Logger.LogDebug(string.Format("Inside {0}", inside));
			try
			{

				var sprocTuple = await db.WeeklyVideoInsert(action.FormVM);
				Logger.LogDebug(string.Format("...sprocTuple.Item2=ReturnValue {0}", sprocTuple.Item2));

				//if (sprocTuple.Item2 != 547) // The %ls statement conflicted with the %ls constraint "%.*ls".
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
		/*	*/
		else
		{
			throw new NotImplementedException("Edit no worky yet");
			/*
			Logger.LogDebug(string.Format("Inside {0}; Id: {1}", inside, action.FormVM.Id));
			try
			{
				var sprocTuple = await db.UpdateVideo(action.FormVM);
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success
					, $"Video Updated for id: [{action.FormVM.Id}], Affected Rows: {sprocTuple.Item1}")); //sprocTuple.RowsAffected
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"{Constants.Effects.RepopulateMessage}"));
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure
					, $"{Constants.Effects.ResponseMessageFailure}. Action: {action.FormMode.Name}"));
				// dispatcher.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.MasterList)); // ToDo: does this make sense?
			}
			*/
		}

	}

	[EffectMethod]
	public async Task PopulateShabbatWeek(DB_Populate_ShabbatWeekList action, IDispatcher dispatcher)
	{
		string inside = nameof(Effects) + "!" + nameof(PopulateShabbatWeek) + "!" + nameof(DB_Populate_ShabbatWeekList);
		Logger.LogDebug(string.Format("Inside {0}", inside));

		try
		{
			List<AV.ShabbatWeek> shabbatWeekList = await db_DELETE!.GetShabbatWeekList(Constants.Effects.WeekCount);
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

}