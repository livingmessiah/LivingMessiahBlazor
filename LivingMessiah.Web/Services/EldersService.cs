using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using LivingMessiah.Web.Pages.Admin.AudioVisual;

namespace LivingMessiah.Web.Services
{
	public interface IEldersService
	{
		string ExceptionMessage { get; set; }

		Task<LivingMessiah.Web.Pages.Admin.AudioVisual.WirecastVM> GetCurrentWirecast();  
		Task<int> UpdateWirecastLink(int id, string wireCastLink);
		Task<int> UpdateScratchpad(string wireCastLink);
	}


	public class EldersService : IEldersService
	{
		#region Constructor and DI
		private readonly LivingMessiah.Data.IShabbatWeekRepository db;
		private readonly ILogger log;

		public EldersService(
			LivingMessiah.Data.IShabbatWeekRepository dbRepository, ILogger<EldersService> logger)
		{
			db = dbRepository;
			log = logger;
		}
		#endregion

		public string ExceptionMessage { get; set; } = "";

		public async Task<WirecastVM> GetCurrentWirecast()
		{
			var vm = new WirecastVM();
			try
			{
				vm.Wirecast = await db.GetCurrentWirecast();
				if (vm.Wirecast == null)
				{
					log.LogInformation($"Wirecast is null, Sql:{db.BaseSqlDump}");
				}

				vm.ScratchPad = await db.GetScratchPadWireCast();
				if (vm.ScratchPad == null)
				{
					log.LogInformation($"ScratchPad is null, Sql:{db.BaseSqlDump}");
				}
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetCurrentWirecast)}";
				log.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return vm;
		}


		public async Task<int> UpdateWirecastLink(int id, string wirecastLink)
		{
			int count = 0;
			try
			{
				count = await db.UpdateWirecastLink(id, wirecastLink);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(UpdateWirecastLink)}, {nameof(db.UpdateWirecastLink)}";
				log.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return count;
		}

		public async Task<int> UpdateScratchpad(string scratchPad)
		{
			int count = 0;
			try
			{
				count = await db.UpdateScratchpad(scratchPad);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(UpdateScratchpad)}, {nameof(db.UpdateScratchpad)}";
				log.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return count;
		}


	}
}