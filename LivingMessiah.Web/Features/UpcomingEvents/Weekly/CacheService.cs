using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Features.UpcomingEvents.Weekly;

public interface ICacheService
{
	Task<IReadOnlyList<vwCurrentWeeklyVideo>>? GetCurrentWeeklyVideos();
	Task<vwCurrentWeeklyVideo>? GetCurrentWeeklyVideoByTypeId(int typeId);
}

public class CacheService : ICacheService
{
	#region Constructor and DI
	private readonly IRepository db;
	private IMemoryCache memoryCache;
	private readonly ILogger log;

	public CacheService(
		IRepository dbRepository
		, IMemoryCache cache
		, ILogger<CacheService> logger
		)
	{
		db = dbRepository;
		memoryCache = cache;
		log = logger;
	}
	#endregion

	public async Task<IReadOnlyList<vwCurrentWeeklyVideo>>? GetCurrentWeeklyVideos()
	{
		var cacheKey = Settings.Constants.CurrentWeeklyVideosCache.Key;
		log.LogDebug($"Inside {nameof(CacheService)}!{nameof(GetCurrentWeeklyVideos)}; cacheKey:{cacheKey}");

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
		if (!memoryCache.TryGetValue(cacheKey, out IReadOnlyList<vwCurrentWeeklyVideo> currentWeeklyVideos))
		{
			//sourceOfData = "Not Cache";
			log.LogDebug($"...Key NOT found in cache, calling {nameof(db.GetCurrentWeeklyVideos)}");
			currentWeeklyVideos = await db.GetCurrentWeeklyVideos(daysOld: 12);
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
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

		// ToDo: CS8603 Don't understand the proper way to remove this type of warning, so I'm sticking my head in the sand
#pragma warning disable CS8603 // Possible null reference return.
		return currentWeeklyVideos;
#pragma warning restore CS8603 // Possible null reference return.
	}

	public async Task<vwCurrentWeeklyVideo>? GetCurrentWeeklyVideoByTypeId(int typeId)
	{
		return (await GetCurrentWeeklyVideos()!).Where(w => w.WvtId == typeId).SingleOrDefault()!;
	}

}
