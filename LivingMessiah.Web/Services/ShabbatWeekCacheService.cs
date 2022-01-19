using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace LivingMessiah.Web.Services
{
	public interface IShabbatWeekCacheService
	{

		// Psalms and Videos
		Task<PsalmAndProverb> GetCurrentPsalmAndProverb();

		// Weekly Videos
		Task<IReadOnlyList<vwCurrentWeeklyVideo>> GetCurrentWeeklyVideos();
		Task<vwCurrentWeeklyVideo> GetCurrentWeeklyVideoByTypeId(int typeId);
	}

	public class ShabbatWeekCacheService : IShabbatWeekCacheService
	{
		#region Constructor and DI
		private readonly LivingMessiah.Data.IShabbatWeekRepository db;
		private IMemoryCache memoryCache;
		private readonly ILogger log;

		public ShabbatWeekCacheService(
			LivingMessiah.Data.IShabbatWeekRepository dbRepository
			, IMemoryCache cache
			, ILogger<ShabbatWeekService> logger
			)
		{
			db = dbRepository;
			memoryCache = cache;
			log = logger;
		}
		#endregion


		public async Task<PsalmAndProverb> GetCurrentPsalmAndProverb()
		{
			var cacheKey = Settings.Constants.PsalmsAndProverbsCache.Key;
			string msg = $"Inside { nameof(ShabbatWeekCacheService)}!{ nameof(GetCurrentPsalmAndProverb)}; cacheKey:{cacheKey};...";

			if (!memoryCache.TryGetValue(cacheKey, out PsalmAndProverb psalmAndProverb))
			{
				//log.LogDebug($"{msg}; Key NOT found in cache, calling {nameof(db.GetCurrentPsalmAndProverb)}");
				psalmAndProverb = await db.GetCurrentPsalmAndProverb();
				var cacheExpiryOptions = new MemoryCacheEntryOptions
				{
					AbsoluteExpiration = DateTime.Now.AddMinutes(Settings.Constants.PsalmsAndProverbsCache.AbsoluteExpirationInMinutes),
					Priority = CacheItemPriority.High,
					SlidingExpiration = TimeSpan.FromMinutes(Settings.Constants.PsalmsAndProverbsCache.SlidingExpirationInMinutes)
				};
				memoryCache.Set(cacheKey, psalmAndProverb, cacheExpiryOptions);
			}
			else
			{
				//log.LogDebug($"{msg}; Key found in cache");
			}
			return psalmAndProverb;
		}

		// Weekly Videos
		public async Task<IReadOnlyList<vwCurrentWeeklyVideo>> GetCurrentWeeklyVideos()
		{
			//string sourceOfData = "Cache";
			var cacheKey = Settings.Constants.CurrentWeeklyVideosCache.Key;
			log.LogDebug($"Inside {nameof(ShabbatWeekCacheService)}!{nameof(GetCurrentWeeklyVideos)}; cacheKey:{cacheKey}");

			if (!memoryCache.TryGetValue(cacheKey, out IReadOnlyList<vwCurrentWeeklyVideo> currentWeeklyVideos))
			{
				//sourceOfData = "Not Cache";
				log.LogDebug($"...Key NOT found in cache, calling {nameof(db.GetCurrentWeeklyVideos)}");
				currentWeeklyVideos = await db.GetCurrentWeeklyVideos(daysOld:12);
				log.LogDebug($"...After calling {nameof(db.GetCurrentWeeklyVideos)}; currentWeeklyVideos.Count: {currentWeeklyVideos.Count}");

				log.LogDebug($"db.BaseSqlDump: {Environment.NewLine} {db.BaseSqlDump}");

				if (currentWeeklyVideos.Count != 1)
				{
					var cacheExpiryOptions = new MemoryCacheEntryOptions
					{
						AbsoluteExpiration = DateTime.Now.AddMinutes(Settings.Constants.CurrentWeeklyVideosCache.AbsoluteExpirationInMinutes),
						Priority = CacheItemPriority.High,
						SlidingExpiration = TimeSpan.FromMinutes(Settings.Constants.CurrentWeeklyVideosCache.SlidingExpirationInMinutes)
					};
					memoryCache.Set(cacheKey, currentWeeklyVideos, cacheExpiryOptions);
				}
				else
				{
					log.LogInformation($"...currentWeeklyVideos.Count == 1 WHICH IS WRONG!!!, so NOT saving to memoryCache. Related to 779-Bug-...");
				}

			}
			else
			{
				log.LogDebug($"...Key found in cache");
			}

			return currentWeeklyVideos;
		}

		public async Task<vwCurrentWeeklyVideo> GetCurrentWeeklyVideoByTypeId(int typeId)
		{
			return (await GetCurrentWeeklyVideos()).Where(w => w.WvtId == typeId).SingleOrDefault();
		}

	}
}

