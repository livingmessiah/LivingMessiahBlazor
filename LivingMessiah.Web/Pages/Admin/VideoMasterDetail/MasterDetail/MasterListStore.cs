using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

using LivingMessiah.Web.Pages.Admin.AudioVisual;
using LivingMessiah.Web.Pages.Admin.VideoMasterDetail.Data;
using LivingMessiah.Web.Pages.Admin.VideoMasterDetail.Enums;
using ParentState = LivingMessiah.Web.Pages.Admin.VideoMasterDetail.Index;
using AV = LivingMessiah.Web.Pages.Admin.AudioVisual;
using AVSvc = LivingMessiah.Web.Pages.Admin.AudioVisual.Services;

namespace LivingMessiah.Web.Pages.Admin.VideoMasterDetail.MasterDetail;

// 1. Action
//ToDo: Should Set_ShabbatWeekId_Action be done inside ActionButtonGroup.Add
public record Set_ShabbatWeekId_Action(int Id);

public record Set_WeeklyVideos_Action(List<AV.WeeklyVideoTable> WeeklyVideoList);
public record Set_YouTubes_Action(List<AVSvc.YouTubeFeedModel> YouTubeList);
public record Get_Action();


// 2. State
public record MasterDetailState
{
	public AV.WeeklyVideoAddVM? VideoVM { get; init; }
	public List<AV.ShabbatWeek>? ShabbatWeekList { get; init; }  // Populates EditForm!InputSelect control (id=shabbatWeekId)
	public int ShabbatWeekId { get; init; }
	public List<AV.WeeklyVideoTable>? WeekVideoList { get; init; }
	public List<AVSvc.YouTubeFeedModel>? YouTubeList { get; init; }
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
	private readonly AV.IWeeklyVideosRepository? db;
	private readonly AVSvc.IYouTubeFeedService? Svc;

	public Effects(ILogger<Effects> logger
		, IRepository repository
		, AV.IWeeklyVideosRepository videoRepository
		, AVSvc.IYouTubeFeedService videoService)
	{
		Logger = logger;
		db = videoRepository;
		Svc = videoService;
	}
	#endregion

	//[Authorize(Roles = Roles.AdminOrAudiovisual)] ToDo: is this applicable?
	[EffectMethod]
	public async Task Get(Get_Action action, IDispatcher dispatcher)
	{
		string inside = "VideoMasterDetail.MasterDetail!" + nameof(Effects) + "!" + nameof(Get) + "!" + nameof(Get_Action);
		Logger.LogDebug(string.Format("Inside {0}", inside));

		try
		{
			List<AV.ShabbatWeek> shabbatWeekList = await db!.GetShabbatWeekList(Constants.Effects.WeekCount);
			if (shabbatWeekList is not null)
			{
				//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"shabbatWeekList RowCnt: {shabbatWeekList.Count}"));
				//ToDo: Should Set_ShabbatWeekId_Action be done inside ActionButtonGroup.Add
				AV.ShabbatWeek sw = shabbatWeekList!.FirstOrDefault()!;
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

			List<AV.WeeklyVideoTable> weeklyVideoTableList = await db!.GetWeeklyVideoTableList(Constants.Effects.WeekVideoCount);
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

			List<AVSvc.YouTubeFeedModel> youTubeFeedList = await Svc!.GetModel(SocialMedia.YouTube.YouTubeFeed(), Constants.Effects.WeekVideoCount);
			//dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Success, $"youTubeFeedList RowCnt: {youTubeFeedList.Count}"));

			if (youTubeFeedList is not null)
			{
				foreach (var item in youTubeFeedList!)
				{
					WeeklyVideoTable ytfm = weeklyVideoTableList!
						.Find(x => x.YouTubeId == item.YouTubeId)!;
					if (ytfm is not null)
					{
						item.Id = ytfm.Id;
						item.Title = ytfm.Title;
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