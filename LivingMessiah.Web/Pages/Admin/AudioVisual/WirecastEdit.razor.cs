using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;
using System.Threading.Tasks;
using LivingMessiah.Data;
using LivingMessiah.Domain;
using System;
using Blazored.Toast.Services;
using Page = LivingMessiah.Web.Links.Wirecast;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

[Authorize(Roles = Roles.AdminOrAudiovisual)]
public partial class WirecastEdit
{
	[Inject] public IShabbatWeekRepository? db { get; set; }
	[Inject] public ILogger<WirecastEdit>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public Wirecast? Wirecast { get; set; }
	public ScratchPad? ScratchPad { get; set; }

	protected int RowCount { get; set; } = 0;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}"
		, Page.Edit.Page, nameof(WirecastEdit) + "!" + nameof(OnInitializedAsync)));

		try
		{
			Wirecast = await db!.GetCurrentWirecast();
			if (Wirecast == null)
			{
				string s = $"Wirecast is null after calling {nameof(db.GetCurrentWirecast)}";
				Logger!.LogWarning(string.Format("...{0}, Sql:{1}", s, db.BaseSqlDump));
				Toast!.ShowWarning($"...{s}");
			}

			ScratchPad = await db!.GetScratchPadWireCast();
			if (ScratchPad == null)
			{
				string s = $"ScratchPad is null after calling {nameof(db.GetScratchPadWireCast)}";
				Logger!.LogWarning(string.Format("...{0}, Sql:{1}", s, db.BaseSqlDump));
				Toast!.ShowWarning($"...{s}");
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}"
				, nameof(WirecastEdit) + "!" + nameof(OnInitializedAsync)));
			Toast!.ShowError("An invalid operation occurred reading database, contact your administrator");
		}
	}

	protected async Task UpdateWirecastLink()
	{
		RowCount = 0;
		try
		{
			RowCount = await db!.UpdateWirecastLink(Wirecast!.Id, Wirecast.WirecastLink!);
			Toast!.ShowInfo($"Updated wirecast link, RowCount: {RowCount}");
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}"
				, nameof(WirecastEdit) + "!" + nameof(UpdateWirecastLink)));
			Toast!.ShowError("An invalid operation occurred updating wirecast link, contact your administrator");
		}
	}

	protected async Task UpdateScratchPad()
	{
		try
		{
			RowCount = 0;
			RowCount = await db!.UpdateScratchpad(ScratchPad!.WireCast!);  
			Toast!.ShowInfo($"Updated scratch pad, RowCount: {RowCount}");
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}"
			, nameof(WirecastEdit) + "!" + nameof(UpdateScratchPad)));
			Toast!.ShowError("An invalid operation occurred updating scratch pad, contact your administrator");
		}
	}

}
