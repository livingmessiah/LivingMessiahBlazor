using Blazored.Toast.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using Page = LivingMessiah.Web.Links.UpcomingEvents;

namespace LivingMessiah.Web.Features.UpcomingEvents;

public partial class DisplayCardList
{
	[Inject] public ILogger<DisplayCardList>? Logger { get; set; }
	[Inject] public SpecialEvents.Data.IRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	readonly string inside = $"page {Page.Index}; class:{nameof(DisplayCardList)}";

	protected List<SpecialEvents.FormVM> SpecialEvents = new();

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}!{1}", inside, nameof(OnInitializedAsync)));
		try
		{
			SpecialEvents = await db!.GetCurrentEvents();
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(OnInitializedAsync)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(OnInitializedAsync)}");
		}
	}


}
