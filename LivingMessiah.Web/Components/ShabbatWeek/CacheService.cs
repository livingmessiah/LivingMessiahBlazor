using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Components.ShabbatWeek;

public interface ICacheService
{
	Task<PsalmAndProverbCurrentVM>? GetCurrentPsalmAndProverb(bool UseCache = false);
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

	public async Task<PsalmAndProverbCurrentVM>? GetCurrentPsalmAndProverb(bool useCache)
	{
		//var cacheKey = Settings.Constants.PsalmsAndProverbsCache.Key;
		string cacheKey = "CalendarVM";
		string msg = $"Inside {nameof(CacheService)}!{nameof(GetCurrentPsalmAndProverb)}; cacheKey:{cacheKey};...";

		if (!useCache)
		{
			log.LogDebug(string.Format("{0}; NOT USING CACHE", msg));
			return await db.GetCurrentPsalmAndProverb();
		}

		// ToDo: CS8600 Don't understand the proper way to remove this type of warning, so I'm sticking my head in the sand
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
		if (!memoryCache.TryGetValue(cacheKey!, out PsalmAndProverbCurrentVM psalmAndProverb))
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
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

		return psalmAndProverb!;
	}
}

