using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;
using System.Threading.Tasks;
using LivingMessiah.Data;
using LivingMessiah.Domain;
using System;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

[Authorize(Roles = Roles.AdminOrAudiovisual)]
public partial class WirecastEdit
{
		[Inject]
		public IShabbatWeekRepository db { get; set; }

		[Inject]
		public ILogger<WirecastEdit> Logger { get; set; }

		public Wirecast Wirecast { get; set; }
		public ScratchPad ScratchPad { get; set; }

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }

		protected int RowCount { get; set; } = 0;

		protected override async Task OnInitializedAsync()
		{
				Logger.LogDebug($"Inside {nameof(WirecastEdit)}!{nameof(OnInitializedAsync)}");
				DatabaseError = false;
				try
				{
						Wirecast = await db.GetCurrentWirecast();
						if (Wirecast == null)
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
				Logger.LogDebug($"Inside {nameof(WirecastEdit)}!{nameof(UpdateWirecastLink)}");
				RowCount = 0;
				DatabaseError = false;
				try
				{
						RowCount = await db.UpdateWirecastLink(Wirecast.Id, Wirecast.WirecastLink);
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
				Logger.LogDebug($"Inside {nameof(WirecastEdit)}!{nameof(UpdateScratchPad)}");
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
