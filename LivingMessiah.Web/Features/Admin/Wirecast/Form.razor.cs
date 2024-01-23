using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

using Page = LivingMessiah.Web.Links.Wirecast;

namespace LivingMessiah.Web.Features.Admin.Wirecast;

public partial class Form
{
	[Inject] public Data.IRepository? db { get; set; }
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public WirecastVM? VM { get; set; }
	public ScratchPad? ScratchPad { get; set; }

	protected int RowCount { get; set; } = 0;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}"
		, Page.Admin.Index, nameof(Form) + "!" + nameof(OnInitializedAsync)));

		try
		{
		  WirecastQuery wirecastQuery = await db!.GetCurrentWirecast();
			if (wirecastQuery is null)
			{
				string s = $"Wirecast is null after calling {nameof(db.GetCurrentWirecast)}";
				Logger!.LogWarning(string.Format("...{0}, Sql:{1}", s, db.BaseSqlDump));
				Toast!.ShowWarning($"...{s}");
			}
			else
			{
				VM = new WirecastVM()
				{
					Id = wirecastQuery!.Id,
					ShabbatDate = wirecastQuery!.ShabbatDate,
					WirecastLink = wirecastQuery!.WirecastLink
				};
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
				, nameof(Form) + "!" + nameof(OnInitializedAsync)));
			Toast!.ShowError("An invalid operation occurred reading database, contact your administrator");
		}
	}

	protected async Task UpdateWirecastLink()
	{
		RowCount = 0;
		try
		{
			RowCount = await db!.UpdateWirecastLink(VM!.Id, VM.WirecastLink!);
			Toast!.ShowInfo($"Updated wirecast link, RowCount: {RowCount}");
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}"
				, nameof(Form) + "!" + nameof(UpdateWirecastLink)));
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
			, nameof(Form) + "!" + nameof(UpdateScratchPad)));
			Toast!.ShowError("An invalid operation occurred updating scratch pad, contact your administrator");
		}
	}

}
