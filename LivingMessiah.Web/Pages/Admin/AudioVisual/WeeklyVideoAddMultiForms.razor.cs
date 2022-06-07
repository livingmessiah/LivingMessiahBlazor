using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.Admin.AudioVisual.Services;
using LivingMessiah.Web.SmartEnums;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

//[Authorize(Roles = Roles.AdminOrAudiovisual)]
public partial class WeeklyVideoAddMultiForms
{
	[Inject]
	public IYouTubeFeedService Svc { get; set; }

	[Inject]
	public ILogger<WeeklyVideoAddMultiForms> Logger { get; set; }

	[Inject]
	public IWeeklyVideosRepository db { get; set; }

	[Inject]
	NavigationManager NavigationManager { get; set; }

	public WeeklyVideoAddVM vm { get; set; } = new WeeklyVideoAddVM();
	public List<WeeklyVideoAddVM> WeeklyVideoAddVMList { get; set; } = new List<WeeklyVideoAddVM>();

	public List<YouTubeFeedModel> YouTubeList { get; set; }

	public List<ShabbatWeek> ShabbatWeekList { get; set; }  // Populates EditForm!InputSelect control (id=shabbatWeekId)
	public List<WeeklyVideoTable> WeeklyVideoTableList { get; set; }
	
	private int _shabbatWeekId = 1;

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideoAddMultiForms) + "!" + nameof(OnInitialized)));
		await PopulateShabbatWeek();

		YouTubeList = await Svc.GetModel(SocialMedia.YouTube.YouTubeFeed(), 5);
		await PopulateWeeklyVideoTableList();
		PopulateWeeklyVideoAddVMList();
		//UpdateYouTubeList();
	}

	#region Shabbat Week Lookup
	private int WeekCount = 3;

	private async Task PopulateShabbatWeek()
	{
		Logger.LogDebug(string.Format("Inside {0}; WeekCount:{1}", nameof(Index) + "!" + nameof(PopulateShabbatWeek), WeekCount));

		try
		{
			ShabbatWeekList = await db.GetShabbatWeekList(WeekCount);

			if (ShabbatWeekList is null)
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = $"{nameof(ShabbatWeekList)} NOT FOUND";
			}
			else
			{
				_shabbatWeekId = ShabbatWeekList.FirstOrDefault().Id;
			}

		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
	}

	#endregion

	private void PopulateWeeklyVideoAddVMList()
	{
		Logger.LogDebug(string.Format("...PopulateWeeklyVideoAddVMList Start"));

		

		foreach (var item in YouTubeList)
		{
			
			WeeklyVideoTable wvt = WeeklyVideoTableList.Find(x => x.YouTubeId == item.YouTubeId);

			if (wvt is null)
			{
				WeeklyVideoAddVMList.Add(new WeeklyVideoAddVM()
				{
					WeeklyVideoTypeId = WeeklyVideoType.MainServiceEnglish,
					ShabbatWeekId = _shabbatWeekId,
					Title = item.Title,
					YouTubeId = item.YouTubeId,
				});
			}


		}

		Logger.LogDebug(string.Format("...PopulateWeeklyVideoAddVMList End"));
	}



	// This should be changed GetMissingYouTubeListOnly
	//private void UpdateYouTubeList()
	//{
	//	foreach (var item in YouTubeList)
	//	{
	//		WeeklyVideoTable wvt = WeeklyVideoTableList.Find(x => x.YouTubeId == item.YouTubeId);

	//		// This if is pointless because in the UI, I ignore those rows
	//		// @foreach (var item in YouTubeList.Where(w => w.Id is null))
	//		if (wvt is not null)
	//		{
	//			Logger.LogDebug(string.Format("... wvt is not null, wvt.Id: {0}", wvt.Id));
	//			item.Id = wvt.Id;
	//			item.Title = wvt.Title;
	//			Logger.LogDebug(string.Format("...Item.Id: {0}; Item.Title: {1}", item.Id, item.Title));
	//		}
	//	}

	//	if (YouTubeList is not null && YouTubeList.Any())
	//	{
	//		Logger.LogDebug(string.Format("... YouTubeList.Count before: {0}", YouTubeList.Count));
	//		//YouTubeList = YouTubeList.Where(w => w.Id is null).ToList();
	//		Logger.LogDebug(string.Format("... YouTubeList.Count after: {0}", YouTubeList.Count));
	//	}
	//}

	private async Task PopulateWeeklyVideoTableList()
	{
		Logger.LogDebug(string.Format("Inside {0}; WeekCount:{1}", nameof(Index) + "!" + nameof(PopulateWeeklyVideoTableList), WeekCount));

		try
		{
			WeeklyVideoTableList = await db.GetWeeklyVideoTableList(5);

			if (WeeklyVideoTableList is null)
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = $"{nameof(WeeklyVideoTableList)} NOT FOUND";
			}

		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database (WeeklyVideoTableList)";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
	}


	#region Events

	void Edit_ButtonClick(int? id)
	{
		NavigationManager.NavigateTo(Links.Admin.AudioVisual.Update.Index + "/" + id);
	}

	protected async Task HandleValidSubmit()
	{
		Logger.LogDebug(string.Format("...{0}", nameof(WeeklyVideoAddMultiForms) + "!" + nameof(HandleValidSubmit)));
		DatabaseInformation = false;
		DatabaseInformationMsg = "";

		int newId = 0;
		WeeklyVideoInsert dto = new WeeklyVideoInsert();
		dto.ShabbatWeekId = vm.ShabbatWeekId;
		dto.WeeklyVideoTypeId = vm.WeeklyVideoTypeId;
		dto.YouTubeId = vm.YouTubeId;
		dto.Title = vm.Title;
		dto.Book = 0;
		dto.Chapter = 0;

		try
		{
			newId = await db.WeeklyVideoAdd(dto);
			//StateHasChanged(); this didn't work, I wanted to update WeeklyVideoTableList bud it didn't work
			//vm.ShabbatWeekId = 0;
			vm.Title = "";
			vm.YouTubeId = "";
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error inserting row in database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}

		Logger.LogDebug(string.Format("...newId: {0}", newId));

		DatabaseInformation = true;
		DatabaseInformationMsg = $"Record Added; newId: {newId}";
	}

	#endregion

	#region ErrorHandling
	private void InitializeErrorHandling()
	{
		DatabaseInformationMsg = "";
		DatabaseInformation = false;
		DatabaseWarningMsg = "";
		DatabaseWarning = false;
		DatabaseErrorMsg = "";
		DatabaseError = false;
	}

	protected bool DatabaseInformation = false;
	protected string DatabaseInformationMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }
	protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
	protected string DatabaseErrorMsg { get; set; }


	#endregion



}
