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
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

[Authorize(Roles = Roles.AdminOrAudiovisual)]
public partial class WeeklyVideoAddForm
{
	[Inject] public IYouTubeFeedService? Svc { get; set; }
	[Inject] public ILogger<WeeklyVideoAddForm>? Logger { get; set; }
	[Inject] public IWeeklyVideosRepository? db { get; set; }
	[Inject] NavigationManager? NavigationManager { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public WeeklyVideoAddVM vm { get; set; } = new WeeklyVideoAddVM();
	public List<YouTubeFeedModel>? YouTubeList { get; set; }

	public List<ShabbatWeek>? ShabbatWeekList { get; set; }  // Populates EditForm!InputSelect control (id=shabbatWeekId)
	public List<WeeklyVideoTable>? WeeklyVideoTableList { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideoAddForm) + "!" + nameof(OnInitialized)));
		await PopulateShabbatWeek();
		YouTubeList = await Svc!.GetModel(SocialMedia.YouTube.YouTubeFeed(), 5);
		await PopulateWeeklyVideoTableList();
		UpdateYouTubeList();
	}

	#region Shabbat Week Lookup
	private int WeekCount = 3;

	private async Task PopulateShabbatWeek()
	{
		Logger!.LogDebug(string.Format("Inside {0}; WeekCount:{1}", nameof(Index) + "!" + nameof(PopulateShabbatWeek), WeekCount));

		try
		{
			ShabbatWeekList = await db!.GetShabbatWeekList(WeekCount);

			if (ShabbatWeekList is null)
			{
				Toast!.ShowWarning($"{nameof(ShabbatWeekList)} NOT FOUND");
			}

		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, $"...Error reading database");
			Toast!.ShowError($"...Error reading database");
		}
	}

	#endregion

	private void UpdateYouTubeList()
	{
		foreach (var item in YouTubeList!)
		{
			WeeklyVideoTable ytfm = WeeklyVideoTableList!.Find(x => x.YouTubeId == item.YouTubeId)!;
			if (ytfm is not null)
			{
				item.Id = ytfm.Id;
				item.Title = ytfm.Title;
			}
		}
	}

	private async Task PopulateWeeklyVideoTableList()
	{
		Logger!.LogDebug(string.Format("Inside {0}; WeekCount:{1}", nameof(Index) + "!" + nameof(PopulateWeeklyVideoTableList), WeekCount));

		try
		{
			WeeklyVideoTableList = await db!.GetWeeklyVideoTableList(5);

			if (WeeklyVideoTableList is null)
			{
				Toast!.ShowWarning($"{nameof(WeeklyVideoTableList)} NOT FOUND");
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Error reading database (WeeklyVideoTableList)");
		}
	}


	#region Events
	void Add(YouTubeFeedModel yTFM)
	{
		//Logger!.LogDebug(string.Format("...{0}", nameof(WeeklyVideoAddForm) + "!" + nameof(Add)));
		vm.Title = yTFM.Title;
		vm.YouTubeId = yTFM.YouTubeId;
		vm.WeeklyVideoTypeId = WeeklyVideoType.MainServiceEnglish;

		ShabbatWeek sw = new ShabbatWeek();
		sw = ShabbatWeekList!.FirstOrDefault()!;
		if (sw is not null)
		{
			vm.ShabbatWeekId = sw.Id;
		}
	}

	void Edit_ButtonClick(int? id)
	{
		NavigationManager!.NavigateTo(Links.Admin.AudioVisual.Update.Index + "/" + id);
	}

	protected async Task HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("...{0}", nameof(WeeklyVideoAddForm) + "!" + nameof(HandleValidSubmit)));
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
			newId = await db!.WeeklyVideoAdd(dto);
			//StateHasChanged(); this didn't work, I wanted to update WeeklyVideoTableList bud it didn't work
			//vm.ShabbatWeekId = 0;
			vm.Title = "";
			vm.YouTubeId = "";
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, $"...Error inserting row in database");
			Toast!.ShowError($"...Error inserting row in database");
		}

		Logger!.LogDebug(string.Format("...newId: {0}", newId));
		Toast!.ShowInfo($"Record Added; newId: {newId}");
	}

	#endregion
}
