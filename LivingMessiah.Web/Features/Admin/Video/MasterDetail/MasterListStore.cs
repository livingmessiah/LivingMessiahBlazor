using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

using LivingMessiah.Web.Features.Admin.Video.Enums;

namespace LivingMessiah.Web.Features.Admin.Video.MasterDetail;

// 1. Action
//ToDo: Should Set_ShabbatWeekId_Action be done inside ActionButtonGroup.Add
public record Set_ShabbatWeekId_Action(int Id);

public record Set_WeeklyVideos_Action(List<Models.WeeklyVideoTable> WeeklyVideoList);
public record Set_YouTubes_Action(List<Models.YouTubeFeed> YouTubeList);
public record Get_Action();


// 2. State
public record MasterDetailState
{
	//public List<Models.ShabbatWeek>? ShabbatWeekList { get; init; }  
	public int ShabbatWeekId { get; init; }
	public List<Models.WeeklyVideoTable>? WeekVideoList { get; init; }
	public List<Models.YouTubeFeed>? YouTubeList { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<MasterDetailState>
{
	public override string GetName() => Constants.FluxorStores.MasterList;

	protected override MasterDetailState GetInitialState()
	{
		return new MasterDetailState
		{
		};
	}
}

// 4. Reducers
public static class Reducers
{


	[ReducerMethod]
	public static MasterDetailState On_Set_ShabbatWeekId(
		MasterDetailState state, Set_ShabbatWeekId_Action action)
	{
		return state with
		{
			ShabbatWeekId = action.Id
		};
	}

	[ReducerMethod]
	public static MasterDetailState On_Set_Data_MasterList_WeeklyVideos(
		MasterDetailState state, Set_WeeklyVideos_Action action)
	{
		return state with
		{
			WeekVideoList = action.WeeklyVideoList
		};
	}


	[ReducerMethod]
	public static MasterDetailState On_Set_Data_MasterList_YouTubes(
		MasterDetailState state, Set_YouTubes_Action action)
	{
		return state with
		{
			YouTubeList = action.YouTubeList
		};
	}

}


// 5. Effects
public class Effects
{
	#region Constructor and DI
	private readonly ILogger Logger;
	private readonly Data.IRepository? db;
	private readonly Services.IYouTubeFeedService? Svc;

	public Effects(ILogger<Effects> logger
		, Data.IRepository repository
		, Services.IYouTubeFeedService videoService)
	{
		Logger = logger;
		db = repository;
		Svc = videoService;
	}
	#endregion

	//[Authorize(Roles = Roles.AdminOrAudiovisual)] ToDo: is this applicable?
	[EffectMethod]
	public async Task Get(Get_Action action, IDispatcher dispatcher)
	{
		string inside = "Video.MasterDetail!" + nameof(Effects) + "!" + nameof(Get) + "!" + nameof(Get_Action);
		Logger.LogDebug(string.Format("Inside {0}", inside));

		try
		{
			List<Models.ShabbatWeek> shabbatWeekList = await db!.GetShabbatWeekList(Constants.Effects.WeekCount);
			if (shabbatWeekList is not null)
			{
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"shabbatWeekList RowCnt: {shabbatWeekList.Count}"));
				//ToDo: Should Set_ShabbatWeekId_Action be done inside ActionButtonGroup.Add
				Models.ShabbatWeek sw = shabbatWeekList!.FirstOrDefault()!;
				if (sw is not null)
				{
					dispatcher.Dispatch(new Set_ShabbatWeekId_Action(sw.Id));
				}
				else
				{
					Logger.LogWarning(string.Format("...{0}; ShabbatWeekId is null", inside));
					dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, "No [default] Shabbat Week Id Found"));
				}

			}
			else
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(shabbatWeekList)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, "No Shabbat Weeks Found"));
			}

			List<Models.WeeklyVideoTable> weeklyVideoTableList = await db!.GetWeeklyVideoTableList(Constants.Effects.WeekVideoCount);
			if (weeklyVideoTableList is not null)
			{
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"weeklyVideoTableList RowCnt: {weeklyVideoTableList.Count}"));
				dispatcher.Dispatch(new Set_WeeklyVideos_Action(weeklyVideoTableList));
			}
			else
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(weeklyVideoTableList)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, "No Weekly Videos Found"));
			}

			List<Models.YouTubeFeed> youTubeFeedList = await Svc!.GetModel(SocialMedia.YouTube.YouTubeFeed(), Constants.Effects.WeekVideoCount);
			//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"youTubeFeedList RowCnt: {youTubeFeedList.Count}"));

			if (youTubeFeedList is not null)
			{
				foreach (var item in youTubeFeedList!)
				{
					Models.WeeklyVideoTable wvt = weeklyVideoTableList!
						.Find(x => x.YouTubeId == item.YouTubeId)!;
					if (wvt is not null)
					{
						item.Id = wvt.Id;
						item.Title = wvt.Title;
					}
				}
				
				dispatcher.Dispatch(new Set_YouTubes_Action(youTubeFeedList));
			}
			else
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(youTubeFeedList)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, "No YouTube Videos Found"));
			}

		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}

	}


}