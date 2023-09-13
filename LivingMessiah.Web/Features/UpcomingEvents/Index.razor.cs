using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Settings;
using Page = LivingMessiah.Web.Links.UpcomingEvents;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Features.UpcomingEvents;

public partial class Index
{
	[Inject] public IOptions<AppSettings>? AppSettings { get; set; }
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public SpecialEvents.Data.IRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	//During development this is usually set to false because it is slow
	protected bool ShowCurrentWeeklyVideos { get; set; }
	protected List<SpecialEvents.FormVM> SpecialEvents = new();


	readonly string inside = $"page {Page.Index}; class:{nameof(Index)}";

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}!{1}", inside, nameof(OnInitialized)));
		ShowCurrentWeeklyVideos = AppSettings!.Value.ShowCurrentWeeklyVideos;
		await PopulateSpecialEvents();
	}

	private async Task PopulateSpecialEvents()
	{
		try
		{
			SpecialEvents = await db!.GetCurrentEvents();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(PopulateSpecialEvents)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(PopulateSpecialEvents)}");
		}
	}

}