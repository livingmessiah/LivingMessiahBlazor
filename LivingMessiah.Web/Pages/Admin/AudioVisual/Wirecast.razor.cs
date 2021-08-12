using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;
using System.Threading.Tasks;
using LivingMessiah.Data;
using LivingMessiah.Domain;
using System;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual
{
	[Authorize(Roles = Roles.AdminOrAudiovisual)]
	public partial class Wirecast
	{
		[Inject]
		public IShabbatWeekRepository db { get; set; }

		[Inject]
		public ILogger<Wirecast> Logger { get; set; }

		public LivingMessiah.Domain.Wirecast WirecastVM { get; set; }
		public LivingMessiah.Domain.ScratchPad ScratchPad { get; set; }

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }

		protected int RowCount { get; set; } = 0;

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(Wirecast)}!{nameof(OnInitializedAsync)}");
			DatabaseError = false;
			try
			{
				WirecastVM = await db.GetCurrentWirecast();
				if (WirecastVM == null)
				{
					Logger.LogDebug($"Wirecast is null, Sql:{db.BaseSqlDump}");
				}

				ScratchPad = await db.GetScratchPadWireCast();
				if (ScratchPad == null)
				{
					Logger.LogDebug($"ScratchPad is null, Sql:{db.BaseSqlDump}");
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}

		protected async Task UpdateWirecastLink()
		{
			Logger.LogDebug($"Inside {nameof(Wirecast)}!{nameof(UpdateWirecastLink)}");
			RowCount = 0;
			DatabaseError = false;
			try
			{
				RowCount = await db.UpdateWirecastLink(WirecastVM.Id, WirecastVM.WirecastLink);
			}
			catch (System.Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error updating database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}

		protected async Task UpdateScratchPad()
		{
			Logger.LogDebug($"Inside {nameof(Wirecast)}!{nameof(UpdateScratchPad)}");
			try
			{
				DatabaseError = false;
				RowCount = 0;
				RowCount = await db.UpdateScratchpad(ScratchPad.WireCast);  //ScratchPad
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error updating database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}

	}
}
