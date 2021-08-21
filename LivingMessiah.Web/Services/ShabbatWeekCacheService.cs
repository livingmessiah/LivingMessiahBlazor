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

		Task<IReadOnlyList<ShabbatWeekCache>> GetCurrentShabbatWeek();

		// Psalms and Videos
		Task<PsalmAndProverb> GetCurrentPsalmAndProverb();

		// Parasha
		Task<LivingMessiah.Domain.Parasha.Queries.Parasha> GetCurrentParasha();
		//Task<IReadOnlyList<Parasha>> GetParashotByBookId(int bookId);

		// Weekly Videos
		//Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top);
		Task<IReadOnlyList<vwCurrentWeeklyVideo>> GetCurrentWeeklyVideos();
		Task<vwCurrentWeeklyVideo> GetCurrentWeeklyVideoByTypeId(int typeId);

		// Bible
		Task<BibleBook> GetCurrentParashaTorahBookById(int id);
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


		//ToDo called by RegularVideoedEvents, but why is currentShabbatWeek returning null?
		public async Task<IReadOnlyList<ShabbatWeekCache>> GetCurrentShabbatWeek()
		{
			var cacheKey = "currentShabbatWeek";
			string msg = $"Inside { nameof(ShabbatWeekCacheService)}!{ nameof(GetCurrentShabbatWeek)}; cacheKey:{cacheKey};...";

			if (!memoryCache.TryGetValue(cacheKey, out IReadOnlyList<ShabbatWeekCache> currentShabbatWeek))
			{
				//log.LogDebug($"{msg}; Key NOT found in cache, calling {nameof(db.GetTorahBookById)}");
				currentShabbatWeek = null; 
				var cacheExpiryOptions = new MemoryCacheEntryOptions
				{
					AbsoluteExpiration = DateTime.Now.AddMinutes(5),
					Priority = CacheItemPriority.High,
					SlidingExpiration = TimeSpan.FromMinutes(2)
				};
				memoryCache.Set(cacheKey, currentShabbatWeek, cacheExpiryOptions);
			}
			else
			{
				await Task.Delay(0);
				//log.LogDebug($"{msg}; Key found in cache");
			}
			return null; // shabbatWeeksList.Where(w => w.IsCurrentShabbat = true).ToList();
		}

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

		// Parasha
		public async Task<LivingMessiah.Domain.Parasha.Queries.Parasha> GetCurrentParasha()
		{

			var cacheKey = Settings.Constants.ParashaCache.Key;

			log.LogDebug($"Inside {nameof(ShabbatWeekCacheService)}!{nameof(GetCurrentParasha)}; cacheKey:{cacheKey}");
			if (!memoryCache.TryGetValue(cacheKey, out LivingMessiah.Domain.Parasha.Queries.Parasha parasha))
			{
				log.LogDebug($"...Key NOT found in cache, calling {nameof(db.GetCurrentParashaAndChildren)}");
				parasha = await db.GetCurrentParashaAndChildren();
				log.LogDebug($"...After calling {nameof(db.GetCurrentParashaAndChildren)}; parasha: {parasha}");


				if (parasha != null && parasha.Id != 0)
				{
					var cacheExpiryOptions = new MemoryCacheEntryOptions
					{
						AbsoluteExpiration = DateTime.Now.AddMinutes(Settings.Constants.ParashaCache.AbsoluteExpirationInMinutes),
						Priority = CacheItemPriority.High,
						SlidingExpiration = TimeSpan.FromMinutes(Settings.Constants.ParashaCache.SlidingExpirationInMinutes)
					};
					memoryCache.Set(cacheKey, parasha, cacheExpiryOptions);
				}
				else
				{
					log.LogInformation($"...parasha.Id == 0 WHICH IS WRONG!!!, so NOT saving to memoryCache. See 779-Bug-...");
				}
			}
			else
			{
				log.LogDebug($"...Key found in cache");
			}
			return parasha;
		}


		//public async Task<IReadOnlyList<Parasha>> GetParashotByBookId(int bookId)
		//{
		//	return await db.GetParashotByBookId(bookId);
		//}

		// Bible
		public async Task<BibleBook> GetCurrentParashaTorahBookById(int id)
		{
			var cacheKey = Settings.Constants.ParashaTorahBookCache.Key;
			string msg = $"Inside { nameof(ShabbatWeekCacheService)}!{ nameof(GetCurrentParashaTorahBookById)}; cacheKey:{cacheKey}; id: {id};...";

			if (!memoryCache.TryGetValue(cacheKey, out BibleBook book))
			{
				//log.LogDebug($"{msg}; Key NOT found in cache, calling {nameof(db.GetTorahBookById)}");
				book = await db.GetTorahBookById(id);
				var cacheExpiryOptions = new MemoryCacheEntryOptions
				{
					AbsoluteExpiration = DateTime.Now.AddMinutes(Settings.Constants.ParashaTorahBookCache.AbsoluteExpirationInMinutes),
					Priority = CacheItemPriority.High,
					SlidingExpiration = TimeSpan.FromMinutes(Settings.Constants.ParashaTorahBookCache.SlidingExpirationInMinutes)
				};
				memoryCache.Set(cacheKey, book, cacheExpiryOptions);
			}
			else
			{
				//log.LogDebug($"{msg}; Key found in cache");
			}
			return book;
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
				currentWeeklyVideos = await db.GetCurrentWeeklyVideos();
				log.LogDebug($"...After calling {nameof(db.GetCurrentWeeklyVideos)}; currentWeeklyVideos.Count: {currentWeeklyVideos.Count}");

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

