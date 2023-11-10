using System.Threading.Tasks;
using System;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.ShabbatService.Services;

public interface ICacheService
{

	// Psalms and Videos
	Task<Models.PsalmAndProverbVM>? GetCurrentPsalmAndProverb(bool UseCache = false);

	// Weekly Videos
	//Task<IReadOnlyList<vwCurrentWeeklyVideo>>? GetCurrentWeeklyVideos();
	//Task<vwCurrentWeeklyVideo>? GetCurrentWeeklyVideoByTypeId(int typeId);
}

public class CacheService : ICacheService
{

	#region Constructor and DI
	private readonly Data.IRepository db;
	private IMemoryCache memoryCache;
	private readonly ILogger log;

	public CacheService(
		Data.IRepository dbRepository
		, IMemoryCache cache
		, ILogger<CacheService> logger
		)
	{
		db = dbRepository;
		memoryCache = cache;
		log = logger;
	}
	#endregion
	public async Task<Models.PsalmAndProverbVM>? GetCurrentPsalmAndProverb(bool useCache)
	{
		//var cacheKey = Settings.Constants.PsalmsAndProverbsCache.Key;
		string cacheKey = "PsalmAndProverbVM"; 
		string msg = $"Inside {nameof(CacheService)}!{nameof(GetCurrentPsalmAndProverb)}; cacheKey:{cacheKey};...";

		if (!useCache)
		{
			log.LogDebug(string.Format("{0}; NOT USING CACHE", msg));
			return await db.GetCurrentPsalmAndProverb();
		}

		// ToDo: CS8600 Don't understand the proper way to remove this type of warning, so I'm sticking my head in the sand
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
		if (!memoryCache.TryGetValue(cacheKey!, out Models.PsalmAndProverbVM psalmAndProverb))
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
