using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;
using LivingMessiah.Web.Services;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual
{
	[Authorize(Roles = Roles.AdminOrAudiovisual)]
	public partial class Wirecast
	{
		[Inject]
		public IEldersService svc { get; set; }
		
		[Inject]
		public ILogger<Wirecast> Logger { get; set; }

		protected WirecastVM WirecastVM { get; set; }

		public LivingMessiah.Domain.ScratchPad ScratchPad { get; set; }


		protected bool LoadFailed;
		protected override async Task OnInitializedAsync()
		{
			WirecastVM = await svc.GetCurrentWirecast();
			ScratchPad = WirecastVM.ScratchPad;
		}

		protected bool UpdateFailed;

		protected async Task UpdateWirecastLink()
		{
			Logger.LogDebug($"Inside {nameof(Wirecast)}!{nameof(UpdateWirecastLink)}");
			try
			{
				UpdateFailed = false;
				int count = await svc.UpdateWirecastLink(WirecastVM.Wirecast.Id, WirecastVM.Wirecast.WirecastLink);
			}
			catch (System.Exception ex)
			{
				UpdateFailed = true;
				Logger.LogWarning(ex, $"Error calling {nameof(svc.UpdateWirecastLink)}");
			}
		}

		protected async Task UpdateScratchPad()
		{
			Logger.LogDebug($"Inside {nameof(Wirecast)}!{nameof(UpdateScratchPad)}");
			try
			{
				UpdateFailed = false;
				int count2 = await svc.UpdateScratchpad(WirecastVM.ScratchPad.WireCast);
			}
			catch (System.Exception ex)
			{
				UpdateFailed = true;
				Logger.LogWarning(ex, $"Error calling {nameof(svc.UpdateWirecastLink)}");
			}
		}

	}
}
