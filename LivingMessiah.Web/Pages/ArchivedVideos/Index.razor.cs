using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Blazored.Toast.Services;
using Page = LivingMessiah.Web.Links.ArchivedVideos;
using LivingMessiah.Web.Shared;

namespace LivingMessiah.Web.Pages.ArchivedVideos;

public partial class Index
{
	[Inject] public IRepository? db { get; set; }
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter]	public int Top { get; set; } = 10;

	protected IReadOnlyList<WeeklyVideoIndex>? ArchivedVideos;
	protected LoadingStatusEnum _status;

	readonly string inside = $"page {Page.Index}; class: {nameof(Index)}";

	protected override async Task OnInitializedAsync()
	{
		try
		{
			_status = LoadingStatusEnum.Loading;
			ArchivedVideos = await db!.GetTopWeeklyVideos(Top);
			_status = LoadingStatusEnum.Loaded;

			if (ArchivedVideos == null)
			{
				Toast!.ShowWarning("Archived Videos NOT FOUND");
			}
		}
		catch (System.Exception ex)
		{
			_status = LoadingStatusEnum.Error;
			Logger!.LogError(ex, string.Format("...Inside catch of {0}"
				, inside + "!" + nameof(OnInitializedAsync)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(OnInitializedAsync)}");
		}

	}
}
