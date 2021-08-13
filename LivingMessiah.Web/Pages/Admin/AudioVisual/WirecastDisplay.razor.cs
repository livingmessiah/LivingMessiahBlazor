using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using LivingMessiah.Data;
using LivingMessiah.Domain;
using System;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual
{
	[AllowAnonymous]
	public partial class WirecastDisplay
	{
		[Inject]
		public IShabbatWeekRepository db { get; set; }

		[Inject]
		public ILogger<WirecastDisplay> Logger { get; set; }

		public Wirecast Wirecast { get; set; }
		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(WirecastDisplay)}!{nameof(OnInitializedAsync)}");
			DatabaseError = false;
			try
			{
				Wirecast = await db.GetCurrentWirecast();
				if (Wirecast == null)
				{
					Logger.LogDebug($"Wirecast is null, Sql:{db.BaseSqlDump}");
				}

			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}
	}
}
