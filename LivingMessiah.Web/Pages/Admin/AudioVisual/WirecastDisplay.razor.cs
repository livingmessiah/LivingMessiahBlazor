using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using LivingMessiah.Data;
using LivingMessiah.Domain;
using System;
using Blazored.Toast.Services;
using Page = LivingMessiah.Web.Links.Wirecast;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

[AllowAnonymous]
public partial class WirecastDisplay
{
	[Inject] public IShabbatWeekRepository db { get; set; }
	[Inject] public ILogger<WirecastDisplay> Logger { get; set; }
	[Inject] public IToastService Toast { get; set; }

	public Wirecast Wirecast { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}"
		, Page.Index, nameof(WirecastDisplay) + "!" + nameof(OnInitializedAsync)));

		try
		{
			Wirecast = await db.GetCurrentWirecast();
			if (Wirecast == null)
			{
				string s = $"Wirecast is null after calling {nameof(db.GetCurrentWirecast)}";
				Logger.LogWarning(string.Format("...{0}, Sql:{1}", s, db.BaseSqlDump));
				Toast.ShowWarning($"...{s}");
			}

		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}"
				, nameof(WirecastDisplay) + "!" + nameof(OnInitializedAsync)));
			Toast.ShowError("An invalid operation occurred reading database, contact your administrator");
		}
	}
}
